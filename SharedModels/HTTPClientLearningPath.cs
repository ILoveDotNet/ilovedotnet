namespace SharedModels;

public class HTTPClientLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(6);

    public HTTPClientLearningPath()
    {
        FullContents =
        new(6)
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
            new ContentMetaData
            {
                Order = 3,
                Title = "Save bandwidth with Compression when sending and reading data using HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet.png",
                ContentUrl = "blogs/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 9, 3, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 9, 3, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Working with API that supports remote streaming using HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet.png",
                ContentUrl = "blogs/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 9, 10, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 9, 10, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Extending HTTPClient with Custom Http Message Handlers in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/extending-http-client-with-custom-http-message-handlers-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/extending-http-client-with-custom-http-message-handlers-in-dotnet.png",
                ContentUrl = "blogs/extending-http-client-with-custom-http-message-handlers-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 9, 17, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 9, 17, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Unit Testing HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/unit-testing-http-client-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/unit-testing-http-client-in-dotnet.png",
                ContentUrl = "blogs/unit-testing-http-client-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "HTTP-Client",
                CreatedOn = new DateTime(2023, 9, 24, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 9, 24, 22, 30, 0)
            },
        };
    }
}
