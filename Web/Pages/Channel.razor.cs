using Microsoft.AspNetCore.Components;
using SharedModels;
using System.Globalization;

namespace Web.Pages;

public class ChannelBase : ComponentBase
{
    protected string ContentType => $"{CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Name)}";
    protected string Title => $"{ContentType} - I ❤️ DotNet";
    protected string Description => $"This is a .NET {ContentType} knowledge sharing channel with live demos crafted by developers for developers with love.";
    protected string BaseUrl => Configuration.GetValue<string>("baseUrl");
    protected string Url => $"{BaseUrl}channels/{Name.ToLower()}";
    protected List<ContentMetaData> Contents = new(0);
    protected List<ContentMetaData> LearningPathContents = new(0);
    protected bool ShowArticleTab = true;
    protected bool ShowLearningPathTab = false;

    [Inject] public IConfiguration Configuration { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private TableOfContents TableOfContents { get; set; } = default!;

    [Parameter, EditorRequired] public string Name { get; set; } = default!;

    protected override void OnInitialized()
    {
        var channelContents = TableOfContents.Contents
                                  .Where(content => content.Type.Equals(Name, StringComparison.OrdinalIgnoreCase))
                                  .ToList();


        Contents = channelContents.OrderByDescending(content => content.CreatedOn)
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
