using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedModels;

namespace CommonComponents.Pages;

public class ChannelBase : ComponentBase
{
  protected string ContentType { get; private set; } = "";
  protected string Title => ContentType.ToLowerInvariant() switch
  {
    "linq" => "LINQ Tutorial in C# - I ❤️ DotNet",
    "blazor" => "Blazor Tutorial - I ❤️ DotNet",
    "web api" => ".NET Core Web API Tutorial - I ❤️ DotNet",
    "efcore" => "EF Core Tutorial in C# - Entity Framework Core, SQL Server & LINQ - I ❤️ DotNet",
    _ => $"{ContentType} - I ❤️ DotNet"
  };
  protected string Description => ContentType.ToLowerInvariant() switch
  {
    "linq" => "LINQ tutorial in C# with practical examples for Select, OrderBy, GroupBy, joins, filtering, and aggregation.",
    "blazor" => "Blazor tutorial guides for building .NET WebAssembly applications with practical examples and live demos.",
    "web api" => ".NET Core Web API tutorial guides covering authentication, testing, performance, OpenAPI, and production practices.",
    "efcore" => "EF Core tutorial in C# covering Entity Framework Core with SQL Server, LINQ, Code First, migrations, relationships, and practical examples.",
    _ => $"Practical .NET {ContentType} tutorials with live demos, examples, and learning resources for developers."
  };
  protected string BaseUrl => Configuration.GetValue<string>("baseUrl")!;
  protected string Url => $"{BaseUrl}channels/{ContentType.ToLowerInvariant().ApplyHyphen()}/";
  protected string? IconUrl;
  protected bool KnownChannel;
  protected string SearchText = "";
  protected IEnumerable<ContentMetaData> FilteredContents => Contents.Where(MatchesSearch);
  protected IEnumerable<(ContentMetaData Content, int Step)> FilteredSequence =>
      LearningPathContents.Select((content, index) => (Content: content, Step: index + 1)).Where(item => MatchesSearch(item.Content));
  protected List<ContentMetaData> Contents = [];
  protected List<ContentMetaData> LearningPathContents = [];
  protected bool ShowArticleTab = true;

  [Inject] public IConfiguration Configuration { get; set; } = default!;
  [Inject] private TableOfContents TableOfContents { get; set; } = default!;

  [Parameter, EditorRequired] public string Name { get; set; } = default!;

  protected override void OnParametersSet()
  {
    var displayName = Name.RemoveHyphen();
    var metadata = TableOfContents.AllContents.FirstOrDefault(content =>
        content.Channel.Equals(displayName, StringComparison.OrdinalIgnoreCase));
    var previousChannel = ContentType;
    ContentType = metadata?.Channel ?? CultureInfo.InvariantCulture.TextInfo.ToTitleCase(displayName);
    KnownChannel = metadata is not null;
    IconUrl = metadata?.IconUrl;
    var channelContents = TableOfContents.Contents
        .Where(content => content.Channel.Equals(ContentType, StringComparison.OrdinalIgnoreCase)).ToList();

    Contents = channelContents.OrderByDescending(content => content.ModifiedOn)
                              .ToList();

    LearningPathContents = channelContents.OrderBy(content => content.Order)
                                          .ToList();

    if (!previousChannel.Equals(ContentType, StringComparison.Ordinal))
    {
      SearchText = "";
      ShowArticleTab = true;
    }
  }

  private bool MatchesSearch(ContentMetaData content) =>
      content.Title.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase)
      || content.Description.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase);

  protected void OnArticleTabClick()
  {
    ShowArticleTab = true;
  }

  protected void OnLearningPathTabClick()
  {
    ShowArticleTab = false;
  }
}
