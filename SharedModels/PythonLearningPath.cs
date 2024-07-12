namespace SharedModels;

public class PythonLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public PythonLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Python Dynamic Interop with Dotnet",
                Description = "In this post I will teach you how to interoperate and run python in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "python-dynamic-interop-with-dotnet",
                PosterUrl = "image/blogs/python/python-dynamic-interop-with-dotnet.webp",
                ThumbnailUrl = "image/blogs/python/python-dynamic-interop-with-dotnet.webp",
                ContentUrl = "blogs/python-dynamic-interop-with-dotnet",
                IconUrl = "image/icons/python.webp",
                Channel = "Python",
                CreatedOn = new DateTime(2022, 9, 11, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 9, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Interoperating", "Iron Python", "Python", "Dynamic Interop"]
            },
        ];
    }
}
