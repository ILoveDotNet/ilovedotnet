using System.Xml.Linq;
using System.Text.Json;
using System.Security.Cryptography;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task LearningChatPage_HasMetadataStructuredDataAndStaticSitemapEntryAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/?source=test");
    await SeoTestHelpers.AssertWebsiteMetadataAsync(Page, "chat/");
    var canonicalBaseUrl = SeoTestHelpers.GetExpectedCanonicalBaseUrl();
    var canonical = SeoTestHelpers.ExpectedUrl(canonicalBaseUrl, "chat/");
    await Expect(Page).ToHaveTitleAsync("Learn with AI - I ❤️ DotNet");
    await Expect(Page.Locator("meta[name='description']")).ToHaveAttributeAsync("content",
        "Search .NET, Blazor, and C# learning articles in your browser, with passages linked to their original sources.");
    await Expect(Page.Locator("meta[name='robots']")).ToHaveAttributeAsync("content", "noindex,follow");
    await SeoTestHelpers.AssertSocialMetadataAsync(Page, canonical, "website", "summary");
    await Expect(Page.Locator("link[rel='sitemap']")).ToHaveAttributeAsync("href",
        SeoTestHelpers.ExpectedUrl(canonicalBaseUrl, "sitemap.xml"));
    await Expect(Page.Locator("nav a[href='/chat/']")).ToHaveCountAsync(1);
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Learn with AI" })).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Enable local article search" })).ToHaveCountAsync(0);

    var response = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "sitemap-static.xml"));
    Assert.Equal(200, response.Status);
    var document = XDocument.Parse(await response.TextAsync());
    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    Assert.Single(document.Descendants(ns + "loc"), element =>
        element.Value == "https://ilovedotnet.org/chat/");
  }

  [Fact]
  public async Task LearningChat_AutomaticallyLoadsAndRetrievesPublishedBlogPassagesAsync()
  {
    var manifestResponse = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "chat/corpus/manifest.json"));
    Assert.Equal(200, manifestResponse.Status);
    using var manifest = JsonDocument.Parse(await manifestResponse.TextAsync());
    var root = manifest.RootElement;
    var vectorsResponse = await Page.APIRequest.GetAsync(
        SeoTestHelpers.ExpectedUrl(WebBaseUrl, $"chat/corpus/{root.GetProperty("vectorsUrl").GetString()}"));
    Assert.Equal(200, vectorsResponse.Status);
    var queryVector = new float[root.GetProperty("dimensions").GetInt32()];
    Buffer.BlockCopy(await vectorsResponse.BodyAsync(), 0, queryVector, 0, queryVector.Length * sizeof(float));

    var fakeTransformersModule = $$"""
        export const env = {};
        export async function pipeline() {
          const extractor = async () => ({ data: new Float32Array({{JsonSerializer.Serialize(queryVector)}}) });
          extractor.tokenizer = { encode: () => [1] };
          return extractor;
        }
        """;
    await Context.RouteAsync("**/lib/transformers/transformers.min.js", route =>
        route.FulfillAsync(new()
        {
          Status = 200,
          ContentType = "text/javascript",
          Body = fakeTransformersModule,
        }));

    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/");
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Enable local article search" })).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync(
        "Local article search is ready. Questions stay in this browser.", new() { Timeout = 30_000 });
    await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);

    await Page.GetByRole(AriaRole.Textbox).FillAsync("How does this work?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.", new() { Timeout = 15_000 });
    var sources = Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" }).GetByRole(AriaRole.Link);
    Assert.NotEmpty(await sources.AllAsync());
    foreach (var source in await sources.AllAsync())
    {
      var url = new Uri((await source.GetAttributeAsync("href"))!);
      Assert.Equal(WebBaseUrl.Authority, url.Authority);
      Assert.StartsWith("/blogs/", url.AbsolutePath, StringComparison.Ordinal);
    }
    await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
  }

  [Theory]
  [InlineData(false)]
  [InlineData(true)]
  public async Task LearningChat_AutomaticLoadingCanBeRetriedAfterFailureOrCancellationAsync(bool cancel)
  {
    var firstRequest = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
    var releaseRequest = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
    var requests = 0;
    await Context.RouteAsync("**/chat/corpus/manifest.json", async route =>
    {
      if (Interlocked.Increment(ref requests) > 1)
      {
        await route.ContinueAsync();
        return;
      }
      firstRequest.TrySetResult();
      if (cancel)
      {
        await releaseRequest.Task;
        await route.AbortAsync();
      }
      else
      {
        await route.FulfillAsync(new() { Status = 503, Body = "Temporarily unavailable" });
      }
    });
    await Context.RouteAsync("**/lib/transformers/transformers.min.js", route =>
        route.FulfillAsync(new()
        {
          ContentType = "text/javascript",
          Body = "export const env = {}; export async function pipeline() { return async () => {}; }",
        }));
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/");
    await firstRequest.Task.WaitAsync(TimeSpan.FromSeconds(30));
    if (cancel)
    {
      try
      {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Cancel loading" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Loading cancelled. No question was sent.");
      }
      finally
      {
        releaseRequest.TrySetResult();
      }
    }
    else
    {
      await Expect(Page.GetByRole(AriaRole.Alert)).ToContainTextAsync("HTTP 503");
    }
    await Page.GetByRole(AriaRole.Button, new() { Name = "Retry loading article search" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Local article search is ready. Questions stay in this browser.");
    await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Retry loading article search" })).ToHaveCountAsync(0);
  }

  [Fact]
  public async Task LearningChat_FocusedQuestionsExcludeUnrelatedHighSimilarityPassagesAsync()
  {
    var passages = new[]
    {
      new { id = "gc:what", slug = "gc", title = "Garbage Collection", section = "What", heading = "What is garbage collection?",
        contentUrl = "blogs/gc/", excerpt = "Garbage collection automatically reclaims unreachable managed objects.", tokenCount = 20 },
      new { id = "gc:sweep", slug = "gc", title = "Garbage Collection", section = "How", heading = "Phase 2 - Sweep",
        contentUrl = "blogs/gc/", excerpt = "Sweep reclaims dead objects' memory after marking.", tokenCount = 20 },
      new { id = "gc:overview", slug = "gc", title = "Garbage Collection", section = "How", heading = "Mark, Sweep, and Compact",
        contentUrl = "blogs/gc/", excerpt = "There are several phases in each collection.", tokenCount = 20 },
      new { id = "oops:how", slug = "oops-abstraction", title = "OOPS Abstraction", section = "How", heading = "What is Abstraction?",
        contentUrl = "blogs/oops-abstraction/", excerpt = "Abstraction eliminates irrelevant details.", tokenCount = 20 },
      new { id = "lifetimes:captive", slug = "lifetimes", title = "Service Lifetimes", section = "How", heading = "Captive Dependency",
        contentUrl = "blogs/lifetimes/", excerpt = "Compare transient scoped and singleton service lifetimes to avoid captive dependencies.", tokenCount = 20 },
      new { id = "lifetimes:transient", slug = "lifetimes", title = "Service Lifetimes", section = "How", heading = "Transient",
        contentUrl = "blogs/lifetimes/", excerpt = "Transient services are created each time requested.", tokenCount = 20 },
      new { id = "lifetimes:scoped", slug = "lifetimes", title = "Service Lifetimes", section = "How", heading = "Scoped",
        contentUrl = "blogs/lifetimes/", excerpt = "Scoped services are disposed at the end of the request.", tokenCount = 20 },
      new { id = "lifetimes:singleton", slug = "lifetimes", title = "Service Lifetimes", section = "How", heading = "Singleton",
        contentUrl = "blogs/lifetimes/", excerpt = "Singleton services reuse the same instance.", tokenCount = 20 },
      new { id = "unrelated:singleton", slug = "unrelated", title = "Other Article", section = "How", heading = "Singleton",
        contentUrl = "blogs/unrelated/", excerpt = "Singleton content from an article below the similarity threshold.", tokenCount = 20 },
    };
    var chunks = JsonSerializer.SerializeToUtf8Bytes(passages);
    var vectors = new float[passages.Length * 384];
    foreach (var (similarity, index) in new[] { 0.5f, 0.4f, 0.8f, 1f, 0.9f, 0.4f, 0.4f, 0.4f, 0.2f }.Select((value, index) => (value, index)))
    {
      vectors[index * 384] = similarity;
      vectors[index * 384 + 1] = MathF.Sqrt(1 - similarity * similarity);
    }
    var vectorBytes = new byte[vectors.Length * sizeof(float)];
    Buffer.BlockCopy(vectors, 0, vectorBytes, 0, vectorBytes.Length);
    var manifest = JsonSerializer.Serialize(new
    {
      schemaVersion = 1,
      corpusVersion = "test",
      model = "Xenova/all-MiniLM-L6-v2",
      modelRevision = "751bff37182d3f1213fa05d7196b954e230abad9",
      dimensions = 384,
      count = passages.Length,
      vectorFormat = "float32-le",
      chunksUrl = "chunks.json",
      vectorsUrl = "chunks.vec",
      chunksSha256 = Convert.ToHexStringLower(SHA256.HashData(chunks)),
      vectorsSha256 = Convert.ToHexStringLower(SHA256.HashData(vectorBytes)),
    });
    await Context.RouteAsync("**/chat/corpus/manifest.json", route =>
        route.FulfillAsync(new() { ContentType = "application/json", Body = manifest }));
    await Context.RouteAsync("**/chat/corpus/chunks.json", route =>
        route.FulfillAsync(new() { ContentType = "application/json", BodyBytes = chunks }));
    await Context.RouteAsync("**/chat/corpus/chunks.vec", route =>
        route.FulfillAsync(new() { ContentType = "application/octet-stream", BodyBytes = vectorBytes }));
    await Context.RouteAsync("**/lib/transformers/transformers.min.js", route =>
        route.FulfillAsync(new()
        {
          ContentType = "text/javascript",
          Body = """
            export const env = {};
            export async function pipeline() {
              const extractor = async () => { const data = new Float32Array(384); data[0] = 1; return { data }; };
              extractor.tokenizer = { encode: () => [1] };
              return extractor;
            }
            """,
        }));
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "chat/");
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Local article search is ready. Questions stay in this browser.");

    foreach (var (question, expectedExcerpt, expectedCount) in new[]
    {
      ("what is garbage collection?", passages[0].excerpt, 2),
      ("what is sweeping?", passages[1].excerpt, 2),
      ("what is abstraction?", passages[3].excerpt, 1),
      ("what is it?", passages[3].excerpt, 1),
      ("Compare transient scoped and singleton service lifetimes.", passages[5].excerpt, 3),
    })
    {
      await Page.GetByRole(AriaRole.Textbox).FillAsync(question);
      await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
      await Expect(Page.GetByRole(AriaRole.Textbox)).ToHaveValueAsync("");
      await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.");
      var sources = Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" }).Last.GetByRole(AriaRole.Listitem);
      await Expect(sources).ToHaveCountAsync(expectedCount);
      await Expect(sources.First).ToContainTextAsync(expectedExcerpt);
      if (question.Contains("collection", StringComparison.Ordinal) || question.Contains("sweeping", StringComparison.Ordinal))
      {
        await Expect(sources.GetByRole(AriaRole.Link, new() { Name = "OOPS Abstraction" })).ToHaveCountAsync(0);
      }
      if (question.StartsWith("Compare", StringComparison.Ordinal))
      {
        await Expect(sources.Nth(1)).ToContainTextAsync(passages[6].excerpt);
        await Expect(sources.Nth(2)).ToContainTextAsync(passages[7].excerpt);
        await Expect(sources.GetByRole(AriaRole.Link, new() { Name = "Other Article" })).ToHaveCountAsync(0);
      }
    }
    await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
  }

  [Fact]
  public async Task LearningChatCorpus_IsPublishedWithMatchingPassageAndVectorAssetsAsync()
  {
    var manifestResponse = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "chat/corpus/manifest.json"));
    Assert.Equal(200, manifestResponse.Status);
    using var manifest = JsonDocument.Parse(await manifestResponse.TextAsync());
    var root = manifest.RootElement;

    Assert.Equal(1, root.GetProperty("schemaVersion").GetInt32());
    Assert.Equal("float32-le", root.GetProperty("vectorFormat").GetString());
    var count = root.GetProperty("count").GetInt32();
    Assert.True(count > 0);

    foreach (var assetProperty in new[] { "chunksUrl", "vectorsUrl" })
    {
      var assetUrl = root.GetProperty(assetProperty).GetString();
      Assert.False(string.IsNullOrWhiteSpace(assetUrl));
      var assetResponse = await Page.APIRequest.GetAsync(
          SeoTestHelpers.ExpectedUrl(WebBaseUrl, $"chat/corpus/{assetUrl}"));
      Assert.Equal(200, assetResponse.Status);
      Assert.True((await assetResponse.BodyAsync()).Length > 0);
      if (assetProperty == "chunksUrl")
      {
        using var chunks = JsonDocument.Parse(await assetResponse.TextAsync());
        Assert.Equal(count, chunks.RootElement.GetArrayLength());
        foreach (var passage in chunks.RootElement.EnumerateArray())
        {
          var url = new Uri(WebBaseUrl, passage.GetProperty("contentUrl").GetString());
          Assert.Equal(WebBaseUrl.Authority, url.Authority);
          Assert.StartsWith("/blogs/", url.AbsolutePath, StringComparison.Ordinal);
        }
      }
    }

    var workerResponse = await Page.APIRequest.GetAsync(SeoTestHelpers.ExpectedUrl(WebBaseUrl, "js/chat/retrieval-worker.js"));
    Assert.Equal(200, workerResponse.Status);
  }
}
