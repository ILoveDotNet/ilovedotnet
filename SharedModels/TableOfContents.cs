namespace SharedModels;

public class TableOfContents
{
  public const int PageSize = 6;
  private readonly List<ContentMetaData> _fullContents = new(165);
  private readonly List<AuthorMetaData> _fullAuthors = new(1);
  private readonly List<SponsorMetaData> _fullSponsors = new(1);

  public IReadOnlyList<ContentMetaData> AllContents => _fullContents;
  public IReadOnlyList<AuthorMetaData> Authors => _fullAuthors;
  public IReadOnlyList<SponsorMetaData> Sponsors => _fullSponsors;

  public IReadOnlyList<ContentMetaData> Contents
          => [.. _fullContents.Where(content => content.ModifiedOn.Date <= DateTime.Today.Date)];

  public IReadOnlyList<ContentMetaData> FilteredAndPagedContents(string? selectedContentType = null, int skip = 0, int take = PageSize)
          => [.. Contents
              .Where(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Channel.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase))
              .OrderByDescending(content => content.ModifiedOn)
              .ThenByDescending(content => content.CreatedOn)
              .Take(skip..take)];

  public IReadOnlyList<string> AvailableContentTypes
          => [.. _fullContents
              .Select(content => content.Channel)
              .OrderBy(channel => channel)
              .Distinct()];

  public int SelectedContentTypeTotalCount(string? selectedContentType = null)
          => Contents
              .Count(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Channel.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase));

  public IReadOnlyList<ContentMetaData> ExceptAndPagedContents(string slug, int skip = 0, int take = PageSize)
          => [.. Contents
              .Where(content => !content.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))
              .OrderByDescending(content => content.ModifiedOn)
              .ThenByDescending(content => content.CreatedOn)
              .Take(skip..take)];

  public ContentMetaData GetContentBySlug(string slug)
          => _fullContents
              .Single(content => content.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

  public ushort GetTotalContentsCountBySlug(string slug)
          => (ushort)_fullContents
                      .Count(content => content.Channel.Equals(GetContentBySlug(slug).Channel, StringComparison.OrdinalIgnoreCase));

  public IReadOnlyList<ContentMetaData> GetContentsByChannel(string channel)
          => [.. _fullContents
                .Where(content => content.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(content => content.ModifiedOn)
                .ThenByDescending(content => content.CreatedOn)];

  public TableOfContents()
  {
    _fullContents.AddRange(new AILearningPath().FullContents);
    _fullContents.AddRange(new BlazorLearningPath().FullContents);
    _fullContents.AddRange(new DatabaseLearningPath().FullContents);
    _fullContents.AddRange(new DependencyInjectionLearningPath().FullContents);
    _fullContents.AddRange(new DesignPatternLearningPath().FullContents);
    _fullContents.AddRange(new HTTPClientLearningPath().FullContents);
    _fullContents.AddRange(new JSONLearningPath().FullContents);
    _fullContents.AddRange(new LINQLearningPath().FullContents);
    _fullContents.AddRange(new MAUILearningPath().FullContents);
    _fullContents.AddRange(new MCPLearningPath().FullContents);
    _fullContents.AddRange(new MiddlewareLearningPath().FullContents);
    _fullContents.AddRange(new MLNETLearningPath().FullContents);
    _fullContents.AddRange(new MSBuildLearningPath().FullContents);
    _fullContents.AddRange(new OOPSLearningPath().FullContents);
    _fullContents.AddRange(new OWASPLearningPath().FullContents);
    _fullContents.AddRange(new PythonLearningPath().FullContents);
    _fullContents.AddRange(new RegexLearningPath().FullContents);
    _fullContents.AddRange(new ReportLearningPath().FullContents);
    _fullContents.AddRange(new SecurityLearningPath().FullContents);
    _fullContents.AddRange(new SignalRLearningPath().FullContents);
    _fullContents.AddRange(new SOLIDLearningPath().FullContents);
    _fullContents.AddRange(new TalkLearningPath().FullContents);
    _fullContents.AddRange(new TDDLearningPath().FullContents);
    _fullContents.AddRange(new TestingLearningPath().FullContents);
    _fullContents.AddRange(new WebAPILearningPath().FullContents);
    _fullAuthors.AddRange(new Authors().FullAuthors);
    _fullSponsors.AddRange(new Sponsors().FullSponsors);
  }
}
