namespace SharedModels;

public class OWASPLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public OWASPLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "OWASP - Secure your dotnet app by scanning for vulnerable nuget dependencies in CI pipelines",
                Description = "In this post I will teach you one of the owasp top 10 requirement to verify code to avoid malicious dependencies using nuget audit. All with live working demo.",
                Author = "Abdul Rahman",
                Slug = "owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines",
                PosterUrl = "image/blogs/owasp/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines.webp",
                ThumbnailUrl = "image/blogs/owasp/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines.webp",
                ContentUrl = "blogs/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines",
                IconUrl = "image/icons/owasp.webp",
                Channel = "OWASP",
                Type = "blogs",
                CreatedOn = new DateTime(2024, 2, 25, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2024, 6, 16, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Security", "Dependency Check", "Nuget Vulnerability", "Malicious Code", "Audit Nuget"]
            }
    ];
  }
}
