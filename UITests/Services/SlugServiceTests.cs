using FluentAssertions;
using Bunit.TestDoubles;
using SharedComponents;
using Microsoft.Extensions.DependencyInjection;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class SlugServiceTests
{
    [Fact]
    public void GetSlug_ReturnsCorrectSlug_FromUri()
    {
        // Arrange
        using var ctx = new TestContext();
        var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
        fakeNavigationManager.NavigateTo("/test-page/some-slug");
        var slugService = new SlugService(fakeNavigationManager);

        // Act
        var result = slugService.GetSlug();

        // Assert
        result.Should().Be("some-slug");
    }

    [Fact]
    public void GetSlug_ThrowsArgumentNullException_WhenSlugIsNull()
    {
        // Arrange
        using var ctx = new TestContext();
        var fakeNavigationManager = ctx.Services.GetRequiredService<FakeNavigationManager>();
        fakeNavigationManager.NavigateTo("/test-page/");
        var slugService = new SlugService(fakeNavigationManager);

        // Act
        var act = () => slugService.GetSlug();

        // Assert
        act.Should().Throw<ArgumentException>().WithParameterName("slug");
    }
}