namespace SharedModels;

public class SecurityLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public SecurityLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Improve Data Security by Redacting Logs in .NET",
          Description = "Learn how to log securely in .NET, avoid leaking sensitive data, and implement compliance best practices including redaction and non-repudiation.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-redacting-logs-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-redacting-logs-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-redacting-logs-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-redacting-logs-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 7, 20, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 7, 20, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Logging", "PII", "Compliance", "Redaction", "Non-Repudiation", "Security"]
        }
    ];
  }
}
