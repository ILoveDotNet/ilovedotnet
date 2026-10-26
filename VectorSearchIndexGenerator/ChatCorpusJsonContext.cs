using System.Text.Json;
using System.Text.Json.Serialization;

namespace VectorSearchIndexGenerator;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
[JsonSerializable(typeof(ChatPassage[]))]
[JsonSerializable(typeof(ChatCorpusManifest))]
internal sealed partial class ChatCorpusJsonContext : JsonSerializerContext;
