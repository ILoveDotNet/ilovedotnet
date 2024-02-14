namespace SharedModels;

public class DependencyInjectionLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public DependencyInjectionLearningPath()
    {
        FullContents =
        new(2)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Introducing Dependency Injection in .NET",
                Author = "Abdul Rahman",
                PosterUrl = "_content/DependencyInjectionDemoComponents/image/blogs/dependency-injection/introducing-dependency-injection-in-dotnet.webp",
                ThumbnailUrl = "_content/DependencyInjectionDemoComponents/image/blogs/dependency-injection/introducing-dependency-injection-in-dotnet.webp",
                ContentUrl = "blogs/introducing-dependency-injection-in-dotnet",
                IconUrl = "image/icons/dependency-injection.webp",
                Type = "Dependency-Injection",
                CreatedOn = new DateTime(2022, 7, 24, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 7, 24, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Dependency Injection Lifetimes in .NET",
                Author = "Abdul Rahman",
                PosterUrl = "_content/DependencyInjectionDemoComponents/image/blogs/dependency-injection/dependency-injection-lifetimes-in-dotnet.webp",
                ThumbnailUrl = "_content/DependencyInjectionDemoComponents/image/blogs/dependency-injection/dependency-injection-lifetimes-in-dotnet.webp",
                ContentUrl = "blogs/dependency-injection-lifetimes-in-dotnet",
                IconUrl = "image/icons/dependency-injection.webp",
                Type = "Dependency-Injection",
                CreatedOn = new DateTime(2022, 7, 31, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 1, 15, 22, 30, 0)
            },
        };
    }
}