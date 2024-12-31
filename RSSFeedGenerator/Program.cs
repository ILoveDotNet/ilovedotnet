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
                content.Slug,
                content.ModifiedOn)),
    ImageUrl = new Uri("https://ilovedotnet.org/image/brand/mini-logo.png"),
};

var person = new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai");
feed.Authors.Add(person);

using var writer = XmlWriter.Create(option.OutputPath ?? "feed.xml", new XmlWriterSettings { Indent = true });
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(writer);