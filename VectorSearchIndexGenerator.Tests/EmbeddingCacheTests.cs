using VectorSearchIndexGenerator;

namespace VectorSearchIndexGenerator.Tests;

public sealed class EmbeddingCacheTests : IDisposable
{
  private readonly string _directory = Path.Combine(AppContext.BaseDirectory, $"ilovedotnet-embeddings-{Guid.NewGuid():N}");

  [Fact]
  public void GetOrCreate_ReusesExactVectorAcrossInstancesAndInvalidatesChangedText()
  {
    var cache = new EmbeddingCache(_directory, "pipeline-v1", 2);
    var expected = new[] { 0.125f, -0.75f };
    Assert.Equal(expected, cache.GetOrCreate("original", _ => expected));

    var reopened = new EmbeddingCache(_directory, "pipeline-v1", 2);
    Assert.Equal(expected, reopened.GetOrCreate("original", _ => throw new InvalidOperationException("Must use cache")));
    Assert.Equal(new[] { 0.5f, 0.25f }, reopened.GetOrCreate("changed", _ => [0.5f, 0.25f]));
  }

  [Fact]
  public void GetOrCreate_DoesNotReuseVectorsFromAnotherPipeline()
  {
    new EmbeddingCache(_directory, "model-and-pipeline-v1", 2).GetOrCreate("same text", _ => [1f, 0f]);

    var changedPipeline = new EmbeddingCache(_directory, "model-and-pipeline-v2", 2);
    Assert.Equal(new[] { 0f, 1f }, changedPipeline.GetOrCreate("same text", _ => [0f, 1f]));
  }

  [Theory]
  [InlineData(true)]
  [InlineData(false)]
  public void GetOrCreate_RejectsTruncatedOrCorruptedCacheEntries(bool truncate)
  {
    var cache = new EmbeddingCache(_directory, "pipeline", 2);
    cache.GetOrCreate("text", _ => [1f, 0f]);
    var path = Assert.Single(Directory.GetFiles(_directory, "*.vec", SearchOption.AllDirectories));
    var bytes = File.ReadAllBytes(path);
    if (truncate)
    {
      bytes = bytes[..^1];
    }
    else
    {
      bytes[^1] ^= 1;
    }

    File.WriteAllBytes(path, bytes);
    Assert.Throws<InvalidDataException>(() => cache.GetOrCreate("text", _ => throw new InvalidOperationException()));
  }

  [Fact]
  public void GetOrCreate_DoesNotCacheFailedGeneration()
  {
    var cache = new EmbeddingCache(_directory, "pipeline", 2);
    Assert.Throws<InvalidOperationException>(() => cache.GetOrCreate("text", _ => throw new InvalidOperationException()));
    Assert.Equal(new[] { 1f, 0f }, cache.GetOrCreate("text", _ => [1f, 0f]));
  }

  [Fact]
  public void GetOrCreate_RejectsInvalidGeneratedVectors()
  {
    var cache = new EmbeddingCache(_directory, "pipeline", 2);
    Assert.Throws<InvalidDataException>(() => cache.GetOrCreate("wrong dimensions", _ => [1f]));
    Assert.Throws<InvalidDataException>(() => cache.GetOrCreate("non-finite", _ => [float.NaN, 0f]));
    Assert.Empty(Directory.GetFiles(_directory, "*.vec", SearchOption.AllDirectories));
  }

  public void Dispose()
  {
    if (Directory.Exists(_directory))
    {
      Directory.Delete(_directory, recursive: true);
    }
  }
}
