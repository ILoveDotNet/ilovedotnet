using System.ComponentModel;
using MCPServer.Services;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpServerToolType]
public static class LearningPathTool
{
    [McpServerTool]
    [Description(
        "Get all blog posts in a specific ilovedotnet learning path/channel, " +
        "ordered by newest first. Use list_channels to discover available channels.")]
    public static string GetLearningPath(
        KnowledgeBaseService kb,
        [Description(
            "Channel / learning path name, e.g. 'Blazor', 'LINQ', 'MCP', 'AI', " +
            "'DependencyInjection', 'DesignPattern', 'OWASP', 'Testing'")]
        string channel)
    {
        var entries = kb.GetByChannel(channel);

        if (entries.Count == 0)
        {
            var channels = kb.GetChannels();
            return channels.Count == 0
                ? "Knowledge base is empty. Run ContentExtractor to generate it."
                : $"No entries found for channel '{channel}'. " +
                  $"Available channels: {string.Join(", ", channels)}";
        }

        var header = $"Learning Path: {channel} ({entries.Count} articles)\n\n";
        var list = string.Join("\n", entries.Select((e, i) =>
            $"{i + 1}. {e.Title}\n" +
            $"   URL:  https://ilovedotnet.org/blogs/{e.Slug}\n" +
            $"   Tags: {string.Join(", ", e.Keywords.Take(5))}\n" +
            $"   {e.Description}"));

        return header + list;
    }

    [McpServerTool]
    [Description("List all available learning path channels in ilovedotnet.")]
    public static string ListChannels(KnowledgeBaseService kb)
    {
        var channels = kb.GetChannels();

        return channels.Count == 0
            ? "Knowledge base is empty. Run ContentExtractor to generate it."
            : $"Available channels ({channels.Count}):\n" +
              string.Join("\n", channels.Select(c => $"  - {c}"));
    }
}
