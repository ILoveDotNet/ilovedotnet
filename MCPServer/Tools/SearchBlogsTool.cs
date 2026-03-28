using System.ComponentModel;
using MCPServer.Models;
using MCPServer.Services;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpServerToolType]
public static class SearchBlogsTool
{
    [McpServerTool]
    [Description("Search ilovedotnet blog posts by keywords. Returns matching entries with slug, title, channel, and content summary.")]
    public static string SearchBlogs(
        KnowledgeBaseService kb,
        [Description("Space-separated search terms, e.g. 'lazy loading blazor' or 'dependency injection'")]
        string query,
        [Description("Maximum number of results to return (default 5)")]
        int maxResults = 5)
    {
        var results = kb.Search(query, maxResults);

        if (results.Count == 0)
            return $"No blog posts found matching '{query}'.";

        return string.Join("\n\n", results.Select(FormatEntry));
    }

    private static string FormatEntry(KnowledgeEntry e) => $"""
        Slug:        {e.Slug}
        Title:       {e.Title}
        Channel:     {e.Channel}
        Keywords:    {string.Join(", ", e.Keywords)}
        Description: {e.Description}
        What:        {Truncate(e.WhatContent, 300)}
        """;

    private static string Truncate(string text, int max) =>
        text.Length <= max ? text : text[..max] + "…";
}
