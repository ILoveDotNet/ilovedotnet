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
        Title = "ClosedXML in .NET: Create and Download Excel Reports",
        Description = "Learn how to create Excel workbooks and downloadable reports with ClosedXML in .NET, including worksheets, tables, and practical examples.",
        Author = "Abdul Rahman",
        Slug = "generate-excel-report-using-closed-xml-in-dotnet",
        PosterUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/report/generate-excel-report-using-closed-xml-in-dotnet.webp",
        ContentUrl = "blogs/generate-excel-report-using-closed-xml-in-dotnet",
        IconUrl = "image/icons/report.webp",
        Channel = "Report",
        Type = "blogs",
        CreatedOn = new DateTime(2023, 1, 29, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 1, 26, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Microsoft Excel", "Excel Report", "Closed XML", "Report Template", "Complex Report", "Simple Report", "xlsx", "xls"]
      },
      new ContentMetaData
      {
        Order = 2,
        Title = "QuestPDF in .NET: Generate PDF Reports with C#",
        Description = "Learn how to generate professional PDF reports with QuestPDF in .NET using C# layouts, tables, and a complete working example.",
        Author = "Abdul Rahman",
        Slug = "generate-pdf-report-using-quest-pdf-in-dotnet",
        PosterUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/report/generate-pdf-report-using-quest-pdf-in-dotnet.webp",
        ContentUrl = "blogs/generate-pdf-report-using-quest-pdf-in-dotnet",
        IconUrl = "image/icons/report.webp",
        Channel = "Report",
        Type = "blogs",
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
        Channel = "Report",
        Type = "blogs",
        CreatedOn = new DateTime(2023, 4, 9, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 1, 12, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["HTML", "PDF"]
      }
    ];
  }
}
