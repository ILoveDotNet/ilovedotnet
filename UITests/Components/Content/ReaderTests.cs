using BaseComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using SharedComponents;
using SharedModels;
using ArticleContent = BaseComponents.Content;

namespace UITests.Components.Content;

[ExcludeFromCodeCoverage]
public class ReaderTests
{
  [Theory]
  [InlineData(false)]
  [InlineData(true)]
  public void ArticleContents_LoadFromTheSharedAssetRootOrExplainFailure(bool fail)
  {
    using var ctx = new BunitContext();
    var contents = new TableOfContents();
    var metadata = contents.Contents.First();
    ctx.Services.AddSingleton(contents);
    ctx.Services.AddSingleton<SlugService>();
    ctx.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
    ctx.Services.AddLogging();
    ctx.ComponentFactories.AddStub<ContentHead>();
    ctx.ComponentFactories.AddStub<Breadcrumbs>();
    ctx.ComponentFactories.AddStub<ContentHeader>();
    ctx.ComponentFactories.AddStub<ContentRecommendation>();
    ctx.ComponentFactories.AddStub<SocialShare>();
    ctx.ComponentFactories.AddStub<GoogleAdSense>();
    ctx.ComponentFactories.AddStub<ArticleAd>();
    var module = ctx.JSInterop.SetupModule("./article.js");
    module.Mode = JSRuntimeMode.Loose;
    var outline = module.Setup<ArticleHeading[]>("getHeadings", _ => true);
    if (fail)
    {
      outline.SetException(new JSException("Module unavailable."));
    }
    else
    {
      outline.SetResult([new("cache-key", "Choosing a cache key", 4)]);
    }

    ctx.Services.GetRequiredService<NavigationManager>().NavigateTo($"/{metadata.ContentUrl}/");
    var cut = ctx.Render<ArticleContent>(parameters => parameters
        .Add(component => component.FileName, "Article")
        .Add(component => component.ContentBody,
            builder => builder.AddMarkupContent(0, "<h4>Choosing a cache key</h4>")));

    cut.WaitForAssertion(() =>
    {
      var navigation = cut.FindAll("nav[aria-label='On this page']");
      Assert.Equal(2, navigation.Count);
      foreach (var menu in navigation)
      {
        Assert.Equal(fail ? 4 : 1, menu.QuerySelectorAll("a").Length);
        Assert.Equal(fail ? 1 : 0, menu.QuerySelectorAll("[role='status']").Length);
        if (!fail)
        {
          Assert.EndsWith("#cache-key", menu.QuerySelector("a")!.GetAttribute("href"));
        }
      }

      Assert.Single(module.Invocations["getHeadings"]);
      var enquiry = cut.Find("aside section[aria-label='Sponsorship enquiries']");
      var contact = enquiry.QuerySelector("a")!;
      Assert.Equal("https://www.linkedin.com/in/thebhai", contact.GetAttribute("href"));
      Assert.Equal("_blank", contact.GetAttribute("target"));
      Assert.Contains("noopener", contact.GetAttribute("rel"));
      Assert.Empty(enquiry.QuerySelectorAll("ins"));
    });
  }

  [Fact]
  public void DetailedContents_UseHeadingAnchorsAndPreserveTheCurrentQuery()
  {
    using var ctx = new BunitContext();
    ctx.Services.GetRequiredService<NavigationManager>()
        .NavigateTo("/blogs/cache/?source=reader#how");
    var cut = ctx.Render<ContentNavigation>(parameters => parameters
        .Add(component => component.Slug, "cache")
        .Add(component => component.Headings,
        [
            new ArticleHeading("how", "Implementation", 3),
            new ArticleHeading("cache/key", "Choosing a cache key", 4)
        ]));

    var links = cut.FindAll("a");
    Assert.Equal(2, links.Count);
    Assert.Equal("/blogs/cache/?source=reader#cache%2Fkey", links[1].GetAttribute("href"));
    Assert.Equal("Choosing a cache key", links[1].TextContent);
  }

  [Fact]
  public void ContentsFailure_ExplainsWhyOnlyMainSectionsAreAvailable()
  {
    using var ctx = new BunitContext();
    var cut = ctx.Render<ContentNavigation>(parameters => parameters
        .Add(component => component.Slug, "cache")
        .Add(component => component.DetailedContentsUnavailable, true));

    Assert.Equal(4, cut.FindAll("a").Count);
    Assert.Contains("couldn't load", cut.Find("[role='status']").TextContent);
    Assert.Equal("/blogs/cache/#how", cut.FindAll("a")[2].GetAttribute("href"));
  }

  [Fact]
  public void ArticleHeader_LeadsWithContentButKeepsTeachingVideos()
  {
    using var ctx = new BunitContext();
    ctx.Services.GetRequiredService<NavigationManager>()
        .NavigateTo("/blogs/cache/?source=reader");
    var metadata = new ContentMetaData
    {
      Order = 1,
      Title = "Understanding caching",
      Description = "Choose an appropriate cache.",
      Slug = "cache",
      ContentUrl = "blogs/cache",
      Channel = "Caching",
      Type = "Caching",
      IconUrl = "image/icons/caching.webp",
      PosterUrl = "image/blogs/caching/cache.webp",
      ThumbnailUrl = "image/blogs/caching/cache.webp",
      Author = "Author",
      CreatedOn = new DateTime(2026, 1, 4),
      ModifiedOn = new DateTime(2026, 1, 4),
      Keywords = []
    };
    var cut = ctx.Render<ContentHeader>(parameters => parameters
        .Add(component => component.Metadata, metadata)
        .Add(component => component.FileName, "Cache")
        .Add(component => component.PublishedCount, 2));

    Assert.Empty(cut.FindAll("figure"));
    Assert.DoesNotContain(metadata.PosterUrl, cut.Markup);
    Assert.Equal("/blogs/cache/?source=reader#how",
        cut.FindAll("a").Single(link => link.TextContent == "Jump to implementation").GetAttribute("href"));

    metadata.VideoUrl = "https://www.youtube-nocookie.com/embed/example";
    cut.Render(parameters => parameters.Add(component => component.Metadata, metadata));

    Assert.Equal(metadata.VideoUrl, cut.Find("iframe").GetAttribute("src"));
    Assert.Equal("Understanding caching video", cut.Find("iframe").GetAttribute("title"));
  }

  [Fact]
  public async Task SnippetContext_DescribesTheCodeWithoutBeingIncludedInCopyAsync()
  {
    using var ctx = new BunitContext();
    ctx.Services.AddLogging();
    var module = ctx.JSInterop.SetupModule("./highlight.js");
    module.Mode = JSRuntimeMode.Loose;
    var cut = ctx.Render<SnippetExample>();

    var code = cut.Find("code");
    var region = cut.Find("pre");
    Assert.Equal("Program.cs code", region.GetAttribute("aria-label"));
    var description = cut.Find($"[id='{region.GetAttribute("aria-describedby")}']");
    Assert.Contains("Partial example", description.TextContent);
    Assert.Equal("builder.Services.AddMemoryCache();", code.TextContent);
    await cut.Find("button").ClickAsync();

    Assert.Equal(code.Id, module.Invocations["copyCode"].Single().Arguments[0]);
    Assert.Contains("Copied to clipboard.", cut.Find("[role='status']").TextContent);
  }
}
