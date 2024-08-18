namespace SharedModels;

public class DesignPatternLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(7);

    public DesignPatternLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Design Pattern Introduction",
                Description = "In this post I will introduce you to Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "design-pattern-introduction",
                PosterUrl = "image/blogs/design-pattern/design-pattern-introduction.webp",
                ThumbnailUrl = "image/blogs/design-pattern/design-pattern-introduction.webp",
                ContentUrl = "blogs/design-pattern-introduction",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 3, 12, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 3, 12, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Creational", "Structural", "Behavioral", "Gang of Four"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Creational Design Pattern - Singleton",
                Description = "In this post I will teach you Creational Singleton Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "creational-design-pattern-singleton",
                PosterUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.webp",
                ThumbnailUrl = "image/blogs/design-pattern/creational-design-pattern-singleton.webp",
                ContentUrl = "blogs/creational-design-pattern-singleton",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2023, 3, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 3, 19, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Creational", "Singleton"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Structural Design Pattern - Decorator",
                Description = "In this post I will teach you Structural Decorator Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "structural-design-pattern-decorator",
                PosterUrl = "image/blogs/design-pattern/structural-design-pattern-decorator.webp",
                ThumbnailUrl = "image/blogs/design-pattern/structural-design-pattern-decorator.webp",
                ContentUrl = "blogs/structural-design-pattern-decorator",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 1, 7, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 1, 7, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Structural", "Decorator"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Structural Design Pattern - Facade",
                Description = "In this post I will teach you Structural Facade Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "structural-design-pattern-facade",
                PosterUrl = "image/blogs/design-pattern/structural-design-pattern-facade.webp",
                ThumbnailUrl = "image/blogs/design-pattern/structural-design-pattern-facade.webp",
                ContentUrl = "blogs/structural-design-pattern-facade",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 1, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 1, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Structural", "Facade"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Creational Design Pattern - Builder",
                Description = "In this post I will teach you Creational Builder Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "creational-design-pattern-builder",
                PosterUrl = "image/blogs/design-pattern/creational-design-pattern-builder.webp",
                ThumbnailUrl = "image/blogs/design-pattern/creational-design-pattern-builder.webp",
                ContentUrl = "blogs/creational-design-pattern-builder",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 1, 28, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 1, 28, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Creational", "Builder"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Enterprise Design Pattern - Repository",
                Description = "In this post I will teach you Enterprise Repository Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "enterprise-design-pattern-repository",
                PosterUrl = "image/blogs/design-pattern/enterprise-design-pattern-repository.webp",
                ThumbnailUrl = "image/blogs/design-pattern/enterprise-design-pattern-repository.webp",
                ContentUrl = "blogs/enterprise-design-pattern-repository",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 8, 18, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 8, 18, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Enterprise", "Repository"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Enterprise Design Pattern - Unit of Work",
                Description = "In this post I will teach you Enterprise Unit of Work Design Pattern in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "enterprise-design-pattern-unit-of-work",
                PosterUrl = "image/blogs/design-pattern/enterprise-design-pattern-unit-of-work.webp",
                ThumbnailUrl = "image/blogs/design-pattern/enterprise-design-pattern-unit-of-work.webp",
                ContentUrl = "blogs/enterprise-design-pattern-unit-of-work",
                IconUrl = "image/icons/design-pattern.webp",
                Channel = "Design-Pattern",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 8, 25, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 8, 25, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Enterprise", "Unit of Work"]
            },
        ];
    }
}
