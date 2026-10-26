using System.Text.Json;
using ILoveDotNet.DistributedTest.Utils;

namespace ILoveDotNet.DistributedTest;

public class LearningChatGenerationBenchmarkTests(AppFixture fixture) : PageTest
{
  public override BrowserNewContextOptions ContextOptions() => new() { IgnoreHTTPSErrors = true };

  [Fact(Explicit = true)]
  [Trait("Category", "RealModel")]
  public async Task ComparePinnedGenerationModelsAsync()
  {
    await Page.GotoAsync(fixture.App.GetEndpoint("ILoveDotNet-Web", "https").ToString());
    var results = await Page.EvaluateAsync<string>("""
        async () => {
          const { models, selectedModel } = await import(new URL('js/chat/generation-contract.js', document.baseURI));
          const adapter = await navigator.gpu?.requestAdapter();
          if (!adapter?.features.has('shader-f16')) throw new Error('A real shader-f16 WebGPU adapter is required for this benchmark.');
          const results = [];
          for (const model of models) {
            const worker = new Worker(new URL('js/chat/generation-worker.js', document.baseURI), { type: 'module' });
            let id = 0;
            function request(type, payload) {
              const requestId = ++id;
              const start = performance.now();
              let firstTokenMs;
              let draft = '';
              return new Promise((resolve, reject) => {
                const timer = setTimeout(() => { worker.terminate(); reject(new Error('Benchmark operation timed out after 10 minutes.')); }, 600000);
                const handler = ({data}) => {
                  if (data.requestId !== requestId) return;
                  if (data.update) {
                    if (data.update.phase === 'streaming') draft = data.update.text;
                    if (data.update.phase === 'streaming' && data.update.text && firstTokenMs === undefined) firstTokenMs = performance.now()-start;
                    return;
                  }
                  clearTimeout(timer);
                  worker.removeEventListener('message', handler);
                  resolve({ result: data.result, error: data.error, rawAnswer:data.rawAnswer, draft, elapsedMs: performance.now()-start, firstTokenMs });
                };
                worker.addEventListener('message', handler);
                worker.addEventListener('error', e => { clearTimeout(timer); reject(new Error(e.message)); }, {once:true});
                worker.postMessage({requestId, type, ...payload});
              });
            }
            try {
              const load = await request('load', {modelId:model.model_id});
              const cases = [];
              if (!load.error) {
                for (const question of ['What is sweeping?', 'Why does compaction update references?', 'Explain the role of the GC.', 'Which database index should I use?']) {
                  cases.push({question, ...await request('generate', {
                    question, history:'',
                    sources:[
                      {title:'Garbage collection',heading:'Sweep',contentUrl:'blogs/gc/',section:'How',excerpt:'After marking identifies live objects, sweeping reclaims the memory occupied by unmarked, unreachable objects. Sweeping does not move surviving objects.'},
                      {title:'Garbage collection',heading:'Compact',contentUrl:'blogs/gc/',section:'How',excerpt:'Compaction moves surviving objects together to close gaps in the heap. The runtime updates references to the moved objects. The GC automatically reclaims unreachable managed objects.'}
                    ]
                  })});
                }
              }
              results.push({model,load,cases});
            } finally {
              worker.terminate();
            }
          }
          return JSON.stringify({selectedModel:selectedModel.model_id,adapter:adapter.info,results});
        }
        """);
    var directory = Path.Combine(AppContext.BaseDirectory, "TestResults");
    Directory.CreateDirectory(directory);
    var path = Path.Combine(directory, "learning-chat-generation-benchmark.json");
    await File.WriteAllTextAsync(path, results, TestContext.Current.CancellationToken);
    using var report = JsonDocument.Parse(results);
    Assert.All(report.RootElement.GetProperty("results").EnumerateArray(), model =>
    {
      Assert.False(model.GetProperty("load").TryGetProperty("error", out var error), error.ToString());
      Assert.Equal(4, model.GetProperty("cases").GetArrayLength());
      if (model.GetProperty("model").GetProperty("model_id").GetString() == report.RootElement.GetProperty("selectedModel").GetString())
      {
        var cases = model.GetProperty("cases").EnumerateArray().ToArray();
        Assert.True(cases.Take(3).Count(result => result.TryGetProperty("result", out _)) >= 2,
          "The selected model must return verified excerpts for at least two of the three supported smoke questions.");
        Assert.All(cases.Take(3), result =>
        {
          if (result.TryGetProperty("error", out _))
          {
            Assert.Equal("INSUFFICIENT_EVIDENCE", result.GetProperty("rawAnswer").GetString());
          }
        });
        Assert.True(cases[3].TryGetProperty("error", out _), "Unsupported question must fall back to original sources.");
      }
    });
    Console.WriteLine($"Generation benchmark: {path}");
  }
}
