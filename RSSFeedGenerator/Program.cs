using CommandLineSwitchParser;
using RSSFeedGenerator;
using SharedModels;

var option = CommandLineSwitch.Parse<CommandLineOptions>(ref args);

// Validate and sanitize the output path to prevent path traversal attacks
var outputPath = ValidateOutputPath(option.OutputPath);

var tableOfContents = new TableOfContents();

var rssFeed = new RssFeed(tableOfContents, outputPath);

rssFeed.GenerateFeed();

static string ValidateOutputPath(string? path)
{
  const string defaultFileName = "atom.xml";

  // Use default if path is null or empty
  if (string.IsNullOrWhiteSpace(path))
  {
    return defaultFileName;
  }

  try
  {
    // Get the full path to resolve any relative paths and remove traversal sequences
    var fullPath = Path.GetFullPath(path);

    // Get the current working directory
    var currentDirectory = Directory.GetCurrentDirectory();

    // Ensure the resolved path is within the current directory or its subdirectories
    if (!fullPath.StartsWith(currentDirectory, StringComparison.OrdinalIgnoreCase))
    {
      Console.WriteLine($"Warning: Output path '{path}' is outside the working directory. Using default: {defaultFileName}");
      return defaultFileName;
    }

    // Validate file extension
    var extension = Path.GetExtension(fullPath);
    if (!string.Equals(extension, ".xml", StringComparison.OrdinalIgnoreCase))
    {
      Console.WriteLine($"Warning: Output path must have .xml extension. Using default: {defaultFileName}");
      return defaultFileName;
    }

    // Ensure parent directory exists, create if needed
    var directory = Path.GetDirectoryName(fullPath);
    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
    {
      Directory.CreateDirectory(directory);
    }

    return fullPath;
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Error validating output path '{path}': {ex.Message}. Using default: {defaultFileName}");
    return defaultFileName;
  }
}
