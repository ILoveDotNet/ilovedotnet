namespace SharedModels;

public class AILearningPath
{
  public readonly List<ContentMetaData> FullContents = new(4);

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
        }
    ];
  }
}
