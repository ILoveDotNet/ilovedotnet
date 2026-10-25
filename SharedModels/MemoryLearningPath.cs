namespace SharedModels;

public class MemoryLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(3);

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
          CreatedOn = new DateTime(2026, 8, 23, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 8, 23, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Garbage Collection", "GC", "Stack", "Heap", "Value Types", "Reference Types", "Memory Fragmentation", "Virtual Memory", "Mark Sweep Compact", "GC Roots", "Stop-the-World", "Generational GC", "CLR", ".NET"]
        },
      new ContentMetaData
        {
          Order = 2,
          Title = "Understanding .NET Garbage Collection: Generations, Scope, and Object Lifetimes",
          Description = "Build a practical mental model of .NET garbage collection by following references from GC roots, comparing short-lived and retained objects, observing generations, understanding the large object heap, and separating managed memory from disposable resources.",
          Author = "Abdul Rahman",
          Slug = "understanding-dotnet-garbage-collection-generations-scope-and-object-lifetimes",
          PosterUrl = "image/blogs/memory/understanding-dotnet-garbage-collection-generations-scope-and-object-lifetimes.webp",
          ThumbnailUrl = "image/blogs/memory/understanding-dotnet-garbage-collection-generations-scope-and-object-lifetimes.webp",
          ContentUrl = "blogs/understanding-dotnet-garbage-collection-generations-scope-and-object-lifetimes",
          IconUrl = "image/icons/memory.webp",
          Channel = "Memory",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 9, 13, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 9, 13, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Garbage Collection", "GC", "Object Lifetime", "GC Roots", "Generations", "Generation 0", "Generation 1", "Generation 2", "Large Object Heap", "Finalizers", "IDisposable", ".NET"]
        },
      new ContentMetaData
        {
          Order = 3,
          Title = "Advanced .NET Garbage Collection: Measure Before You Tune",
          Description = "Learn how to measure and reduce allocation pressure, see what .NET 10 escape analysis can do, control object lifetimes with disposal and event cleanup, and evaluate server GC, DATAS, LOH compaction, and no-GC regions without tuning by guesswork.",
          Author = "Abdul Rahman",
          Slug = "advanced-dotnet-garbage-collection-measure-before-you-tune",
          PosterUrl = "image/blogs/memory/advanced-dotnet-garbage-collection-measure-before-you-tune.webp",
          ThumbnailUrl = "image/blogs/memory/advanced-dotnet-garbage-collection-measure-before-you-tune.webp",
          ContentUrl = "blogs/advanced-dotnet-garbage-collection-measure-before-you-tune",
          IconUrl = "image/icons/memory.webp",
          Channel = "Memory",
          Type = "blogs",
          CreatedOn = new DateTime(2026, 11, 1, 22, 30, 0, DateTimeKind.Utc),
          ModifiedOn = new DateTime(2026, 11, 1, 22, 30, 0, DateTimeKind.Utc),
          Keywords = ["Garbage Collection", "GC", "Allocation Pressure", "Escape Analysis", "ArrayPool", "Large Object Heap", "LOH Compaction", "Server GC", "Workstation GC", "DATAS", "No GC Region", "IDisposable", "Finalizer", "Weak Reference", ".NET"]
        },
    ];
  }
}
