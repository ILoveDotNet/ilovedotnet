# Agent Instructions

> **Note:** This file serves as a reference for AI coding agents and tools that require instructions at the repository root.

For complete GitHub Copilot instructions and project documentation, see [`.github/copilot-instructions.md`](.github/copilot-instructions.md).

---

## MCP Knowledge Base Server

This repository includes a local MCP (Model Context Protocol) server that exposes ilovedotnet blog content and architectural conventions to AI coding assistants.

### Projects

| Project | Type | Purpose |
|---|---|---|
| `ContentExtractor/` | Console app | Parses all `*DemoComponents/**/*.razor` files → `MCPServer/Data/knowledge-base.json` |
| `MCPServer/` | ASP.NET Core | Serves the knowledge base over HTTP as MCP tools |

### Quick Start

```bash
# Step 1 – generate / refresh the knowledge base
dotnet run --project ContentExtractor/ContentExtractor.csproj

# Step 2 – start the MCP server  (keep running while using AI assistants)
dotnet run --project MCPServer/MCPServer.csproj -- --urls http://localhost:5100
```

The server listens at **`http://localhost:5100/mcp`** and is pre-configured in `.vscode/mcp.json` as `ilovedotnet-kb`.

### Available MCP Tools

| Tool | Signature | Description |
|---|---|---|
| `search_blogs` | `(query: string, maxResults: int = 5)` | Full-text search across all blog posts |
| `get_learning_path` | `(channel: string)` | All posts in a channel, newest first |
| `list_channels` | `()` | Lists every available learning-path channel |
| `get_pattern_guide` | `(topic: string)` | Coding conventions; topics: `lazy loading`, `content structure`, `component`, `routing`, `metadata`, `rcl` |
| `get_architecture_rules` | `()` | Full architecture overview, project structure, package versions, URL conventions |
| `get_test_guide` | `(type: string = "all")` | XUnit unit-test and Playwright E2E conventions; types: `unit`, `e2e`, `all` |

### Adding a New MCP Tool

1. Create a static class in `MCPServer/Tools/` decorated with `[McpServerToolType]`.
2. Add one or more static methods decorated with `[McpServerTool]`.
3. Inject services (e.g., `KnowledgeBaseService`) as method parameters — the framework resolves them from DI.
4. No registration needed; `WithToolsFromAssembly()` discovers tools automatically.

```csharp
[McpServerToolType]
public static class MyNewTool
{
    [McpServerTool(Name = "my_tool", Description = "Does something useful.")]
    public static string Execute(KnowledgeBaseService kb, [Description("Input")] string input)
    {
        // implementation
        return "result";
    }
}
```

### Refreshing the Knowledge Base

Run `ContentExtractor` any time new blog posts are added. The extractor:
- Scans all `*DemoComponents/**/*.razor` files for `@page "/blogs/{slug}"` routes
- Parses `<What>`, `<Why>`, `<How>`, and `<CodeSnippet>` sections via AngleSharp
- Looks up metadata from `SharedModels.TableOfContents`
- Writes to `MCPServer/Data/knowledge-base.json` (currently ~199 entries)

The CI pipeline (`build-blazor-app.yml`) runs the extractor automatically before the Blazor build.