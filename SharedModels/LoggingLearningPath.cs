namespace SharedModels;

public class LoggingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(5);

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
      new ContentMetaData
        {
          Order = 2,
          Title = "Mastering Modern .NET Logging - Structured Logging and Advanced Concepts",
          Description = "In this deep-dive guide, you'll learn advanced structured logging techniques in .NET, including message templates, log parameters, event IDs, and why string interpolation breaks production observability.",
          Author = "Abdul Rahman",
          Slug = "mastering-modern-dotnet-logging-structured-logging-and-advanced-concepts",
          PosterUrl = "image/blogs/logging/mastering-modern-dotnet-logging-structured-logging-and-advanced-concepts.webp",
          ThumbnailUrl = "image/blogs/logging/mastering-modern-dotnet-logging-structured-logging-and-advanced-concepts.webp",
          ContentUrl = "blogs/mastering-modern-dotnet-logging-structured-logging-and-advanced-concepts",
          IconUrl = "image/icons/logging.webp",
          Channel = "Logging",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 3, 1, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 3, 1, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "ILogger", "Structured Logging", "Semantic Logging", "Message Templates", "Log Levels", "Log Categories", "Event IDs", ".NET"]
        },
      new ContentMetaData
        {
          Order = 3,
          Title = "Understanding .NET Logging Providers - From Console to Custom Implementations",
          Description = "Learn about log providers in .NET - what they are, which providers ship out of the box, how to integrate external providers like Application Insights, how to control provider registration per environment, and how to build a custom provider from scratch.",
          Author = "Abdul Rahman",
          Slug = "understanding-dotnet-logging-providers-from-console-to-custom-implementations",
          PosterUrl = "image/blogs/logging/understanding-dotnet-logging-providers-from-console-to-custom-implementations.webp",
          ThumbnailUrl = "image/blogs/logging/understanding-dotnet-logging-providers-from-console-to-custom-implementations.webp",
          ContentUrl = "blogs/understanding-dotnet-logging-providers-from-console-to-custom-implementations",
          IconUrl = "image/icons/logging.webp",
          Channel = "Logging",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 3, 8, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 3, 8, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "Log Providers", "ILoggerProvider", "ILogger", "Console Provider", "Application Insights", "Custom Logger", "ClearProviders", ".NET"]
        },
      new ContentMetaData
        {
          Order = 4,
          Title = "Getting Started with Serilog in .NET - From Installation to Production-Ready Logging",
          Description = "In this comprehensive guide, you'll learn how to integrate Serilog into .NET applications — from creating your first logger to driving configuration from appsettings.json, enriching logs with metadata, destructuring structured data, timing operations, and integrating with Application Insights.",
          Author = "Abdul Rahman",
          Slug = "getting-started-with-serilog-in-dotnet-from-installation-to-production-ready-logging",
          PosterUrl = "image/blogs/logging/getting-started-with-serilog-in-dotnet-from-installation-to-production-ready-logging.webp",
          ThumbnailUrl = "image/blogs/logging/getting-started-with-serilog-in-dotnet-from-installation-to-production-ready-logging.webp",
          ContentUrl = "blogs/getting-started-with-serilog-in-dotnet-from-installation-to-production-ready-logging",
          IconUrl = "image/icons/logging.webp",
          Channel = "Logging",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 3, 15, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 3, 15, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "Serilog", "Structured Logging", "Sinks", "Enrichers", "Log Context", "Application Insights", "Destructuring", "SerilogTimings", ".NET"]
        },
      new ContentMetaData
        {
          Order = 5,
          Title = "High Performance Logging in .NET",
          Description = "Learn how to optimise logging on hot paths in .NET using LoggerMessage.Define and the [LoggerMessage] source generator. Both techniques eliminate boxing and heap allocation, delivering zero-cost logging when the target log level is disabled.",
          Author = "Abdul Rahman",
          Slug = "high-performance-logging-in-dotnet",
          PosterUrl = "image/blogs/logging/high-performance-logging-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/logging/high-performance-logging-in-dotnet.webp",
          ContentUrl = "blogs/high-performance-logging-in-dotnet",
          IconUrl = "image/icons/logging.webp",
          Channel = "Logging",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 3, 22, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 3, 22, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "High Performance Logging", "LoggerMessage", "Source Generator", "LoggerMessage.Define", "Hot Path", "Boxing", "ILogger", ".NET"]
        },
    ];
  }
}
