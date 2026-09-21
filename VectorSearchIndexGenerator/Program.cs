using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using SharedModels;
using VectorSearchIndexGenerator;

const string ModelId = "Xenova/all-MiniLM-L6-v2";
const string ModelRevision = "751bff37182d3f1213fa05d7196b954e230abad9";

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
await DownloadIfMissingAsync(
  $"https://huggingface.co/{ModelId}/resolve/{ModelRevision}/onnx/model_quantized.onnx",
  modelPath);
await DownloadIfMissingAsync(
  $"https://huggingface.co/{ModelId}/resolve/{ModelRevision}/vocab.txt",
  vocabularyPath);

var contents = new TableOfContents().AllContents;
if (contents.Count == 0)
{
  throw new InvalidOperationException("The search index cannot be generated because there is no published content.");
}

using var embedder = new MiniLmEmbedder(modelPath, vocabularyPath);
var entries = new List<SearchIndexEntry>(contents.Count);
var temporaryVectorsPath = $"{vectorsPath}.tmp";
var temporaryMetadataPath = $"{metadataPath}.tmp";
await using (var vectorStream = File.Create(temporaryVectorsPath))
{
  foreach (var content in contents)
  {
    var vector = embedder.Embed(CreateSearchDocument(content));
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

static string CreateSearchDocument(ContentMetaData content)
  => SearchTextNormalizer.Normalize(
    $"{content.Title} {content.Description} Keywords {string.Join(" ", content.Keywords)} Channel {content.Channel}");

static string DefaultModelDirectory([CallerFilePath] string sourceFilePath = "")
  => Path.Combine(Path.GetDirectoryName(sourceFilePath) ?? Directory.GetCurrentDirectory(), ".model-cache");

static async Task DownloadIfMissingAsync(string url, string path)
{
  if (File.Exists(path))
  {
    return;
  }

  Console.WriteLine($"Downloading {url}...");
  var temporaryPath = $"{path}.tmp";
  using var httpClient = new HttpClient();
  await using var source = await httpClient.GetStreamAsync(url);
  await using (var destination = File.Create(temporaryPath))
  {
    await source.CopyToAsync(destination);
    await destination.FlushAsync();
  }
  File.Move(temporaryPath, path, true);
}

