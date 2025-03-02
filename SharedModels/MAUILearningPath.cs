namespace SharedModels;

public class MAUILearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public MAUILearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Step by Step Setup Guide for .NET MAUI with Visual Studio Code",
          Description = "In this post I will teach you how to setup .NET MAUI with detailed steps using Visual Studio Code. All with live working demo.",
          Author = "Abdul Rahman",
          Slug = "step-by-step-setup-guide-for-dotnet-maui-with-visual-studio-code",
          PosterUrl = "image/blogs/maui/step-by-step-setup-guide-for-dotnet-maui-with-visual-studio-code.webp",
          ThumbnailUrl = "image/blogs/maui/step-by-step-setup-guide-for-dotnet-maui-with-visual-studio-code.webp",
          ContentUrl = "blogs/step-by-step-setup-guide-for-dotnet-maui-with-visual-studio-code",
          IconUrl = "image/icons/maui.webp",
          Channel = "MAUI",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 3, 2, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 3, 2, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Mac OS", "Windows", "Linux", "Visual Studio Code"]
        }
    ];
  }
}
