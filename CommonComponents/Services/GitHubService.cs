using System.Text.Json;
using CommonComponents.Models;

namespace CommonComponents.Services;

public class GitHubService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Repository> GetRepositoryAsync(string owner, string name, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"repos/{owner}/{name}", HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            using var streamContent = await response.Content.ReadAsStreamAsync(cancellationToken);
            var repository = (await JsonSerializer.DeserializeAsync<Repository>(streamContent, new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken))!;
            return repository;
        }

        return new();
    }
}