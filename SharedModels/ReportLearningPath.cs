namespace SharedModels;

public class ReportLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(3);

    public ReportLearningPath()
    {
        FullContents =
        new(3)
        {
            new ContentMetaData
            {
                Order = 1,
                Title = "Generate Excel Report using Closed XML in .NET",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.svg",
                ThumbnailUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.svg",
                ContentUrl = "blogs/generate-excel-report-using-closed-xml-in-dotnet",
                IconUrl = "image/icons/report.png",
                Type = "Report",
                CreatedOn = new DateTime(2023, 1, 29, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 1, 29, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 2,
                Title = "Generate PDF Report using Quest PDF in .NET",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.svg",
                ThumbnailUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.svg",
                ContentUrl = "blogs/generate-pdf-report-using-quest-pdf-in-dotnet",
                IconUrl = "image/icons/report.png",
                Type = "Report",
                CreatedOn = new DateTime(2023, 2, 19, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 2, 19, 22, 30, 0)
            },
            new ContentMetaData
            {
                Order = 3,
                Title = "Convert HTML to PDF Report in .NET",
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/report/convert-html-to-pdf-report-in-dotnet.svg",
                ThumbnailUrl = "image/blogs/report/convert-html-to-pdf-report-in-dotnet.svg",
                ContentUrl = "blogs/convert-html-to-pdf-report-in-dotnet",
                IconUrl = "image/icons/report.png",
                Type = "Report",
                CreatedOn = new DateTime(2023, 4, 9, 22, 30, 0),
                ModifiedOn = new DateTime(2023, 4, 9, 22, 30, 0)
            }
        };
    }
}