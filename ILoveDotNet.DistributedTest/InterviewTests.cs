using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class InterviewTests(AppFixture fixture) : PageTest
{
  private const string Heading = "C# and .NET Interview Questions and Answers";
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task InterviewPage_HasMetadataStructuredDataAndWorkingAnswerTopicsAsync()
  {
    const string path = "interview/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    await Expect(Page.Locator("h1")).ToHaveTextAsync(Heading);
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    var canonical = await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);
    foreach (var content in await Page.Locator("meta[property='robots'], meta[name='robots']")
        .EvaluateAllAsync<string[]>("elements => elements.map(element => element.content)"))
    {
      Assert.DoesNotContain("noindex", content, StringComparison.OrdinalIgnoreCase);
    }
    await SeoTestHelpers.AssertSocialMetadataAsync(Page, canonical, "website", "summary");

    var topicLinks = Page.Locator("nav[aria-label='Interview preparation topics'] a");
    await Expect(topicLinks).ToHaveCountAsync(6);
    var topicHrefs = await topicLinks.EvaluateAllAsync<string[]>("links => links.map(link => link.getAttribute('href'))");
    foreach (var href in topicHrefs)
    {
      Assert.StartsWith("interview/#", href, StringComparison.Ordinal);
      var target = new Uri(WebBaseUrl, href);
      Assert.Equal("/interview/", target.AbsolutePath);
      await Expect(Page.Locator(target.Fragment)).ToHaveCountAsync(1);
    }

    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
    var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      var collection = SeoTestHelpers.GetSchema(documents, "CollectionPage").RootElement;
      Assert.Equal(canonical, collection.GetProperty("@id").GetString());
      Assert.Equal(canonical, collection.GetProperty("url").GetString());
      Assert.Equal(await Page.TitleAsync(), collection.GetProperty("name").GetString());
      Assert.Equal("I ❤️ DotNet", collection.GetProperty("publisher").GetProperty("name").GetString());
      Assert.Equal("en", collection.GetProperty("inLanguage").GetString());
      Assert.True(collection.GetProperty("isAccessibleForFree").GetBoolean());
      var items = collection.GetProperty("mainEntity").GetProperty("itemListElement");
      Assert.Equal(6, items.GetArrayLength());
      var fragments = topicHrefs.Select(href => new Uri(WebBaseUrl, href).Fragment).ToArray();
      var headingIds = await Page.Locator("h2").EvaluateAllAsync<string[]>(
          "headings => headings.map(heading => '#' + heading.id)");
      Assert.Equal(fragments, headingIds);
      var position = 1;
      foreach (var item in items.EnumerateArray())
      {
        Assert.Equal(position, item.GetProperty("position").GetInt32());
        var url = new Uri(item.GetProperty("url").GetString()!);
        Assert.Equal(fragments[position - 1], url.Fragment);
        Assert.Equal(canonical, url.GetLeftPart(UriPartial.Path));
        await Expect(Page.Locator(url.Fragment)).ToHaveTextAsync(item.GetProperty("name").GetString()!);
        position++;
      }
      SeoTestHelpers.AssertBreadcrumbSchema(SeoTestHelpers.GetSchema(documents, "BreadcrumbList"));
    }
    finally
    {
      foreach (var document in documents)
      {
        document.Dispose();
      }
    }

    foreach (var (sectionId, answerCount) in new[]
    {
      ("csharp-interview-questions", 6),
      ("dotnet-interview-questions", 6),
      ("system-design-interview", 3),
    })
    {
      var section = Page.Locator($"section[aria-labelledby='{sectionId}']");
      await Expect(section.Locator("details")).ToHaveCountAsync(answerCount);
      foreach (var answer in await section.Locator("details p").AllTextContentsAsync())
      {
        Assert.True(answer.Length >= 200, $"Expected a substantive answer in {sectionId}.");
      }
      var disclosure = section.Locator("details").First;
      await Expect(disclosure.Locator("p")).ToBeHiddenAsync();
      await disclosure.Locator("summary").FocusAsync();
      await Page.Keyboard.PressAsync("Enter");
      await Expect(disclosure.Locator("p")).ToBeVisibleAsync();
      await disclosure.Locator("summary").ClickAsync();
      await Expect(disclosure.Locator("p")).ToBeHiddenAsync();
    }

    await topicLinks.Nth(2).ClickAsync();
    Assert.Equal("/interview/", new Uri(Page.Url).AbsolutePath);
    Assert.Equal("#csharp-interview-questions", new Uri(Page.Url).Fragment);
    await Expect(Page.Locator("#csharp-interview-questions")).ToBeInViewportAsync();
    var headingBox = await Page.Locator("#csharp-interview-questions").BoundingBoxAsync();
    var headerBox = await Page.Locator("#header").BoundingBoxAsync();
    Assert.NotNull(headingBox);
    Assert.NotNull(headerBox);
    Assert.True(headingBox.Y >= headerBox.Y + headerBox.Height,
        "Topic headings should not be hidden beneath the sticky header.");
  }

  [Fact]
  public async Task InterviewPage_HasActionFirstResponsiveLayoutAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "interview/");
    await Expect(Page.Locator("main h1")).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "14-point interview readiness checklist", Exact = true })).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "14. Contributions beyond project work" })).ToBeVisibleAsync();
    var readiness = Page.Locator("section[aria-labelledby='interview-readiness']");
    await Expect(readiness.Locator("article")).ToHaveCountAsync(14);
    await Expect(readiness.Locator("h3")).ToHaveCountAsync(3);
    await Expect(Page.Locator("section[aria-labelledby='preparation-plan'] li")).ToHaveCountAsync(7);
    Assert.Equal("interview-readiness", await Page.Locator("h2").First.GetAttributeAsync("id"));
    Assert.Equal("preparation-plan", await Page.Locator("h2").Nth(1).GetAttributeAsync("id"));
    var faq = Page.Locator("section[aria-labelledby='interview-faq'] details");
    await Expect(faq).ToHaveCountAsync(3);
    await faq.First.Locator("summary").ClickAsync();
    await Expect(faq.First.Locator("p")).ToBeVisibleAsync();
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 844);
      foreach (var darkMode in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", darkMode);
        Assert.True(await Page.EvaluateAsync<bool>(
            "() => { const main = document.querySelector('main'); return main.scrollWidth <= main.clientWidth && main.getBoundingClientRect().right <= innerWidth; }"),
            $"Interview should not overflow at {width}px in {(darkMode ? "dark" : "light")} mode.");
        Assert.True(await Page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth <= innerWidth"),
            $"Interview content must not widen the page at {width}px.");
        var cards = Page.Locator("section[aria-labelledby='delivery-readiness-title'] article");
        var first = await cards.Nth(0).BoundingBoxAsync();
        var second = await cards.Nth(1).BoundingBoxAsync();
        Assert.NotNull(first);
        Assert.NotNull(second);
        if (width < 768)
        {
          Assert.True(second.Y > first.Y, "Substantial cards should stack on mobile.");
        }
        else
        {
          Assert.Equal(first.Y, second.Y);
          Assert.True(second.X > first.X, "Substantial cards should use two columns on larger screens.");
        }
      }
    }
  }

  [Theory]
  [InlineData("")]
  [InlineData("learningpath/")]
  public async Task InterviewPage_IsLinkedFromDiscoveryPageAsync(string path)
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    var link = Page.GetByRole(AriaRole.Link, new()
    {
      Name = "Start preparing for your C# and .NET interview",
      Exact = true,
    });
    await Expect(link).ToHaveCountAsync(1);
    await Expect(link).ToHaveAttributeAsync("href", "interview/");
    await Expect(link).ToHaveTextAsync("Start preparing");
    await link.ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync(Heading);
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(2);
  }
}
