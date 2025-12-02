# GitHub Copilot Instructions for I ❤️ .NET

## Architecture Overview
This is a **Blazor WebAssembly + .NET MAUI hybrid learning platform** with dual entry points:
- **Web**: Blazor WASM SPA at `/Web/Program.cs`
- **MAUI**: Native shell with BlazorWebView at `/MAUI/MauiProgram.cs`

## Component Architecture
**Modular RCL Design**: 20+ Razor Class Libraries organized by topic:
- **Core**: `BaseComponents`, `CommonComponents`, `SharedComponents`, `SharedModels`
- **Topics**: `BlazorDemoComponents`, `LINQDemoComponents`, `DesignPatternDemoComponents`, etc.
- **Lazy Loading**: All demo components are lazy-loaded via `BlazorWebAssemblyLazyLoad` entries in `Web/Web.csproj`

## Key Patterns & Conventions

### Lazy Loading Implementation
- **Configuration**: `<BlazorWebAssemblyLazyLoad Include="ComponentName.wasm" />` in Web.csproj
- **Service**: `LazyLoaderService` handles dynamic assembly loading based on URL patterns
- **Router**: Both `Web/App.razor` and `MAUI/Components/Routes.razor` use `OnNavigateAsync` for lazy loading

### Content Management
- **Metadata**: `SharedModels/BlazorLearningPath.cs` contains structured learning content
- **Pattern**: `ContentMetaData` objects with Order, Title, Description, Slug, ContentUrl
- **Routing**: Blog content follows `/blogs/{slug}` pattern with corresponding Razor pages

### Component Inheritance
- **Base**: Most content pages inherit from `BasePage` class
- **Layout**: Uses `Content` component wrapper with `ContentBody`, `What`, `Why`, `How` sections
- **Demos**: `DemoSnippet` for live examples, `GithubGistSnippet` for code samples

### Blog Content Structure
- **Required Sections**: All blogs follow `What`, `Why`, `How`, `Summary` pattern
- **Page Declaration**: `@page "/blogs/{slug}"` and `@inherits BasePage`
- **Content Wrapper**: `<Content FileName=@nameof(ActualFileName) UseNewTableOfContentsMenu=true>`
- **Highlighting**: Use `<ContentHighlight>` for important terms and concepts
- **Code Samples**: Wrap in `<CodeSnippet CssClass="language-{type}">` with proper escaping

### Development Workflow
- **Build**: `dotnet build Web/Web.csproj`
- **Watch**: `dotnet watch run --project Web/Web.csproj`
- **Publish**: `dotnet publish Web/Web.csproj`

## Project-Specific Conventions
- **Namespaces**: Each RCL has its own namespace matching folder name
- **Imports**: Use `_Imports.razor` for shared using statements
- **Performance**: Dynamic image loading pattern (`_imageSource` variable pattern)
- **SEO**: Pre-rendering enabled via `BlazorWasmPreRendering.Build` package
- **Blog Placement**: Create blogs in appropriate `{Category}DemoComponents` project
- **Asset Organization**: Images in `wwwroot/image/blogs/{category}/{slug}/`

## Integration Points
- **MAUI Integration**: Shared components work in both Web and MAUI contexts via BlazorWebView
- **Analytics**: Blazor Analytics integration across both platforms
- **Logging**: Serilog for structured logging with browser console output
- **Testing**: XUnit for unit tests, Playwright for E2E tests
- **Documentation**: Mermaid diagrams for architecture visualization

## Key Files to Check First
- `Web/Program.cs` - Main configuration and service registration
- `CommonComponents/Services/LazyLoaderService.cs` - Assembly loading logic
- `SharedModels/BlazorLearningPath.cs` - Content structure and metadata
- `Web/Web.csproj` - Lazy loading configuration and package references
- `copilot/blog-generation-guidelines.md` - Complete blog writing standards

## Content Creation Guidelines
When creating new blog content:
1. **Structure**: Follow What-Why-How-Summary sections
2. **Metadata**: Add entry to appropriate `{Topic}LearningPath.cs`
3. **Table of Contents**: Update the total count in `SharedModels/TableOfContents.cs` by incrementing `_fullContents` capacity by 1 for each new article
4. **Placement**: Create in matching `{Topic}DemoComponents` project
5. **Assets**: Store images in `wwwroot/image/blogs/{topic}/{slug}/`
6. **Code**: Escape HTML characters (`<` as `&lt;`, `>` as `&gt;`)

When adding new features, follow the established RCL pattern and ensure both Web and MAUI compatibility.

## Creating a New Topic/Demo Components Project

When creating a new `{Topic}DemoComponents` Razor Class Library:

### Step 1: Create the RCL Project
```bash
dotnet new razorclasslib -n {Topic}DemoComponents -o {Topic}DemoComponents
```

### Step 2: Add to Solution
```bash
dotnet sln ILoveDotNet.slnx add {Topic}DemoComponents/{Topic}DemoComponents.csproj
```

### Step 3: Add Required Project References
```bash
# Add BaseComponents reference
dotnet add {Topic}DemoComponents/{Topic}DemoComponents.csproj reference BaseComponents/BaseComponents.csproj

# Add SharedComponents reference
dotnet add {Topic}DemoComponents/{Topic}DemoComponents.csproj reference SharedComponents/SharedComponents.csproj
```

### Step 4: Add to Web and MAUI Projects
```bash
# Add to Web project
dotnet add Web/Web.csproj reference {Topic}DemoComponents/{Topic}DemoComponents.csproj

# Add to MAUI project
dotnet add MAUI/MAUI.csproj reference {Topic}DemoComponents/{Topic}DemoComponents.csproj
```

### Step 5: Configure Lazy Loading in Web.csproj
Add the following to the `<ItemGroup>` containing `BlazorWebAssemblyLazyLoad` entries (maintain alphabetical order):
```xml
<BlazorWebAssemblyLazyLoad Include="{Topic}DemoComponents.wasm" />
```

### Step 6: Update LazyLoaderService.cs
Add lazy loading logic in `CommonComponents/Services/LazyLoaderService.cs` in the `OnNavigateAsync` method (maintain alphabetical order):
```csharp
if (path.Contains("{topic-slug}", StringComparison.OrdinalIgnoreCase))
{
  var assemblies = await lazyAssemblyLoader.LoadAssembliesAsync(["{Topic}DemoComponents.wasm"]);
  AdditionalAssemblies.AddRange(assemblies);
}
```

### Step 7: Update Project File
Replace the generated `.csproj` content with the standard pattern:
```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">

  <ItemGroup>
    <SupportedPlatform Include="browser" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="10.0.0" />
  </ItemGroup>

  <ItemGroup>
  	<ProjectReference Include="..\BaseComponents\BaseComponents.csproj" />
  	<ProjectReference Include="..\SharedComponents\SharedComponents.csproj" />
  </ItemGroup>

  <Target Name="GenerateSitemapXml" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)'!='Release'">
    <PropertyGroup>
      <SitemapXmlFilePath>$([System.IO.Path]::Combine('wwwroot', 'sitemap-blog-{topic}.xml'))</SitemapXmlFilePath>
    </PropertyGroup>

    <Exec Command="dotnet run --project &quot;$(ProjectDir)../SitemapGenerator&quot; -p BuildScope=$(ProjectName) -o &quot;$(ProjectDir)$(SitemapXmlFilePath)&quot; --channel &quot;{Topic}Channel&quot;" />

    <ItemGroup>
      <Content Remove="$(SitemapXmlFilePath)" />
      <Content Include="$(SitemapXmlFilePath)" />
    </ItemGroup>
  </Target>

  <Target Name="GeneratePoster" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)'!='Release'">
    <PropertyGroup>
      <PosterOutputPath>$([System.IO.Path]::Combine('wwwroot', 'image', 'blogs', '{topic}'))</PosterOutputPath>
    </PropertyGroup>

    <Exec Command="dotnet run --project &quot;$(ProjectDir)../PosterGenerator&quot; -p BuildScope=$(ProjectName) -o &quot;$(ProjectDir)$(PosterOutputPath)&quot; --channel &quot;{Topic}Channel&quot;" />

    <ItemGroup>
      <Content Remove="$(PosterOutputPath)\**\*" />
      <Content Include="$(PosterOutputPath)\**\*" />
    </ItemGroup>
  </Target>

</Project>
```

### Step 8: Update _Imports.razor
Replace the generated `_Imports.razor` content with:
```razor
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using BaseComponents
@using SharedComponents
@using SharedModels
```

### Step 9: Clean Up Template Files
Remove default template files that are not needed:
```bash
cd {Topic}DemoComponents
rm -f ExampleJsInterop.cs Component1.razor Component1.razor.css wwwroot/exampleJsInterop.js wwwroot/background.png
rm -rf wwwroot/image
```

### Step 10: Update SEO Keywords in CommonComponents/Pages
Add the new topic to the keywords meta tag in all pages within `CommonComponents/Pages`:
- About.razor
- Analytics.razor
- Author.razor
- Career.razor
- Channel.razor
- Disclaimer.razor
- Index.razor
- LearningPath.razor
- Privacy.razor

Add `{Topic}` to the `Meta Property="keywords"` Content attribute in each file (maintain alphabetical order).

### Step 11: Create Learning Path and Register in TableOfContents
Create a new learning path file in `SharedModels/{Topic}LearningPath.cs`:
```csharp
namespace SharedModels;

public class {Topic}LearningPath
{
  public readonly List<ContentMetaData> FullContents = new(0);

  public {Topic}LearningPath()
  {
    FullContents =
    [
    ];
  }
}
```

Add the learning path to `SharedModels/TableOfContents.cs` constructor (maintain alphabetical order):
```csharp
_fullContents.AddRange(new {Topic}LearningPath().FullContents);
```

### Step 12: Verify Build
```bash
# Build SharedModels to verify learning path integration
dotnet build SharedModels/SharedModels.csproj

# Build the new project
dotnet build {Topic}DemoComponents/{Topic}DemoComponents.csproj

# Build the Web project to ensure integration works
dotnet build Web/Web.csproj
```

### Notes:
- Replace `{Topic}` with the actual topic name (e.g., `JSON`, `Security`, `LINQ`)
- Replace `{topic}` with lowercase version for file paths (e.g., `json`, `security`, `linq`)
- Replace `{topic-slug}` with the URL path segment (e.g., `json`, `security`, `linq`)
- Replace `{Topic}Channel` with the appropriate channel name for sitemap/poster generation
- Maintain alphabetical order when adding entries to Web.csproj, LazyLoaderService.cs, keywords meta tags, and TableOfContents.cs
- Ensure package versions match the rest of the solution (currently `10.0.0` for ASP.NET Core packages)
- Initialize FullContents with capacity 0 for empty learning paths; increase as content is added

## Lint, Build and Test

- **Lint:** `dotnet format --verbosity quiet whitespace --folder`
- **Build:** `dotnet build ILoveDotNet.slnx`
- **Test:** `dotnet test`. To run a single test, use `--filter Name=<test_name>`.
  - You should rarely need to run all tests except after larger changes, usually running the specific test class or method you are working on is sufficient.
