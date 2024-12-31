using System.ServiceModel.Syndication;
using System.Xml;
using CommandLineSwitchParser;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var feed = new SyndicationFeed(
                "I ❤️ DotNet",
                "This is a .NET knowledge sharing platform with live demos crafted by developers for developers with love.",
                new Uri("http://ilovedotnet.org"),
                "ilovedotnet",
                DateTimeOffset.UtcNow)
{
    Copyright = new TextSyndicationContent($"Copyright {DateTime.UtcNow.Year}"),
    Language = "en-IN",
    Items = new TableOfContents()
            .AllContents
            .Select(content => new SyndicationItem(
                content.Title, 
                content.Description, 
                new Uri($"http://ilovedotnet.org/{content.Type}/{content.Slug}"),
                $"http://ilovedotnet.org/{content.Type}/{content.Slug}",
                content.ModifiedOn)
                {
                    PublishDate = content.CreatedOn,
                    Summary = new TextSyndicationContent(content.Description),
                    Content = new TextSyndicationContent(content.Description),
                    Categories = { new SyndicationCategory(content.Channel.Replace("-", " ")) },
                    Authors = { new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai") },
                }),
    ImageUrl = new Uri("https://ilovedotnet.org/image/brand/mini-logo.png"),
};

var person = new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai");
feed.Authors.Add(person);

using var writer = XmlWriter.Create(option.OutputPath ?? "feed.xml", new XmlWriterSettings { Indent = true });
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(writer);