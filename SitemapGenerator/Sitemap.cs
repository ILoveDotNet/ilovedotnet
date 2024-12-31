using System.Xml.Linq;

internal class Sitemap
{
    public List<Url> Urls { get; set; } = [];

    public void GenerateSitemap(string filePath)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XElement urlset = new(ns + "urlset");

        foreach (var url in Urls)
        {
            XElement urlElement = new(ns + "url",
                new XElement(ns + "loc", url.Loc),
                new XElement(ns + "lastmod", url.LastMod.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                new XElement(ns + "changefreq", url.ChangeFreq),
                new XElement(ns + "priority", url.Priority)
            );
            urlset.Add(urlElement);
        }

        XDocument sitemap = new(new XDeclaration("1.0", "UTF-8", "yes"), urlset);
        sitemap.Save(filePath);
    }
}
