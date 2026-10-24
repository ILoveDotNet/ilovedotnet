using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class AuthorTests(AppFixture fixture) : PageTest
{
  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task AuthorPage_HasMetadataProfileAndResponsivePublishedArticlesAsync()
  {
    const string path = "authors/abdul-rahman/";
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, path);
    var metadata = new SharedModels.TableOfContents();
    var author = metadata.Authors.Single(item => item.Name == "Abdul Rahman");
    var articles = metadata.Contents.Where(content => content.Author == author.Name).ToList();
    await Expect(Page.Locator("h1")).ToHaveTextAsync(author.Name);
    await Expect(Page.Locator("h1")).ToBeVisibleAsync();
    await Expect(Page.Locator("#brand h1, h1#brand")).ToHaveCountAsync(0);
    var canonical = await SeoTestHelpers.AssertCanonicalMetadataAsync(Page, path);
    await Expect(Page.Locator("script[type='application/ld+json']")).ToHaveCountAsync(1);
    var documents = await SeoTestHelpers.GetJsonLdDocumentsAsync(Page);
    try
    {
      var profile = SeoTestHelpers.GetSchema(documents, "ProfilePage").RootElement;
      Assert.Equal(canonical, profile.GetProperty("url").GetString());
      var person = profile.GetProperty("mainEntity");
      Assert.Equal("Person", person.GetProperty("@type").GetString());
      Assert.Equal($"{canonical}#person", person.GetProperty("@id").GetString());
      Assert.Equal(author.Name, person.GetProperty("name").GetString());
      Assert.Equal(canonical, person.GetProperty("url").GetString());
      Assert.NotEmpty(person.GetProperty("sameAs").EnumerateArray());
    }
    finally
    {
      foreach (var document in documents)
      {
        document.Dispose();
      }
    }

    await Expect(Page.Locator("dl[aria-label='Author contributions'] dd")).ToHaveTextAsync(new[]
    {
      articles.Count.ToString(),
      articles.Select(content => content.Channel).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(),
      $"{author.IndustryExperienceYears}+",
    });
    var socialProfiles = Page.GetByRole(AriaRole.Navigation, new() { Name = "Author social profiles" });
    await Expect(socialProfiles.GetByRole(AriaRole.Link)).ToHaveCountAsync(author.SocialUrls.Count);
    await Expect(socialProfiles.Locator("a svg")).ToHaveCountAsync(author.SocialUrls.Count);
    await Expect(socialProfiles.GetByRole(AriaRole.Link, new() { Name = "LinkedIn", Exact = true })).ToBeVisibleAsync();
    var latest = Page.GetByRole(AriaRole.Region, new() { Name = $"Latest articles by {author.Name}" });
    foreach (var article in articles.OrderByDescending(content => content.ModifiedOn).ThenByDescending(content => content.CreatedOn).Take(6))
    {
      await Expect(latest.Locator($"a[href='/{article.ContentUrl.Trim('/')}/']")).ToHaveCountAsync(2);
    }
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      Assert.True(await socialProfiles.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
      Assert.True(await latest.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
    }
    await SeoTestHelpers.AssertResponsiveThumbnailCardAsync(Page, clamped: true);
  }

  [Fact]
  public async Task AuthorPage_IsLinkedFromResponsiveHomeAuthorCardAsync()
  {
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, WebBaseUrl, "");
    var authors = Page.GetByRole(AriaRole.List, new() { Name = "Meet the author", Exact = true });
    var card = authors.GetByRole(AriaRole.Link);
    await Expect(card).ToHaveCountAsync(1);
    await Expect(card).ToContainTextAsync("Abdul Rahman");
    await Expect(card).ToContainTextAsync("Microsoft MVP");
    await Expect(card).ToContainTextAsync("View author profile");
    await Expect(card).ToHaveAttributeAsync("href", "/authors/abdul-rahman/");
    await authors.ScrollIntoViewIfNeededAsync();
    await Expect(card.Locator("img")).ToHaveJSPropertyAsync("complete", true);
    Assert.True(await card.Locator("img").EvaluateAsync<bool>("element => element.naturalWidth > 0"));
    foreach (var width in new[] { 320, 390, 768, 1280 })
    {
      await Page.SetViewportSizeAsync(width, 900);
      Assert.True(await card.EvaluateAsync<bool>("element => element.scrollWidth <= element.clientWidth"));
      var direction = await card.EvaluateAsync<string>("element => getComputedStyle(element).flexDirection");
      Assert.Equal(width < 640 ? "column" : "row", direction);
    }
    await card.ClickAsync();
    await Expect(Page.Locator("h1")).ToHaveTextAsync("Abdul Rahman");
  }
}
