namespace SharedModels;

public class SecurityLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(10);

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
        },
      new ContentMetaData
        {
          Order = 5,
          Title = "Improve Data Security with Hashing Techniques in .NET",
          Description = "Master secure hashing in .NET: password hashing with ASP.NET Core Identity, SHA256 with salt and pepper, and digital signatures for non-repudiation.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-with-hashing-techniques-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-with-hashing-techniques-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-with-hashing-techniques-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-with-hashing-techniques-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 8, 24, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 8, 24, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Hashing", "Password Security", "SHA256", "Salt", "Pepper", "Digital Signatures", "PBKDF2", "Azure Key Vault", "Non-Repudiation", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 6,
          Title = "Improve Data Security with Cryptographically Secure Random Generation in .NET",
          Description = "Master secure random number generation in .NET: avoid predictable System.Random, implement secure OTPs, create bulletproof GUIDs, and protect against randomness attacks.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-with-cryptographically-secure-random-generation-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-with-cryptographically-secure-random-generation-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-with-cryptographically-secure-random-generation-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-with-cryptographically-secure-random-generation-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 8, 31, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 8, 31, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Cryptographically Secure Random", "RandomNumberGenerator", "System.Random", "OTP", "GUID Security", "API Tokens", "Session Security", "Modulo Bias", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 7,
          Title = "Improve Data Security by Safely Storing Files in .NET",
          Description = "Learn how to protect against path traversal attacks, implement secure file storage, configure OS permissions, and defend your .NET applications from file upload vulnerabilities.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-safely-storing-files-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-safely-storing-files-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-safely-storing-files-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-safely-storing-files-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 10, 12, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 10, 12, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["File Upload Security", "Path Traversal", "File Storage", "IIS Security", "Least Privilege", "Input Validation", "OS Permissions", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 8,
          Title = "Improve Data Security by Validating File Contents in .NET",
          Description = "Master file content validation in .NET: implement file size limits, validate file types using magic numbers, and integrate antivirus scanning to protect against malicious uploads.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-validating-file-contents-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-validating-file-contents-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-validating-file-contents-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-validating-file-contents-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 10, 19, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 10, 19, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["File Validation", "File Upload Security", "Magic Numbers", "File Signatures", "Antivirus", "ClamAV", "File Size Limits", "Malware Detection", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 9,
          Title = "Improve Data Security by Implementing Secure File Retrieval in .NET",
          Description = "Master secure file downloads in .NET: prevent path traversal and SSRF attacks, implement proper Content-Type and Content-Disposition headers, and protect against browser-based exploits.",
          Author = "Abdul Rahman",
          Slug = "improve-data-security-by-implementing-secure-file-retrieval-in-dotnet",
          PosterUrl = "image/blogs/security/improve-data-security-by-implementing-secure-file-retrieval-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-data-security-by-implementing-secure-file-retrieval-in-dotnet.webp",
          ContentUrl = "blogs/improve-data-security-by-implementing-secure-file-retrieval-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 10, 26, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 10, 26, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["File Download Security", "Path Traversal", "SSRF", "Server-Side Request Forgery", "Content-Type", "Content-Disposition", "XSS Prevention", "File Retrieval", "Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 10,
          Title = "Improve Security with Dependency Management and SBOM in .NET",
          Description = "Master supply chain security in .NET: implement dependency governance, generate Software Bill of Materials (SBOM), manage private repositories, and defend against vulnerabilities in third-party components.",
          Author = "Abdul Rahman",
          Slug = "improve-security-with-dependency-management-and-sbom-in-dotnet",
          PosterUrl = "image/blogs/security/improve-security-with-dependency-management-and-sbom-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/security/improve-security-with-dependency-management-and-sbom-in-dotnet.webp",
          ContentUrl = "blogs/improve-security-with-dependency-management-and-sbom-in-dotnet",
          IconUrl = "image/icons/security.webp",
          Channel = "Security",
          Type = "blogs",
          CreatedOn = new DateTime(2025, 11, 2, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2025, 11, 2, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["SBOM", "Software Bill of Materials", "Dependency Management", "Supply Chain Security", "Component Governance", "NuGet", "Vulnerability Scanning", "Private Repository", "Third-Party Dependencies", "Log4j", "Security", ".NET"]
        }
    ];
  }
}
