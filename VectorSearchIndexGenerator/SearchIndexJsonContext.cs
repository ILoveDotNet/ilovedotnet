using System.Text.Json.Serialization;

namespace VectorSearchIndexGenerator;

[JsonSerializable(typeof(List<SearchIndexEntry>))]
internal sealed partial class SearchIndexJsonContext : JsonSerializerContext;
