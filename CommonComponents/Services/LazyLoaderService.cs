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
  private HashSet<string> _loadedAssemblies = [];
  public List<Assembly> AdditionalAssemblies { get; } = [typeof(AppState).Assembly];

  [RequiresUnreferencedCode("The following members are used by lazyAssemblyLoader.LoadAsync")]
  public async Task OnNavigateAsync(NavigationContext context) =>
      await OnNavigateAsync(context.Path.Trim('/'));

  [RequiresUnreferencedCode("The following members are used by lazyAssemblyLoader.LoadAsync")]
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
        await LoadAssembliesAsync("BaseComponents.wasm", addAdditionalAssemblies: false);

        if (path.Contains("ai", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("AIDemoComponents.wasm");
        }

        if (path.Contains("blazor", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("BlazorDemoComponents.wasm");
        }

        if (path.Contains("cache", StringComparison.OrdinalIgnoreCase) || path.Contains("caching", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("CachingDemoComponents.wasm");
        }

        if (path.Contains("database", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("DatabaseDemoComponents.wasm");
        }

        if (path.Contains("ddd", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("DDDDemoComponents.wasm");
        }

        if (path.Contains("dependency-injection", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("DependencyInjectionDemoComponents.wasm");
        }

        if (path.Contains("design-pattern", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("DesignPatternDemoComponents.wasm");
        }

        if (path.Contains("http-client", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("HTTPClientDemoComponents.wasm");
        }

        if (path.Contains("json", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("JSONDemoComponents.wasm");
        }

        if (path.Contains("linq", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("LINQDemoComponents.wasm");
        }

        if (path.Contains("logging", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("LoggingDemoComponents.wasm");
        }

        if (path.Contains("mlnet", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("MLNETDemoComponents.wasm");
        }

        if (path.Contains("maui", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("MAUIDemoComponents.wasm");
        }

        if (path.Contains("mcp", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("MCPDemoComponents.wasm");
        }

        if (path.Contains("middleware", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("MiddlewareDemoComponents.wasm");
        }

        if (path.Contains("msbuild", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("MSBuildDemoComponents.wasm");
        }

        if (path.Contains("nuget", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("NugetDemoComponents.wasm");
        }

        if (path.Contains("oops", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("OOPSDemoComponents.wasm");
        }

        if (path.Contains("owasp", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("OWASPDemoComponents.wasm");
        }

        if (path.Contains("python", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("PythonDemoComponents.wasm");
        }

        if (path.Contains("regex", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("RegexDemoComponents.wasm");
        }

        if (path.Contains("report", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("ReportDemoComponents.wasm");
        }

        if (path.Contains("security", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("SecurityDemoComponents.wasm");
        }

        if (path.Contains("signalr", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("SignalRDemoComponents.wasm");
        }

        if (path.Contains("solid", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("SOLIDDemoComponents.wasm");
        }

        if (path.Contains("testing", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("TestingDemoComponents.wasm");
        }

        if (path.Contains("tdd", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("TDDDemoComponents.wasm");
        }

        if (path.Contains("webapi", StringComparison.OrdinalIgnoreCase))
        {
          await LoadAssembliesAsync("WebAPIDemoComponents.wasm");
        }
      }

      if (path.Contains("talks", StringComparison.OrdinalIgnoreCase))
      {
        await LoadAssembliesAsync("BaseComponents.wasm");

        await LoadAssembliesAsync("TalkDemoComponents.wasm");
      }
    }
    catch (Exception ex)
    {
      logger.LogError("Error: {Message}", ex.Message);
    }
  }

  [RequiresUnreferencedCode("The following members are used by lazyAssemblyLoader.LoadAsync")]
  private async ValueTask LoadAssembliesAsync(string assemblyName, bool addAdditionalAssemblies = true)
  {
    if (_loadedAssemblies.Contains(assemblyName))
      return;

    _loadedAssemblies.Add(assemblyName);

    var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync([assemblyName]);
    if (addAdditionalAssemblies)
    {
      AdditionalAssemblies.AddRange(assemblies);
    }
  }
}
