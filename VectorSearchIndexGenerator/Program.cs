using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using SharedModels;
using VectorSearchIndexGenerator;

const string ModelId = "Xenova/all-MiniLM-L6-v2";
const string ModelRevision = "751bff37182d3f1213fa05d7196b954e230abad9";

if (args.Length > 0 && args[0] == "--chat")
{
  if (args.Length < 2 || args.Length % 2 != 0)
  {
    throw new ArgumentException("Usage: VectorSearchIndexGenerator --chat <output-directory> [--cutoff yyyy-MM-dd] [--inventory <path>]");
  }

  var cutoff = DateOnly.FromDateTime(DateTime.UtcNow);
  string? inventoryPath = null;
  for (var optionIndex = 2; optionIndex < args.Length; optionIndex += 2)
  {
    if (args[optionIndex] == "--inventory")
    {
      inventoryPath = Path.GetFullPath(args[optionIndex + 1]);
    }
    else if (args[optionIndex] != "--cutoff" || !DateOnly.TryParseExact(args[optionIndex + 1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out cutoff))
    {
      throw new ArgumentException("The chat corpus cutoff must use --cutoff yyyy-MM-dd.");
    }
  }

  await ChatCorpusGenerator.GenerateAsync(Path.GetFullPath(args[1]), cutoff, ModelId, ModelRevision, inventoryPath);
  return;
}

var outputDirectory = args.Length == 1
  ? Path.GetFullPath(args[0])
  : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "CommonComponents", "wwwroot", "search"));
var modelDirectory = Path.Combine(DefaultModelDirectory(), ModelRevision);
var metadataPath = Path.Combine(outputDirectory, "index.json");
var vectorsPath = Path.Combine(outputDirectory, "index.vec");

Directory.CreateDirectory(outputDirectory);
Directory.CreateDirectory(modelDirectory);

var modelPath = Path.Combine(modelDirectory, "model_quantized.onnx");
var vocabularyPath = Path.Combine(modelDirectory, "vocab.txt");
await ModelDownloader.DownloadIfMissingAsync(
  $"https://huggingface.co/{ModelId}/resolve/{ModelRevision}/onnx/model_quantized.onnx",
  modelPath);
await ModelDownloader.DownloadIfMissingAsync(
  $"https://huggingface.co/{ModelId}/resolve/{ModelRevision}/vocab.txt",
  vocabularyPath);

var contents = new TableOfContents().AllContents;
if (contents.Count == 0)
{
  throw new InvalidOperationException("The search index cannot be generated because there is no published content.");
}

using var embedder = new MiniLmEmbedder(modelPath, vocabularyPath);
var cache = EmbeddingCache.Create(
  Path.Combine(Path.GetDirectoryName(DefaultModelDirectory())!, ".embedding-cache"),
  modelPath, vocabularyPath, ModelId, ModelRevision);
var entries = new List<SearchIndexEntry>(contents.Count);
var temporaryVectorsPath = $"{vectorsPath}.tmp";
var temporaryMetadataPath = $"{metadataPath}.tmp";
await using (var vectorStream = File.Create(temporaryVectorsPath))
{
  foreach (var content in contents)
  {
    var vector = cache.GetOrCreate(CreateSearchDocument(content), embedder.Embed);
    if (vector.Length != MiniLmEmbedder.Dimensions)
    {
      throw new InvalidOperationException($"Expected a {MiniLmEmbedder.Dimensions}-dimensional vector for '{content.Slug}', but received {vector.Length} dimensions.");
    }

    vectorStream.Write(MemoryMarshal.AsBytes(vector.AsSpan()));
    entries.Add(new SearchIndexEntry(
      content.Slug,
      content.ModifiedOn.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
  }

  await vectorStream.FlushAsync();
}

var index = new SearchIndexFile(ModelId, ModelRevision, MiniLmEmbedder.Dimensions, entries.Count, entries);
await File.WriteAllTextAsync(temporaryMetadataPath, JsonSerializer.Serialize(index, SearchIndexJsonContext.Default.SearchIndexFile));
File.Move(temporaryVectorsPath, vectorsPath, true);
File.Move(temporaryMetadataPath, metadataPath, true);
Console.WriteLine($"Generated {entries.Count} normalized {MiniLmEmbedder.Dimensions}-dimensional vectors in '{outputDirectory}'.");
Console.WriteLine($"Embeddings: {cache.Hits} reused, {cache.Misses} generated.");

static string CreateSearchDocument(ContentMetaData content)
  => SearchTextNormalizer.Normalize(
    $"{content.Title} {content.Description} Keywords {string.Join(" ", content.Keywords)} Channel {content.Channel}");

static string DefaultModelDirectory([CallerFilePath] string sourceFilePath = "")
  => Path.Combine(Path.GetDirectoryName(sourceFilePath) ?? Directory.GetCurrentDirectory(), ".model-cache");
