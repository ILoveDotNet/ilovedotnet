using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.Tokenizers;

namespace VectorSearchIndexGenerator;

public sealed class EmbeddingCache
{
  private readonly string _directory;
  private readonly int _dimensions;
  public int Hits { get; private set; }
  public int Misses { get; private set; }

  public EmbeddingCache(string directory, string pipelineFingerprint, int dimensions)
  {
    _directory = Path.Combine(directory, Hash(Encoding.UTF8.GetBytes(pipelineFingerprint)));
    _dimensions = dimensions;
    Directory.CreateDirectory(_directory);
  }

  internal static EmbeddingCache Create(string directory, string modelPath, string vocabularyPath, string modelId, string modelRevision)
  {
    var files = new[]
    {
      modelPath,
      vocabularyPath,
      typeof(EmbeddingCache).Assembly.Location,
      typeof(InferenceSession).Assembly.Location,
      typeof(BertTokenizer).Assembly.Location
    };
    var fingerprints = files.Select(path =>
    {
      using var stream = File.OpenRead(path);
      return Convert.ToHexStringLower(SHA256.HashData(stream));
    });
    var pipeline = string.Join("\n", fingerprints)
      + $"\n{modelId}\n{modelRevision}\n{RuntimeInformation.OSDescription}\n{RuntimeInformation.ProcessArchitecture}\n{Environment.Version}\n{BitConverter.IsLittleEndian}";
    return new EmbeddingCache(
      directory,
      pipeline,
      MiniLmEmbedder.Dimensions);
  }

  public float[] GetOrCreate(string text, Func<string, float[]> generate)
  {
    var path = Path.Combine(_directory, $"{Hash(Encoding.UTF8.GetBytes(text))}.vec");
    if (File.Exists(path))
    {
      var bytes = File.ReadAllBytes(path);
      if (bytes.Length != SHA256.HashSizeInBytes + _dimensions * sizeof(float)
        || !SHA256.HashData(bytes.AsSpan(SHA256.HashSizeInBytes)).AsSpan().SequenceEqual(bytes.AsSpan(0, SHA256.HashSizeInBytes)))
      {
        throw new InvalidDataException($"Embedding cache entry '{path}' is corrupt. Delete it to regenerate the vector.");
      }

      var cached = MemoryMarshal.Cast<byte, float>(bytes.AsSpan(SHA256.HashSizeInBytes)).ToArray();
      Validate(cached, path);
      Hits++;
      return cached;
    }

    var vector = generate(text);
    Validate(vector, path);
    var payload = MemoryMarshal.AsBytes(vector.AsSpan()).ToArray();
    var checksum = SHA256.HashData(payload);
    var temporaryPath = $"{path}.{Guid.NewGuid():N}.tmp";
    try
    {
      using (var stream = File.Create(temporaryPath))
      {
        stream.Write(checksum);
        stream.Write(payload);
      }

      File.Move(temporaryPath, path, overwrite: true);
    }
    finally
    {
      if (File.Exists(temporaryPath))
      {
        File.Delete(temporaryPath);
      }
    }

    Misses++;
    return vector;
  }

  private void Validate(float[] vector, string path)
  {
    if (vector.Length != _dimensions || vector.Any(value => !float.IsFinite(value)))
    {
      throw new InvalidDataException($"Embedding cache entry '{path}' must contain {_dimensions} finite values.");
    }
  }

  private static string Hash(byte[] bytes) => Convert.ToHexStringLower(SHA256.HashData(bytes));
}
