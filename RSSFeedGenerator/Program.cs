using CommandLineSwitchParser;
using RSSFeedGenerator;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var rssFeed = new RssFeed(tableOfContents, option.OutputPath!);

rssFeed.GenerateFeed();
