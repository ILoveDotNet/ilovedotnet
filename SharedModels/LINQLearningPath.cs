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
                Title = "Using LINQ to Select and Project Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-to-select-and-project-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-to-select-and-project-data.svg",
                ContentUrl = "blogs/using-linq-to-select-and-project-data",
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
                Title = "Using LINQ First to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-first-to-select-single-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-first-to-select-single-data.svg",
                ContentUrl = "blogs/using-linq-first-to-select-single-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 25, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 9, 25, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Using LINQ Last to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-last-to-select-single-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-last-to-select-single-data.svg",
                ContentUrl = "blogs/using-linq-last-to-select-single-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 2, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 2, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Using LINQ Single to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-single-to-select-single-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-single-to-select-single-data.svg",
                ContentUrl = "blogs/using-linq-single-to-select-single-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 9, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 9, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Using LINQ Take to Select Specific Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-take-to-select-specific-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-take-to-select-specific-data.svg",
                ContentUrl = "blogs/using-linq-take-to-select-specific-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 16, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 16, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Using LINQ Skip to Select Specific Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-skip-to-select-specific-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-skip-to-select-specific-data.svg",
                ContentUrl = "blogs/using-linq-skip-to-select-specific-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 23, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 23, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Using LINQ Distinct to Select Unique Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-distinct-to-select-unique-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-distinct-to-select-unique-data.svg",
                ContentUrl = "blogs/using-linq-distinct-to-select-unique-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 30, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 10, 30, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Using LINQ Chunk to Split Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-chunk-to-split-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-chunk-to-split-data.svg",
                ContentUrl = "blogs/using-linq-chunk-to-split-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 6, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 11, 6, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Using LINQ All to Find Type of Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-all-to-find-type-of-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-all-to-find-type-of-data.svg",
                ContentUrl = "blogs/using-linq-all-to-find-type-of-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 13, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 11, 13, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Using LINQ Any to Find Type of Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-any-to-find-type-of-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-any-to-find-type-of-data.svg",
                ContentUrl = "blogs/using-linq-any-to-find-type-of-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 20, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 11, 20, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Using LINQ Contains to Check Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-contains-to-check-data.svg",
                ThumbnailUrl = "image/blogs/linq/using-linq-contains-to-check-data.svg",
                ContentUrl = "blogs/using-linq-contains-to-check-data",
                IconUrl = "image/icons/linq.png",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 27, 22, 30, 0),
                ModifiedOn = new DateTime(2022, 11, 27, 22, 30, 0)
            }
        };
    }
}