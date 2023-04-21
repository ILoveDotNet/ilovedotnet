namespace SharedModels;

public class TableOfContents
{
    private readonly List<ContentMetaData> FullContents = new(67);
    public IReadOnlyList<ContentMetaData> Contents => FullContents.Where(content => content.CreatedOn.Date <= DateTime.Today.Date).ToList();

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