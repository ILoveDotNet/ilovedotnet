namespace SharedModels;

public class TableOfContents
{
    public const int PageSize = 6;
    private readonly List<ContentMetaData> FullContents = new(80);
    public IReadOnlyList<ContentMetaData> Contents 
            => FullContents
                .Where(content => content.CreatedOn.Date <= DateTime.Today.Date)
                .ToList();

    public IReadOnlyList<ContentMetaData> FilteredAndPagedContents(string? selectedContentType = null, int skip = 0, int take = PageSize) 
            => Contents
                .Where(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Type.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(content => content.CreatedOn)
                .Take(skip..take)
                .ToList();

    public IReadOnlyList<string> AvailableContentTypes 
            => FullContents
                .Select(content => content.Type)
                .Distinct()
                .ToList();

    public int SelectedContentTypeTotalCount(string? selectedContentType = null) 
            => Contents
                .Where(content => string.IsNullOrWhiteSpace(selectedContentType) || content.Type.Equals(selectedContentType, StringComparison.OrdinalIgnoreCase))
                .Count();

    public TableOfContents()
    {
        FullContents.AddRange(new BlazorLearningPath().FullContents);
        FullContents.AddRange(new DependencyInjectionLearningPath().FullContents);
        FullContents.AddRange(new DesignPatternLearningPath().FullContents);
        FullContents.AddRange(new LINQLearningPath().FullContents);
        FullContents.AddRange(new MiddlewareLearningPath().FullContents);
        FullContents.AddRange(new OOPSLearningPath().FullContents);
        FullContents.AddRange(new PythonLearningPath().FullContents);
        FullContents.AddRange(new ReportLearningPath().FullContents);
        FullContents.AddRange(new SOLIDLearningPath().FullContents);
        FullContents.AddRange(new TalkLearningPath().FullContents);
        FullContents.AddRange(new TDDLearningPath().FullContents);
        FullContents.AddRange(new WebAPILearningPath().FullContents);
    }
}