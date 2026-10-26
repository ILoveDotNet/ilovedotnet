using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace VectorSearchIndexGenerator;

public sealed class ChatArticleExtractor
{
  private static readonly Regex SectionRegex = new(
    @"<(?<name>What|Why|How|Summary)\b[^>]*>(?<body>.*?)</\k<name>\s*>",
    RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
  private static readonly Regex CodeSnippetRegex = new(
    @"<CodeSnippet\b[^>]*>.*?</CodeSnippet\s*>",
    RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
  private static readonly Regex DemoSnippetRegex = new(
    @"<DemoSnippet\b[^>]*>.*?</DemoSnippet\s*>",
    RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
  private static readonly Regex DynamicMarkupRegex = new(
    @"@\s*(?:if|foreach|for|while|switch)\b|@\s*\{",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

  public IReadOnlyList<ChatArticleParent> Extract(string source, string slug, string title)
  {
    var parents = new List<ChatArticleParent>();
    var sectionsFound = false;

    foreach (Match sectionMatch in SectionRegex.Matches(source))
    {
      sectionsFound = true;
      var sectionName = sectionMatch.Groups["name"].Value;
      var markup = DemoSnippetRegex.Replace(sectionMatch.Groups["body"].Value, string.Empty);
      var markupWithoutCode = CodeSnippetRegex.Replace(markup, string.Empty);
      if (DynamicMarkupRegex.IsMatch(markupWithoutCode))
      {
        throw new InvalidDataException($"'{slug}' contains dynamic Razor markup in its {sectionName} section.");
      }

      var document = new HtmlParser().ParseDocument($"<html><body>{markup}</body></html>");
      var blocks = new List<ExtractedBlock>();
      var heading = sectionName;
      CollectBlocks(document.Body!.ChildNodes, blocks, ref heading);
      AddParents(slug, sectionName, blocks, parents);
    }

    if (!sectionsFound || parents.Count == 0)
    {
      throw new InvalidDataException($"'{slug}' has no extractable What/Why/How/Summary article content.");
    }

    return parents;
  }

  private static void CollectBlocks(
    INodeList nodes,
    List<ExtractedBlock> blocks,
    ref string heading)
  {
    foreach (var node in nodes)
    {
      if (node is not IElement element)
      {
        continue;
      }

      var tag = element.LocalName;
      if (IsHeading(tag))
      {
        var headingText = NormalizeText(element.TextContent);
        if (headingText.Length > 0)
        {
          heading = headingText;
        }

        continue;
      }

      if (tag is "demosnippet" or "figure" or "script" or "style" or "svg")
      {
        if (tag == "figure")
        {
          var caption = element.QuerySelector("figcaption");
          var captionText = NormalizeText(caption?.TextContent ?? string.Empty);
          if (captionText.Length > 0)
          {
            blocks.Add(new ExtractedBlock(heading, captionText, IsCode: false));
          }
        }

        continue;
      }

      if (tag is "codesnippet" or "pre")
      {
        var code = NormalizeCode(element.TextContent);
        if (code.Length > 0)
        {
          blocks.Add(new ExtractedBlock(heading, code, IsCode: true));
        }

        continue;
      }

      if (tag is "p" or "li" or "blockquote" or "figcaption")
      {
        var text = NormalizeText(element.TextContent);
        if (text.Length > 0)
        {
          blocks.Add(new ExtractedBlock(heading, text, IsCode: false));
        }

        continue;
      }

      if (tag == "tr")
      {
        var cells = element.QuerySelectorAll("th, td")
          .Select(cell => NormalizeText(cell.TextContent))
          .Where(text => text.Length > 0);
        var row = string.Join(" | ", cells);
        if (row.Length > 0)
        {
          blocks.Add(new ExtractedBlock(heading, row, IsCode: false));
        }

        continue;
      }

      CollectBlocks(element.ChildNodes, blocks, ref heading);
    }
  }

  private static void AddParents(
    string slug,
    string sectionName,
    IReadOnlyList<ExtractedBlock> blocks,
    List<ChatArticleParent> parents)
  {
    var headingRuns = new List<(string Heading, List<ExtractedBlock> Blocks)>();
    foreach (var block in blocks)
    {
      if (headingRuns.Count == 0 || headingRuns[^1].Heading != block.Heading)
      {
        headingRuns.Add((block.Heading, []));
      }

      headingRuns[^1].Blocks.Add(block);
    }

    foreach (var run in headingRuns)
    {
      var text = string.Join(
        "\n\n",
        run.Blocks.Select(block => block.IsCode ? $"```text\n{block.Text}\n```" : block.Text));
      if (text.Length > 0)
      {
        parents.Add(new ChatArticleParent(slug, sectionName, run.Heading, text, parents.Count));
      }
    }
  }

  private static bool IsHeading(string tag)
    => tag.Length == 2 && tag[0] == 'h' && tag[1] is >= '1' and <= '6';

  private static string NormalizeText(string text)
    => Regex.Replace(text, @"\s+", " ").Trim();

  private static string NormalizeCode(string text)
  {
    var lines = text.Replace("\r\n", "\n", StringComparison.Ordinal)
      .Replace('\r', '\n')
      .Split('\n')
      .ToList();
    while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0]))
    {
      lines.RemoveAt(0);
    }

    while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1]))
    {
      lines.RemoveAt(lines.Count - 1);
    }

    var indentation = lines
      .Where(line => !string.IsNullOrWhiteSpace(line))
      .Select(line => line.Length - line.TrimStart().Length)
      .DefaultIfEmpty(0)
      .Min();
    return string.Join('\n', lines.Select(line => line.Length >= indentation ? line[indentation..] : line));
  }

  private sealed record ExtractedBlock(string Heading, string Text, bool IsCode);
}

public sealed record ChatArticleParent(string Slug, string Section, string Heading, string Text, int ParentOrder);

public sealed record ChatPassage(
  string Id,
  string Slug,
  string Title,
  string Section,
  string Heading,
  string ContentUrl,
  string Excerpt,
  int TokenCount,
  [property: JsonIgnore] string EmbeddingPrefix);

public static class ChatPassageChunker
{
  private static readonly Regex SentenceRegex = new(@"(?<=[.!?])\s+", RegexOptions.Compiled);

  public static IReadOnlyList<ChatPassage> Create(
    string title,
    string contentUrl,
    ChatArticleParent parent,
    Func<string, int> countTokens,
    int maximumTokens = MiniLmEmbedder.MaxTokens)
  {
    var prefix = CreatePrefix(title, parent, countTokens, maximumTokens);
    var pieces = SplitText(parent.Text)
      .SelectMany(piece => SplitIfOversized(piece, prefix, countTokens, maximumTokens))
      .ToArray();
    var output = new List<ChatPassage>();
    var builder = new List<string>();
    var passageNumber = 0;

    void Flush()
    {
      if (builder.Count == 0)
      {
        return;
      }

      var excerpt = string.Join("\n\n", builder);
      var input = prefix + excerpt;
      var tokenCount = countTokens(input);
      if (tokenCount > maximumTokens)
      {
        throw new InvalidDataException($"Chat chunk for '{parent.Slug}' exceeds the {maximumTokens}-token limit.");
      }

      output.Add(new ChatPassage(
          $"{parent.Slug}:{parent.Section.ToLowerInvariant()}:{parent.ParentOrder}:{passageNumber++}",
        parent.Slug,
        title,
        parent.Section,
        parent.Heading,
        contentUrl,
        excerpt,
        tokenCount,
        prefix));
      builder.Clear();
    }

    foreach (var piece in pieces)
    {
      var candidate = builder.Count == 0
        ? piece
        : $"{string.Join("\n\n", builder)}\n\n{piece}";
      if (countTokens(prefix + candidate) <= maximumTokens)
      {
        builder.Add(piece);
        continue;
      }

      Flush();
      builder.Add(piece);
    }

    Flush();
    if (output.Count == 0 && parent.Text.Length > 0)
    {
      throw new InvalidDataException($"No chat passages were produced for '{parent.Slug}' (text length {parent.Text.Length}).");
    }

    return output;
  }

  private static string CreatePrefix(
    string title,
    ChatArticleParent parent,
    Func<string, int> countTokens,
    int maximumTokens)
  {
    var titleWords = title.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    var headingWords = parent.Heading.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    string Build() => $"{string.Join(" ", titleWords)}\n{parent.Section}: {string.Join(" ", headingWords)}\n";

    while (countTokens(Build()) > maximumTokens / 2 && headingWords.Count > 0)
    {
      headingWords.RemoveAt(headingWords.Count - 1);
    }

    while (countTokens(Build()) > maximumTokens / 2 && titleWords.Count > 1)
    {
      titleWords.RemoveAt(titleWords.Count - 1);
    }

    var prefix = Build();
    if (countTokens(prefix) >= maximumTokens)
    {
      throw new InvalidDataException($"The title for '{parent.Slug}' exceeds the chat embedding token limit.");
    }

    return prefix;
  }

  private static IEnumerable<string> SplitText(string text)
  {
    var segments = Regex.Split(text, @"(```.*?```)", RegexOptions.Singleline);
    foreach (var segment in segments)
    {
      if (segment.Length == 0)
      {
        continue;
      }

      if (segment.StartsWith("```", StringComparison.Ordinal))
      {
        yield return segment;
        continue;
      }

      foreach (var sentence in SentenceRegex.Split(segment))
      {
        var normalized = sentence.Trim();
        if (normalized.Length > 0)
        {
          yield return normalized;
        }
      }
    }
  }

  private static IEnumerable<string> SplitIfOversized(
    string text,
    string prefix,
    Func<string, int> countTokens,
    int maximumTokens)
  {
    if (countTokens(prefix + text) <= maximumTokens)
    {
      yield return text;
      yield break;
    }

    var words = Regex.Split(text, @"(\s+)")
    .Where(word => word.Length > 0)
    .SelectMany(word => SplitOversizedToken(word, prefix, countTokens, maximumTokens))
    .ToArray();
    var chunk = new List<string>();
    foreach (var word in words)
    {
    var current = string.Concat(chunk);
    var candidate = current + word;
    if (chunk.Count > 0 && countTokens(prefix + candidate) > maximumTokens)
    {
      yield return current.Trim();
        chunk.Clear();
      }

      chunk.Add(word);
    }

    if (chunk.Count > 0)
    {
      yield return string.Concat(chunk).Trim();
    }
  }

  private static IEnumerable<string> SplitOversizedToken(
    string token,
    string prefix,
    Func<string, int> countTokens,
    int maximumTokens)
  {
    if (countTokens(prefix + token) <= maximumTokens)
    {
      yield return token;
      yield break;
    }

    var current = new StringBuilder();
    foreach (var rune in token.EnumerateRunes())
    {
      var candidate = current.ToString() + rune;
      if (current.Length > 0 && countTokens(prefix + candidate) > maximumTokens)
      {
        yield return current.ToString();
        current.Clear();
      }

      if (countTokens(prefix + rune) > maximumTokens)
      {
        throw new InvalidDataException("An individual Unicode character exceeds the chat embedding token limit.");
      }

      current.Append(rune);
    }

    if (current.Length > 0)
    {
      yield return current.ToString();
    }
  }
}
