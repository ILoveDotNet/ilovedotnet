namespace SharedModels;

public class DatabaseLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public DatabaseLearningPath()
  {
    FullContents =
    [
        new ContentMetaData
            {
                Order = 1,
                Title = "Database Normalization",
                Description = "Master the art of database normalization to eliminate wasteful determinism and build rock-solid data foundations. Learn all five normal forms with practical examples.",
                Author = "Abdul Rahman",
                Slug = "database-normalization-eliminating-wasteful-determinism",
                PosterUrl = "image/blogs/database/database-normalization-eliminating-wasteful-determinism.webp",
                ThumbnailUrl = "image/blogs/database/database-normalization-eliminating-wasteful-determinism.webp",
                ContentUrl = "blogs/database-normalization-eliminating-wasteful-determinism",
                IconUrl = "image/icons/database.webp",
                Type = "Database",
                Channel = "Database",
                CreatedOn = new DateTime(2025, 12, 21, 22, 30, 0, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2025, 12, 21, 22, 30, 0, DateTimeKind.Utc),
                Keywords = ["Database", "Normalization", "1NF", "2NF", "3NF", "Boyce-Codd", "4NF", "5NF", "Data Modeling", "Schema Design", "SQL"]
            }
    ];
  }
}
