using System.Diagnostics;
using System.Text.Json;
using System.Security.Cryptography;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatEvaluationTests(AppFixture fixture) : PageTest
{
  public override BrowserNewContextOptions ContextOptions() => new() { IgnoreHTTPSErrors = true };

  [Fact(Explicit = true)]
  [Trait("Category", "RealModel")]
  public async Task RealEmbeddings_RetrieveSupportingEvidenceAndRejectUnsupportedQuestionsAsync()
  {
    var cases = JsonSerializer.Deserialize<EvaluationCase[]>(
        await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "TestData", "learning-chat-retrieval.json"), TestContext.Current.CancellationToken),
        new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    Assert.True(cases.Length >= 40);
    Assert.Equal(cases.Length, cases.Select(item => item.Id).Distinct().Count());
    var baseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");
    var manifestResponse = await Page.APIRequest.GetAsync(new Uri(baseUrl, "chat/corpus/manifest.json").ToString());
    Assert.Equal(200, manifestResponse.Status);
    using var manifest = JsonDocument.Parse(await manifestResponse.TextAsync());
    var workerResponse = await Page.APIRequest.GetAsync(new Uri(baseUrl, "js/chat/retrieval-worker.js").ToString());
    Assert.Equal(200, workerResponse.Status);
    var chunksResponse = await Page.APIRequest.GetAsync(new Uri(baseUrl,
        $"chat/corpus/{manifest.RootElement.GetProperty("chunksUrl").GetString()}").ToString());
    Assert.Equal(200, chunksResponse.Status);
    var corpus = JsonSerializer.Deserialize<Passage[]>(await chunksResponse.TextAsync(),
        new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    foreach (var item in cases.Where(item => item.Slug is not null))
    {
      var evidence = corpus.Where(passage => MatchesReference(item, passage)).ToArray();
      Assert.NotEmpty(evidence);
      Assert.True(ContainsFacts(item, evidence), $"Evaluation facts for '{item.Id}' are absent from the published corpus.");
    }

    await Page.GotoAsync(new Uri(baseUrl, "chat/").ToString());
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync(
        "Local article search is ready. Questions stay in this browser.", new() { Timeout = 240_000 });
    var results = new List<object>();
    var supportedPassed = 0;
    var unsupportedPassed = 0;
    var criticalPassed = true;
    foreach (var item in cases)
    {
      var timer = Stopwatch.StartNew();
      var sourcesJson = await Page.EvaluateAsync<string>("""
          async ({ question, context }) => {
            const chat = await import(new URL('js/learning-chat.js', document.baseURI));
            return JSON.stringify(await chat.search(question, 6, context ?? ''));
          }
          """, new { question = item.Question, context = item.Context });
      var sources = JsonSerializer.Deserialize<Passage[]>(sourcesJson, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
      var evidence = sources.Where(passage => MatchesReference(item, passage)).ToArray();
      var passed = item.Slug is null
          ? sources.Length == 0
          : evidence.Length > 0 && ContainsFacts(item, evidence);
      var firstPassed = item.FirstHeading is null || sources.Length > 0
          && MatchesReference(item, sources[0]) && ContainsFacts(item, [sources[0]])
          && sources[0].Heading.Contains(item.FirstHeading, StringComparison.OrdinalIgnoreCase);
      criticalPassed &= firstPassed;
      if (item.Slug is null) unsupportedPassed += passed ? 1 : 0;
      else supportedPassed += passed ? 1 : 0;
      results.Add(new
      {
        item.Id,
        item.Category,
        item.Holdout,
        item.Question,
        item.Context,
        passed,
        firstPassed,
        milliseconds = timer.ElapsedMilliseconds,
        sources
      });
    }
    var supportedCount = cases.Count(item => item.Slug is not null);
    var unsupportedCount = cases.Length - supportedCount;
    var report = new
    {
      generatedAt = DateTimeOffset.UtcNow,
      model = manifest.RootElement.GetProperty("model").GetString(),
      modelRevision = manifest.RootElement.GetProperty("modelRevision").GetString(),
      corpusVersion = manifest.RootElement.GetProperty("corpusVersion").GetString(),
      browser = Browser.Version,
      evaluationSha256 = Convert.ToHexStringLower(SHA256.HashData(
          await File.ReadAllBytesAsync(Path.Combine(AppContext.BaseDirectory, "TestData", "learning-chat-retrieval.json"), TestContext.Current.CancellationToken))),
      workerSha256 = Convert.ToHexStringLower(SHA256.HashData(await workerResponse.BodyAsync())),
      supportedPassed,
      supportedCount,
      unsupportedPassed,
      unsupportedCount,
      criticalPassed,
      results,
    };
    var reportDirectory = Path.Combine(AppContext.BaseDirectory, "TestResults");
    Directory.CreateDirectory(reportDirectory);
    var reportPath = Path.Combine(reportDirectory, "learning-chat-evaluation.json");
    await File.WriteAllTextAsync(reportPath, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }), TestContext.Current.CancellationToken);
    Console.WriteLine($"Real-model evaluation: {supportedPassed}/{supportedCount} supported, {unsupportedPassed}/{unsupportedCount} unsupported. Report: {reportPath}");
    Assert.True(supportedPassed >= Math.Ceiling(supportedCount * 0.9),
        $"Supporting evidence recall is below 90%: {supportedPassed}/{supportedCount}. See {reportPath}");
    Assert.True(unsupportedPassed >= Math.Ceiling(unsupportedCount * 0.9),
        $"Unsupported-question abstention is below 90%: {unsupportedPassed}/{unsupportedCount}. See {reportPath}");
    Assert.True(criticalPassed, $"A definition/sweeping first-result regression failed. See {reportPath}");
  }

  private static bool MatchesReference(EvaluationCase item, Passage passage)
      => item.Slug is not null
          && new[] { item.Slug }.Concat(item.AlternativeSlugs ?? [])
              .Any(slug => passage.ContentUrl.Trim('/').Equals($"blogs/{slug}", StringComparison.Ordinal))
          && (item.Heading is null || passage.Heading.Contains(item.Heading, StringComparison.OrdinalIgnoreCase));

  private static bool ContainsFacts(EvaluationCase item, Passage[] evidence)
      => (item.Facts ?? []).All(fact => evidence.Any(passage => passage.Excerpt.Contains(fact, StringComparison.OrdinalIgnoreCase)));

  private sealed record EvaluationCase(string Id, string Category, string Question, string? Context,
      string? Slug, string? Heading, string? FirstHeading, string[]? Facts, string[]? AlternativeSlugs, bool Holdout);
  private sealed record Passage(string Title, string Section, string Heading, string ContentUrl, string Excerpt);
}
