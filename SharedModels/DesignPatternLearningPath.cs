namespace SharedModels;

public class DesignPatternLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public DesignPatternLearningPath()
    {
        FullContents =
        new(1)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Design Pattern Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/design-pattern-introduction.svg",
                ThumbnailUrl = "image/blogs/design-pattern/design-pattern-introduction.svg",
                ContentUrl = "blogs/design-pattern-introduction",
                IconUrl = "image/icons/design-pattern.png",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2023, 3, 12, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 12, 22, 30, 0)
            },
        };
    }
}
