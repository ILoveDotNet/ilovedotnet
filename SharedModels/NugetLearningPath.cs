namespace SharedModels;

public class NugetLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(6);

  public NugetLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "How to Find NuGet Dependency Vulnerabilities Before They Find You in .NET",
          Description = "In this post I will teach you how to inventory your full NuGet dependency tree, detect packages with known CVEs using NuGet Audit, identify deprecated and outdated packages, and configure your CI pipeline to catch dependency risks automatically.",
          Author = "Abdul Rahman",
          Slug = "how-to-find-nuget-dependency-vulnerabilities-before-they-find-you-in-dotnet",
          PosterUrl = "image/blogs/nuget/how-to-find-nuget-dependency-vulnerabilities-before-they-find-you-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/how-to-find-nuget-dependency-vulnerabilities-before-they-find-you-in-dotnet.webp",
          ContentUrl = "blogs/how-to-find-nuget-dependency-vulnerabilities-before-they-find-you-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 11, 8, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 11, 8, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "NuGet Audit", "Dependency Audit", "CVE", "Vulnerabilities", "Transitive Dependencies", "dotnet list package", "NuGetAuditLevel", "Supply Chain Security", ".NET"]
        },
      new ContentMetaData
        {
          Order = 2,
          Title = "Swap Out a Vulnerable NuGet Package Without Touching Your Domain Code in .NET",
          Description = "In this post I will teach you how to safely isolate and remove a vulnerable NuGet dependency using the Wrap, Replace, and Rewrite strategies — with an Anti-Corruption Layer so your domain code never needs to change.",
          Author = "Abdul Rahman",
          Slug = "swap-out-a-vulnerable-nuget-package-without-touching-your-domain-code-in-dotnet",
          PosterUrl = "image/blogs/nuget/swap-out-a-vulnerable-nuget-package-without-touching-your-domain-code-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/swap-out-a-vulnerable-nuget-package-without-touching-your-domain-code-in-dotnet.webp",
          ContentUrl = "blogs/swap-out-a-vulnerable-nuget-package-without-touching-your-domain-code-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 11, 15, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 11, 15, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Wrap", "Replace", "Rewrite", "Anti-Corruption Layer", "Dependency Abstraction", "Refactoring", "Vulnerability", "Dependency Injection", ".NET"]
        },
      new ContentMetaData
        {
          Order = 3,
          Title = "Stop NuGet Packages From Corrupting Your Domain: The Anti-Corruption Layer Pattern in .NET",
          Description = "In this post I will teach you what an Anti-Corruption Layer is, why it is an architectural decision that must be enforced at the project level, and how to implement a complete ACL for a NuGet dependency in .NET — with the build system itself preventing leakage into your domain code.",
          Author = "Abdul Rahman",
          Slug = "stop-nuget-packages-from-corrupting-your-domain-the-anti-corruption-layer-pattern-in-dotnet",
          PosterUrl = "image/blogs/nuget/stop-nuget-packages-from-corrupting-your-domain-the-anti-corruption-layer-pattern-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/stop-nuget-packages-from-corrupting-your-domain-the-anti-corruption-layer-pattern-in-dotnet.webp",
          ContentUrl = "blogs/stop-nuget-packages-from-corrupting-your-domain-the-anti-corruption-layer-pattern-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 11, 22, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 11, 22, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Anti-Corruption Layer", "ACL", "Dependency Sprawl", "Domain-Driven Design", "DDD", "Adapter Pattern", "Dependency Injection", "Architectural Decision Record", ".NET"]
        },
      new ContentMetaData
        {
          Order = 4,
          Title = "Characterization Tests: The Safety Net Every NuGet Package Swap Needs in .NET",
          Description = "In this post I will teach you what characterization tests are, how they differ from unit tests, and how to implement them using the Verify library in .NET to create a golden master that guarantees a NuGet package replacement does not silently change your application's behaviour.",
          Author = "Abdul Rahman",
          Slug = "characterization-tests-the-safety-net-every-nuget-package-swap-needs-in-dotnet",
          PosterUrl = "image/blogs/nuget/characterization-tests-the-safety-net-every-nuget-package-swap-needs-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/characterization-tests-the-safety-net-every-nuget-package-swap-needs-in-dotnet.webp",
          ContentUrl = "blogs/characterization-tests-the-safety-net-every-nuget-package-swap-needs-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 11, 29, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 11, 29, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Characterization Tests", "Golden Master", "Verify", "Snapshot Testing", "Package Replacement", "Dependency Safety", "xUnit", "Testing", ".NET"]
        },
      new ContentMetaData
        {
          Order = 5,
          Title = "Contract Tests: The Acceptance Gate That Proves Your NuGet Replacement Behaves Identically in .NET",
          Description = "In this post I will teach you what contract tests are, how they differ from characterization tests, how to build an abstract contract test base class in xUnit, and how to combine it with BenchmarkDotNet and rollback criteria tests to gate both correctness and performance before any NuGet replacement ships.",
          Author = "Abdul Rahman",
          Slug = "contract-tests-the-acceptance-gate-that-proves-your-nuget-replacement-behaves-identically-in-dotnet",
          PosterUrl = "image/blogs/nuget/contract-tests-the-acceptance-gate-that-proves-your-nuget-replacement-behaves-identically-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/contract-tests-the-acceptance-gate-that-proves-your-nuget-replacement-behaves-identically-in-dotnet.webp",
          ContentUrl = "blogs/contract-tests-the-acceptance-gate-that-proves-your-nuget-replacement-behaves-identically-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 12, 6, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 12, 6, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Contract Tests", "Abstract Test Base", "Behavioral Equivalence", "BenchmarkDotNet", "Rollback Criteria", "Performance Regression", "xUnit", "Package Replacement", ".NET"]
        },
      new ContentMetaData
        {
          Order = 6,
          Title = "From Debt to Discipline: Managing NuGet Dependency Technical Debt in .NET",
          Description = "In this post I will teach you how to classify NuGet dependency technical debt as healthy debt or rot, how to document deferral decisions with Architectural Decision Records, how to automate continuous vulnerability scanning, and how to define upgrade SLAs and ownership models that keep your dependency portfolio manageable.",
          Author = "Abdul Rahman",
          Slug = "from-debt-to-discipline-managing-nuget-dependency-technical-debt-in-dotnet",
          PosterUrl = "image/blogs/nuget/from-debt-to-discipline-managing-nuget-dependency-technical-debt-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/nuget/from-debt-to-discipline-managing-nuget-dependency-technical-debt-in-dotnet.webp",
          ContentUrl = "blogs/from-debt-to-discipline-managing-nuget-dependency-technical-debt-in-dotnet",
          IconUrl = "image/icons/nuget.webp",
          Channel = "NuGet",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 12, 13, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 12, 13, 22, 30, 0, DateTimeKind.Utc),
          Keywords = [ "Technical Debt", "Architectural Decision Record", "ADR", "Dependency Ownership", "Dependabot", "dotnet list package", "Upgrade SLA", "CVE", "Version Lag", ".NET"]
        },
    ];
  }
}
