namespace SharedModels;

public class LINQLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(26);

    public LINQLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "LINQ Introduction",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/linq-introduction.webp",
                ThumbnailUrl = "image/blogs/linq/linq-introduction.webp",
                ContentUrl = "blogs/linq-introduction",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 14, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 8, 14, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Language Integrated Querying"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Using LINQ to Select and Project Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-to-select-and-project-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-to-select-and-project-data.webp",
                ContentUrl = "blogs/using-linq-to-select-and-project-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 8, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Select", "Projection", "Shape"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Using LINQ OrderBy to Sort Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-orderby-to-sort-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-orderby-to-sort-data.webp",
                ContentUrl = "blogs/using-linq-orderby-to-sort-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 8, 28, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 8, 28, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["OrderBy", "ThenBy", "OrderByDescending", "ThenByDescending", "Sort", "Ascending", "Descending"]
            },
            new ContentMetaData
            {
                Order = 4,
                Title = "Using LINQ Where to Filter Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-where-to-filter-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-where-to-filter-data.webp",
                ContentUrl = "blogs/using-linq-where-to-filter-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 4, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 9, 4, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Where", "Filter"]
            },
            new ContentMetaData
            {
                Order = 5,
                Title = "Using LINQ First to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-first-to-select-single-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-first-to-select-single-data.webp",
                ContentUrl = "blogs/using-linq-first-to-select-single-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 9, 25, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 9, 25, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["First", "FirstOrDefault", "Search forward"]
            },
            new ContentMetaData
            {
                Order = 6,
                Title = "Using LINQ Last to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-last-to-select-single-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-last-to-select-single-data.webp",
                ContentUrl = "blogs/using-linq-last-to-select-single-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 2, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 2, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Last", "LastOrDefault", "Search backward"]
            },
            new ContentMetaData
            {
                Order = 7,
                Title = "Using LINQ Single to Select Single Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-single-to-select-single-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-single-to-select-single-data.webp",
                ContentUrl = "blogs/using-linq-single-to-select-single-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 9, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 9, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Single", "SingleOrDefault", "Full Search"]
            },
            new ContentMetaData
            {
                Order = 8,
                Title = "Using LINQ Take to Select Specific Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-take-to-select-specific-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-take-to-select-specific-data.webp",
                ContentUrl = "blogs/using-linq-take-to-select-specific-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 16, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 16, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Take", "TakeWhile", "Partition"]
            },
            new ContentMetaData
            {
                Order = 9,
                Title = "Using LINQ Skip to Select Specific Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-skip-to-select-specific-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-skip-to-select-specific-data.webp",
                ContentUrl = "blogs/using-linq-skip-to-select-specific-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 23, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 23, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Skip", "SkipWhile", "Partition"]
            },
            new ContentMetaData
            {
                Order = 10,
                Title = "Using LINQ Distinct to Select Unique Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-distinct-to-select-unique-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-distinct-to-select-unique-data.webp",
                ContentUrl = "blogs/using-linq-distinct-to-select-unique-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 10, 30, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 10, 30, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Distinct", "DistinctBy", "Unique"]
            },
            new ContentMetaData
            {
                Order = 11,
                Title = "Using LINQ Chunk to Split Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-chunk-to-split-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-chunk-to-split-data.webp",
                ContentUrl = "blogs/using-linq-chunk-to-split-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 6, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 11, 6, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Chunk", "Split"]
            },
            new ContentMetaData
            {
                Order = 12,
                Title = "Using LINQ All to Find Type of Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-all-to-find-type-of-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-all-to-find-type-of-data.webp",
                ContentUrl = "blogs/using-linq-all-to-find-type-of-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 13, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 11, 13, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["All"]
            },
            new ContentMetaData
            {
                Order = 13,
                Title = "Using LINQ Any to Find Type of Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-any-to-find-type-of-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-any-to-find-type-of-data.webp",
                ContentUrl = "blogs/using-linq-any-to-find-type-of-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 20, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 11, 20, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Any"]
            },
            new ContentMetaData
            {
                Order = 14,
                Title = "Using LINQ Contains to Check Data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-contains-to-check-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-contains-to-check-data.webp",
                ContentUrl = "blogs/using-linq-contains-to-check-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 11, 27, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 11, 27, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Contains", "Check", "EqualityComparer"]
            },
            new ContentMetaData
            {
                Order = 15,
                Title = "Using LINQ Sequence Equal to Find Equality of data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-sequence-equal-to-find-equality-of-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-sequence-equal-to-find-equality-of-data.webp",
                ContentUrl = "blogs/using-linq-sequence-equal-to-find-equality-of-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 12, 4, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 12, 4, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["SequenceEqual", "Equality", "Compare"]
            },
            new ContentMetaData
            {
                Order = 16,
                Title = "Using LINQ Except to Find Difference in data",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/linq/using-linq-except-to-find-difference-in-data.webp",
                ThumbnailUrl = "image/blogs/linq/using-linq-except-to-find-difference-in-data.webp",
                ContentUrl = "blogs/using-linq-except-to-find-difference-in-data",
                IconUrl = "image/icons/linq.webp",
                Type = "LINQ",
                CreatedOn = new DateTime(2022, 12, 11, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2022, 12, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Except", "ExceptBy", "Difference", "Compare"]
            },
            new ContentMetaData
            {
               Order = 17,
               Title = "Using LINQ Intersect to Find Common data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-intersect-to-find-common-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-intersect-to-find-common-data.webp",
               ContentUrl = "blogs/using-linq-intersect-to-find-common-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2022, 12, 18, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2022, 12, 18, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Intersect", "IntersectBy", "Common", "Same"]
            },
            new ContentMetaData
            {
               Order = 18,
               Title = "Using LINQ Union to combine data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-union-to-combine-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-union-to-combine-data.webp",
               ContentUrl = "blogs/using-linq-union-to-combine-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2022, 12, 25, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2022, 12, 25, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Union", "UnionBy", "Concatenate", "Combine"]
            },
            new ContentMetaData
            {
               Order = 19,
               Title = "Using LINQ Concat to combine data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-concat-to-combine-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-concat-to-combine-data.webp",
               ContentUrl = "blogs/using-linq-concat-to-combine-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 1, 1, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 1, 1, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Concat", "Concatenate", "Combine", "Duplicate"]
            },
            new ContentMetaData
            {
               Order = 20,
               Title = "Using LINQ Join to combine data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-join-to-combine-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-join-to-combine-data.webp",
               ContentUrl = "blogs/using-linq-join-to-combine-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 1, 8, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 1, 8, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Join", "Combine", "Equi Join", "Inner Join"]
            },
            new ContentMetaData
            {
               Order = 21,
               Title = "Using LINQ Group Join to combine data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-group-join-to-combine-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-group-join-to-combine-data.webp",
               ContentUrl = "blogs/using-linq-group-join-to-combine-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 1, 15, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 1, 15, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["GroupJoin", "Combine", "into"]
            },
            new ContentMetaData
            {
               Order = 22,
               Title = "Simulating Left Outer Join using LINQ",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/simulating-left-outer-join-using-linq.webp",
               ThumbnailUrl = "image/blogs/linq/simulating-left-outer-join-using-linq.webp",
               ContentUrl = "blogs/simulating-left-outer-join-using-linq",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 1, 22, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 1, 22, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Combine", "into", "Left Outer Join", "Left Join"]
            },
            new ContentMetaData
            {
               Order = 23,
               Title = "Using LINQ Group By to group data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-group-by-to-group-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-group-by-to-group-data.webp",
               ContentUrl = "blogs/using-linq-group-by-to-group-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 2, 5, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 2, 5, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["GroupBy", "Group"]
            },
            new ContentMetaData
            {
               Order = 24,
               Title = "Using LINQ Count Min Max Average and Sum to Aggregate data",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-count-min-max-average-sum-to-aggregate-data.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-count-min-max-average-sum-to-aggregate-data.webp",
               ContentUrl = "blogs/using-linq-count-min-max-average-sum-to-aggregate-data",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 2, 12, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 2, 12, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Count", "Min", "Max", "MinBy", "MaxBy", "Average", "Sum", "Aggregate"]
            },
            new ContentMetaData
            {
               Order = 25,
               Title = "Using LINQ For Each to Iterate Collections",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/using-linq-for-each-to-iterate-collections.webp",
               ThumbnailUrl = "image/blogs/linq/using-linq-for-each-to-iterate-collections.webp",
               ContentUrl = "blogs/using-linq-for-each-to-iterate-collections",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 2, 26, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 2, 26, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["ForEach", "Loop"]
            },
            new ContentMetaData
            {
               Order = 26,
               Title = "Understanding LINQ Deferred, Immediate, Streaming and Non-Streaming Executions",
               Author = "Abdul Rahman",
               PosterUrl = "image/blogs/linq/understanding-linq-deferred-immediate-streaming-and-non-streaming-executions.webp",
               ThumbnailUrl = "image/blogs/linq/understanding-linq-deferred-immediate-streaming-and-non-streaming-executions.webp",
               ContentUrl = "blogs/understanding-linq-deferred-immediate-streaming-and-non-streaming-executions",
               IconUrl = "image/icons/linq.webp",
               Type = "LINQ",
               CreatedOn = new DateTime(2023, 3, 5, 22, 30, 0, DateTimeKind.Utc),
               ModifiedOn = new DateTime(2023, 3, 5, 22, 30, 0, DateTimeKind.Utc),
               Keywords = ["Deferred", "Immediate", "Streaming", "Non-Streaming", "Execution", "Operators"]
            }
        ];
    }
}