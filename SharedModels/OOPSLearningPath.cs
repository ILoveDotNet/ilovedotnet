namespace SharedModels;

public class OOPSLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(2);

    public OOPSLearningPath()
    {
        FullContents =
        new(2)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "OOPS Encapsulation",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/oops/oops-encapsulation.png",
                ThumbnailUrl = "image/blogs/oops/oops-encapsulation.png",
                ContentUrl = "blogs/oops-encapsulation",
                IconUrl = "image/icons/oops.png",
                Type = "OOPS",
                CreatedOn = new DateTime(2022, 5, 1, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 1, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "OOPS Abstraction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/oops/oops-abstraction.png",
                ThumbnailUrl = "image/blogs/oops/oops-abstraction.png",
                ContentUrl = "blogs/oops-abstraction",
                IconUrl = "image/icons/oops.png",
                Type = "OOPS",
                CreatedOn = new DateTime(2022, 5, 15, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 5, 15, 22, 30, 0)
            },
        };
    }
}