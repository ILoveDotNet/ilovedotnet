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
                Author = "Abdul Rahman",
                PosterUrl = "image/blogs/owasp/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines.webp",
                ThumbnailUrl = "image/blogs/owasp/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines.webp",
                ContentUrl = "blogs/owasp-secure-your-dotnet-app-by-scanning-for-vulnerable-nuget-dependency-in-ci-pipelines",
                IconUrl = "image/icons/owasp.webp",
                Type = "OWASP",
                CreatedOn = new DateTime(2024, 2, 25, 22, 30, 0),
                ModifiedOn = new DateTime(2024, 2, 25, 22, 30, 0)
            }
        ];
    }
}