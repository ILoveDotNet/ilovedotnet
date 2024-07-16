using FluentAssertions;
using Bunit.TestDoubles;
using SharedComponents;
using Microsoft.Extensions.DependencyInjection;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class SlugServiceTests
{
    [Fact]
    public void TestSlugFromValidUrl()
    {
        using var ctx = new TestContext();
        var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
        fakeNavigationManager.NavigateTo("http://example.com/test-slug");

        var slugService = new SlugService(fakeNavigationManager);
        var result = slugService.GetSlug();

        result.Should().Be("test-slug");
    }

    [Fact]
    public void TestSlugFromUrlEndingWithSlash()
    {
        using var ctx = new TestContext();
        var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
        fakeNavigationManager.NavigateTo("http://example.com/test-slug/");

        var slugService = new SlugService(fakeNavigationManager);
        var result = slugService.GetSlug();

        result.Should().Be("test-slug");
    }

    [Fact]
    public void TestSlugFromUrlWithoutSlug()
    {
        using var ctx = new TestContext();
        var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
        fakeNavigationManager.NavigateTo("http://example.com/");
        var slugService = new SlugService(fakeNavigationManager);

        var act = () => slugService.GetSlug();

        act.Should().Throw<ArgumentException>().WithParameterName("slug");
    }
}