using SharedModels;
using SitemapGenerator;
using System.Xml.Linq;

namespace VectorSearchIndexGenerator.Tests;

public sealed class SitemapTests : IDisposable
{
  private readonly string _directory = Path.Combine(AppContext.BaseDirectory, $"ilovedotnet-sitemap-{Guid.NewGuid():N}");

  [Fact]
  public void GenerateSitemap_DoesNotRewriteUnchangedNamespacedOutput()
  {
    Directory.CreateDirectory(_directory);
    var path = Path.Combine(_directory, "sitemap.xml");
    var sitemap = new Sitemap(new TableOfContents(), "AI");
    sitemap.GenerateSitemap(path);
    var original = File.ReadAllBytes(path);
    var timestamp = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    File.SetLastWriteTimeUtc(path, timestamp);

    new Sitemap(new TableOfContents(), "AI").GenerateSitemap(path);

    Assert.Equal(original, File.ReadAllBytes(path));
    Assert.Equal(timestamp, File.GetLastWriteTimeUtc(path));
  }

  [Fact]
  public void GenerateSitemap_DetectsChangedUrlsEvenWhenCountIsUnchanged()
  {
    Directory.CreateDirectory(_directory);
    var path = Path.Combine(_directory, "sitemap.xml");
    var contents = new TableOfContents();
    new Sitemap(contents, "AI").GenerateSitemap(path);
    var article = contents.GetContentsByChannel("AI")[0];
    var oldSlug = article.Slug;
    article.Slug = "changed-slug";

    new Sitemap(contents, "AI").GenerateSitemap(path);

    var updated = File.ReadAllText(path);
    Assert.Contains("/blogs/changed-slug/", updated);
    Assert.DoesNotContain($"/blogs/{oldSlug}/", updated);
  }

  [Fact]
  public void GenerateSitemap_RemovesDuplicateUrlsFromExistingOutput()
  {
    Directory.CreateDirectory(_directory);
    var path = Path.Combine(_directory, "sitemap.xml");
    var contents = new TableOfContents();
    var sitemap = new Sitemap(contents, "AI");
    sitemap.GenerateSitemap(path);
    var document = XDocument.Load(path);
    document.Root!.Add(new XElement(document.Root.Elements().First()));
    document.Save(path);

    sitemap.GenerateSitemap(path);

    Assert.Equal(contents.GetContentsByChannel("AI").Count, XDocument.Load(path).Root!.Elements().Count());
  }

  public void Dispose()
  {
    if (Directory.Exists(_directory))
    {
      Directory.Delete(_directory, recursive: true);
    }
  }
}
