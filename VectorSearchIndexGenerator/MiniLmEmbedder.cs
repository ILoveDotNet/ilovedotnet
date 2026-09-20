using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;

namespace VectorSearchIndexGenerator;

internal sealed class MiniLmEmbedder : IDisposable
{
  public const int Dimensions = 384;
  public const int MaxTokens = 256;

  private readonly InferenceSession _session;
  private readonly BertTokenizer _tokenizer;

  public MiniLmEmbedder(string onnxModelPath, string vocabularyPath)
  {
    _session = new InferenceSession(onnxModelPath);
    _tokenizer = BertTokenizer.Create(vocabularyPath, new BertOptions
    {
      LowerCaseBeforeTokenization = true,
      ApplyBasicTokenization = true
    });
  }

  public float[] Embed(string text)
  {
    var tokenIds = _tokenizer.EncodeToIds(text).ToArray();
    if (tokenIds.Length > MaxTokens)
    {
      tokenIds = tokenIds[..MaxTokens];
      tokenIds[^1] = _tokenizer.SeparatorTokenId;
    }

    var length = tokenIds.Length;
    var inputIds = new DenseTensor<long>([1, length]);
    var attentionMask = new DenseTensor<long>([1, length]);
    var tokenTypeIds = new DenseTensor<long>([1, length]);

    for (var index = 0; index < length; index++)
    {
      inputIds[0, index] = tokenIds[index];
      attentionMask[0, index] = 1;
    }

    using var results = _session.Run(
    [
      NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
      NamedOnnxValue.CreateFromTensor("attention_mask", attentionMask),
      NamedOnnxValue.CreateFromTensor("token_type_ids", tokenTypeIds)
    ]);

    var hidden = results[0].AsTensor<float>();
    var vector = new float[Dimensions];
    for (var token = 0; token < length; token++)
    {
      for (var dimension = 0; dimension < Dimensions; dimension++)
      {
        vector[dimension] += hidden[0, token, dimension];
      }
    }

    for (var dimension = 0; dimension < Dimensions; dimension++)
    {
      vector[dimension] /= length;
    }

    var norm = MathF.Sqrt(vector.Sum(value => value * value));
    if (norm > 0)
    {
      for (var dimension = 0; dimension < Dimensions; dimension++)
      {
        vector[dimension] /= norm;
      }
    }

    return vector;
  }

  public void Dispose() => _session.Dispose();
}
