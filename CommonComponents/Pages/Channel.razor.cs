using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedModels;

namespace CommonComponents.Pages;

public class ChannelBase : ComponentBase
{
  protected string ContentType => $"{CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Name)}";
  protected string Title => $"{ContentType} - I ❤️ DotNet";
  protected string Description => $"This is a .NET {ContentType} knowledge sharing channel with live demos crafted by developers for developers with love.";
  protected string BaseUrl => Configuration.GetValue<string>("baseUrl")!;
  protected string Url => $"{BaseUrl}channels/{Name.ToLower()}";
  protected List<ContentMetaData> Contents = [];
  protected List<ContentMetaData> LearningPathContents = [];
  protected bool ShowArticleTab = true;
  protected bool ShowLearningPathTab = false;

  [Inject] public IConfiguration Configuration { get; set; } = default!;
  [Inject] private NavigationManager NavigationManager { get; set; } = default!;
  [Inject] private TableOfContents TableOfContents { get; set; } = default!;

  [Parameter, EditorRequired, SupplyParameterFromQuery] public string Name { get; set; } = default!;

  protected override void OnInitialized()
  {
    Name = Name.RemoveHyphen();

    var channelContents = TableOfContents.AllContents
                              .Where(content => content.Channel.Equals(Name, StringComparison.OrdinalIgnoreCase))
                              .ToList();


    Contents = channelContents.OrderByDescending(content => content.ModifiedOn)
                              .ToList();

    LearningPathContents = channelContents.OrderBy(content => content.Order)
                                          .ToList();

    if (Contents.Count == 0)
    {
      NavigationManager.NavigateTo("/");
    }
  }

  protected void OnArticleTabClick()
  {
    ShowArticleTab = true;
    ShowLearningPathTab = false;
  }

  protected void OnLearningPathTabClick()
  {
    ShowArticleTab = false;
    ShowLearningPathTab = true;
  }
}
