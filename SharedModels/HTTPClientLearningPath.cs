namespace SharedModels;

public class HTTPClientLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public HTTPClientLearningPath()
    {
        FullContents =
        new(2)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Improving performance and memory use while accessing APIs using HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet.png",
                ContentUrl = "blogs/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 8, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 8, 20, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Free up resources with Cancellation while accessing APIs using HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet.png",
                ContentUrl = "blogs/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 8, 27, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 8, 27, 22, 30, 0)
            },
        };
    }
}
