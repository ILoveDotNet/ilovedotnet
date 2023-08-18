namespace SharedModels;

public class HTTPClientLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public HTTPClientLearningPath()
    {
        FullContents =
        new(1)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Improving performance and memory use while accessing APIs using HTTPClient in dotnet",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-httpclient-in-dotnet.png",
                ThumbnailUrl = "image/blogs/http-client/improving-performance-and-memory-use-while-accessing-apis-using-httpclient-in-dotnet.png",
                ContentUrl = "blogs/improving-performance-and-memory-use-while-accessing-apis-using-httpclient-in-dotnet",
                IconUrl = "image/icons/http-client.png",
                Type = "Python",
                CreatedOn = new DateTime(2023, 8, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 8, 20, 22, 30, 0)
            },
        };
    }
}
