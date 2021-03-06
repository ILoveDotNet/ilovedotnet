namespace SharedModels;

public class Sitemaps
{
    public IReadOnlyList<string> Files => new List<string>(9) {
        "sitemap.xml",
        "sitemap-blog-blazor-wasm.xml",
        "sitemap-blog-dependency-injection.xml",
        "sitemap-blog-middleware.xml",
        "sitemap-blog-oops.xml",
        "sitemap-blog-tdd.xml",
        "sitemap-blog-webapi.xml",
        "sitemap-channels.xml",
        "sitemap-talk.xml"
    };
}