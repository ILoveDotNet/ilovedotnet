namespace SharedModels;

public class SignalRLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public SignalRLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Fundamentals of SignalR in .Net",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/fundamentals-of-signalr-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/signalr/fundamentals-of-signalr-in-dotnet.webp",
                ContentUrl = "blogs/fundamentals-of-signalr-in-dotnet",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 4, 7, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 4, 7, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Signal R", "Two Way", "Realtime", "Full Duplex", "WebSockets"]
            }
        ];
    }
}