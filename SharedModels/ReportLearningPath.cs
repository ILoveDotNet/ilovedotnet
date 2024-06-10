namespace SharedModels;

public class ReportLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(3);

    public ReportLearningPath()
    {
        FullContents =
        [
            new ContentMetaData
            {
                Order = 1,
                Title = "Generate Excel Report using Closed XML in .NET",
                Description = "In this post I will teach you how to generate excel report using Closed XML in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "generate-excel-report-using-closed-xml-in-dotnet",
                PosterUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.webp",
                ContentUrl = "blogs/generate-excel-report-using-closed-xml-in-dotnet",
                IconUrl = "image/icons/report.webp",
                Type = "Report",
                CreatedOn = new DateTime(2023, 1, 29, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 2, 11, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Microsoft Excel", "Excel Report", "Closed XML", "Report Template", "Complex Report", "Simple Report", "xlsx", "xls"]
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Generate PDF Report using Quest PDF in .NET",
                Description = "In this post I will teach you how to generate pdf report using Quest PDF in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "generate-pdf-report-using-quest-pdf-in-dotnet",
                PosterUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.webp",
                ContentUrl = "blogs/generate-pdf-report-using-quest-pdf-in-dotnet",
                IconUrl = "image/icons/report.webp",
                Type = "Report",
                CreatedOn = new DateTime(2023, 2, 19, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 3, 3, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Quest PDF", "PDF Report", "PDF"]
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Convert HTML to PDF Report in .NET",
                Description = "In this post I will teach you how to convert html to pdf report in .NET. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "convert-html-to-pdf-report-in-dotnet",
                PosterUrl = "image/blogs/report/convert-html-to-pdf-report-in-dotnet.webp",
                ThumbnailUrl = "image/blogs/report/convert-html-to-pdf-report-in-dotnet.webp",
                ContentUrl = "blogs/convert-html-to-pdf-report-in-dotnet",
                IconUrl = "image/icons/report.webp",
                Type = "Report",
                CreatedOn = new DateTime(2023, 4, 9, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2023, 4, 9, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["HTML", "PDF"]
            }
        ];
    }
}