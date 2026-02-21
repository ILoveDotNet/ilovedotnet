namespace SharedModels;

public class LoggingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public LoggingLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Introduction to Logging in .NET - From Basics to Best Practices",
          Description = "In this comprehensive guide, you'll master the fundamentals of logging in .NET applications - from understanding log levels and categories to implementing providers and writing your first structured logs.",
          Author = "Abdul Rahman",
          Slug = "introduction-to-logging-in-dotnet-from-basics-to-best-practices",
          PosterUrl = "image/blogs/logging/introduction-to-logging-in-dotnet-from-basics-to-best-practices.webp",
          ThumbnailUrl = "image/blogs/logging/introduction-to-logging-in-dotnet-from-basics-to-best-practices.webp",
          ContentUrl = "blogs/introduction-to-logging-in-dotnet-from-basics-to-best-practices",
          IconUrl = "image/icons/logging.webp",
          Channel = "Logging",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 2, 22, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 2, 22, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "ILogger", "Log Levels", "Log Categories", "Log Providers", "Serilog", "Structured Logging", ".NET"]
        },
    ];
  }
}
