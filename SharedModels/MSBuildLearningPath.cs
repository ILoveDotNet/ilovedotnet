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
          Title = "Automating Git Hook Setup in .NET Projects with MSBuild",
          Description = "In this post I will introduce how to automate setting up git hooks using MSBuild in dotnet apps. All with live working demo.",
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
