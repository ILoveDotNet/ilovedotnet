using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CommonComponents.Pages;

public class LearningChatBase : ComponentBase, IAsyncDisposable
{
  private const string PreviewReply = "This is a UI preview. Article retrieval and local AI responses will be connected in a later phase.";

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
  private IJSObjectReference? module;
  private bool scrollToLatest;
  private bool disposed;

  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

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
      await module.InvokeVoidAsync("initialize", questionInput);
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

  protected void SendQuestion()
  {
    var question = draft.Trim();
    if (question.Length == 0)
    {
      return;
    }

    messages.Add(new ChatMessage(question, IsUser: true));
    messages.Add(new ChatMessage(PreviewReply, IsUser: false));
    draft = string.Empty;
    scrollToLatest = true;
  }

  public async ValueTask DisposeAsync()
  {
    disposed = true;
    if (module is not null)
    {
      await module.InvokeVoidAsync("dispose", questionInput);
      await module.DisposeAsync();
    }
  }

  protected sealed record ChatMessage(string Text, bool IsUser);
}
