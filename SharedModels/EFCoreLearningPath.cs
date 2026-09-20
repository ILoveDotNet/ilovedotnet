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
                Title = "EF Core Interceptors: Audit DbContext Saves and Diagnose Connections",
                Description = "Learn how EF Core interceptors hook into DbContext to audit saves, diagnose database connections, and centralize cross-cutting behavior without changing every repository.",
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
                Keywords = ["EF Core interceptors", "DbContext interception", "Entity Framework Core auditing", "connection diagnostics", "SaveChanges interceptor", ".NET"]
            },
        new ContentMetaData
            {
                Order = 2,
                Title = "EF Core Conventions for Code First Schema Standards and Migrations",
                Description = "Use EF Core conventions to standardize Code First tables, string lengths, foreign keys, and delete behavior, then apply the schema changes safely with migrations.",
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
                Keywords = ["EF Core conventions", "Code First schema standards", "Entity Framework Core migrations", "foreign keys", "IConvention", ".NET"]
            },
        new ContentMetaData
            {
                Order = 3,
                Title = "EF Core Query Performance: Catch Slow SQL with Command Interceptors",
                Description = "Improve EF Core query performance by detecting slow SQL commands, centralizing command failures, and rewriting provider-specific SQL through ADO.NET command interception.",
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
                Keywords = ["EF Core query performance", "slow SQL commands", "ADO.NET command interception", "DbCommandInterceptor", "query hints", ".NET"]
            }
    ];
  }
}
