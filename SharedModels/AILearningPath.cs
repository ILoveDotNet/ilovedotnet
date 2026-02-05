namespace SharedModels;

public class AILearningPath
{
  public readonly List<ContentMetaData> FullContents = new(7);

  public AILearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Using GitHub Copilot AI for Commit Message Generation",
          Description = "In this post I will teach you how to use GitHub Copilot for commit message generation. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "using-github-copilot-ai-for-commit-message-generation",
          PosterUrl = "image/blogs/ai/using-github-copilot-ai-for-commit-message-generation.webp",
          ThumbnailUrl = "image/blogs/ai/using-github-copilot-ai-for-commit-message-generation.webp",
          ContentUrl = "blogs/using-github-copilot-ai-for-commit-message-generation",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 6, 8, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 6, 8, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "Commit Message"]
        },
      new ContentMetaData
        {
          Order = 2,
          Title = "Using GitHub Copilot AI for Architecture Diagram Generation",
          Description = "Learn how to leverage GitHub Copilot to automatically generate and maintain beautiful architecture diagrams using Mermaid syntax.",
          Author = "Abdul Rahman",
          Slug = "using-github-copilot-ai-for-architecture-diagram-generation",
          PosterUrl = "image/blogs/ai/using-github-copilot-ai-for-architecture-diagram-generation.webp",
          ThumbnailUrl = "image/blogs/ai/using-github-copilot-ai-for-architecture-diagram-generation.webp",
          ContentUrl = "blogs/using-github-copilot-ai-for-architecture-diagram-generation",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 6, 15, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 6, 15, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "Architecture", "Mermaid", "Diagram"]
        },
      new ContentMetaData
        {
          Order = 3,
          Title = "Using GitHub Copilot AI for Documenting and Diagramming CI/CD Pipelines",
          Description = "Learn how to leverage GitHub Copilot to automatically generate and maintain beautiful diagrams using Mermaid syntax for ci cd pipelines.",
          Author = "Abdul Rahman",
          Slug = "using-github-copilot-ai-for-documenting-and-diagramming-ci-cd-pipelines",
          PosterUrl = "image/blogs/ai/using-github-copilot-ai-for-documenting-and-diagramming-ci-cd-pipelines.webp",
          ThumbnailUrl = "image/blogs/ai/using-github-copilot-ai-for-documenting-and-diagramming-ci-cd-pipelines.webp",
          ContentUrl = "blogs/using-github-copilot-ai-for-documenting-and-diagramming-ci-cd-pipelines",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 6, 22, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 6, 22, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "Documenting", "CI", "CD", "Pipelines", "Diagram"]
        },
      new ContentMetaData
        {
          Order = 4,
          Title = "Using GitHub Copilot AI for Navigating New Codebase",
          Description = "Learn how to leverage GitHub Copilot to navigate new codebase and understand the functionality.",
          Author = "Abdul Rahman",
          Slug = "using-github-copilot-ai-for-navigating-new-codebase",
          PosterUrl = "image/blogs/ai/using-github-copilot-ai-for-navigating-new-codebase.webp",
          ThumbnailUrl = "image/blogs/ai/using-github-copilot-ai-for-navigating-new-codebase.webp",
          ContentUrl = "blogs/using-github-copilot-ai-for-navigating-new-codebase",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 6, 29, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 6, 29, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "New Codebase", "Analysis"]
        },
      new ContentMetaData
        {
          Order = 5,
          Title = "Building AI Chat Applications in .NET with Microsoft.Extensions.AI",
          Description = "In this post I will teach you how to build production-ready AI chat applications in .NET using Microsoft.Extensions.AI and OllamaSharp, proving that AI development belongs in C# just as much as Python. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "building-ai-chat-applications-in-dotnet-with-microsoft-extensions-ai",
          PosterUrl = "image/blogs/ai/building-ai-chat-applications-in-dotnet-with-microsoft-extensions-ai.webp",
          ThumbnailUrl = "image/blogs/ai/building-ai-chat-applications-in-dotnet-with-microsoft-extensions-ai.webp",
          ContentUrl = "blogs/building-ai-chat-applications-in-dotnet-with-microsoft-extensions-ai",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 1, 18, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 1, 18, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Chat", "Microsoft.Extensions.AI", "OllamaSharp", "Ollama", "LLM", "Azure OpenAI"]
        },
      new ContentMetaData
        {
          Order = 6,
          Title = "Automate .NET Framework Upgrades with GitHub Copilot AI Agents",
          Description = "In this post I will teach you how to set up a GitHub Copilot AI Agent that automates .NET framework upgrades across your entire solution—from analyzing dependencies to updating CI/CD pipelines—all with a single natural language command.",
          Author = "Abdul Rahman",
          Slug = "automate-dotnet-framework-upgrades-with-github-copilot-ai-agents",
          PosterUrl = "image/blogs/ai/automate-dotnet-framework-upgrades-with-github-copilot-ai-agents.webp",
          ThumbnailUrl = "image/blogs/ai/automate-dotnet-framework-upgrades-with-github-copilot-ai-agents.webp",
          ContentUrl = "blogs/automate-dotnet-framework-upgrades-with-github-copilot-ai-agents",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 2, 8, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 2, 8, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "AI Agent", ".NET Upgrade", "Framework Migration", "Automation"]
        },
      new ContentMetaData
        {
          Order = 7,
          Title = "Automate NuGet Upgrades with GitHub Copilot AI Skills and Live Documentation Lookup",
          Description = "In this post I will teach you how to build a GitHub Copilot Skill that automates NuGet package upgrades with intelligent breaking change detection and live documentation lookup via Model Context Protocol (MCP).",
          Author = "Abdul Rahman",
          Slug = "automate-nuget-upgrades-with-github-copilot-ai-skills",
          PosterUrl = "image/blogs/ai/automate-nuget-upgrades-with-github-copilot-ai-skills.webp",
          ThumbnailUrl = "image/blogs/ai/automate-nuget-upgrades-with-github-copilot-ai-skills.webp",
          ContentUrl = "blogs/automate-nuget-upgrades-with-github-copilot-ai-skills",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 2, 15, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 2, 15, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "AI Skill", "NuGet", "Package Management", "MCP", "Breaking Changes"]
        }
    ];
  }
}
