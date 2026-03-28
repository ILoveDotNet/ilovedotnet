using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpServerToolType]
public static class PatternGuideTool
{
    private static readonly IReadOnlyDictionary<string, string> Guides =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["lazy loading"] = """
                Lazy Loading Pattern in ilovedotnet:
                - All demo component assemblies are lazy-loaded via BlazorWebAssemblyLazyLoad in Web/Web.csproj
                - LazyLoaderService.cs (CommonComponents/Services/) does path-keyword matching in OnNavigateAsync
                - Seeded with AppState assembly; BaseComponents is ALWAYS loaded first for any /blogs/ path
                - LazyLoaderService.PreloadAsync() is called in Web/Program.cs before host.RunAsync()
                - Both App.razor (Web) and Routes.razor (MAUI) wire up OnNavigateAsync

                Example entry in LazyLoaderService.cs (keep alphabetical):
                  if (path.Contains("{topic-slug}", StringComparison.OrdinalIgnoreCase))
                  {
                      var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["{Topic}DemoComponents.wasm"]);
                      AdditionalAssemblies.AddRange(assemblies);
                  }

                Example entry in Web/Web.csproj (keep alphabetical):
                  <BlazorWebAssemblyLazyLoad Include="{Topic}DemoComponents.wasm" />
                """,

            ["content structure"] = """
                Blog Content Structure in ilovedotnet:
                - Required sections: What → Why → How → Summary
                - Top of every Razor page:
                    @page "/blogs/{slug}"
                    @using BaseComponents      ← always add this even with _Imports.razor
                    @inherits BasePage
                - Outer wrapper: <Content FileName=@nameof(YourPageClass)>
                - Section components: <ContentBody>, <What>, <Why>, <How>, <Summary>
                - Important terms: wrap in <Highlight>term</Highlight>
                - Code samples: <CodeSnippet CssClass="language-csharp">…escaped HTML…</CodeSnippet>
                  * Escape < as &lt;  and > as &gt; inside CodeSnippet
                - Live demos: <DemoSnippet> wrapping your interactive component
                """,

            ["component"] = """
                Blazor Component Patterns in ilovedotnet:
                - Content pages inherit from BasePage (in BaseComponents)
                - Layout via Content component (BaseComponents/Content.razor)
                - Images: dynamic loading pattern with _imageSource variable
                - Each RCL namespace matches its folder name
                - _Imports.razor in each RCL for shared usings
                - DemoSnippet shows live demos alongside CodeSnippet
                - Prefer @using BaseComponents at top of each file (in addition to _Imports.razor)
                """,

            ["routing"] = """
                Routing in ilovedotnet:
                - Every blog page: @page "/blogs/{slug}"
                - ContentMetaData.ContentUrl = "blogs/{slug}"
                - ContentMetaData.Slug must match the URL segment exactly
                - Router lives in App.razor (Web) and Routes.razor (MAUI)
                - Lazy loading is triggered by OnNavigateAsync based on path keywords
                - MAUI and Web share the same route patterns via BlazorWebView
                """,

            ["metadata"] = """
                ContentMetaData fields (SharedModels/ContentMetaData.cs):
                  Order       sbyte      – display order within learning path
                  Title       string
                  Description string     – used for SEO
                  Author      string
                  Slug        string     – URL segment (e.g. "my-blazor-demo")
                  PosterUrl   string     – "image/blogs/{channel}/{slug}.webp"
                  ThumbnailUrl string    – same as PosterUrl
                  ContentUrl  string     – "blogs/{slug}"
                  IconUrl     string     – "image/icons/{channel}.webp"
                  Channel     string     – learning path group e.g. "Blazor", "MCP"
                  Type        string     – same value as Channel
                  CreatedOn   DateTime   – UTC
                  ModifiedOn  DateTime   – UTC; future date = scheduled (hidden until then)
                  Keywords    List<string>
                  VideoUrl    string?    – optional YouTube link
                """,

            ["rcl"] = """
                Razor Class Library (RCL) conventions in ilovedotnet:
                - 28+ topic RCLs: each references BaseComponents + SharedComponents
                - ASP.NET Core package version: 10.0.3 (match exactly in new projects)
                - Standard _Imports.razor:
                    @using Microsoft.AspNetCore.Components.Routing
                    @using Microsoft.AspNetCore.Components.Web
                    @using BaseComponents
                    @using SharedComponents
                    @using SharedModels
                - New RCL checklist:
                    1. dotnet new razorclasslib → add to solution → add project refs
                    2. Add to Web.csproj AND MAUI.csproj
                    3. Add <BlazorWebAssemblyLazyLoad> in Web.csproj (alphabetical)
                    4. Add if-block in LazyLoaderService.cs (alphabetical)
                    5. Add {Topic}LearningPath.cs in SharedModels
                    6. Register in TableOfContents.cs constructor (alphabetical)
                    7. Update SEO keywords in all CommonComponents/Pages/*.razor
                """,
        };

    [McpServerTool]
    [Description(
        "Get coding conventions and patterns for ilovedotnet. " +
        "Topics: 'lazy loading', 'content structure', 'component', 'routing', 'metadata', 'rcl'.")]
    public static string GetPatternGuide(
        [Description("Topic name. Options: lazy loading | content structure | component | routing | metadata | rcl")]
        string topic)
    {
        if (Guides.TryGetValue(topic, out var exact)) return exact;

        var partial = Guides.FirstOrDefault(kv =>
            kv.Key.Contains(topic, StringComparison.OrdinalIgnoreCase) ||
            topic.Contains(kv.Key, StringComparison.OrdinalIgnoreCase));

        if (partial.Value is not null) return partial.Value;

        return $"No pattern guide for '{topic}'. Available topics: {string.Join(", ", Guides.Keys)}";
    }
}
