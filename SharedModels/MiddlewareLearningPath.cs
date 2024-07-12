namespace SharedModels;

public class MiddlewareLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public MiddlewareLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Introducing Middleware in ASP.NET",
                Description = "In this post I will introduce you to Middleware in ASP.NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "introducing-middleware-in-aspnet",
                PosterUrl = "image/blogs/middleware/introducing-middleware-in-aspnet.webp",
                ThumbnailUrl = "image/blogs/middleware/introducing-middleware-in-aspnet.webp",
                ContentUrl = "blogs/introducing-middleware-in-aspnet",
                IconUrl = "image/icons/middleware.webp",
                Channel = "Middleware",
                CreatedOn = new DateTime(2022, 6, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 6, 19, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["ASP.NET", "HTTP Request Pipeline"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Types of Middleware in ASP.NET",
                Description = "In this post I will teach you different types of Middleware in ASP.NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "types-of-middleware-in-aspnet",
                PosterUrl = "image/blogs/middleware/types-of-middleware-in-aspnet.webp",
                ThumbnailUrl = "image/blogs/middleware/types-of-middleware-in-aspnet.webp",
                ContentUrl = "blogs/types-of-middleware-in-aspnet",
                IconUrl = "image/icons/middleware.webp",
                Channel = "Middleware",
                CreatedOn = new DateTime(2022, 6, 26, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 6, 26, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["ASP.NET", "HTTP Request Pipeline"]
            },
        ];
    }
}