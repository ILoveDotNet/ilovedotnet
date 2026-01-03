namespace SharedModels;

public class CachingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(2);

  public CachingLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "Understanding Caching Fundamentals in .NET",
                Description = "In this post, I will teach you the fundamentals of caching in .NET - what it is, when to use it, and how to implement effective caching strategies that dramatically improve application performance. All with practical examples and best practices.",
                Author = "Abdul Rahman",
                Slug = "understanding-caching-fundamentals-in-dotnet",
                PosterUrl = "image/blogs/caching/understanding-caching-fundamentals-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/caching/understanding-caching-fundamentals-in-dotnet.webp",
                ContentUrl = "blogs/understanding-caching-fundamentals-in-dotnet",
                IconUrl = "image/icons/caching.webp",
                Type = "Caching",
                Channel = "Caching",
                CreatedOn = new DateTime(2026, 1, 4, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 1, 4, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Caching", "Performance", "In-Memory Cache", "Distributed Cache", "Data Caching", "Memoization", "Cache Strategy", ".NET Performance"]
            },
        new ContentMetaData
            {
                Order = 2,
                Title = "Avoiding Cache Stampede and Handling Nulls in .NET",
                Description = "In this post, I will teach you how to prevent cache stampede attacks, implement proper GetOrSet patterns, and handle null values in cache to protect your application from DOS attacks and database overload. All with practical examples.",
                Author = "Abdul Rahman",
                Slug = "avoiding-cache-stampede-and-handling-nulls-in-dotnet",
                PosterUrl = "image/blogs/caching/avoiding-cache-stampede-and-handling-nulls-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/caching/avoiding-cache-stampede-and-handling-nulls-in-dotnet.webp",
                ContentUrl = "blogs/avoiding-cache-stampede-and-handling-nulls-in-dotnet",
                IconUrl = "image/icons/caching.webp",
                Type = "Caching",
                Channel = "Caching",
                CreatedOn = new DateTime(2026, 1, 11, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 1, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Cache Stampede", "GetOrSet Pattern", "Null Handling", "DOS Protection", "FusionCache", "HybridCache", "Cache Coordination", "Result Pattern", ".NET Caching"]
            }
    ];
  }
}
