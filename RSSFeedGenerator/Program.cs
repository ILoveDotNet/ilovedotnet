using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using CommandLineSwitchParser;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var author = new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai");
                foreach (var social in tableOfContents.Authors.First().SocialUrls)
                {
                    author.ElementExtensions.Add(new XElement(social.Type.ToString(), social.Url.ToString()));
                    author.AttributeExtensions.Add(new XmlQualifiedName(social.Type.ToString()), social.Url.ToString());
                }

var feed = new SyndicationFeed(
                "I ❤️ DotNet",
                "This is a .NET knowledge sharing platform with live demos crafted by developers for developers with love.",
                new Uri("http://ilovedotnet.org"),
                "ilovedotnet",
                DateTimeOffset.UtcNow)
{
    Copyright = new TextSyndicationContent($"Copyright {DateTime.UtcNow.Year}"),
    Language = "en-IN",
    Items = tableOfContents
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
                    Authors = { author }
                }),
    ImageUrl = new Uri("https://ilovedotnet.org/image/brand/mini-logo.png"),
    Authors = { author }
};

using var rssWriter = XmlWriter.Create(option.OutputPath ?? "rss.xml", new XmlWriterSettings { Indent = true });
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(rssWriter);