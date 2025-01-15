using CommandLineSwitchParser;
using SharedModels;
using SitemapGenerator;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var sitemap = new Sitemap(tableOfContents, option.Channel!);

sitemap.GenerateSitemap(option.OutputPath!);
