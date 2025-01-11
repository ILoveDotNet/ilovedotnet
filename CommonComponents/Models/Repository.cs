using System.Text.Json.Serialization;

namespace CommonComponents.Models;

public class Repository
{
  [JsonPropertyName("stargazers_count")]
  public int StargazersCount { get; set; }
}
