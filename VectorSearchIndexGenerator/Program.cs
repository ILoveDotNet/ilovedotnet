using System.Runtime.InteropServices;
using System.Text.Json;
using System.Globalization;
using SharedModels;
using VectorSearchIndexGenerator;

var outputDirectory = args.Length == 1
  ? Path.GetFullPath(args[0])
  : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "CommonComponents", "wwwroot", "search"));
var modelDirectory = Path.Combine(AppContext.BaseDirectory, ".model-cache");
var metadataPath = Path.Combine(outputDirectory, "index.json");
var vectorsPath = Path.Combine(outputDirectory, "index.vec");

Directory.CreateDirectory(outputDirectory);
Directory.CreateDirectory(modelDirectory);

var modelPath = Path.Combine(modelDirectory, "model_quantized.onnx");
var vocabularyPath = Path.Combine(modelDirectory, "vocab.txt");
await DownloadIfMissingAsync(
  "https://huggingface.co/Xenova/all-MiniLM-L6-v2/resolve/main/onnx/model_quantized.onnx",
  modelPath);
await DownloadIfMissingAsync(
  "https://huggingface.co/Xenova/all-MiniLM-L6-v2/resolve/main/vocab.txt",
  vocabularyPath);

var contents = new TableOfContents().AllContents;
if (contents.Count == 0)
{
  throw new InvalidOperationException("The search index cannot be generated because there is no published content.");
}

using var embedder = new MiniLmEmbedder(modelPath, vocabularyPath);
var entries = new List<SearchIndexEntry>(contents.Count);
await using var vectorStream = File.Create(vectorsPath);

foreach (var content in contents)
{
  var vector = embedder.Embed(CreateSearchDocument(content));
  if (vector.Length != 384)
  {
    throw new InvalidOperationException($"Expected a 384-dimensional vector for '{content.Slug}', but received {vector.Length} dimensions.");
  }

  vectorStream.Write(MemoryMarshal.AsBytes(vector.AsSpan()));
  entries.Add(new SearchIndexEntry(
    content.Slug,
    content.ModifiedOn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
}

await File.WriteAllTextAsync(metadataPath, JsonSerializer.Serialize(entries, SearchIndexJsonContext.Default.ListSearchIndexEntry));
Console.WriteLine($"Generated {entries.Count} normalized 384-dimensional vectors in '{outputDirectory}'.");

static string CreateSearchDocument(ContentMetaData content)
  => SearchTextNormalizer.Normalize(
    $"{content.Title} {content.Description} Keywords {string.Join(" ", content.Keywords)} Channel {content.Channel}");

static async Task DownloadIfMissingAsync(string url, string path)
{
  if (File.Exists(path))
  {
    return;
  }

  Console.WriteLine($"Downloading {url}...");
  using var httpClient = new HttpClient();
  await using var source = await httpClient.GetStreamAsync(url);
  await using var destination = File.Create(path);
  await source.CopyToAsync(destination);
}

