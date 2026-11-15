using System.Text.Json;
using VectorSearchIndexGenerator;

namespace VectorSearchIndexGenerator.Tests;

public sealed class ChatCorpusGeneratorTests : IDisposable
{
  private readonly string _directory = Path.Combine(AppContext.BaseDirectory, $"ilovedotnet-corpus-{Guid.NewGuid():N}");
  private readonly DateOnly _cutoff = new(2026, 11, 15);

  [Theory]
  [InlineData("chunks.json", true)]
  [InlineData("chunks.vec", true)]
  [InlineData("chunks.json", false)]
  [InlineData("chunks.vec", false)]
  public async Task WriteCorpus_RepairsMissingOrCorruptedVersionFiles(string name, bool delete)
  {
    await WriteAsync(_cutoff);
    var path = Path.Combine(Assert.Single(Directory.GetDirectories(Path.Combine(_directory, "versions"))), name);
    var original = File.ReadAllBytes(path);
    var manifest = File.ReadAllBytes(Path.Combine(_directory, "manifest.json"));
    if (delete)
    {
      File.Delete(path);
    }
    else
    {
      File.WriteAllBytes(path, [0]);
    }

    await WriteAsync(_cutoff);

    Assert.Equal(original, File.ReadAllBytes(path));
    Assert.Equal(manifest, File.ReadAllBytes(Path.Combine(_directory, "manifest.json")));
    Assert.Equal(3, File.ReadAllLines(Path.Combine(_directory, "outputs.txt")).Length);
  }

  [Fact]
  public async Task WriteCorpus_PreservesUnchangedVersionFileTimestamps()
  {
    await WriteAsync(_cutoff);
    var version = Assert.Single(Directory.GetDirectories(Path.Combine(_directory, "versions")));
    var timestamp = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    foreach (var path in Directory.GetFiles(version))
    {
      File.SetLastWriteTimeUtc(path, timestamp);
    }

    await WriteAsync(_cutoff);

    Assert.All(Directory.GetFiles(version), path => Assert.Equal(timestamp, File.GetLastWriteTimeUtc(path)));
  }

  [Fact]
  public async Task WriteCorpus_UpdatesCutoffWithoutChangingUnchangedPassages()
  {
    await WriteAsync(_cutoff);
    var version = Assert.Single(Directory.GetDirectories(Path.Combine(_directory, "versions")));

    await WriteAsync(_cutoff.AddDays(1));

    Assert.Equal(version, Assert.Single(Directory.GetDirectories(Path.Combine(_directory, "versions"))));
    using var manifest = JsonDocument.Parse(File.ReadAllText(Path.Combine(_directory, "manifest.json")));
    Assert.Equal("2026-11-16", manifest.RootElement.GetProperty("publicationCutoff").GetString());
  }

  private Task WriteAsync(DateOnly cutoff) => ChatCorpusGenerator.WriteCorpusAsync(
    _directory, cutoff, "model", "revision",
    [new ChatPassage("id", "example", "Example", "How", "Heading", "blogs/example", "Example passage", 4, "")],
    [new float[MiniLmEmbedder.Dimensions]],
    Path.Combine(_directory, "outputs.txt"));

  public void Dispose()
  {
    if (Directory.Exists(_directory))
    {
      Directory.Delete(_directory, recursive: true);
    }
  }
}
