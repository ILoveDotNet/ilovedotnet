namespace SharedModels;

public class SecurityLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(4);

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
        },
      new ContentMetaData
        {
          Order = 2,
          Title = "Improve Data Security with Right to be Forgotten in .NET",
          Description = "How to implement the right to be forgotten in .NET: deleting or anonymizing user data across databases, logs, backups, and more.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-with-right-to-be-forgotten-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-with-right-to-be-forgotten-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-with-right-to-be-forgotten-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-with-right-to-be-forgotten-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 7, 27, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 7, 27, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["PII", "GDPR", "Compliance", "Security", "Anonymization", "Logs", "Backups", ".NET"]
        },
      new ContentMetaData
        {
          Order = 3,
          Title = "Improve Data Security by Doing a Secure User Data Download in .NET",
          Description = "Learn how to securely implement GDPR-compliant user data export in .NET, including encryption, authorization, and best practices for protecting PII.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-doing-a-secure-user-data-download-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-doing-a-secure-user-data-download-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-doing-a-secure-user-data-download-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-doing-a-secure-user-data-download-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 8, 3, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 8, 3, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["GDPR", "Data Export", "Encryption", "Authorization", "PII", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 4,
          Title = "Improve Data Security by Preventing Excessive Data Exposure in .NET",
          Description = "How to prevent excessive data exposure in .NET APIs using DTOs, role-based authorization, and secure backups. Practical steps for GDPR compliance.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-preventing-excessive-data-exposure-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-preventing-excessive-data-exposure-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-preventing-excessive-data-exposure-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-preventing-excessive-data-exposure-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 8, 10, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 8, 10, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Excessive Data Exposure", "DTO", "Role-Based Authorization", "Backups", "GDPR", "Security", ".NET"]
        }
    ];
  }
}
