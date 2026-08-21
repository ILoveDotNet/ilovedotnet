namespace SharedModels;

public class MemoryLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(1);

  public MemoryLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
        {
          Order = 1,
          Title = "Garbage Collection Fundamentals in .NET: Stack, Heap, Virtual Memory, Mark, Sweep, and Compact",
          Description = "Learn how the CLR's Garbage Collector works: understand stack versus heap memory, how value types and reference types differ on assignment, how the mark-sweep-compact algorithm reclaims unreachable objects, what memory fragmentation is, and how virtual memory gives each .NET process an isolated address space.",
          Author = "Abdul Rahman",
          Slug = "garbage-collection-fundamentals-in-dotnet",
          PosterUrl = "image/blogs/memory/garbage-collection-fundamentals-in-dotnet.webp",
          ThumbnailUrl = "image/blogs/memory/garbage-collection-fundamentals-in-dotnet.webp",
          ContentUrl = "blogs/garbage-collection-fundamentals-in-dotnet",
          IconUrl = "image/icons/memory.webp",
          Channel = "Memory",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 12, 20, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 12, 20, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Garbage Collection", "GC", "Stack", "Heap", "Value Types", "Reference Types", "Memory Fragmentation", "Virtual Memory", "Mark Sweep Compact", "GC Roots", "Stop-the-World", "Generational GC", "CLR", ".NET"]
        },
    ];
  }
}
