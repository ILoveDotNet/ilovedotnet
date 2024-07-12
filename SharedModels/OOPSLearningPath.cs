namespace SharedModels;

public class OOPSLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public OOPSLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "OOPS Encapsulation",
                Description = "In this post I will teach what is encapsulation in object oriented programming. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "oops-encapsulation",
                PosterUrl = "image/blogs/oops/oops-encapsulation.webp",
                ThumbnailUrl = "image/blogs/oops/oops-encapsulation.webp",
                ContentUrl = "blogs/oops-encapsulation",
                IconUrl = "image/icons/oops.webp",
                Channel = "OOPS",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 5, 1, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 5, 1, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Encapsulation"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "OOPS Abstraction",
                Description = "In this post I will teach what is abstraction in object oriented programming. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "oops-abstraction",
                PosterUrl = "image/blogs/oops/oops-abstraction.webp",
                ThumbnailUrl = "image/blogs/oops/oops-abstraction.webp",
                ContentUrl = "blogs/oops-abstraction",
                IconUrl = "image/icons/oops.webp",
                Channel = "OOPS",
                Type = "blogs",
                CreatedOn = new DateTime(2022, 5, 15, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 5, 15, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Abstraction"]
            },
        ];
    }
}