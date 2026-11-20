using BaseComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using SharedComponents;
using SharedModels;
using AdComponent = SharedComponents.GoogleAdSense;

namespace UITests.Components.Content;

[ExcludeFromCodeCoverage]
public class ArticleAdTests
{
  [Theory]
  [InlineData(GoogleAdSenseAdType.InFeed)]
  [InlineData(GoogleAdSenseAdType.InArticle)]
  [InlineData(GoogleAdSenseAdType.Multiplex)]
  [InlineData(GoogleAdSenseAdType.Display)]
  public void ProductionUnit_UsesTheSdkObserverInsteadOfInjectedScripts(GoogleAdSenseAdType type)
  {
    using var ctx = CreateContext("Production");
    var module = ctx.JSInterop.SetupModule("./js/ads.js");
    module.Mode = JSRuntimeMode.Loose;
    var cut = ctx.Render<AdComponent>(parameters => parameters
        .Add(component => component.Type, type)
        .Add(component => component.Format, GoogleAdSenseAdFormat.Fluid)
        .Add(component => component.LayoutKey, "-7m+de+1z+n+3")
        .Add(component => component.Slot, "existing-slot"));

    var ad = cut.Find("ins");
    Assert.Equal("ca-pub-test", ad.GetAttribute("data-ad-client"));
    Assert.Equal("existing-slot", ad.GetAttribute("data-ad-slot"));
    Assert.DoesNotContain("adsbygoogle", ad.ClassList);
    Assert.Empty(cut.FindAll("script"));
    Assert.Equal(type == GoogleAdSenseAdType.Display ? "auto" : "fluid",
        ad.GetAttribute("data-ad-format"));
    Assert.Equal(type == GoogleAdSenseAdType.InFeed, ad.HasAttribute("data-ad-layout-key"));
    Assert.Equal(type == GoogleAdSenseAdType.InArticle, ad.HasAttribute("data-ad-layout"));
    cut.WaitForAssertion(() =>
        Assert.Equal(cut.Find("section").Id, module.Invocations["observeAd"].Single().Arguments[0]));
  }

  [Fact]
  public void AdUnit_RerendersDoNotRefreshButNewUnitsReleaseTheirPreviousObserver()
  {
    using var ctx = CreateContext("Production");
    var module = ctx.JSInterop.SetupModule("./js/ads.js");
    module.Mode = JSRuntimeMode.Loose;
    var cut = ctx.Render<AdComponent>(parameters => parameters
        .Add(component => component.Type, GoogleAdSenseAdType.InArticle)
        .Add(component => component.Format, GoogleAdSenseAdFormat.Fluid)
        .Add(component => component.Slot, "first-slot"));
    var firstId = cut.Find("section").Id;

    cut.Render(parameters => parameters.Add(component => component.Style, "text-align:center;"));
    Assert.Single(module.Invocations["observeAd"]);
    Assert.Equal(firstId, cut.Find("section").Id);
    cut.Render(parameters => parameters.Add(component => component.Slot, "second-slot"));

    Assert.NotEqual(firstId, cut.Find("section").Id);
    Assert.Equal(firstId, module.Invocations["stopObservingAd"].Single().Arguments[0]);
    Assert.Equal(2, module.Invocations["observeAd"].Count);
  }

  [Fact]
  public void AdInitialisationFailure_IsAnnouncedWithoutRetryingOnRerender()
  {
    using var ctx = CreateContext("Production");
    var module = ctx.JSInterop.SetupModule("./js/ads.js");
    module.Mode = JSRuntimeMode.Loose;
    module.SetupVoid("observeAd", _ => true).SetException(new JSException("Module unavailable."));
    var cut = ctx.Render<AdComponent>(parameters => parameters
        .Add(component => component.Type, GoogleAdSenseAdType.InArticle)
        .Add(component => component.Format, GoogleAdSenseAdFormat.Fluid)
        .Add(component => component.Slot, "existing-slot"));

    cut.WaitForAssertion(() => Assert.False(cut.Find("[role='status']").HasAttribute("hidden")));
    Assert.Equal("Advertisement unavailable.", cut.Find("[role='status']").TextContent);
    cut.Render();
    Assert.Single(module.Invocations["observeAd"]);
  }

  [Theory]
  [InlineData("Development")]
  [InlineData("Prerendering")]
  [InlineData("Local")]
  public void NonProductionUnits_DoNotQueueGoogleRequests(string environment)
  {
    using var ctx = CreateContext(environment);
    var cut = ctx.Render<AdComponent>(parameters => parameters
        .Add(component => component.Type, GoogleAdSenseAdType.InArticle)
        .Add(component => component.Format, GoogleAdSenseAdFormat.Fluid)
        .Add(component => component.Slot, "existing-slot"));

    Assert.Empty(cut.FindAll("ins"));
    Assert.Empty(ctx.JSInterop.Invocations);
  }

  [Fact]
  public void ArticleBoundary_EnablesOnlyTheSelectedPlacementWithoutEditingArticles()
  {
    using var ctx = CreateContext("Development");
    var cut = ctx.Render<ArticleAdExample>();
    var boundaries = cut.FindAll("[data-article-ad-break]");
    Assert.Equal(2, boundaries.Count);
    Assert.Empty(cut.FindAll("[data-article-ad]"));

    cut.Render(parameters => parameters.Add(component => component.Context, new(boundaries[0].Id)));

    Assert.Single(cut.FindAll("[data-article-ad='mid-article']"));
    Assert.NotNull(cut.FindAll("[data-article-ad-break]")[0].QuerySelector("[data-article-ad]"));
    Assert.Null(cut.FindAll("[data-article-ad-break]")[1].QuerySelector("[data-article-ad]"));
    Assert.Empty(ctx.JSInterop.Invocations);
  }

  [Fact]
  public void WhyOutsideAnArticle_DoesNotIntroduceAnAdBoundary()
  {
    using var ctx = new BunitContext();
    var cut = ctx.Render<Why>(parameters => parameters.AddChildContent("An explanation."));

    Assert.Empty(cut.FindAll("[data-article-ad-break]"));
  }

  private static BunitContext CreateContext(string environment)
  {
    var ctx = new BunitContext();
    ctx.Services.AddLogging();
    ctx.Services.AddSingleton<IHostEnvironment>(new TestHostEnvironment(environment));
    ctx.Services.AddGoogleAdSense("ca-pub-test");
    return ctx;
  }

  private sealed class TestHostEnvironment(string environment) : IHostEnvironment
  {
    public bool IsProduction() => environment == "Production";
    public bool IsDevelopment() => environment == "Development";
    public bool IsPrerendering() => environment == "Prerendering";
  }
}
