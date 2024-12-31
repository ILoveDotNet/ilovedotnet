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
                DateTimeOffset.UtcNow);

var tableOfContents = new TableOfContents();

var items = tableOfContents
            .AllContents
            .Select(content => new SyndicationItem(
                content.Title, 
                content.Description, 
                new Uri($"http://ilovedotnet.org/{content.Type}/{content.Slug}"),
                content.Slug,
                content.ModifiedOn));

feed.Items = items;

using var writer = XmlWriter.Create(option.OutputPath ?? "feed.xml", new XmlWriterSettings { Indent = true });
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(writer);