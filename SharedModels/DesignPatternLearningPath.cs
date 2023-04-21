namespace SharedModels;

public class DesignPatternLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public DesignPatternLearningPath()
    {
        FullContents =
        new(2)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Design Pattern Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/design-pattern-introduction.png",
                ThumbnailUrl = "image/blogs/design-pattern/design-pattern-introduction.png",
                ContentUrl = "blogs/design-pattern-introduction",
                IconUrl = "image/icons/design-pattern.png",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2023, 3, 12, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 12, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Creational Design Pattern - Singleton",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.png",
                ThumbnailUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.png",
                ContentUrl = "blogs/creational-design-pattern-singleton",
                IconUrl = "image/icons/design-pattern.png",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2023, 3, 19, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 19, 22, 30, 0)
            },
        };
    }
}
