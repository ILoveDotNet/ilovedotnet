using CommandLineSwitchParser;
using SharedModels;
using SitemapGenerator;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var sitemap = new Sitemap(tableOfContents, option.Channel!);

sitemap.LoadSitemap(option.OutputPath!);

if (!sitemap.IsAnyContentUpdatedAndRepublished())
{
  return;
}

sitemap.GenerateSitemap(option.OutputPath!);
