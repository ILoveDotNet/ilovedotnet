using AngleSharp;
using AngleSharp.Dom;
using SharedModels;
using System.Text.Json;
using System.Text.RegularExpressions;

var repoRoot = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
var outputPath = args.Length > 1
    ? args[1]
    : Path.Combine(repoRoot, "MCPServer", "Data", "knowledge-base.json");

Console.WriteLine($"Repo root:  {repoRoot}");
Console.WriteLine($"Output:     {outputPath}");

Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

// Load all content metadata from SharedModels
var toc = new TableOfContents();
var metadataBySlug = toc.AllContents.ToDictionary(c => c.Slug, StringComparer.OrdinalIgnoreCase);

var browsingConfig = Configuration.Default;

// Find all .razor files in *DemoComponents directories
var demoComponentDirs = Directory
    .GetDirectories(repoRoot, "*DemoComponents", SearchOption.TopDirectoryOnly);

var razorFiles = demoComponentDirs
    .SelectMany(dir => Directory.GetFiles(dir, "*.razor", SearchOption.AllDirectories))
    .Where(f => !Path.GetFileName(f).StartsWith('_'))
    .OrderBy(f => f)
    .ToList();

Console.WriteLine($"Found {razorFiles.Count} razor files\n");

var pageDirectiveRegex = new Regex(
    @"@page\s+""(?:/blogs/([^""]+))""",
    RegexOptions.Compiled);

var entries = new List<KnowledgeEntry>();

foreach (var filePath in razorFiles)
{
    var content = await File.ReadAllTextAsync(filePath);

    var pageMatch = pageDirectiveRegex.Match(content);
    if (!pageMatch.Success) continue;

    var slug = pageMatch.Groups[1].Value;

    if (!metadataBySlug.TryGetValue(slug, out var meta))
    {
        Console.WriteLine($"  SKIP (no metadata): {slug}");
        continue;
    }

    var htmlContent = StripRazorDirectives(content);

    using var context = BrowsingContext.New(browsingConfig);
    var document = await context.OpenAsync(req => req.Content(htmlContent));

    var whatText  = ExtractAndNormalise(document, "what");
    var whyText   = ExtractAndNormalise(document, "why");
    var howText   = ExtractAndNormalise(document, "how");
    var codeText  = string.Join("\n---\n",
        document.QuerySelectorAll("codesnippet")
                .Select(e => e.TextContent.Trim())
                .Where(t => t.Length > 0));

    entries.Add(new KnowledgeEntry(
        Slug:         meta.Slug,
        Title:        meta.Title,
        Description:  meta.Description,
        Channel:      meta.Channel,
        Author:       meta.Author,
        Keywords:     meta.Keywords,
        WhatContent:  whatText,
        WhyContent:   whyText,
        HowContent:   howText,
        CodeSnippets: codeText,
        CreatedOn:    meta.CreatedOn.ToString("yyyy-MM-dd"),
        ModifiedOn:   meta.ModifiedOn.ToString("yyyy-MM-dd")));

    Console.WriteLine($"  OK: {slug}");
}

var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
await File.WriteAllTextAsync(outputPath, json);
Console.WriteLine($"\nWrote {entries.Count} entries to {outputPath}");

// ── helpers ─────────────────────────────────────────────────────────────────

static string StripRazorDirectives(string fileContent)
{
    var lines = fileContent.Split('\n');
    var result = new List<string>(lines.Length);
    var codeDepth = 0;
    var inCodeBlock = false;

    foreach (var line in lines)
    {
        var trimmed = line.TrimStart();

        if (inCodeBlock)
        {
            foreach (var ch in line)
            {
                if (ch == '{') codeDepth++;
                else if (ch == '}') codeDepth--;
            }
            if (codeDepth <= 0) inCodeBlock = false;
            continue;
        }

        if (trimmed.StartsWith("@code", StringComparison.Ordinal))
        {
            inCodeBlock = true;
            codeDepth = 1;
            continue;
        }

        if (trimmed.StartsWith('@'))
            continue;

        result.Add(line);
    }

    return string.Join('\n', result);
}

static string ExtractAndNormalise(IDocument document, string tagName)
{
    var el = document.QuerySelector(tagName);
    if (el is null) return string.Empty;
    return Regex.Replace(el.TextContent, @"\s+", " ").Trim();
}

// ── model (local, matches MCPServer/Models/KnowledgeEntry) ──────────────────

record KnowledgeEntry(
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
    string       ModifiedOn);

