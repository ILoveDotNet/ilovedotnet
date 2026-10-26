using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace CommonComponents.Pages;

public class LearningChatBase : ComponentBase, IAsyncDisposable
{
  protected string Title => "Learn with AI - I ❤️ DotNet";
  protected string Description => "Search .NET, Blazor, and C# learning articles in your browser, with passages linked to their original sources.";
  protected string BaseUrl => Configuration.GetValue<string>("baseUrl")!;
  protected string PageUrl => $"{BaseUrl}chat/";

  protected readonly string[] SuggestedPrompts =
  [
    "How does Blazor WebAssembly work?",
    "When should I use dependency injection?",
    "What is the difference between a record and a class?",
    "How can I make an EF Core query faster?"
  ];

  protected readonly List<ChatMessage> messages = [];
  protected string draft = string.Empty;
  protected ElementReference conversation;
  protected ElementReference questionInput;
  protected bool retrievalReady;
  protected bool isLoading;
  protected bool isSearching;
  protected string statusMessage = "Preparing local article search…";
  protected string errorMessage = string.Empty;
  private IJSObjectReference? module;
  private bool scrollToLatest;
  private bool disposed;
  protected bool loadingCancelled;
  private long activeRequest;

  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private IConfiguration Configuration { get; set; } = default!;
  [Inject] private NavigationManager Navigation { get; set; } = default!;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      var importedModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/learning-chat.js");
      if (disposed)
      {
        await importedModule.DisposeAsync();
        return;
      }

      module = importedModule;
      await module.InvokeVoidAsync("initialize", questionInput, conversation);
      await InitializeRetrievalAsync();
    }

    if (!disposed && scrollToLatest && module is not null)
    {
      scrollToLatest = false;
      await module.InvokeVoidAsync("scrollToLatest", conversation);
    }
  }

  protected void SelectPrompt(string prompt)
  {
    draft = prompt;
  }

  protected async Task InitializeRetrievalAsync()
  {
    if (retrievalReady || isLoading)
    {
      return;
    }

    isLoading = true;
    loadingCancelled = false;
    errorMessage = string.Empty;
    statusMessage = "Loading the article index and pinned embedding model in this browser…";
    await InvokeAsync(StateHasChanged);
    try
    {
      var chatModule = await GetModuleAsync();
      await chatModule.InvokeVoidAsync("enableRetrieval", Navigation.BaseUri);
      if (!disposed && !loadingCancelled)
      {
        retrievalReady = true;
        statusMessage = "Local article search is ready. Questions stay in this browser.";
      }
    }
    catch (JSException exception)
    {
      if (!disposed && !loadingCancelled)
      {
        errorMessage = $"Local article search could not be loaded: {exception.Message}";
        statusMessage = "Article search is unavailable.";
      }
    }
    finally
    {
      isLoading = false;
      if (!disposed)
      {
        await InvokeAsync(StateHasChanged);
      }
    }
  }

  protected async Task CancelLoadingAsync()
  {
    loadingCancelled = true;
    statusMessage = "Loading cancelled. No question was sent.";
    if (module is not null)
    {
      await module.InvokeVoidAsync("stop");
    }
  }

  protected async Task SendQuestionAsync()
  {
    var question = draft.Trim();
    if (question.Length == 0 || !retrievalReady || isSearching)
    {
      return;
    }

    messages.Add(new ChatMessage(question, IsUser: true, []));
    draft = string.Empty;
    errorMessage = string.Empty;
    scrollToLatest = true;
    isSearching = true;
    var request = ++activeRequest;
    statusMessage = "Searching article passages locally…";

    try
    {
      var context = string.Join(
        "\n",
        messages.Where(message => message.IsUser).SkipLast(1).TakeLast(2).Select(message => message.Text));
      var chatModule = await GetModuleAsync();
      var sources = await chatModule.InvokeAsync<ChatSource[]>("search", question, 6, context);
      if (disposed || request != activeRequest)
      {
        return;
      }

      var response = sources.Length == 0
        ? "I couldn't find a sufficiently close passage in the indexed articles. Try a more specific question or browse the learning paths."
        : $"I found {sources.Length} relevant article passage{(sources.Length == 1 ? string.Empty : "s")}. These are search results, not an AI-generated answer; check the original articles for context.";
      messages.Add(new ChatMessage(response, IsUser: false, sources));
      statusMessage = sources.Length == 0 ? "No strong article matches found." : "Search complete.";
      scrollToLatest = true;
    }
    catch (JSException exception)
    {
      if (request == activeRequest)
      {
        errorMessage = $"Article search failed: {exception.Message}";
        statusMessage = "Search failed. Try again or start a new conversation.";
      }
    }
    finally
    {
      if (request == activeRequest)
      {
        isSearching = false;
      }
    }
  }

  protected async Task StopSearchAsync()
  {
    activeRequest++;
    isSearching = false;
    statusMessage = "Search stopped.";
    if (module is not null)
    {
      await module.InvokeVoidAsync("stop");
    }
  }

  protected async Task NewConversationAsync()
  {
    if (isSearching)
    {
      await StopSearchAsync();
    }

    activeRequest++;
    isSearching = false;
    messages.Clear();
    errorMessage = string.Empty;
    statusMessage = retrievalReady
      ? "Conversation cleared. Local article search is ready."
      : "Local article search is not ready.";
  }

  protected async Task RetryLastQuestionAsync()
  {
    var lastQuestion = messages.LastOrDefault(message => message.IsUser)?.Text;
    if (!string.IsNullOrWhiteSpace(lastQuestion))
    {
      draft = lastQuestion;
      await SendQuestionAsync();
    }
  }

  protected string SourceUrl(ChatSource source)
  {
    var sectionAnchor = source.Section.ToLowerInvariant() switch
    {
      "why" => "why",
      "how" => "how",
      "summary" => "summary",
      _ => "what",
    };
    var relativeUrl = $"{source.ContentUrl.Trim('/')}/#{sectionAnchor}";
    return new Uri(new Uri(Navigation.BaseUri), relativeUrl).ToString();
  }

  private async Task<IJSObjectReference> GetModuleAsync()
  {
    if (module is not null)
    {
      return module;
    }

    var importedModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/learning-chat.js");
    if (disposed)
    {
      await importedModule.DisposeAsync();
      throw new ObjectDisposedException(nameof(LearningChatBase));
    }

    module = importedModule;
    return module;
  }

  public async ValueTask DisposeAsync()
  {
    disposed = true;
    activeRequest++;
    if (module is not null)
    {
      await module.InvokeVoidAsync("dispose", questionInput);
      await module.DisposeAsync();
    }
  }

  protected sealed record ChatMessage(string Text, bool IsUser, IReadOnlyList<ChatSource> Sources);

  public sealed record ChatSource(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("section")] string Section,
    [property: JsonPropertyName("heading")] string Heading,
    [property: JsonPropertyName("contentUrl")] string ContentUrl,
    [property: JsonPropertyName("excerpt")] string Excerpt);
}
