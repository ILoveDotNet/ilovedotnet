namespace SharedModels;

public class Sitemaps
{
    public IReadOnlyList<string> Files => new List<string>(17) {
        "sitemap.xml",
        "sitemap-authors.xml",
        "sitemap-blog-blazor-wasm.xml",
        "sitemap-blog-dependency-injection.xml",
        "sitemap-blog-design-pattern.xml",
        "sitemap-blog-http-client.xml",
        "sitemap-blog-linq.xml",
        "sitemap-blog-middleware.xml",
        "sitemap-blog-oops.xml",
        "sitemap-blog-owasp.xml",
        "sitemap-blog-python.xml",
        "sitemap-blog-report.xml",
        "sitemap-blog-solid.xml",
        "sitemap-blog-tdd.xml",
        "sitemap-blog-webapi.xml",
        "sitemap-channels.xml",
        "sitemap-talk.xml"
    };
}