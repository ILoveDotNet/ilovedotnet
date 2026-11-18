namespace SharedModels;

public class AspireLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(2);

  public AspireLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
        {
            Order = 1,
            Title = "Getting Started with Aspire in .NET",
            Description = "Add Aspire to an existing production application with CLI commands. Understand AppHost, ServiceDefaults, one-launch startup, and isolated worktrees.",
            Author = "Abdul Rahman",
            Slug = "getting-started-with-aspire-in-dotnet",
            PosterUrl = "image/blogs/aspire/getting-started-with-aspire-in-dotnet.webp",
            ThumbnailUrl = "image/blogs/aspire/getting-started-with-aspire-in-dotnet.webp",
            ContentUrl = "blogs/getting-started-with-aspire-in-dotnet",
            IconUrl = "image/icons/aspire.webp",
            Channel = "Aspire",
            Type = "blogs",
            CreatedOn = new DateTime(2027, 2, 14, 22, 30, 0, DateTimeKind.Utc),
            ModifiedOn = new DateTime(2027, 2, 14, 22, 30, 0, DateTimeKind.Utc),
            Keywords = ["Aspire", ".NET", "AppHost", "Service Defaults", "PostgreSQL", "Service Discovery", "OpenTelemetry", "Docker", "Kafka", "Polyglot", "Database Seeding", "Git Worktrees"]
        },
        new ContentMetaData
        {
            Order = 2,
            Title = "Distributed Testing with Aspire and Playwright in .NET",
            Description = "Use shared Aspire fixtures and Playwright to run disposable end-to-end tests, verify results in the UI, and avoid test-data buildup in shared environments.",
            Author = "Abdul Rahman",
            Slug = "distributed-testing-with-aspire-and-playwright-in-dotnet",
            PosterUrl = "image/blogs/aspire/distributed-testing-with-aspire-and-playwright-in-dotnet.webp",
            ThumbnailUrl = "image/blogs/aspire/distributed-testing-with-aspire-and-playwright-in-dotnet.webp",
            ContentUrl = "blogs/distributed-testing-with-aspire-and-playwright-in-dotnet",
            IconUrl = "image/icons/aspire.webp",
            Channel = "Aspire",
            Type = "blogs",
            CreatedOn = new DateTime(2027, 2, 21, 22, 30, 0, DateTimeKind.Utc),
            ModifiedOn = new DateTime(2027, 2, 21, 22, 30, 0, DateTimeKind.Utc),
            Keywords = ["Aspire", "Distributed Testing", "Distributed Fixture", "Playwright", "xUnit", "Functional Testing", "Integration Testing", "End-to-End Testing", "UI Testing", "PostgreSQL", "Disposable Environments", "Multi-Repository", "Containers", "Message Brokers"]
        }
    ];
  }
}
