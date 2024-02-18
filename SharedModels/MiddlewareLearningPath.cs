namespace SharedModels;

public class MiddlewareLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public MiddlewareLearningPath()
    {
        FullContents =
        new(2)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Introducing Middleware in ASP.NET",
                Author = "Abdul Rahman",
                PosterUrl = "_content/MiddlewareDemoComponents/image/blogs/middleware/introducing-middleware-in-aspnet.webp",
                ThumbnailUrl = "_content/MiddlewareDemoComponents/image/blogs/middleware/introducing-middleware-in-aspnet.webp",
                ContentUrl = "blogs/introducing-middleware-in-aspnet",
                IconUrl = "_content/CommonComponents/image/icons/middleware.webp",
                Type = "Middleware",
                CreatedOn = new DateTime(2022, 6, 19, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 19, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Types of Middleware in ASP.NET",
                Author = "Abdul Rahman",
                PosterUrl = "_content/MiddlewareDemoComponents/image/blogs/middleware/types-of-middleware-in-aspnet.webp",
                ThumbnailUrl = "_content/MiddlewareDemoComponents/image/blogs/middleware/types-of-middleware-in-aspnet.webp",
                ContentUrl = "blogs/types-of-middleware-in-aspnet",
                IconUrl = "_content/CommonComponents/image/icons/middleware.webp",
                Type = "Middleware",
                CreatedOn = new DateTime(2022, 6, 26, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 6, 26, 22, 30, 0)
            },
        };
    }
}