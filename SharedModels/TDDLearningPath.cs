namespace SharedModels;

public class TDDLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public TDDLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Introducing TDD in C# .Net",
                Description = "In this post I will introduce you to TDD in C# .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "introducing-tdd-in-csharp-dotnet",
                PosterUrl = "image/blogs/tdd/introducing-tdd-in-csharp-dotnet.webp",
                ThumbnailUrl = "image/blogs/tdd/introducing-tdd-in-csharp-dotnet.webp",
                ContentUrl = "blogs/introducing-tdd-in-csharp-dotnet",
                IconUrl = "image/icons/tdd.webp",
                Channel = "TDD",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 3, 27, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 9, 22, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Test Driven Development"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Implementing TDD in C# .Net",
                Description = "In this post I will teach you how to practice and implement TDD in C# .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "implementing-tdd-in-csharp-dotnet",
                PosterUrl = "image/blogs/tdd/implementing-tdd-in-csharp-dotnet.webp",
                ThumbnailUrl = "image/blogs/tdd/implementing-tdd-in-csharp-dotnet.webp",
                ContentUrl = "blogs/implementing-tdd-in-csharp-dotnet",
                IconUrl = "image/icons/tdd.webp",
                Channel = "TDD",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 4, 3, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 9, 29, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Test Driven Development"]
            }
        ];
    }
}