using System.Text.RegularExpressions;

namespace VectorSearchIndexGenerator;

internal static partial class SearchTextNormalizer
{
  public static string Normalize(string text)
    => WhitespaceRegex().Replace(SymbolRegex().Replace(text, " "), " ").Trim();

  [GeneratedRegex(@"\p{S}")]
  private static partial Regex SymbolRegex();

  [GeneratedRegex(@"\s+")]
  private static partial Regex WhitespaceRegex();
}
