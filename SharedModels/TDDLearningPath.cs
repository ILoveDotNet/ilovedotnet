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
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/tdd/introducing-tdd-in-csharp-dotnet.webp",
                ThumbnailUrl = "image/blogs/tdd/introducing-tdd-in-csharp-dotnet.webp",
                ContentUrl = "blogs/introducing-tdd-in-csharp-dotnet",
                IconUrl = "image/icons/tdd.webp",
                Type = "TDD",
                CreatedOn = new DateTime(2022, 3, 27, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 3, 27, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Implementing TDD in C# .Net",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/tdd/implementing-tdd-in-csharp-dotnet.webp",
                ThumbnailUrl = "image/blogs/tdd/implementing-tdd-in-csharp-dotnet.webp",
                ContentUrl = "blogs/implementing-tdd-in-csharp-dotnet",
                IconUrl = "image/icons/tdd.webp",
                Type = "TDD",
                CreatedOn = new DateTime(2022, 4, 3, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 4, 3, 22, 30, 0)
            },
        ];
    }
}