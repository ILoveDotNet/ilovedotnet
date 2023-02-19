namespace SharedModels;

public class ReportLearningPath
{
    public readonly List<ContentMetaData> FullContents = new(1);

    public ReportLearningPath()
    {
        FullContents =
        new(1)
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
            }
        };
    }
}