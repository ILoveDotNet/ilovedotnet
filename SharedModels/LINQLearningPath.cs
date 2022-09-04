namespace SharedModels;

public class LINQLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(4);

    public LINQLearningPath()
    {
        FullContents =
        new(4)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "LINQ Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/linq-introduction.svg",
                ThumbnailUrl = "image/blogs/linq/linq-introduction.svg",
                ContentUrl = "blogs/linq-introduction",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 14, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 14, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "LINQ Select",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/linq-select.svg",
                ThumbnailUrl = "image/blogs/linq/linq-select.svg",
                ContentUrl = "blogs/linq-select",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 21, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 21, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "LINQ OrderBy",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/linq-orderby.svg",
                ThumbnailUrl = "image/blogs/linq/linq-orderby.svg",
                ContentUrl = "blogs/linq-orderby",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 28, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 28, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "LINQ Where",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/linq-where.svg",
                ThumbnailUrl = "image/blogs/linq/linq-where.svg",
                ContentUrl = "blogs/linq-where",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 4, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 4, 22, 30, 0)
            },
        };
    }
}