using System.Text.Json.Serialization;

namespace CommonComponents.Models;

[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(LearningChatHistory))]
internal sealed partial class LearningChatJsonContext : JsonSerializerContext;
