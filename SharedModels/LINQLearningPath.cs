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
                Title = "Using LINQ OrderBy to Sort Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-orderby-to-sort-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-orderby-to-sort-data.svg",
                ContentUrl = "blogs/using-linq-orderby-to-sort-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 28, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 8, 28, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Using LINQ Where to Filter Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-where-to-filter-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-where-to-filter-data.svg",
                ContentUrl = "blogs/using-linq-where-to-filter-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 4, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 4, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Using LINQ to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-to-select-single-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-to-select-single-data.svg",
                ContentUrl = "blogs/using-linq-to-select-single-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 25, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 25, 22, 30, 0)
            }
        };
    }
}