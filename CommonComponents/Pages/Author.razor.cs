using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedModels;
using System.Globalization;

namespace CommonComponents.Pages;

public class AuthorBase : ComponentBase
{
    protected AuthorMetaData Author = AuthorMetaData.NullAuthor;
    protected string ContentType => $"{CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Name)}";
    protected string Title => $"{ContentType} - I ❤️ DotNet";
    protected string Description => $"This is a .NET {ContentType} knowledge sharing channel with live demos crafted by developers for developers with love.";
    protected string BaseUrl => Configuration.GetValue<string>("baseUrl")!;
    protected string Url => $"{BaseUrl}authors/{Name.ToLower()}";

    [Inject] public IConfiguration Configuration { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private TableOfContents TableOfContents { get; set; } = default!;

    [Parameter, EditorRequired, SupplyParameterFromQuery] public string Name { get; set; } = default!;

    protected override void OnInitialized()
    {
        Author = TableOfContents.Authors
                                .FirstOrDefault(author => author.Name.Equals(Name, StringComparison.OrdinalIgnoreCase), AuthorMetaData.NullAuthor);

        if (Author.Order == 0)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
