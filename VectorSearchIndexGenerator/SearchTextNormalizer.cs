using System.Text.RegularExpressions;

namespace VectorSearchIndexGenerator;

internal static partial class SearchTextNormalizer
{
  public static string Normalize(string text)
    => SeparatorRegex().Replace(text, " ").Trim();

  [GeneratedRegex(@"[^\p{L}\p{N}]+")]
  private static partial Regex SeparatorRegex();
}
