namespace SharedModels;

public class DependencyInjectionLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

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
        ];
    }
}