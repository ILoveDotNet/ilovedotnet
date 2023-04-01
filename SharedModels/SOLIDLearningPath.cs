namespace SharedModels;

public class SOLIDLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public SOLIDLearningPath()
    {
        FullContents =
        new(1)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "SOLID Principles Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/solid/solid-principles-introduction.svg",
                ThumbnailUrl = "image/blogs/solid/solid-principles-introduction.svg",
                ContentUrl = "blogs/solid-principles-introduction",
                IconUrl = "image/icons/solid.png",
                Type = "SOLID",
                CreatedOn = new DateTime(2023, 4, 2, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 4, 2, 22, 30, 0)
            },
        };
    }
}
