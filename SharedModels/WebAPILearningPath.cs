namespace SharedModels;

public class WebAPILearningPath
{
    public readonly List<ContentMetaData> FullContents = new(4);

    public WebAPILearningPath()
    {
        FullContents =
        new(4)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Importance of Status Code in Web API",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/webapi/webapi-importance-of-status-code.png",
                ThumbnailUrl = "image/blogs/webapi/webapi-importance-of-status-code.png",
                ContentUrl = "blogs/webapi-importance-of-status-code",
                IconUrl = "image/icons/webapi.png",
                Type = "WebAPI",
                CreatedOn = new DateTime(2022, 3, 6, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 3, 6, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Profiling Web API with Mini Profiler",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/webapi/profiling-webapi-with-mini-profiler.png",
                ThumbnailUrl = "image/blogs/webapi/profiling-webapi-with-mini-profiler.png",
                ContentUrl = "blogs/profiling-webapi-with-mini-profiler",
                IconUrl = "image/icons/webapi.png",
                Type = "WebAPI",
                CreatedOn = new DateTime(2022, 9, 18, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 18, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Unit Testing Filters in ASP.NET Web API",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/webapi/unit-testing-filters-in-asp-net-webapi.png",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-filters-in-asp-net-webapi.png",
                ContentUrl = "blogs/unit-testing-filters-in-asp-net-webapi",
                IconUrl = "image/icons/webapi.png",
                Type = "WebAPI",
                CreatedOn = new DateTime(2023, 5, 28, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 5, 28, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Unit Testing Middlewares in ASP.NET Web API",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/webapi/unit-testing-middlewares-in-asp-net-webapi.png",
                ThumbnailUrl = "image/blogs/webapi/unit-testing-middlewares-in-asp-net-webapi.png",
                ContentUrl = "blogs/unit-testing-middlewares-in-asp-net-webapi",
                IconUrl = "image/icons/webapi.png",
                Type = "WebAPI",
                CreatedOn = new DateTime(2023, 6, 4, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 6, 4, 22, 30, 0)
            }
        };
    }
}