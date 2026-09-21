using System.Globalization;
using System.Text.Json;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class VectorSearchTests(AppFixture fixture) : PageTest
{
  private const string ExpectedModel = "Xenova/all-MiniLM-L6-v2";
  private const string ExpectedModelRevision = "751bff37182d3f1213fa05d7196b954e230abad9";
  private const int ExpectedDimensions = 384;

  private readonly Uri WebBaseUrl = fixture.App.GetEndpoint("ILoveDotNet-Web", "https");

  public override BrowserNewContextOptions ContextOptions() => new()
  {
    IgnoreHTTPSErrors = true,
  };

  [Fact]
  public async Task VectorSearchAssets_AreAvailableAndConsistentAsync()
  {
    var module = await GetSuccessfulResponseAsync("js/vector-search.js");
    var moduleText = await module.TextAsync();
    Assert.Contains("../lib/transformers/transformers.min.js", moduleText, StringComparison.Ordinal);
    Assert.DoesNotContain("cdn.jsdelivr.net/npm/@xenova/transformers", moduleText, StringComparison.OrdinalIgnoreCase);

    var transformers = await GetSuccessfulResponseAsync("lib/transformers/transformers.min.js");
    Assert.True((await transformers.BodyAsync()).Length > 100_000, "Expected a non-empty local Transformers.js bundle.");

    var metadata = await GetSuccessfulResponseAsync("search/index.json");
    var index = JsonSerializer.Deserialize<SearchIndexFile>(await metadata.TextAsync());
    Assert.NotNull(index);
    Assert.Equal(ExpectedModel, index.Model);
    Assert.Equal(ExpectedModelRevision, index.ModelRevision);
    Assert.Equal(ExpectedDimensions, index.Dimensions);
    Assert.Equal(index.Count, index.Entries.Length);
    Assert.NotEmpty(index.Entries);
    Assert.Equal(index.Entries.Length, index.Entries.Select(entry => entry.Slug).Distinct(StringComparer.OrdinalIgnoreCase).Count());

    foreach (var entry in index.Entries)
    {
      Assert.False(string.IsNullOrWhiteSpace(entry.Slug));
      Assert.True(
          DateOnly.TryParseExact(entry.PublishOn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _),
          $"Expected '{entry.PublishOn}' to be a valid publication date for '{entry.Slug}'.");
    }

    var vectors = await GetSuccessfulResponseAsync("search/index.vec");
    Assert.Equal((long)index.Count * index.Dimensions * sizeof(float), (await vectors.BodyAsync()).LongLength);
  }

  private async Task<IAPIResponse> GetSuccessfulResponseAsync(string path)
  {
    var response = await Page.Context.APIRequest.GetAsync(new Uri(WebBaseUrl, path).AbsoluteUri);
    Assert.True(response.Ok, $"Expected {path} to return a successful response, but received {response.Status}.");
    return response;
  }

  private sealed record SearchIndexFile(
      string Model,
      string ModelRevision,
      int Dimensions,
      int Count,
      SearchIndexEntry[] Entries);

  private sealed record SearchIndexEntry(string Slug, string PublishOn);
}
