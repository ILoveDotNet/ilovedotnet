using SharedModels;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class TableOfContentsTests
{
  [Fact]
  public void TestContents()
  {
    // Arrange
    var toc = new TableOfContents(new FixedTimeProvider(new DateTimeOffset(2026, 11, 1, 0, 0, 0, TimeSpan.Zero)));

    // Act
    var contents = toc.Contents;

    // Assert
    Assert.NotNull(contents);
    Assert.NotEmpty(contents);
    Assert.Equal(toc.AllContents.Where(content => content.ModifiedOn.Date <= new DateTime(2026, 11, 1)), contents);
  }

  [Theory]
  [InlineData("2026-10-31T18:29:59Z", false)]
  [InlineData("2026-10-31T18:30:00Z", true)]
  [InlineData("2026-10-31T22:24:31Z", true)]
  [InlineData("2026-11-01T00:00:00Z", true)]
  public void Contents_PublishesAtMidnightIstRegardlessOfLocalTimezone(string utcNow, bool published)
  {
    var clock = new FixedTimeProvider(DateTimeOffset.Parse(utcNow, System.Globalization.CultureInfo.InvariantCulture));
    var toc = new TableOfContents(clock);
    const string slug = "advanced-dotnet-garbage-collection-measure-before-you-tune";
    var article = toc.GetContentBySlug(slug);
    var expected = toc.AllContents.Where(content => content.Channel == "Memory"
        && (published || content.Slug != slug)).OrderByDescending(content => content.ModifiedOn)
        .ThenByDescending(content => content.CreatedOn).ToArray();

    Assert.Equal(TimeZoneInfo.Utc, clock.LocalTimeZone);
    Assert.Equal(published, toc.Contents.Contains(article));
    Assert.Equal(expected.Length, toc.SelectedContentTypeTotalCount("Memory"));
    Assert.Equal(expected, toc.FilteredAndPagedContents("Memory", 0, int.MaxValue));
    Assert.Equal(expected.Where(content => content.Slug != slug), toc.GetRelatedContents(slug));
    Assert.Equal(expected.Where(content => content.Slug != slug),
        toc.ExceptAndPagedContents(slug, 0, int.MaxValue).Where(content => content.Channel == "Memory"));
  }

  [Fact]
  public void TestFilteredAndPagedContents()
  {
    // Arrange
    var toc = new TableOfContents();

    // Act
    var contents = toc.FilteredAndPagedContents("Blazor", 0, 10);

    // Assert
    Assert.NotNull(contents);
    Assert.NotEmpty(contents);
    Assert.All(contents, content => Assert.Equal("Blazor", content.Channel, ignoreCase: true));
    Assert.True(contents.Count <= 10);
  }

  [Fact]
  public void TestAvailableContentTypes()
  {
    // Arrange
    var toc = new TableOfContents();

    // Act
    var types = toc.AvailableContentTypes;

    // Assert
    Assert.NotNull(types);
    Assert.NotEmpty(types);
    Assert.All(types, type => Assert.False(string.IsNullOrWhiteSpace(type)));
  }

  [Fact]
  public void AspireLearningPath_IsPublishedAndLinksItsTutorials()
  {
    var toc = new TableOfContents(new FixedTimeProvider(new DateTimeOffset(2027, 2, 21, 0, 0, 0, TimeSpan.Zero)));
    var tutorials = toc.GetContentsByChannel("Aspire").OrderBy(content => content.Order).ToArray();

    Assert.Contains("Aspire", toc.AvailableContentTypes);
    Assert.Equal(
        ["getting-started-with-aspire-in-dotnet", "distributed-testing-with-aspire-and-playwright-in-dotnet"],
        tutorials.Select(content => content.Slug).ToArray());
    Assert.All(tutorials, content =>
    {
      Assert.Contains(content, toc.Contents);
      Assert.Equal($"blogs/{content.Slug}/", content.ContentUrl);
      Assert.Equal("blogs", content.Type);
    });
    Assert.Equal(tutorials.Reverse(), toc.FilteredAndPagedContents("Aspire", 0, 12));
    Assert.Equal([tutorials[1]], toc.GetRelatedContents(tutorials[0].Slug));
  }

  [Theory]
  [InlineData("2027-02-13T18:29:59Z", 0)]
  [InlineData("2027-02-13T18:30:00Z", 1)]
  [InlineData("2027-02-20T18:30:00Z", 2)]
  public void AspireLearningPath_RespectsScheduledPublicationDates(string utcNow, int expectedCount)
  {
    var clock = new FixedTimeProvider(DateTimeOffset.Parse(utcNow, System.Globalization.CultureInfo.InvariantCulture));
    var toc = new TableOfContents(clock);

    Assert.Equal(expectedCount, toc.SelectedContentTypeTotalCount("Aspire"));
    Assert.Equal(expectedCount, toc.FilteredAndPagedContents("Aspire", 0, 12).Count);
  }

  private sealed class FixedTimeProvider(DateTimeOffset utcNow) : TimeProvider
  {
    public override DateTimeOffset GetUtcNow() => utcNow;
    public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
  }
}
