using System.Text.Json.Serialization;

namespace Web.Models;

internal class Repository
{
    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }
}