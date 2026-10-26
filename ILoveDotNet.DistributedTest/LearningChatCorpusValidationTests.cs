using System.Security.Cryptography;
using System.Text.Json;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatCorpusValidationTests(AppFixture fixture) : PageTest
{
  public override BrowserNewContextOptions ContextOptions() => new() { IgnoreHTTPSErrors = true };

  [Theory]
  [InlineData("checksum", "integrity check")]
  [InlineData("duplicate", "invalid or duplicate passage")]
  [InlineData("url", "invalid or duplicate passage")]
  [InlineData("vectors", "vectors contain invalid values")]
  [InlineData("count", "counts do not match")]
  public async Task CorruptCorpus_IsRejectedBeforeModelLoadingAndCanBeRetriedAsync(string failure, string expectedError)
  {
    var passage = new
    {
      id = "test:what:0:0",
      slug = "test",
      title = "Test",
      section = "What",
      heading = "What",
      contentUrl = failure == "url" ? "https://example.com/blogs/test/" : "blogs/test/",
      excerpt = "Original article evidence.",
      tokenCount = 10,
    };
    var chunks = JsonSerializer.SerializeToUtf8Bytes(new[]
    {
      passage, passage with { id = failure == "duplicate" ? passage.id : "test:what:0:1" },
    });
    var vectors = new float[384 * 2];
    vectors[0] = failure == "vectors" ? float.NaN : 1;
    var bytes = new byte[vectors.Length * sizeof(float)];
    Buffer.BlockCopy(vectors, 0, bytes, 0, bytes.Length);
    var manifest = JsonSerializer.Serialize(new
    {
      schemaVersion = 1,
      corpusVersion = "test",
      model = "Xenova/all-MiniLM-L6-v2",
      modelRevision = "751bff37182d3f1213fa05d7196b954e230abad9",
      dimensions = 384,
      count = failure == "count" ? 3 : 2,
      vectorFormat = "float32-le",
      chunksUrl = "chunks.json",
      vectorsUrl = "chunks.vec",
      chunksSha256 = failure == "checksum" ? new string('0', 64) : Convert.ToHexStringLower(SHA256.HashData(chunks)),
      vectorsSha256 = Convert.ToHexStringLower(SHA256.HashData(bytes)),
    });
    var manifestRequests = 0;
    var modelRequests = 0;
    await Context.RouteAsync("**/chat/corpus/manifest.json", route => Interlocked.Increment(ref manifestRequests) == 1
        ? route.FulfillAsync(new() { ContentType = "application/json", Body = manifest })
        : route.ContinueAsync());
    await Context.RouteAsync("**/chat/corpus/chunks.json", route =>
        route.FulfillAsync(new() { ContentType = "application/json", BodyBytes = chunks }));
    await Context.RouteAsync("**/chat/corpus/chunks.vec", route =>
        route.FulfillAsync(new() { ContentType = "application/octet-stream", BodyBytes = bytes }));
    await Context.RouteAsync("**/lib/transformers/transformers.min.js", route =>
        route.FulfillAsync(new()
        {
          ContentType = "text/javascript",
          Body = """
            export const env = {};
            export async function pipeline() {
              await fetch(new URL('model-request', import.meta.url));
              return async () => {};
            }
            """,
        }));
    await Context.RouteAsync("**/lib/transformers/model-request", route =>
    {
      Interlocked.Increment(ref modelRequests);
      return route.FulfillAsync(new() { Body = "model initialized" });
    });
    await Page.GotoAsync(new Uri(fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/").ToString());
    await Expect(Page.GetByRole(AriaRole.Alert)).ToContainTextAsync(expectedError);
    Assert.Equal(0, modelRequests);
    await Page.GetByRole(AriaRole.Button, new() { Name = "Retry loading article search" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Local article search is ready. Questions stay in this browser.");
    Assert.Equal(1, modelRequests);
    await Expect(Page.GetByRole(AriaRole.Alert)).ToHaveCountAsync(0);
  }
}
