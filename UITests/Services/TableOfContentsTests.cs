using SharedModels;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class TableOfContentsTests
{
  [Fact]
  public void TestContents()
  {
    // Arrange
    var toc = new TableOfContents();

    // Act
    var contents = toc.Contents;

    // Assert
    Assert.NotNull(contents);
    Assert.NotEmpty(contents);
    Assert.All(contents, content => Assert.True(content.CreatedOn.Date <= DateTime.Today.Date));
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
}
