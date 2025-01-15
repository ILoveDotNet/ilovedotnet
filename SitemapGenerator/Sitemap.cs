using System.Xml.Linq;
using SharedModels;

namespace SitemapGenerator;

public class Sitemap(TableOfContents tableOfContents, string channel)
{
  private XDocument? _sitemap;
  private readonly List<Url> _urls = [.. tableOfContents.GetContentsByChannel(channel).Select(content => new Url
  {
    Loc = $"https://ilovedotnet.org/blogs/{content.Slug}",
    LastMod = new DateTime(content.ModifiedOn.Year, content.ModifiedOn.Month, content.ModifiedOn.Day, content.ModifiedOn.Hour, content.ModifiedOn.Minute, content.ModifiedOn.Second),
    ChangeFreq = "weekly",
    Priority = 0.5
  })];

  public void GenerateSitemap(string filePath)
  {
    LoadSitemap(filePath);

    if (!IsAnyContentUpdatedAndRepublished() && !isAnyNewContentAddedOrScheduled)
    {
      return;
    }

    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
    XElement urlset = new(ns + "urlset");

    foreach (var url in _urls)
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

  private void LoadSitemap(string filePath)
  {
    if (File.Exists(filePath))
    {
      _sitemap = XDocument.Load(filePath);
    }
  }

  private bool IsAnyContentUpdatedAndRepublished()
  {
    if (_sitemap is null)
    {
      return false;
    }

    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

    var isAnyContentUpdatedAndRepublished = _urls.Any(url =>
        _sitemap.Descendants(ns + "url").Any(existingUrl =>
            existingUrl.Element(ns + "loc")?.Value == url.Loc &&
            existingUrl.Element(ns + "lastmod")?.Value != url.LastMod.ToString("yyyy-MM-ddTHH:mm:sszzz")));

    return isAnyContentUpdatedAndRepublished;
  }

  private bool isAnyNewContentAddedOrScheduled => _urls.Count != _sitemap?.Descendants("url").Count();
}
