﻿using System.ServiceModel.Syndication;
using System.Xml;
using CommandLineSwitchParser;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var author = new SyndicationPerson("abdulrahman.smsi+ilovedotnet@gmail.com", "Abdul Rahman", "https://linkedin.com/in/thebhai");

var feed = new SyndicationFeed(
                "I Love .NET",
                "This is a .NET knowledge sharing platform with live demos crafted by developers for developers with love.",
                new Uri("http://ilovedotnet.org"),
                "http://ilovedotnet.org",
                DateTime.Now)
{
    TimeToLive = TimeSpan.FromHours(24),
    Copyright = new TextSyndicationContent($"Copyright {DateTime.Now.Year}"),
    Language = "en",
    Items = tableOfContents
            .AllContents
            .OrderByDescending(content => content.ModifiedOn)
            .Select(content => new SyndicationItem(
                content.Title, 
                content.Description, 
                new Uri($"http://ilovedotnet.org/{content.Type}/{content.Slug}"),
                $"http://ilovedotnet.org/{content.Type}/{content.Slug}",
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

using var rssWriter = XmlWriter.Create(option.OutputPath ?? "atom.xml", new XmlWriterSettings { Indent = true });
var rssFormatter = new Rss20FeedFormatter(feed);
rssFormatter.WriteTo(rssWriter);