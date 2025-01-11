namespace SharedModels;

public class HTTPClientLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(6);

  public HTTPClientLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "Improving performance and memory use while accessing APIs using HTTPClient in dotnet",
                Description = "In this post I will teach how to improve performance and memory use while accessing APIs with Streams using HTTP Client. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet",
                PosterUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet.webp",
                ContentUrl = "blogs/improving-performance-and-memory-use-while-accessing-apis-using-http-client-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 8, 20, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 4, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Memory Stream", "Performance"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Free up resources with Cancellation while accessing APIs using HTTPClient in dotnet",
                Description = "In this post I will teach how to free up resources with cancellation while accessing APIs using HTTP Client. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet",
                PosterUrl = "image/blogs/http-client/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet.webp",
                ContentUrl = "blogs/free-up-resources-with-cancellation-while-accessing-apis-using-http-client-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 8, 27, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 12, 8, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["CancellationTokenSource", "CancellationToken", "Cancellation"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Save bandwidth with Compression when sending and reading data using HTTPClient in dotnet",
                Description = "In this post I will teach how to save bandwidth with compression while sending and reading data using HTTP Client. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet",
                PosterUrl = "image/blogs/http-client/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet.webp",
                ContentUrl = "blogs/save-bandwidth-with-compression-when-sending-and-reading-data-using-http-client-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 9, 3, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 9, 3, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Compression", "Save Bandwidth"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Working with API that supports remote streaming using HTTPClient in dotnet",
                Description = "In this post I will teach how to work with API that supports remote streaming using HTTP Client. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet",
                PosterUrl = "image/blogs/http-client/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet.webp",
                ContentUrl = "blogs/working-with-api-that-supports-remote-streaming-using-http-client-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 9, 10, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 9, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Streaming", "Remote"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Extending HTTPClient with Custom Http Message Handlers in dotnet",
                Description = "In this post I will teach how to extend HTTP Client with custom Http Message Handlers. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "extending-http-client-with-custom-http-message-handlers-in-dotnet",
                PosterUrl = "image/blogs/http-client/extending-http-client-with-custom-http-message-handlers-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/extending-http-client-with-custom-http-message-handlers-in-dotnet.webp",
                ContentUrl = "blogs/extending-http-client-with-custom-http-message-handlers-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 9, 17, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 9, 17, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Delegating Handler", "Http Message Handler"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Unit Testing HTTPClient in dotnet",
                Description = "In this post I will teach how to unit test http client in dotnet. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "unit-testing-http-client-in-dotnet",
                PosterUrl = "image/blogs/http-client/unit-testing-http-client-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/http-client/unit-testing-http-client-in-dotnet.webp",
                ContentUrl = "blogs/unit-testing-http-client-in-dotnet",
                IconUrl = "image/icons/http client.webp",
                Channel = "HTTP Client",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 9, 24, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 11, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Unit Testing"]
            },
        ];
  }
}
