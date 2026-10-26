using System.Text.Json.Serialization;

namespace CommonComponents.Models;

[JsonSerializable(typeof(string))]
internal sealed partial class LearningChatJsonContext : JsonSerializerContext;
