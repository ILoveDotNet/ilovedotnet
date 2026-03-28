namespace MCPServer.Models;

public sealed record KnowledgeEntry(
    string       Slug,
    string       Title,
    string       Description,
    string       Channel,
    string       Author,
    List<string> Keywords,
    string       WhatContent,
    string       WhyContent,
    string       HowContent,
    string       CodeSnippets,
    string       CreatedOn,
    string       ModifiedOn)
{
    public string FullText =>
        $"{Title} {Description} {string.Join(' ', Keywords)} {WhatContent} {WhyContent} {HowContent}";
}
