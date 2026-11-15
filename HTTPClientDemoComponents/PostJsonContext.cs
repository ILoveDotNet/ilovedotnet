using System.Text.Json.Serialization;

namespace HTTPClientDemoComponents;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(IEnumerable<Post>))]
internal partial class PostJsonContext : JsonSerializerContext
{
}
