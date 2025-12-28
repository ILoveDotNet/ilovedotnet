namespace SharedModels;

public class CachingLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

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
            }
    ];
  }
}
