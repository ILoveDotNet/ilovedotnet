using CommandLineSwitchParser;
using PosterGenerator;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

var tableOfContents = new TableOfContents();

var posterGenerator = new PosterGenerator.PosterGenerator(tableOfContents, option.Channel!, option.OutputPath!);

posterGenerator.GeneratePosters();
