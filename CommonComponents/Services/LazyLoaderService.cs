using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CommonComponents.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.Extensions.Logging;

namespace CommonComponents.Services;

public class LazyLoaderService(
    LazyAssemblyLoader lazyAssemblyLoader,
    NavigationManager navigationManager,
    ILogger<LazyLoaderService> logger)
{
  public List<Assembly> AdditionalAssemblies { get; } = [typeof(AppState).Assembly];

  public async Task OnNavigateAsync(NavigationContext context) =>
      await OnNavigateAsync(context.Path.Trim('/'));

  public async Task PreloadAsync()
  {
    var uri = new Uri(navigationManager.Uri);
    await OnNavigateAsync(uri.LocalPath.Trim('/'));
  }

  [RequiresUnreferencedCode("The following members are used by lazyAssemblyLoader.LoadAsync")]
  public async Task OnNavigateAsync(string path)
  {
    try
    {
      if (path.Contains("blogs", StringComparison.OrdinalIgnoreCase))
      {
        await lazyAssemblyLoader.LoadAssembliesAsync(["BaseComponents.wasm"]);

        if (path.Contains("ai", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["AIDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("blazor", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["BlazorDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("cache", StringComparison.OrdinalIgnoreCase) || path.Contains("caching", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["CachingDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("database", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["DatabaseDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("ddd", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["DDDDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("dependency-injection", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["DependencyInjectionDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("design-pattern", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["DesignPatternDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("http-client", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["HTTPClientDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("json", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["JSONDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("linq", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["LINQDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("mlnet", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["MLNETDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("maui", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["MAUIDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("mcp", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["MCPDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("middleware", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["MiddlewareDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("msbuild", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["MSBuildDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("oops", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["OOPSDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("owasp", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["OWASPDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("python", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["PythonDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("regex", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["RegexDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("report", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["ReportDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("security", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["SecurityDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("signalr", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["SignalRDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("solid", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["SOLIDDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("testing", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["TestingDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("tdd", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["TDDDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }

        if (path.Contains("webapi", StringComparison.OrdinalIgnoreCase))
        {
          var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["WebAPIDemoComponents.wasm"]);
          AdditionalAssemblies.AddRange(assemblies);
        }
      }

      if (path.Contains("talks", StringComparison.OrdinalIgnoreCase))
      {
        await lazyAssemblyLoader.LoadAssembliesAsync(["BaseComponents.wasm"]);

        var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["TalkDemoComponents.wasm"]);
        AdditionalAssemblies.AddRange(assemblies);
      }
    }
    catch (Exception ex)
    {
      logger.LogError("Error: {Message}", ex.Message);
    }
  }
}
