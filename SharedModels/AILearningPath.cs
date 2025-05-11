namespace SharedModels;

public class AILearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

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
          PosterUrl = "image/blogs/maui/using-github-copilot-ai-for-commit-message-generation.webp",
          ThumbnailUrl = "image/blogs/maui/using-github-copilot-ai-for-commit-message-generation.webp",
          ContentUrl = "blogs/using-github-copilot-ai-for-commit-message-generation",
          IconUrl = "image/icons/ai.webp",
          Channel = "AI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 6, 8, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 6, 8, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Copilot", "GitHub", "Commit Message"]
        }
    ];
  }
}
