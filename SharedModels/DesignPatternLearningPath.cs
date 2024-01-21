namespace SharedModels;

public class DesignPatternLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(4);

    public DesignPatternLearningPath()
    {
        FullContents =
        new(4)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Design Pattern Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/design-pattern-introduction.webp",
                ThumbnailUrl = "image/blogs/design-pattern/design-pattern-introduction.webp",
                ContentUrl = "blogs/design-pattern-introduction",
                IconUrl = "image/icons/design-pattern.webp",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2023, 3, 12, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 12, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Creational Design Pattern - Singleton",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.webp",
                ThumbnailUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.webp",
                ContentUrl = "blogs/creational-design-pattern-singleton",
                IconUrl = "image/icons/design-pattern.webp",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2023, 3, 19, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 3, 19, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Structural Design Pattern - Decorator",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/structural-design-pattern-decorator.webp",
                ThumbnailUrl = "image/blogs/design-pattern/structural-design-pattern-decorator.webp",
                ContentUrl = "blogs/structural-design-pattern-decorator",
                IconUrl = "image/icons/design-pattern.webp",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2024, 1, 7, 22, 30, 0),
                ModifiedOn = new DateTime(2024, 1, 7, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Structural Design Pattern - Facade",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/design-pattern/structural-design-pattern-facade.webp",
                ThumbnailUrl = "image/blogs/design-pattern/structural-design-pattern-facade.webp",
                ContentUrl = "blogs/structural-design-pattern-facade",
                IconUrl = "image/icons/design-pattern.webp",
                Type = "Design-Pattern",
                CreatedOn = new DateTime(2024, 1, 21, 22, 30, 0),
                ModifiedOn = new DateTime(2024, 1, 21, 22, 30, 0)
            },
        };
    }
}
