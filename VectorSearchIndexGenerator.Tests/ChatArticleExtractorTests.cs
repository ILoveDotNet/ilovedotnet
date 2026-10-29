using VectorSearchIndexGenerator;

namespace VectorSearchIndexGenerator.Tests;

public sealed class ChatArticleExtractorTests
{
  [Fact]
  public void Extract_DecodesArticleEntitiesAndKeepsCodeAndHeadingContext()
  {
    const string source = """
      @page "/blogs/example"
      <Content>
        <What>
          <p>Use <Highlight>List&lt;T&gt;</Highlight> to store items.</p>
        </What>
        <How>
          <h4>Step 1: Create the list</h4>
          <p>Initialize the collection before use.</p>
          <CodeSnippet CssClass="language-csharp">
      var items = new List&lt;string&gt;();
          </CodeSnippet>
          <DemoSnippet><p>This placeholder is not article evidence.</p></DemoSnippet>
        </How>
      </Content>
      """;

    var parents = new ChatArticleExtractor().Extract(source, "example", "Example article");

    Assert.Equal(2, parents.Count);
    Assert.Contains("List<T>", parents[0].Text);
    Assert.Equal("Step 1: Create the list", parents[1].Heading);
    Assert.Contains("var items = new List<string>();", parents[1].Text);
    Assert.DoesNotContain("placeholder", parents[1].Text);
  }

  [Fact]
  public void Extract_RejectsDynamicContentInsteadOfSilentlyIndexingIt()
  {
    const string source = """
      <What>
        @if (showDetails)
        {
          <p>Dynamic content</p>
        }
      </What>
      """;

    Assert.Throws<InvalidDataException>(() => new ChatArticleExtractor().Extract(source, "example", "Example"));
  }

  [Fact]
  public void Chunker_KeepsEveryEmbeddingInputWithinLimitAndUsesDistinctParentIds()
  {
    static int CountTokens(string text) => text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length + 2;

    var firstParent = new ChatArticleParent(
      "example",
      "How",
      "First step",
      string.Join(' ', Enumerable.Repeat("word", 60)),
      ParentOrder: 0);
    var secondParent = firstParent with { ParentOrder = 1 };

    var first = ChatPassageChunker.Create("Example", "blogs/example/", firstParent, CountTokens, maximumTokens: 24);
    var second = ChatPassageChunker.Create("Example", "blogs/example/", secondParent, CountTokens, maximumTokens: 24);

    Assert.NotEmpty(first);
    Assert.All(first.Concat(second), passage => Assert.InRange(passage.TokenCount, 1, 24));
    Assert.DoesNotContain(first.Select(passage => passage.Id), id => second.Any(passage => passage.Id == id));
  }
}
