using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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
  protected bool generationSupported;
  protected string generationModelId = string.Empty;
  protected bool generationReady;
  protected bool generationLoading;
  protected double generationProgress;
  protected string generationStatus = "Checking local answer support…";
  private IJSObjectReference? generationModule;
  private DotNetObjectReference<LearningChatBase>? callback;
  private long generationOperation;
  private ChatMessage? streamingMessage;
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
      if (disposed)
      {
        return;
      }
      try
      {
        var importedGenerationModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/chat/generation.js");
        if (disposed)
        {
          await importedGenerationModule.DisposeAsync();
          return;
        }
        generationModule = importedGenerationModule;
        callback = DotNetObjectReference.Create(this);
        var capability = await generationModule.InvokeAsync<GenerationCapability>("capability");
        if (disposed)
        {
          return;
        }
        generationSupported = capability.Supported;
        generationModelId = capability.ModelId ?? string.Empty;
        generationStatus = capability.Supported
          ? "Optional local answers: Qwen3 4B, approximately 2.26 GB of weights plus runtime downloads. Several GB of GPU memory and browser overhead are needed; actual requirements vary by device. Files come from Hugging Face and GitHub; questions stay here."
          : capability.Reason;
        if (capability.Supported && capability.Consent)
        {
          await LoadGenerationAsync();
        }
      }
      catch (JSException exception)
      {
        generationStatus = $"Local answers unavailable: {exception.Message} Original article search remains available.";
      }
      if (!disposed)
      {
        await InvokeAsync(StateHasChanged);
      }
    }

    if (!disposed && scrollToLatest && module is not null)
    {
      scrollToLatest = false;
      await module.InvokeVoidAsync("scrollToLatest", conversation);
    }
  }

  protected async Task LoadGenerationAsync()
  {
    if (generationModule is null || generationLoading || isSearching)
    {
      return;
    }
    var operation = ++generationOperation;
    generationLoading = true;
    generationProgress = 0;
    generationStatus = "Loading the local answer model…";
    await InvokeAsync(StateHasChanged);
    try
    {
      await generationModule.InvokeVoidAsync("load", callback, operation);
      if (!disposed && operation == generationOperation)
      {
        generationReady = true;
        generationStatus = "Local answers ready. AI-generated explanations use article passages; check the linked sources for accuracy.";
      }
    }
    catch (JSException exception)
    {
      if (!disposed && operation == generationOperation)
      {
        generationReady = false;
        generationStatus = $"Local model loading failed: {exception.Message} Original article search remains available.";
      }
    }
    finally
    {
      if (!disposed && operation == generationOperation)
      {
        generationLoading = false;
        await InvokeAsync(StateHasChanged);
      }
    }
  }

  [JSInvokable]
  public Task OnGenerationUpdate(long operation, string phase, string text, double progress)
      => InvokeAsync(() =>
      {
        if (disposed || operation != generationOperation)
        {
          return;
        }
        if (phase == "loading")
        {
          generationProgress = Math.Clamp(progress, 0, 1);
          generationStatus = text;
        }
        else if (phase == "streaming" && streamingMessage is not null)
        {
          streamingMessage.Text = text;
          scrollToLatest = true;
        }
        StateHasChanged();
      });

  protected async Task UseSourcesOnlyAsync()
  {
    generationOperation++;
    generationReady = false;
    generationLoading = false;
    if (streamingMessage is not null)
    {
      streamingMessage.Text = SourceSummary(streamingMessage.Sources.Count);
      streamingMessage.IsStreaming = false;
      streamingMessage = null;
    }
    if (generationModule is not null)
    {
      await generationModule.InvokeVoidAsync("stop");
    }
    generationStatus = "Source-only mode. You can load local answers again when needed.";
  }

  protected async Task ClearGenerationCacheAsync()
  {
    await UseSourcesOnlyAsync();
    try
    {
      await generationModule!.InvokeVoidAsync("clear");
      generationStatus = "Answer model cache cleared. Download again to generate answers; article search is unchanged.";
    }
    catch (JSException exception)
    {
      generationStatus = $"Could not clear the answer model cache: {exception.Message}";
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
        : SourceSummary(sources.Length);
      var answer = new ChatMessage(response, IsUser: false, sources);
      messages.Add(answer);
      if (sources.Length > 0 && generationReady && generationModule is not null)
      {
        var operation = ++generationOperation;
        streamingMessage = answer;
        answer.Text = "Reading article passages…";
        answer.IsStreaming = true;
        statusMessage = "Generating locally. Draft text and citations are provisional…";
        await InvokeAsync(StateHasChanged);
        try
        {
          var generated = await generationModule.InvokeAsync<GeneratedAnswer>("generate", question, sources, context, callback, operation);
          if (!disposed && operation == generationOperation && request == activeRequest)
          {
            if (generated.SourceCount < 1 || generated.SourceCount > sources.Length
              || (generated.Outcome != "answered" && generated.Outcome != "refused")
              || (generated.Outcome == "answered" && string.IsNullOrWhiteSpace(generated.Text))
              || (generated.Outcome == "refused" && generated.Text != string.Empty))
            {
              throw new JSException("The local answer returned invalid source metadata. Showing original sources instead.");
            }
            if (generated.Outcome == "refused")
            {
              answer.Text = response;
              generationStatus = "The local model could not answer from the supplied passages. Original sources are shown below; this does not mean the articles lack an answer.";
            }
            else
            {
              answer.Text = generated.Text;
              answer.Sources = sources.Take(generated.SourceCount).ToArray();
              answer.IsGenerated = true;
              generationStatus = "Local answers ready. AI-generated explanations use article passages; check the linked sources for accuracy.";
            }
          }
        }
        catch (JSException exception)
        {
          if (!disposed && operation == generationOperation && request == activeRequest)
          {
            answer.Text = response;
            generationStatus = $"Local answer unavailable: {exception.Message}";
          }
        }
        finally
        {
          answer.IsStreaming = false;
          if (streamingMessage == answer)
          {
            streamingMessage = null;
          }
        }
      }
      if (disposed || request != activeRequest)
      {
        return;
      }
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
    await UseSourcesOnlyAsync();
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

  protected static IEnumerable<AnswerPart> AnswerParts(ChatMessage message)
  {
    var offset = 0;
    foreach (Match match in Regex.Matches(message.Text, @"\[(\d+)\]"))
    {
      if (match.Index > offset)
      {
        yield return new AnswerPart(message.Text[offset..match.Index], null);
      }
      var validNumber = int.TryParse(match.Groups[1].Value, System.Globalization.NumberStyles.None,
        System.Globalization.CultureInfo.InvariantCulture, out var number);
      yield return new AnswerPart(match.Value, validNumber && number >= 1 && number <= message.Sources.Count ? message.Sources[number - 1] : null);
      offset = match.Index + match.Length;
    }
    if (offset < message.Text.Length)
    {
      yield return new AnswerPart(message.Text[offset..], null);
    }
  }

  private static string SourceSummary(int count)
      => $"I found {count} relevant article passage{(count == 1 ? string.Empty : "s")}. These are search results, not an AI-generated answer; check the original articles for context.";

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
    generationOperation++;
    if (generationModule is not null)
    {
      await generationModule.InvokeVoidAsync("stop");
      await generationModule.DisposeAsync();
    }
    callback?.Dispose();
    if (module is not null)
    {
      await module.InvokeVoidAsync("dispose", questionInput);
      await module.DisposeAsync();
    }
  }

  protected sealed class ChatMessage(string Text, bool IsUser, IReadOnlyList<ChatSource> Sources)
  {
    public string Text { get; set; } = Text;
    public bool IsUser { get; } = IsUser;
    public IReadOnlyList<ChatSource> Sources { get; set; } = Sources;
    public bool IsStreaming { get; set; }
    public bool IsGenerated { get; set; }
  }

  protected sealed record AnswerPart(string Text, ChatSource? Source);
  private sealed record GenerationCapability(
    [property: JsonPropertyName("supported")] bool Supported,
    [property: JsonPropertyName("reason")] string Reason,
    [property: JsonPropertyName("consent")] bool Consent,
    [property: JsonPropertyName("modelId")] string? ModelId);
  private sealed record GeneratedAnswer(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("sourceCount")] int SourceCount,
    [property: JsonPropertyName("outcome")] string Outcome);

  public sealed record ChatSource(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("section")] string Section,
    [property: JsonPropertyName("heading")] string Heading,
    [property: JsonPropertyName("contentUrl")] string ContentUrl,
    [property: JsonPropertyName("excerpt")] string Excerpt);
}
