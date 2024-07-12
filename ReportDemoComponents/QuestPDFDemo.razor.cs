using Microsoft.AspNetCore.Components;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SharedModels;

namespace ReportDemoComponents;
public class QuestPDFDemoBase : ComponentBase
{
    [Inject] public required TableOfContents TableOfContents { get; set; }
    [Inject] public required Achievements Achievements { get; set; }

    protected string? Base64String { get; set; }

    protected override void OnInitialized()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.ContinuousSize(3, Unit.Inch);
                page.MarginVertical(5);
                page.MarginHorizontal(10);

                // page content
                page
                .Content()
                .AlignCenter()
                .Column(column =>
                {
                    column
                    .Item()
                    .Image(Convert.FromBase64String("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"));

                    column
                    .Item()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Line("Address: https://ilovedotnet.org")
                            .Italic();
                        text.Line("Description: Interactive .NET Knowledge Sharing Platform with In Browser Demos");
                    });

                    column
                    .Item()
                    .LineHorizontal(1)
                    .LineColor(Colors.Grey.Medium);

                    column
                    .Item()
                    .PaddingTop(5)
                    .PaddingBottom(-10)
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Line("Abdul Rahman").Bold();
                    });

                    column
                    .Item()
                    .LineHorizontal(1)
                    .LineColor(Colors.Grey.Medium);

                    column
                    .Item()
                    .PaddingTop(5)
                    .PaddingBottom(-10)
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Line("Frequency: Weekly Once");
                        text.Line("Day: Every Sunday");
                        text.Line("Payment Mode: Free");
                        text.Line($"Active Users: {Achievements.GoogleSearchImpacts.Last().Clicks}+");
                        text.Line("Owned By: Abdul Rahman");
                        text.Line("Powered By: .NET");
                    });

                    column
                    .Item()
                    .LineHorizontal(1)
                    .LineColor(Colors.Grey.Medium);

                    column
                    .Item()
                    .PaddingVertical(10)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(8);
                            columns.RelativeColumn(2);
                        });

                        table.Cell().Text("Channel").Bold();
                        table.Cell().AlignRight().Text("Article Count").Bold();
                        
                        table.Cell().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                        table.Cell().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                        foreach (var channel in TableOfContents.Contents.GroupBy(content => content.Channel))
                        {
                            table.Cell().Text(channel.Key);
                            table.Cell().AlignRight().Text($"{channel.Count()}");
                        }

                        table.Cell().Text("Total").Bold();
                        table.Cell().AlignRight().Text($"{TableOfContents.Contents.Count}").Bold();
                    });

                    column
                    .Item()
                    .LineHorizontal(1)
                    .LineColor(Colors.Grey.Medium);

                    column
                    .Item()
                    .PaddingTop(10)
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Line("Thank you for reading");
                        text.Line("This is a computer generated receipt");
                        text.Line("This site is built by Bhai");
                        text.Line("And I Love .NET");
                    });
                });
            });
        });

        document.WithMetadata(new DocumentMetadata
        {
            Title = "Articles Summary",
            Author = "Abdul Rahman",
            Creator = "I Love .NET",
            PdfA = true
        });

        Base64String = Convert.ToBase64String(document.GeneratePdf());
    }
}
