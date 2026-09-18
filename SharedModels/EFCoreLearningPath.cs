namespace SharedModels;

public class EFCoreLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(3);

  public EFCoreLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "Add Cross-Cutting Concerns to EF Core Without Touching Your Repositories",
                Description = "Learn how EF Core interceptors let you log connections, cap runaway queries, and audit every save from one place, without scattering the same code across every repository.",
                Author = "Abdul Rahman",
                Slug = "add-cross-cutting-concerns-to-ef-core-without-touching-your-repositories",
                PosterUrl = "image/blogs/efcore/add-cross-cutting-concerns-to-ef-core-without-touching-your-repositories.webp",
                ThumbnailUrl = "image/blogs/efcore/add-cross-cutting-concerns-to-ef-core-without-touching-your-repositories.webp",
                ContentUrl = "blogs/add-cross-cutting-concerns-to-ef-core-without-touching-your-repositories",
                IconUrl = "image/icons/efcore.webp",
                Type = "EFCore",
                Channel = "EFCore",
                CreatedOn = new DateTime(2027, 1, 3, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2027, 1, 3, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["EF Core", "Entity Framework Core", "Interceptors", "DbContext", "Auditing", "Connection Interceptor", "SaveChanges Interceptor", ".NET"]
            },
        new ContentMetaData
            {
                Order = 2,
                Title = "Enforce Database Schema Standards Automatically With EF Core Conventions",
                Description = "Stop policing table names, string lengths, and cascade behavior in code review. EF Core conventions let you bake schema standards straight into your model.",
                Author = "Abdul Rahman",
                Slug = "enforce-database-schema-standards-automatically-with-ef-core-conventions",
                PosterUrl = "image/blogs/efcore/enforce-database-schema-standards-automatically-with-ef-core-conventions.webp",
                ThumbnailUrl = "image/blogs/efcore/enforce-database-schema-standards-automatically-with-ef-core-conventions.webp",
                ContentUrl = "blogs/enforce-database-schema-standards-automatically-with-ef-core-conventions",
                IconUrl = "image/icons/efcore.webp",
                Type = "EFCore",
                Channel = "EFCore",
                CreatedOn = new DateTime(2027, 1, 10, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2027, 1, 10, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["EF Core", "Entity Framework Core", "Conventions", "IConvention", "Schema Design", "Model Builder", "Migrations", ".NET"]
            },
        new ContentMetaData
            {
                Order = 3,
                Title = "Catch Slow Queries and Rewrite Them on the Fly With EF Core Command Interceptors",
                Description = "Go beneath DbContext to the raw ADO.NET calls EF Core makes. Detect slow queries, inject query hints, centralize error handling, and spot Cartesian explosions before they hit production.",
                Author = "Abdul Rahman",
                Slug = "catch-slow-queries-and-rewrite-them-on-the-fly-with-ef-core-command-interceptors",
                PosterUrl = "image/blogs/efcore/catch-slow-queries-and-rewrite-them-on-the-fly-with-ef-core-command-interceptors.webp",
                ThumbnailUrl = "image/blogs/efcore/catch-slow-queries-and-rewrite-them-on-the-fly-with-ef-core-command-interceptors.webp",
                ContentUrl = "blogs/catch-slow-queries-and-rewrite-them-on-the-fly-with-ef-core-command-interceptors",
                IconUrl = "image/icons/efcore.webp",
                Type = "EFCore",
                Channel = "EFCore",
                CreatedOn = new DateTime(2027, 1, 17, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2027, 1, 17, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["EF Core", "Entity Framework Core", "DbCommandInterceptor", "Slow Query Detection", "Query Hints", "Cartesian Explosion", "Lazy Loading", ".NET"]
            }
    ];
  }
}
