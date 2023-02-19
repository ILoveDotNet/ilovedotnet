namespace SharedModels;

public class TalkLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public TalkLearningPath()
    {
        FullContents =
        new(1)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Blazor - SPA from ASP.NET Family",
                Author = "Abdul Rahman",
                PosterUrl = "image/talks/blazor-spa-from-aspnet-family.svg",
                ThumbnailUrl = "image/talks/blazor-spa-from-aspnet-family.svg",
                ContentUrl = "talks/blazor-spa-from-aspnet-family",
                IconUrl = "image/icons/talk.png",
                Type = "Talk",
                CreatedOn = new DateTime(2022, 3, 13, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 3, 13, 22, 30, 0)
            },
        };
    }
}