using CommandLineSwitchParser;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var sitemap = new Sitemap() 
{
    Urls = tableOfContents.GetContentsByChannel(option.Channel!).Select(content => new Url
    {
        Loc = $"https://ilovedotnet.org/blogs/{content.Slug}",
        LastMod = new DateTime(content.ModifiedOn.Year, content.ModifiedOn.Month, content.ModifiedOn.Day, content.ModifiedOn.Hour, content.ModifiedOn.Minute, content.ModifiedOn.Second),
        ChangeFreq = "weekly",
        Priority = 0.5
    }).ToList() 
};
sitemap.GenerateSitemap(option.OutputPath!);