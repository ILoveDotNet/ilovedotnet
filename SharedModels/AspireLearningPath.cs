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
            Description = "Run a .NET API, UI, and PostgreSQL with one AppHost. Learn resource references, health checks, service defaults, and how the same model scales to dozens of resources.",
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
            Keywords = ["Aspire", ".NET", "AppHost", "Service Defaults", "PostgreSQL", "Service Discovery", "OpenTelemetry", "Docker", "Kafka"]
        },
        new ContentMetaData
        {
            Order = 2,
            Title = "Distributed Testing with Aspire and Playwright in .NET",
            Description = "Test real APIs, PostgreSQL, and a Blazor UI with Aspire and Playwright. Learn bounded startup, isolated data, and how to extend the harness to multi-repository systems.",
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
            Keywords = ["Aspire", "Distributed Testing", "Playwright", "xUnit", "Integration Testing", "End-to-End Testing", "PostgreSQL", "CI", "Multi-Repository"]
        }
    ];
  }
}
