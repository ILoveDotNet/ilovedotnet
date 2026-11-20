namespace BaseComponents;

public sealed record ArticleAdContext(string? MidArticleId);

public enum ArticleAdPosition
{
  Sidebar,
  MidArticle,
  EndOfArticle
}
