namespace SharedModels;

public class DependencyInjectionLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(7);

  public DependencyInjectionLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "Introducing Dependency Injection in .NET",
                Description = "In this post I will introduce you to Dependency Injection in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "introducing-dependency-injection-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/introducing-dependency-injection-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/introducing-dependency-injection-in-dotnet.webp",
                ContentUrl = "blogs/introducing-dependency-injection-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 7, 24, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 10, 6, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["DI", "Dependency Inversion", "IoC", "Inversion of Control"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Dependency Injection Lifetimes in .NET",
                Description = "In this post I will teach you Dependency Injection Lifetimes in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-lifetimes-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-lifetimes-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-lifetimes-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-lifetimes-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 7, 31, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 10, 13, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Lifetimes", "Transient", "Singleton", "Scoped", "Dependency Captivity", "Captive Dependencies"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Control Freak Antipattern in Dependency Injection",
                Description = "In this post I will teach you about the Control Freak antipattern in Dependency Injection and how to avoid it in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-control-freak-antipattern-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-control-freak-antipattern-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-control-freak-antipattern-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-control-freak-antipattern-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 9, 7, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 9, 7, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Control Freak", "Antipattern", "DI", "Dependency Creation", "Constructor Injection"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Service Locator Antipattern in Dependency Injection",
                Description = "In this post I will teach you about the Service Locator antipattern in Dependency Injection and why you should avoid it in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-service-locator-antipattern-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-service-locator-antipattern-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-service-locator-antipattern-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-service-locator-antipattern-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 9, 14, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 9, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Service Locator", "Antipattern", "DI", "Dependency Resolution", "Hidden Dependencies"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Ambient Context Antipattern in Dependency Injection",
                Description = "In this post I will teach you about the Ambient Context antipattern in Dependency Injection and how to avoid it in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-ambient-context-antipattern-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-ambient-context-antipattern-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-ambient-context-antipattern-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-ambient-context-antipattern-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 9, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 9, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Ambient Context", "Antipattern", "DI", "Static Dependencies", "Thread Local Storage"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Constrained Construction Antipattern in Dependency Injection",
                Description = "In this post I will teach you about the Constrained Construction antipattern in Dependency Injection and how to avoid it in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-constrained-construction-antipattern-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-constrained-construction-antipattern-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-constrained-construction-antipattern-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-constrained-construction-antipattern-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 9, 28, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 9, 28, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Constrained Construction", "Antipattern", "DI", "Factory Method", "Object Creation"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Constructor Over-injection Antipattern in Dependency Injection",
                Description = "In this post I will teach you about the Constructor Over-injection antipattern in Dependency Injection and how to avoid it in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "dependency-injection-constructor-over-injection-antipattern-in-dotnet",
                PosterUrl = "image/blogs/dependency-injection/dependency-injection-constructor-over-injection-antipattern-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/dependency-injection/dependency-injection-constructor-over-injection-antipattern-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-constructor-over-injection-antipattern-in-dotnet",
                IconUrl = "image/icons/dependency injection.webp",
                Channel = "Dependency Injection",
                Type = "blogs",
                CreatedOn = new DateTime(2025, 10, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 10, 5, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Constructor Over-injection", "Antipattern", "DI", "Too Many Dependencies", "Code Smell"]
            },
        ];
  }
}
