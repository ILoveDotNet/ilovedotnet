using System.Globalization;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SharedModels;

namespace CommonComponents.Pages;

public class AuthorBase : ComponentBase
{
  protected AuthorMetaData Author = AuthorMetaData.NullAuthor;
  protected IReadOnlyList<ContentMetaData> LatestArticles = [];
  protected int PublishedArticleCount;
  protected int PublishedTopicCount;
  protected string ProfileStructuredData
  {
    get
    {
      var knowsAbout = new JsonArray();
      foreach (var topic in TableOfContents.Contents
          .Where(content => content.Author.Equals(Author.Name, StringComparison.OrdinalIgnoreCase))
          .Select(content => content.Channel)
          .Distinct(StringComparer.OrdinalIgnoreCase)
          .Order())
      {
        knowsAbout.Add(topic);
      }

      var sameAs = new JsonArray();
      foreach (var profileUrl in Author.SameAs)
      {
        sameAs.Add(profileUrl);
      }

      var person = new JsonObject
      {
        ["@type"] = "Person",
        ["@id"] = $"{Url}#person",
        ["name"] = Author.Name,
        ["url"] = Url,
        ["image"] = new Uri(new Uri(BaseUrl), Author.ImageUrl).AbsoluteUri,
        ["description"] = Author.Description,
        ["jobTitle"] = Author.Role,
        ["sameAs"] = sameAs,
        ["knowsAbout"] = knowsAbout
      };

      if (Author.IsMVP)
      {
        person["award"] = "Microsoft MVP";
      }

      return new JsonObject
      {
        ["@context"] = "https://schema.org",
        ["@type"] = "ProfilePage",
        ["url"] = Url,
        ["mainEntity"] = person
      }.ToJsonString();
    }
  }
  protected string ContentType => $"{CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(Name)}";
  protected string Title => $"{ContentType} - I ❤️ DotNet";
  protected string Description => Author.Introduction;
  protected string BaseUrl => Configuration.GetValue<string>("baseUrl")!;
  protected string Url => $"{BaseUrl}authors/{Name.ToLower().ApplyHyphen()}/";

  [Inject] public IConfiguration Configuration { get; set; } = default!;
  [Inject] private NavigationManager NavigationManager { get; set; } = default!;
  [Inject] private TableOfContents TableOfContents { get; set; } = default!;

  [Parameter, EditorRequired, SupplyParameterFromQuery] public string Name { get; set; } = default!;

  protected override void OnParametersSet()
  {
    Name = Name.Replace("-", " ");

    Author = TableOfContents.Authors
                            .FirstOrDefault(author => author.Name.Equals(Name, StringComparison.OrdinalIgnoreCase), AuthorMetaData.NullAuthor);

    if (Author.Order == 0)
    {
      NavigationManager.NavigateTo("/");
      return;
    }

    var publishedArticles = TableOfContents.Contents
        .Where(content => content.Author.Equals(Author.Name, StringComparison.OrdinalIgnoreCase))
        .ToList();
    PublishedArticleCount = publishedArticles.Count;
    PublishedTopicCount = publishedArticles.Select(content => content.Channel).Distinct(StringComparer.OrdinalIgnoreCase).Count();
    LatestArticles = publishedArticles
        .OrderByDescending(content => content.ModifiedOn)
        .ThenByDescending(content => content.CreatedOn)
        .Take(6)
        .ToList();
  }
}
