namespace SharedModels;

public class TableOfContents
{
    public const int PageSize = 6;
    private readonly List<ContentMetaData> FullContents = new(121);
    private readonly List<AuthorMetaData> FullAuthors = new(1);
    private readonly List<SponsorMetaData> FullSponsors = new(1);

    public IReadOnlyList<ContentMetaData> AllContents => FullContents;
    public IReadOnlyList<AuthorMetaData> Authors => FullAuthors;
    public IReadOnlyList<SponsorMetaData> Sponsors => FullSponsors;

    public IReadOnlyList<ContentMetaData> Contents 
            => FullContents
                .Where(content => content.ModifiedOn.Date <= DateTime.Today.Date)
                .ToList();

    public IReadOnlyList<ContentMetaData> FilteredAndPagedContents(string? selectedContentType = null, int skip = 0, int take = PageSize) 
            => Contents
                .Where(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Channel.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(content => content.ModifiedOn)
                .ThenByDescending(content => content.CreatedOn)
                .Take(skip..take)
                .ToList();

    public IReadOnlyList<string> AvailableContentTypes 
            => FullContents
                .Select(content => content.Channel)
                .Distinct()
                .ToList();

    public int SelectedContentTypeTotalCount(string? selectedContentType = null) 
            => Contents
                .Where(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Channel.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase))
                .Count();

    public IReadOnlyList<ContentMetaData> ExceptAndPagedContents(string slug, int skip = 0, int take = PageSize) 
            => Contents
                .Where(content => !content.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(content => content.ModifiedOn)
                .ThenByDescending(content => content.CreatedOn)
                .Take(skip..take)
                .ToList();

    public ContentMetaData GetContentBySlug(string slug) 
            => FullContents
                .Single(content => content.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public ushort GetTotalContentsCountBySlug(string slug) 
            => (ushort)FullContents
                        .Count(content => content.Channel.Equals(GetContentBySlug(slug).Channel, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<ContentMetaData> GetContentsByChannel(string channel) 
            => [.. FullContents
                .Where(content => content.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(content => content.ModifiedOn)
                .ThenByDescending(content => content.CreatedOn)];

    public TableOfContents()
    {
        FullContents.AddRange(new BlazorLearningPath().FullContents);
        FullContents.AddRange(new DependencyInjectionLearningPath().FullContents);
        FullContents.AddRange(new DesignPatternLearningPath().FullContents);
        FullContents.AddRange(new HTTPClientLearningPath().FullContents);
        FullContents.AddRange(new LINQLearningPath().FullContents);
        FullContents.AddRange(new MiddlewareLearningPath().FullContents);
        FullContents.AddRange(new OOPSLearningPath().FullContents);
        FullContents.AddRange(new OWASPLearningPath().FullContents);
        FullContents.AddRange(new PythonLearningPath().FullContents);
        FullContents.AddRange(new ReportLearningPath().FullContents);
        FullContents.AddRange(new SignalRLearningPath().FullContents);
        FullContents.AddRange(new SOLIDLearningPath().FullContents);
        FullContents.AddRange(new TalkLearningPath().FullContents);
        FullContents.AddRange(new TDDLearningPath().FullContents);
        FullContents.AddRange(new WebAPILearningPath().FullContents);
        FullAuthors.AddRange(new Authors().FullAuthors);
        FullSponsors.AddRange(new Sponsors().FullSponsors);
    }
}