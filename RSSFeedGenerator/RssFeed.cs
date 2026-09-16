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
    var feedContents = _tableOfContents.AllContents;

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
      Items = feedContents
                .OrderByDescending(content => content.ModifiedOn)
                .Select(content => new SyndicationItem(
                    content.Title,
                    content.Description,
                    new Uri($"https://ilovedotnet.org/{content.ContentUrl}"),
                    $"https://ilovedotnet.org/{content.ContentUrl}",
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

    using var atomWriter = XmlWriter.Create(_filePath, new XmlWriterSettings { Indent = true });
    var atomFormatter = new Atom10FeedFormatter(feed);
    atomFormatter.WriteTo(atomWriter);
  }

  private void LoadExistingFeed()
  {
    if (File.Exists(_filePath))
    {
      using var reader = XmlReader.Create(_filePath);
      var formatter = new Atom10FeedFormatter();
      formatter.ReadFrom(reader);
      _feed = formatter.Feed;
    }
  }

  public bool IsAnyContentUpdatedAndRepublished()
  {
    if (_feed is null)
    {
      return true;
    }

    var feedContents = _tableOfContents.AllContents;
    var existingItems = _feed.Items.ToList();

    if (existingItems.Count != feedContents.Count)
    {
      return true;
    }

    return feedContents.Any(content =>
    {
      var canonicalUrl = $"https://ilovedotnet.org/{content.ContentUrl}";
      var existingItem = existingItems.FirstOrDefault(item =>
          item.Links.Any(link => link.Uri.AbsoluteUri == canonicalUrl)
          || item.Id.EndsWith(content.Slug, StringComparison.OrdinalIgnoreCase));

      return existingItem is null
          || existingItem.Title?.Text != content.Title
          || existingItem.Summary?.Text != content.Description
          || existingItem.LastUpdatedTime.DateTime != content.ModifiedOn
          || existingItem.Links.All(link => link.Uri.AbsoluteUri != canonicalUrl);
    });
  }
}
