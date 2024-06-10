namespace SharedModels;

public class SignalRLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(8);

    public SignalRLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Fundamentals of SignalR in .Net",
                Description = "In this post I will introduce you to SignalR in .NET. All with live working demo.",
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
                Description = "In this post I will teach you different types of client in SignalR .NET. All with live working demo.",
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
                Description = "In this post I will teach you how to send notifications using IHubContext and Caller in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/send-notifications-using-ihubcontext-and-caller-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/send-notifications-using-ihubcontext-and-caller-in-signalr.webp",
                ContentUrl = "blogs/send-notifications-using-ihubcontext-and-caller-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 5, 5, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 5, 5, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["IHubContext", "Caller", "Notifications"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Send Notifications to Groups and Connection Id in SignalR",
                Description = "In this post I will teach you how to send notifications to Groups and Connection Id in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/send-notifications-to-groups-and-connectionid-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/send-notifications-to-groups-and-connectionid-in-signalr.webp",
                ContentUrl = "blogs/send-notifications-to-groups-and-connectionid-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 5, 12, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 5, 12, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Groups", "Connection Id", "Notifications"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Message Pack Hub Protocol and Keep Alive in SignalR",
                Description = "In this post I will teach you how to change message pack hub protocol and keep the connection alive in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/message-pack-hub-protocol-and-keep-alive-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/message-pack-hub-protocol-and-keep-alive-in-signalr.webp",
                ContentUrl = "blogs/message-pack-hub-protocol-and-keep-alive-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 5, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 5, 19, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Message Pack", "Hub Protocol", "Keep Alive"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Exception Handling and Logging in SignalR",
                Description = "In this post I will teach you how to handle exceptions and do logging in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/exception-handling-and-logging-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/exception-handling-and-logging-in-signalr.webp",
                ContentUrl = "blogs/exception-handling-and-logging-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 5, 26, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 5, 26, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Exception", "Error", "Logging"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Streaming and Authentication and Authorization in SignalR",
                Description = "In this post I will teach you about streaming and authentication and authorization in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/streaming-and-authentication-and-authorization-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/streaming-and-authentication-and-authorization-in-signalr.webp",
                ContentUrl = "blogs/streaming-and-authentication-and-authorization-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 6, 2, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 6, 2, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Streaming", "Authentication", "Authorization"]
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Design and Deployment and Scaling Consideration in SignalR",
                Description = "In this post I will teach you about design and deployment and scaling considerations in SignalR .NET. All with live working demo.",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/signalr/design-and-deployment-and-scaling-in-signalr.webp",
                ThumbnailUrl = "image/blogs/signalr/design-and-deployment-and-scaling-in-signalr.webp",
                ContentUrl = "blogs/design-and-deployment-and-scaling-in-signalr",
                IconUrl = "image/icons/signalr.webp",
                Type = "SignalR",
                CreatedOn = new DateTime(2024, 6, 9, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 6, 9, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Design", "Deployment", "Scaling"]
            }
        ];
    }
}