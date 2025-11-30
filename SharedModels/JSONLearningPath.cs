namespace SharedModels;

public class JSONLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(5);

  public JSONLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
      {
        Order = 1,
        Title = "Working with Unknown JSON Using JsonDocument in .NET",
        Description = "Learn how to efficiently parse and navigate large JSON structures without predefined classes using JsonDocument's immutable DOM API for maximum performance.",
        Author = "Abdul Rahman",
        Slug = "working-with-unknown-json-using-jsondocument-in-dotnet",
        PosterUrl = "image/blogs/json/working-with-unknown-json-using-jsondocument-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/json/working-with-unknown-json-using-jsondocument-in-dotnet.webp",
        ContentUrl = "blogs/working-with-unknown-json-using-jsondocument-in-dotnet",
        Channel = "JSON",
        Type = "blogs",
        IconUrl = "image/icons/json.webp",
        CreatedOn = new DateTime(2025, 11, 9, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 11, 9, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["JsonDocument", "JSON", "DOM", "Immutable", "Performance", "System.Text.Json", "JsonElement", "RootElement"]
      },
      new ContentMetaData
      {
        Order = 2,
        Title = "Creating and Modifying JSON Using JsonNode in .NET",
        Description = "Discover how to dynamically build and manipulate JSON structures with JsonNode's mutable DOM API—add, remove, and update properties on the fly.",
        Author = "Abdul Rahman",
        Slug = "creating-and-modifying-json-using-jsonnode-in-dotnet",
        PosterUrl = "image/blogs/json/creating-and-modifying-json-using-jsonnode-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/json/creating-and-modifying-json-using-jsonnode-in-dotnet.webp",
        ContentUrl = "blogs/creating-and-modifying-json-using-jsonnode-in-dotnet",
        Channel = "JSON",
        Type = "blogs",
        IconUrl = "image/icons/json.webp",
        CreatedOn = new DateTime(2025, 11, 16, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 11, 16, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["JsonNode", "JSON", "Mutable", "JsonObject", "JsonArray", "JsonValue", "System.Text.Json", "Dynamic JSON"]
      },
      new ContentMetaData
      {
        Order = 3,
        Title = "Maximizing JSON Performance with Utf8JsonReader and Utf8JsonWriter in .NET",
        Description = "Achieve blazing-fast JSON processing with low-level Utf8JsonReader and Utf8JsonWriter APIs for maximum performance and minimal memory allocation.",
        Author = "Abdul Rahman",
        Slug = "maximizing-json-performance-with-utf8-reader-writer-in-dotnet",
        PosterUrl = "image/blogs/json/maximizing-json-performance-with-utf8-reader-writer-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/json/maximizing-json-performance-with-utf8-reader-writer-in-dotnet.webp",
        ContentUrl = "blogs/maximizing-json-performance-with-utf8-reader-writer-in-dotnet",
        Channel = "JSON",
        Type = "blogs",
        IconUrl = "image/icons/json.webp",
        CreatedOn = new DateTime(2025, 11, 23, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 11, 23, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Utf8JsonReader", "Utf8JsonWriter", "JSON", "Performance", "Low-level", "System.Text.Json", "Streaming", "UTF-8"]
      },
      new ContentMetaData
      {
        Order = 4,
        Title = "Handling JSON Errors and Best Practices in .NET",
        Description = "Master JSON error handling with JsonException, custom exceptions, circular references, immutable types, and polymorphic serialization for robust .NET applications.",
        Author = "Abdul Rahman",
        Slug = "handling-json-errors-and-best-practices-in-dotnet",
        PosterUrl = "image/blogs/json/handling-json-errors-and-best-practices-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/json/handling-json-errors-and-best-practices-in-dotnet.webp",
        ContentUrl = "blogs/handling-json-errors-and-best-practices-in-dotnet",
        Channel = "JSON",
        Type = "blogs",
        IconUrl = "image/icons/json.webp",
        CreatedOn = new DateTime(2025, 11, 30, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 11, 30, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["JsonException", "JSON", "Error Handling", "JsonExtensionData", "Circular References", "Immutable Types", "JsonConstructor", "Polymorphic Serialization"]
      },
      new ContentMetaData
      {
        Order = 5,
        Title = "Mastering Advanced JSON with Custom Converters and Source Generation in .NET",
        Description = "Take full control of JSON serialization with custom converters (basic and factory patterns) and boost performance with System.Text.Json source generation.",
        Author = "Abdul Rahman",
        Slug = "mastering-advanced-json-with-custom-converters-and-source-generation-in-dotnet",
        PosterUrl = "image/blogs/json/mastering-advanced-json-with-custom-converters-and-source-generation-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/json/mastering-advanced-json-with-custom-converters-and-source-generation-in-dotnet.webp",
        ContentUrl = "blogs/mastering-advanced-json-with-custom-converters-and-source-generation-in-dotnet",
        Channel = "JSON",
        Type = "blogs",
        IconUrl = "image/icons/json.webp",
        CreatedOn = new DateTime(2025, 12, 7, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 12, 7, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Custom Converters", "JSON", "Source Generation", "JsonConverter", "JsonConverterFactory", "JsonSerializerContext", "Performance", "Reflection", "System.Text.Json"]
      }
    ];
  }
}
