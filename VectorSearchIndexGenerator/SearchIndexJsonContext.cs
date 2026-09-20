using System.Text.Json.Serialization;

namespace VectorSearchIndexGenerator;

[JsonSerializable(typeof(SearchIndexFile))]
internal sealed partial class SearchIndexJsonContext : JsonSerializerContext;
