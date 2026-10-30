using System.Text.Json;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatGenerationBenchmarkTests(AppFixture fixture) : PageTest
{
  public override BrowserNewContextOptions ContextOptions() => new() { IgnoreHTTPSErrors = true };

  [Fact(Explicit = true)]
  [Trait("Category", "RealModel")]
  public async Task ProductionChatExplainsTddRedWithRealModelAndCitationsAsync()
  {
    var profile = Environment.GetEnvironmentVariable("LEARNING_CHAT_BENCHMARK_PROFILE");
    await using var cachedContext = profile is null ? null : await BrowserType.LaunchPersistentContextAsync(profile,
      new() { IgnoreHTTPSErrors = true, Headless = Environment.GetEnvironmentVariable("HEADED") != "1" });
    var page = cachedContext is null ? Page : await cachedContext.NewPageAsync();
    page.Console += (_, message) => Console.WriteLine($"Browser {message.Type}: {message.Text}");
    page.PageError += (_, error) => Console.WriteLine($"Browser error: {error}");
    await page.AddInitScriptAsync("""
      const OriginalWorker = window.Worker;
      window.Worker = class extends OriginalWorker {
        constructor(...args) {
          super(...args);
          if (String(args[0]).includes('generation-worker')) this.addEventListener('message', ({data}) => {
            if (data.rawAnswer || data.result?.text) window.chatWorkerAnswer = data.rawAnswer || data.result.text;
          });
        }
      };
      """);
    await page.GotoAsync(new Uri(fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/").ToString());
    await Expect(page.GetByRole(AriaRole.Status)).ToHaveTextAsync(
      "Local article search is ready. Questions stay in this browser.", new() { Timeout = 240_000 });
    var download = page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" });
    if (await download.IsVisibleAsync()) await download.ClickAsync();
    var panel = page.GetByRole(AriaRole.Complementary, new() { Name = "Local answer model" });
    await Expect(panel).ToContainTextAsync("Local answers ready", new() { Timeout = 900_000 });
    await Expect(panel).ToContainTextAsync("Qwen3-4B-q4f16_1-MLC");
    var results = new List<JsonElement>();
    var directory = Path.Combine(AppContext.BaseDirectory, "TestResults");
    Directory.CreateDirectory(directory);
    var path = Path.Combine(directory, "learning-chat-production-generation.json");
    foreach (var question in new[]
    {
      "what is red phase in TDD?",
      "what is sweeping?",
      "Compare transient scoped and singleton service lifetimes.",
      "How do I modify an existing JSON property with JsonNode?",
      "what is a value object and why we need it?",
      "How do I call GC.EnableTeleportation() to eliminate collection pauses?",
    })
    {
      var evidence = await page.EvaluateAsync<JsonElement>("""
        async question => {
          const chat = await import(new URL('js/learning-chat.js', document.baseURI));
          const {createGroundedPrompt,selectedModel} = await import(new URL('js/chat/generation-contract.js', document.baseURI));
          const sources = await chat.search(question,6,'');
          let packed,error;
          try { packed = createGroundedPrompt(question,sources); }
          catch(failure) { error = failure.message; }
          return {sources,packed,error,model:selectedModel.model_id};
        }
        """, question);
      await page.EvaluateAsync("""
        () => {
          window.chatWorkerAnswer = null;
          window.chatDraftObserved = false;
          window.chatObserver = new MutationObserver(() => {
            if (document.body.textContent.includes('AI-generated draft.')) window.chatDraftObserved = true;
          });
          window.chatObserver.observe(document.querySelector('[aria-label="Conversation"]'), {childList:true,subtree:true,characterData:true});
        }
        """);
      await page.GetByRole(AriaRole.Textbox).FillAsync(question);
      await page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
      await Expect(page.GetByRole(AriaRole.Status)).ToHaveTextAsync(
        new System.Text.RegularExpressions.Regex(@"^(Search complete\.|No strong article matches found\.)$"),
        new() { Timeout = 180_000 });
      var rendered = await page.EvaluateAsync<JsonElement>("""
        () => {
          window.chatObserver.disconnect();
          const answer = document.querySelector('[aria-label="Conversation"] article:last-child');
          return {
            label:answer.querySelector('p').textContent.trim(),
            text:answer.querySelectorAll('p')[1].textContent.trim(),
            citations:Array.from(answer.querySelectorAll('p:nth-child(2) a')).map(a=>({text:a.textContent,url:a.href})),
            sourceUrls:Array.from(answer.querySelectorAll('ol a')).map(a=>a.href),
            status:document.querySelector('[aria-label="Local answer model"]').textContent.trim(),
            workerAnswer:window.chatWorkerAnswer,
            draftObserved:window.chatDraftObserved
          };
        }
        """);
      results.Add(JsonSerializer.SerializeToElement(new { question, evidence, rendered }));
      await File.WriteAllTextAsync(path, JsonSerializer.Serialize(new
      {
        generatedAt = DateTimeOffset.UtcNow,
        purpose = "Actual reader pipeline; TDD acceptance assertions plus diagnostic semantic review, not general factuality approval",
        results,
      }), TestContext.Current.CancellationToken);
      await page.GetByRole(AriaRole.Button, new() { Name = "New conversation" }).ClickAsync();
    }
    Console.WriteLine($"Production chat evaluation: {path}");
    var red = results[0].GetProperty("rendered");
    Assert.Contains("article-grounded answer", red.GetProperty("label").GetString());
    Assert.Contains("test", red.GetProperty("text").GetString(), StringComparison.OrdinalIgnoreCase);
    Assert.Contains("fail", red.GetProperty("text").GetString(), StringComparison.OrdinalIgnoreCase);
    Assert.True(red.GetProperty("draftObserved").GetBoolean());
    Assert.NotEmpty(red.GetProperty("citations").EnumerateArray());
    Assert.All(red.GetProperty("citations").EnumerateArray(), citation =>
    {
      var url = citation.GetProperty("url").GetString()!;
      Assert.Contains("tdd-in-csharp-dotnet/", url);
      Assert.EndsWith("#how", url);
    });
    var redEvidence = results[0].GetProperty("evidence").GetProperty("packed").GetRawText();
    Assert.Contains("fail", redEvidence, StringComparison.OrdinalIgnoreCase);
    foreach (var citation in red.GetProperty("citations").EnumerateArray())
    {
      await page.GotoAsync(citation.GetProperty("url").GetString()!);
      await Expect(page.Locator("#how")).ToBeAttachedAsync(new() { Timeout = 30_000 });
    }
    var unsupported = results[^1].GetProperty("rendered");
    Assert.Equal("Article search", unsupported.GetProperty("label").GetString());
  }
}
