namespace SharedModels;

public class PythonLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public PythonLearningPath()
    {
        FullContents =
        new(1)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Python Dynamic Interop with Dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "_content/PythonDemoComponents/image/blogs/python/python-dynamic-interop-with-dotnet.webp",
                ThumbnailUrl = "_content/PythonDemoComponents/image/blogs/python/python-dynamic-interop-with-dotnet.webp",
                ContentUrl = "blogs/python-dynamic-interop-with-dotnet",
                IconUrl = "image/icons/python.webp",
                Type = "Python",
                CreatedOn = new DateTime(2022, 9, 11, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 11, 22, 30, 0)
            },
        };
    }
}
