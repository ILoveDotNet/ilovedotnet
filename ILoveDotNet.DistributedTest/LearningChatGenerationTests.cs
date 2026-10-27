using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatGenerationTests(AppFixture fixture) : PageTest
{
  public override BrowserNewContextOptions ContextOptions() => new() { IgnoreHTTPSErrors = true };

  [Theory]
  [InlineData("valid")]
  [InlineData("invalid")]
  [InlineData("changed")]
  [InlineData("slow")]
  [InlineData("refused")]
  [InlineData("truncated")]
  [InlineData("skipped")]
  [InlineData("error")]
  [InlineData("html")]
  public async Task StreamedAnswersAreValidatedOrReplacedWithSourcesAsync(string mode)
  {
    await StubSearchAsync(mode == "skipped");
    var runtimeRequests = 0;
    Page.Request += (_, request) =>
    {
      if (request.Url.Contains("/lib/webllm/", StringComparison.Ordinal))
      {
        Interlocked.Increment(ref runtimeRequests);
      }
    };
    await Context.AddInitScriptAsync("""
      Object.defineProperty(navigator, 'gpu', {value: {
        requestAdapter: async () => ({features: new Set(['shader-f16']), limits: {maxStorageBufferBindingSize:134217728}})
      }});
      localStorage.setItem('ilovedotnet-answer-consent:https://huggingface.co/mlc-ai/SmolLM2-360M-Instruct-q4f16_1-MLC/resolve/3a622fd89e0216e8bb10c410c007c786baa8a033/', 'accepted');
      """);
    await Context.RouteAsync("**/lib/webllm/webllm.js", route => route.FulfillAsync(new()
    {
      ContentType = "text/javascript",
      Body = $$$$"""
        export async function CreateMLCEngine(id, options) {
          options.initProgressCallback({text:'Loading test model',progress:1});
          return {
            resetChat:async()=>{}, unload:async()=>{},
            chat:{completions:{create:async function*(request) {
              if (request.response_format) throw new Error('Composed answers must not use excerpt grammar');
              if ('{{{{mode}}}}' === 'error') throw new Error('Test inference failure');
              if ('{{{{mode}}}}' === 'refused' || '{{{{mode}}}}' === 'truncated') {
                yield {choices:[{delta:{content:'INSUFFICIENT_EVIDENCE'},
                  finish_reason:'{{{{(mode == "truncated" ? "length" : "stop")}}}}'}]};
                return;
              }
              yield {choices:[{delta:{content:'<think>\n\n</think>\nSweeping reclaims unreachable objects. '}}]};
              await new Promise(resolve=>setTimeout(resolve, {{{{(mode == "slow" ? 60000 : 1500)}}}}));
              yield {choices:[{delta:{content:'{{{{(mode == "changed" ? "This is naturally paraphrased. " : mode == "html" ? "<script>alert(1)</script> " : "")}}}}[{{{{(mode == "invalid" ? 9 : mode == "skipped" ? 2 : 1)}}}}]'},finish_reason:'stop'}]};
            }}}
          };
        }
        export async function deleteModelAllInfoInCache() {}
        """
    }));
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/");
    await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Local article search is ready");
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" })).ToBeVisibleAsync();
    Assert.Equal(0, runtimeRequests);
    await Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" }).ClickAsync();
    var modelPanel = Page.GetByRole(AriaRole.Complementary, new() { Name = "Local answer model" });
    await Expect(modelPanel).ToContainTextAsync("Local answers ready");
    await Expect(modelPanel).ToContainTextAsync("Active model: Qwen3-4B-q4f16_1-MLC");
    await Page.GetByRole(AriaRole.Textbox).FillAsync(mode == "refused" || mode == "truncated"
      ? "what is garbage collection" : "What is sweeping?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    if (mode != "refused" && mode != "truncated" && mode != "error")
    {
      await Expect(Page.Locator("article p").Filter(new() { HasText = "Sweeping reclaims unreachable objects." })).ToBeVisibleAsync();
    }
    if (mode == "slow")
    {
      await Page.GetByRole(AriaRole.Button, new() { Name = "Stop", Exact = true }).ClickAsync();
      await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search stopped.");
    }
    else
    {
      await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.");
    }
    if (mode == "valid" || mode == "skipped" || mode == "changed")
    {
      await Expect(Page.GetByRole(AriaRole.Link, new() { Name = mode == "skipped" ? "[2]" : "[1]", Exact = true }))
        .ToHaveAttributeAsync("href", new System.Text.RegularExpressions.Regex(@"/blogs/gc/#how$"));
      await Expect(Page.GetByText("AI-generated explanation.", new() { Exact = false })).ToBeVisibleAsync();
      await Expect(Page.Locator("script").Filter(new() { HasText = "alert(1)" })).ToHaveCountAsync(0);
    }
    else
    {
      await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "[9]", Exact = true })).ToHaveCountAsync(0);
      await Expect(Page.GetByText("These are search results", new() { Exact = false })).ToBeVisibleAsync();
      if (mode == "invalid")
      {
        await Expect(modelPanel).ToContainTextAsync("invalid citations");
      }
      else if (mode == "refused")
      {
        await Expect(modelPanel).ToContainTextAsync("could not answer from the supplied passages");
        await Expect(modelPanel).Not.ToContainTextAsync("Local answer unavailable");
        await Expect(Page.GetByText("AI-generated explanation.", new() { Exact = false })).ToHaveCountAsync(0);
      }
      else if (mode == "truncated")
      {
        await Expect(modelPanel).ToContainTextAsync("Local answer unavailable");
      }
      await Expect(modelPanel).Not.ToContainTextAsync("Error:");
      await Expect(modelPanel).Not.ToContainTextAsync("generation.");
    }
    await Expect(Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" })).ToBeVisibleAsync();
    await Page.GetByRole(AriaRole.Button, new() { Name = "New conversation" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" })).ToHaveCountAsync(0);
    if (mode == "valid")
    {
      await Page.ReloadAsync();
      await Expect(modelPanel).ToContainTextAsync("Local answers ready");
      await Expect(modelPanel).ToContainTextAsync("Active model: Qwen3-4B-q4f16_1-MLC");
      await Page.GetByRole(AriaRole.Button, new() { Name = "Clear answer model cache" }).ClickAsync();
      await Expect(modelPanel).ToContainTextAsync("Answer model cache cleared");
      await Expect(modelPanel).Not.ToContainTextAsync("Active model:");
      await Page.ReloadAsync();
      await Expect(modelPanel).ToContainTextAsync("Optional local answers");
      await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" })).ToBeVisibleAsync();
      foreach (var dark in new[] { false, true })
      {
        await Page.EvaluateAsync("dark => document.documentElement.classList.toggle('dark', dark)", dark);
        var contrasts = await modelPanel.EvaluateAsync<double[]>("""
          panel => {
            const canvas = document.createElement('canvas');
            canvas.width = canvas.height = 1;
            const context = canvas.getContext('2d', {willReadFrequently:true});
            function luminance() {
              const channels = Array.from(context.getImageData(0,0,1,1).data).slice(0,3)
                .map(value => value / 255)
                .map(value => value <= 0.04045 ? value / 12.92 : ((value + 0.055) / 1.055) ** 2.4);
              return channels[0]*0.2126 + channels[1]*0.7152 + channels[2]*0.0722;
            }
            return Array.from(panel.querySelectorAll('p, button')).map(element => {
              const ancestors = [];
              for (let node = element; node; node = node.parentElement) ancestors.unshift(node);
              context.fillStyle = 'white';
              context.fillRect(0,0,1,1);
              for (const node of ancestors) {
                context.fillStyle = getComputedStyle(node).backgroundColor;
                context.fillRect(0,0,1,1);
              }
              const background = luminance();
              context.fillStyle = getComputedStyle(element).color;
              context.fillRect(0,0,1,1);
              const foreground = luminance();
              return (Math.max(background,foreground)+0.05) / (Math.min(background,foreground)+0.05);
            });
          }
          """);
        Assert.All(contrasts, contrast => Assert.True(contrast >= 4.5,
          $"Model notice and controls must meet 4.5:1 text contrast (dark={dark}, actual={contrast:F2})."));
      }
      await Page.SetViewportSizeAsync(390, 844);
      var input = await Page.GetByRole(AriaRole.Textbox).BoundingBoxAsync();
      Assert.NotNull(input);
      Assert.True(input.Y + input.Height <= 844, "Mobile input must stay inside the viewport.");
    }
  }

  [Fact]
  public async Task ModelLoadFailureCanRetryWithoutBreakingArticleSearchAsync()
  {
    await StubSearchAsync();
    await Context.AddInitScriptAsync("""
      Object.defineProperty(navigator, 'gpu', {value: {
        requestAdapter: async () => ({features: new Set(['shader-f16']), limits: {maxStorageBufferBindingSize:134217728}})
      }});
      """);
    await Context.RouteAsync("**/lib/webllm/webllm.js", route => route.FulfillAsync(new()
    {
      ContentType = "text/javascript",
      Body = """
        let attempts = 0;
        export async function CreateMLCEngine() {
          if (++attempts === 1) throw new Error('Test model download failed');
          return {unload:async()=>{}};
        }
        export async function deleteModelAllInfoInCache() {}
        """
    }));
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" }).ClickAsync();
    var modelPanel = Page.GetByRole(AriaRole.Complementary, new() { Name = "Local answer model" });
    await Expect(modelPanel).ToContainTextAsync("Test model download failed");
    await Page.GetByRole(AriaRole.Textbox).FillAsync("What is sweeping?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.");
    await Expect(Page.GetByText("These are search results", new() { Exact = false })).ToBeVisibleAsync();
    await Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" }).ClickAsync();
    await Expect(modelPanel).ToContainTextAsync("Local answers ready");
    await Expect(Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" })).ToBeVisibleAsync();
  }

  [Fact]
  public async Task UnsupportedGpuKeepsSearchAndNeverDownloadsGenerationRuntimeAsync()
  {
    await StubSearchAsync();
    await Context.AddInitScriptAsync("Object.defineProperty(navigator, 'gpu', {value: undefined});");
    var generationDownloads = 0;
    Page.Request += (_, request) =>
    {
      if (request.Url.Contains("/lib/webllm/", StringComparison.Ordinal))
      {
        Interlocked.Increment(ref generationDownloads);
      }
    };
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/");
    await Expect(Page.GetByRole(AriaRole.Complementary, new() { Name = "Local answer model" })).ToContainTextAsync("does not expose WebGPU");
    await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" })).ToHaveCountAsync(0);
    await Page.GetByRole(AriaRole.Textbox).FillAsync("What is sweeping?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" })).ToBeVisibleAsync();
    Assert.Equal(0, generationDownloads);
  }

  [Theory]
  [InlineData(false)]
  [InlineData(true)]
  public async Task ArticleAndCitationNavigationCleansUpChatWithoutRenderingErrorsAsync(bool citation)
  {
    const string slug = "garbage-collection-fundamentals-in-dotnet";
    await StubSearchAsync(contentUrl: $"blogs/{slug}/");
    var errors = new System.Collections.Concurrent.ConcurrentQueue<string>();
    Page.PageError += (_, error) => errors.Enqueue(error);
    Page.Console += (_, message) =>
    {
      if (message.Type == "error" && (message.Text.Contains("removeEventListener", StringComparison.Ordinal)
        || message.Text.Contains("Unhandled exception rendering component", StringComparison.Ordinal)))
      {
        errors.Enqueue(message.Text);
      }
    };
    await Context.AddInitScriptAsync("""
      Object.defineProperty(navigator, 'gpu', {value: {
        requestAdapter: async () => ({features: new Set(['shader-f16']), limits: {maxStorageBufferBindingSize:134217728}})
      }});
      """);
    await Context.RouteAsync("**/lib/webllm/webllm.js", route => route.FulfillAsync(new()
    {
      ContentType = "text/javascript",
      Body = """
        export async function CreateMLCEngine() {
          return {
            resetChat:async()=>{}, unload:async()=>{},
            chat:{completions:{create:async function*() {
              yield {choices:[{delta:{content:'Sweeping reclaims unreachable objects. [1]'},finish_reason:'stop'}]};
            }}}
          };
        }
        export async function deleteModelAllInfoInCache() {}
        """
    }));
    await SeoTestHelpers.GotoSuccessfulPageAsync(Page, fixture.App.GetEndpoint("ILoveDotNet-Web", "https"), "chat/");
    await Expect(Page.GetByRole(AriaRole.Status)).ToContainTextAsync("Local article search is ready");
    if (citation)
    {
      await Page.GetByRole(AriaRole.Button, new() { Name = "Download local answer model" }).ClickAsync();
      await Expect(Page.GetByRole(AriaRole.Complementary, new() { Name = "Local answer model" })).ToContainTextAsync("Local answers ready");
    }
    await Page.GetByRole(AriaRole.Textbox).FillAsync("What is sweeping?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.");
    var link = citation
      ? Page.GetByRole(AriaRole.Link, new() { Name = "[1]", Exact = true })
      : Page.GetByRole(AriaRole.List, new() { Name = "Retrieved article sources" }).GetByRole(AriaRole.Link);
    await link.ClickAsync();
    await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex($@"/blogs/{slug}/#how$"));
    await Expect(Page.Locator("#how")).ToBeAttachedAsync();
    await Expect(Page.Locator("#blazor-error-ui")).Not.ToBeVisibleAsync();
    await Page.Locator("nav a[href='/chat/']").ClickAsync();
    await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex(@"/chat/$"));
    await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Learn with AI", Exact = true })).ToBeVisibleAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Local article search is ready. Questions stay in this browser.");
    await Page.GetByRole(AriaRole.Textbox).FillAsync("What is sweeping?");
    await Page.GetByRole(AriaRole.Button, new() { Name = "Send question" }).ClickAsync();
    await Expect(Page.GetByRole(AriaRole.Status)).ToHaveTextAsync("Search complete.");
    Assert.Empty(errors);
  }

  private Task StubSearchAsync(bool leadingList = false, string contentUrl = "blogs/gc/")
  {
    Page.Console += (_, message) => Console.WriteLine($"Browser {message.Type}: {message.Text}");
    Page.PageError += (_, error) => Console.WriteLine($"Browser error: {error}");
    return Context.RouteAsync("**/js/chat/retrieval-worker.js", route => route.FulfillAsync(new()
    {
      ContentType = "text/javascript",
      Body = $$"""
      self.onmessage = ({data}) => {
        self.postMessage({requestId:data.requestId,result:data.type === 'initialize' ? true : [
        ...({{(leadingList ? "true" : "false")}} ? [{
        title:'Decision guide', heading:'List', section:'What', contentUrl:'blogs/decision/',
        excerpt:'Use a value object when: values are interchangeable Examples: Money, DateRange'
        }] : []), {
        title:'Garbage Collection', heading:'Sweep', section:'How', contentUrl:{{System.Text.Json.JsonSerializer.Serialize(contentUrl)}},
        excerpt:'Sweeping reclaims unreachable objects. Garbage collection automatically reclaims unreachable managed objects.'
        }]});
      };
      """
    }));
  }
}
