using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpServerToolType]
public static class ArchitectureRulesTool
{
    [McpServerTool]
    [Description(
        "Get the complete architecture overview, project structure, and key conventions of " +
        "ilovedotnet. Read this before making structural changes to the codebase.")]
    public static string GetArchitectureRules() => """
        ## ilovedotnet Architecture

        ### Stack
        - Blazor WebAssembly + .NET MAUI hybrid learning platform
        - Target: net10.0, LangVersion: latest, Nullable enabled, ImplicitUsings on
        - Dual entry points:
            * Web  → Blazor WASM SPA        (Web/Program.cs)
            * MAUI → Native + BlazorWebView (MAUI/MauiProgram.cs)

        ### Package Versions
        - ASP.NET Core packages: 10.0.3
          Always match this version when adding new <PackageReference> entries.

        ### Project Layout
        - 28+ Razor Class Libraries (RCLs), each covering one topic
        - Core RCLs always loaded:  BaseComponents, CommonComponents, SharedComponents, SharedModels
        - Topic RCLs lazy-loaded:   AIDemoComponents, BlazorDemoComponents, LINQDemoComponents, …
        - All topic assemblies referenced in Web.csproj + MAUI.csproj

        ### Adding Content – Required Steps
        1. Add ContentMetaData entry to SharedModels/{Topic}LearningPath.cs
        2. Register in SharedModels/TableOfContents.cs constructor (alphabetical)
        3. Increment _fullContents initial capacity in TableOfContents.cs by 1
        4. Create the .razor page in {Topic}DemoComponents/
           - @page "/blogs/{slug}"
           - @using BaseComponents   ← required in every file
           - @inherits BasePage
        5. Store images under wwwroot/image/blogs/{topic}/{slug}/

        ### Scheduling Content
        - Set ModifiedOn to a future date to hide until that date
        - TableOfContents.Contents filters: content.ModifiedOn.Date <= DateTime.Today

        ### URL Conventions
        - ContentUrl  = "blogs/{slug}"
        - PosterUrl   = "image/blogs/{channel}/{slug}.webp"
        - IconUrl     = "image/icons/{channel}.webp"

        ### Performance
        - WasmBuildNative=true; globalization and timezone data disabled
        - Pre-rendering via BlazorWasmPreRendering.Build (WebAssemblyPrerendered mode)
        - Dynamic image loading pattern (_imageSource variable)

        ### Security (OWASP-aligned)
        - ImageRandomization=true, HighEntropyVA=true, NXCompat=true (in Directory.Build.props)
        - NugetAudit=true at critical level

        ### Build / Dev Commands
        - Lint:    dotnet format --verbosity quiet whitespace --folder
        - Build:   dotnet build Web/Web.csproj
        - Watch:   dotnet watch run --project Web/Web.csproj
        - Publish: dotnet publish Web/Web.csproj
        - Test:    dotnet test UITests/UITests.csproj

        ### Key Files
        - Web/Program.cs                              – DI, Serilog, PreloadAsync()
        - CommonComponents/Services/LazyLoaderService.cs – assembly loading + path map
        - SharedModels/TableOfContents.cs             – all learning paths, capacity
        - Web/Web.csproj                              – lazy-load config, package versions
        - .github/copilot-instructions.md             – full Copilot instructions
        - copilot/blog-generation-guidelines.md       – blog writing standards
        """;
}
