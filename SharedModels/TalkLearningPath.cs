namespace SharedModels;

public class TalkLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public TalkLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Blazor - SPA from ASP.NET Family",
                Description = "In this talk I will teach you what is blazor and why do we need to go for blazor. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "blazor-spa-from-aspnet-family",
                PosterUrl = "image/talks/blazor-spa-from-aspnet-family.webp",
                ThumbnailUrl = "image/talks/blazor-spa-from-aspnet-family.webp",
                ContentUrl = "talks/blazor-spa-from-aspnet-family",
                IconUrl = "image/icons/talk.webp",
                Type = "Talk",
                CreatedOn = new DateTime(2022, 3, 13, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 3, 13, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Blazor", "Web Assembly", "Blazor WASM", "SPA", "ASP.NET"],
                VideoUrl = "https://www.youtube.com/embed/MUVyb9T5gU0"
            },
        ];
    }
}