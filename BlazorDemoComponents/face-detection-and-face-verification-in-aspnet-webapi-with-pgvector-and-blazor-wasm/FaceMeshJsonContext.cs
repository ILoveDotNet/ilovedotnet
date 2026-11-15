using System.Text.Json.Serialization;

namespace BlazorDemoComponents;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(FeatureVectorResult))]
[JsonSerializable(typeof(FaceMeshResult))]
internal partial class FaceMeshJsonContext : JsonSerializerContext
{
}
