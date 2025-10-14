using System.ServiceModel.Syndication;
using System.Xml;
using SharedModels;

namespace RSSFeedGenerator;

public class RssFeed
{
  private SyndicationFeed? _feed;
  private readonly DateTime _lastPublishedDateTime = DateTime.Now;
  private readonly string _filePath;
  private readonly TableOfContents _tableOfContents;

  public RssFeed(TableOfContents tableOfContents, string? filePath)
  {
    // Use default if path is null or empty (defense in depth)
    const string defaultFileName = "atom.xml";
    
    if (string.IsNullOrWhiteSpace(filePath))
    {
      filePath = defaultFileName;
    }

    // Ensure the path doesn't contain dangerous sequences
    var fileName = Path.GetFileName(filePath);
    if (string.IsNullOrWhiteSpace(fileName) || fileName.Contains(".."))
    {
      throw new ArgumentException("Invalid file path detected", nameof(filePath));
    }

    _tableOfContents = tableOfContents;
    _filePath = filePath;
  }

  public void GenerateFeed()
  {
    LoadExistingFeed();

    if (!IsAnyContentUpdatedAndRepublished())
    {
      return;
    }

    var author = new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai");

    var feed = new SyndicationFeed(
                    "I Love .NET",
                    "This is a .NET knowledge sharing platform with live demos crafted by developers for developers with love.",
                    new Uri("https://ilovedotnet.org"),
                    "https://ilovedotnet.org",
                    _lastPublishedDateTime)
    {
      TimeToLive = TimeSpan.FromHours(24),
      Copyright = new TextSyndicationContent($"Copyright {_lastPublishedDateTime.Year}"),
      Language = "en",
      Items = _tableOfContents
                .AllContents
                .OrderByDescending(content => content.ModifiedOn)
                .Select(content => new SyndicationItem(
                    content.Title,
                    content.Description,
                    new Uri($"https://ilovedotnet.org/{content.Type}/{content.Slug}"),
                    $"https://ilovedotnet.org/{content.Type}/{content.Slug}",
                    new DateTime(content.ModifiedOn.Year, content.ModifiedOn.Month, content.ModifiedOn.Day, content.ModifiedOn.Hour, content.ModifiedOn.Minute, content.ModifiedOn.Second))
                {
                  PublishDate = new DateTime(content.CreatedOn.Year, content.CreatedOn.Month, content.CreatedOn.Day, content.CreatedOn.Hour, content.CreatedOn.Minute, content.CreatedOn.Second),
                  Summary = new TextSyndicationContent(content.Description),
                  Categories = { new SyndicationCategory(content.Channel) },
                  Authors = { author }
                }),
      ImageUrl = new Uri("https://ilovedotnet.org/image/brand/mini-logo.png"),
      Authors = { author }
    };

    using var rssWriter = XmlWriter.Create(_filePath, new XmlWriterSettings { Indent = true });
    var rssFormatter = new Rss20FeedFormatter(feed);
    rssFormatter.WriteTo(rssWriter);
  }

  private void LoadExistingFeed()
  {
    if (File.Exists(_filePath))
    {
      using var reader = XmlReader.Create(_filePath);
      _feed = SyndicationFeed.Load(reader);
    }
  }

  public bool IsAnyContentUpdatedAndRepublished()
  {
    if (_feed != null)
    {
      var existingItemsCount = _feed.Items.Count();
      var isAnyContentUpdatedAndRepublished = _feed
          .Items.Any(existingItem => _tableOfContents
                                      .AllContents
                                      .Any(content => existingItem.Id.EndsWith(content.Slug)
                                              && content.ModifiedOn != existingItem.LastUpdatedTime.DateTime));

      if (existingItemsCount == _tableOfContents.AllContents.Count && !isAnyContentUpdatedAndRepublished)
      {
        return false;
      }
    }

    return true;
  }
}
