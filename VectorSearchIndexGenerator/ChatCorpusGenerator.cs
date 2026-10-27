using System.Buffers.Binary;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using SharedModels;

namespace VectorSearchIndexGenerator;

internal static partial class ChatCorpusGenerator
{
  private const int SchemaVersion = 1;
  private static readonly Regex PageRouteRegex = new(
    @"@page\s+""(?<route>[^""]+)""",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
  public static async Task GenerateAsync(
    string outputDirectory,
    DateOnly publicationCutoff,
    string modelId,
    string modelRevision)
  {
    if (!BitConverter.IsLittleEndian)
    {
      throw new PlatformNotSupportedException("Chat corpus vectors require a little-endian build host.");
    }

    var repositoryRoot = GetRepositoryRoot();
    var routeFiles = DiscoverArticleRoutes(repositoryRoot);
    var eligibleArticles = new TableOfContents().AllContents
      .Where(content => NormalizeRoute(content.ContentUrl).StartsWith("/blogs/", StringComparison.Ordinal))
      .Where(content => DateOnly.FromDateTime(content.ModifiedOn) <= publicationCutoff)
      .OrderBy(content => content.Slug, StringComparer.Ordinal)
      .ToArray();
    if (eligibleArticles.Length == 0)
    {
      throw new InvalidOperationException($"No articles are eligible for the {publicationCutoff:yyyy-MM-dd} chat corpus.");
    }

    var modelDirectory = Path.Combine(repositoryRoot, "VectorSearchIndexGenerator", ".model-cache", modelRevision);
    Directory.CreateDirectory(modelDirectory);
    var modelPath = Path.Combine(modelDirectory, "model_quantized.onnx");
    var vocabularyPath = Path.Combine(modelDirectory, "vocab.txt");
    await ModelDownloader.DownloadIfMissingAsync(
      $"https://huggingface.co/{modelId}/resolve/{modelRevision}/onnx/model_quantized.onnx",
      modelPath);
    await ModelDownloader.DownloadIfMissingAsync(
      $"https://huggingface.co/{modelId}/resolve/{modelRevision}/vocab.txt",
      vocabularyPath);

    using var embedder = new MiniLmEmbedder(modelPath, vocabularyPath);
    var extractor = new ChatArticleExtractor();
    var passages = new List<ChatPassage>();
    var vectors = new List<float[]>();

    foreach (var article in eligibleArticles)
    {
      var route = NormalizeRoute(article.ContentUrl);
      if (!routeFiles.TryGetValue(route, out var sourcePath))
      {
        throw new InvalidDataException($"No Razor source was found for eligible article '{article.Slug}' at route '{route}'.");
      }

      var source = await File.ReadAllTextAsync(sourcePath);
      var parents = extractor.Extract(source, article.Slug, article.Title);
      foreach (var parent in parents)
      {
        var chunks = ChatPassageChunker.Create(
          article.Title,
          article.ContentUrl,
          parent,
          embedder.CountTokens);

        foreach (var chunk in chunks)
        {
          var vector = embedder.EmbedStrict(chunk.EmbeddingPrefix + chunk.Excerpt);
          if (vector.Length != MiniLmEmbedder.Dimensions || vector.Any(value => !float.IsFinite(value)))
          {
            throw new InvalidDataException($"Invalid embedding for '{article.Slug}' passage '{chunk.Id}'.");
          }

          passages.Add(chunk);
          vectors.Add(vector);
        }
      }
    }

    if (passages.Count == 0)
    {
      throw new InvalidOperationException("The chat corpus contains no passages.");
    }

    await WriteCorpusAsync(outputDirectory, publicationCutoff, modelId, modelRevision, passages, vectors);
    Console.WriteLine($"Generated {passages.Count} article passages from {eligibleArticles.Length} published articles in '{outputDirectory}'.");
  }

  private static Dictionary<string, string> DiscoverArticleRoutes(string repositoryRoot)
  {
    var routes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    foreach (var path in Directory.EnumerateFiles(repositoryRoot, "*.razor", SearchOption.AllDirectories)
      .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
        && !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)))
    {
      var source = File.ReadAllText(path);
      var route = PageRouteRegex.Match(source);
      if (!route.Success)
      {
        continue;
      }

      var normalizedRoute = NormalizeRoute(route.Groups["route"].Value);
      if (!routes.TryAdd(normalizedRoute, path))
      {
        throw new InvalidDataException($"Multiple Razor files declare the article route '{normalizedRoute}'.");
      }
    }

    return routes;
  }

  private static string NormalizeRoute(string route)
    => $"/{route.Trim().Trim('/')}";

  private static string GetRepositoryRoot()
  {
    var sourcePath = typeof(ChatCorpusGenerator).Assembly.Location;
    var current = new DirectoryInfo(Path.GetDirectoryName(sourcePath)!);
    while (current is not null && !File.Exists(Path.Combine(current.FullName, "ILoveDotNet.slnx")))
    {
      current = current.Parent;
    }

    if (current is null)
    {
      throw new DirectoryNotFoundException("Could not locate the repository root for chat corpus generation.");
    }

    return current.FullName;
  }

  private static async Task WriteCorpusAsync(
    string outputDirectory,
    DateOnly cutoff,
    string modelId,
    string modelRevision,
    IReadOnlyList<ChatPassage> passages,
    IReadOnlyList<float[]> vectors)
  {
    Directory.CreateDirectory(outputDirectory);
    var passagesJson = JsonSerializer.SerializeToUtf8Bytes(
      passages.ToArray(),
      ChatCorpusJsonContext.Default.ChatPassageArray);
    var vectorBytes = new byte[vectors.Count * MiniLmEmbedder.Dimensions * sizeof(float)];
    for (var row = 0; row < vectors.Count; row++)
    {
      for (var dimension = 0; dimension < MiniLmEmbedder.Dimensions; dimension++)
      {
        var offset = (row * MiniLmEmbedder.Dimensions + dimension) * sizeof(float);
        BinaryPrimitives.WriteInt32LittleEndian(
          vectorBytes.AsSpan(offset, sizeof(float)),
          BitConverter.SingleToInt32Bits(vectors[row][dimension]));
      }
    }

    var versionInput = SHA256.HashData([.. passagesJson, .. vectorBytes]);
    var version = Convert.ToHexStringLower(versionInput);
    var versionDirectory = Path.Combine(outputDirectory, "versions", version);
    Directory.CreateDirectory(Path.GetDirectoryName(versionDirectory)!);

    if (!Directory.Exists(versionDirectory))
    {
      var temporaryDirectory = $"{versionDirectory}.tmp";
      if (Directory.Exists(temporaryDirectory))
      {
        Directory.Delete(temporaryDirectory, recursive: true);
      }

      Directory.CreateDirectory(temporaryDirectory);
      await File.WriteAllBytesAsync(Path.Combine(temporaryDirectory, "chunks.json"), passagesJson);
      await File.WriteAllBytesAsync(Path.Combine(temporaryDirectory, "chunks.vec"), vectorBytes);
      Directory.Move(temporaryDirectory, versionDirectory);
    }

    var manifest = new ChatCorpusManifest(
      SchemaVersion,
      version,
      cutoff.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
      modelId,
      modelRevision,
      MiniLmEmbedder.Dimensions,
      passages.Count,
      "float32-le",
      $"versions/{version}/chunks.json",
      $"versions/{version}/chunks.vec",
      Convert.ToHexStringLower(SHA256.HashData(passagesJson)),
      Convert.ToHexStringLower(SHA256.HashData(vectorBytes)));

    var temporaryManifest = Path.Combine(outputDirectory, "manifest.json.tmp");
    await File.WriteAllTextAsync(
      temporaryManifest,
      JsonSerializer.Serialize(manifest, ChatCorpusJsonContext.Default.ChatCorpusManifest));
    File.Move(temporaryManifest, Path.Combine(outputDirectory, "manifest.json"), overwrite: true);

    foreach (var staleDirectory in Directory.EnumerateDirectories(Path.Combine(outputDirectory, "versions"))
      .Where(path => !Path.GetFileName(path).Equals(version, StringComparison.Ordinal)))
    {
      Directory.Delete(staleDirectory, recursive: true);
    }
  }
}

internal sealed record ChatCorpusManifest(
  int SchemaVersion,
  string CorpusVersion,
  string PublicationCutoff,
  string Model,
  string ModelRevision,
  int Dimensions,
  int Count,
  string VectorFormat,
  string ChunksUrl,
  string VectorsUrl,
  string ChunksSha256,
  string VectorsSha256);
