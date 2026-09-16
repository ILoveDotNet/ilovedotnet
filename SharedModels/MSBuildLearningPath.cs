namespace SharedModels;

public class MSBuildLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public MSBuildLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "What Is a Git Hook? Automate Setup with .NET MSBuild",
          Description = "Learn what Git hooks are and how to automate pre-commit hook setup in .NET projects using MSBuild with a practical team-friendly example.",
          Author = "Abdul Rahman",
          Slug = "automating-git-hook-setup-in-dotnet-with-msbuild",
          PosterUrl = "image/blogs/msbuild/automating-git-hook-setup-in-dotnet-with-msbuild.webp",
          ThumbnailUrl = "image/blogs/msbuild/automating-git-hook-setup-in-dotnet-with-msbuild.webp",
          ContentUrl = "blogs/automating-git-hook-setup-in-dotnet-with-msbuild",
          IconUrl = "image/icons/msbuild.webp",
          Channel = "MSBuild",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 2, 2, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 2, 2, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Git Hook", "Pre-Commit"]
        }
    ];
  }
}
