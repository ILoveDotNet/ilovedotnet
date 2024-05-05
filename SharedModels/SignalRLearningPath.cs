namespace SharedModels;

public class SignalRLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(3);

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
                Keywords = ["Two Way", "Realtime", "Full Duplex", "WebSockets"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Types of Clients in SignalR",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/types-of-clients-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/types-of-clients-in-signalr.webp",
                ContentUrl = "blogs/types-of-clients-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 4, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 4, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Clients", "Javascript", "Java", ".NET"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Send Notifications using IHubContext and Caller in SignalR",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/send-notifications-using-ihubcontext-and-caller-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/send-notifications-using-ihubcontext-and-caller-in-signalr.webp",
                ContentUrl = "blogs/send-notifications-using-ihubcontext-and-caller-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 5, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 5, 5, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["IHubContext", "Caller", "Notifications"]
            }
        ];
    }
}