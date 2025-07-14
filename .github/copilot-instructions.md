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
3. **Placement**: Create in matching `{Topic}DemoComponents` project
4. **Assets**: Store images in `wwwroot/image/blogs/{topic}/{slug}/`
5. **Code**: Escape HTML characters (`<` as `&lt;`, `>` as `&gt;`)

When adding new features, follow the established RCL pattern and ensure both Web and MAUI compatibility.
