namespace VectorSearchIndexGenerator;

internal sealed record SearchIndexFile(
  string Model,
  string ModelRevision,
  int Dimensions,
  int Count,
  IReadOnlyList<SearchIndexEntry> Entries);
