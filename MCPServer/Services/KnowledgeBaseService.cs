using System.Text.Json;
using MCPServer.Models;

namespace MCPServer.Services;

public sealed class KnowledgeBaseService
{
    private readonly IReadOnlyList<KnowledgeEntry> _entries;

    public KnowledgeBaseService(IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, "Data", "knowledge-base.json");
        if (!File.Exists(path))
        {
            _entries = [];
            return;
        }

        var json = File.ReadAllText(path);
        _entries = JsonSerializer.Deserialize<List<KnowledgeEntry>>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
    }

    public IReadOnlyList<KnowledgeEntry> GetAll() => _entries;

    public IReadOnlyList<KnowledgeEntry> Search(string query, int maxResults = 10)
    {
        var terms = query.Split(' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return [.. _entries
            .Where(e => terms.All(t =>
                e.FullText.Contains(t, StringComparison.OrdinalIgnoreCase)))
            .Take(maxResults)];
    }

    public IReadOnlyList<KnowledgeEntry> GetByChannel(string channel) =>
        [.. _entries.Where(e =>
            e.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase))];

    public IReadOnlyList<string> GetChannels() =>
        [.. _entries.Select(e => e.Channel).Distinct().Order()];
}
