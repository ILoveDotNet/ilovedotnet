namespace SharedModels;

public class AdSpaceService 
{
    private readonly Random random = new();

    public readonly List<int> AdSpace = new();

    public AdSpaceService(TableOfContents tableOfContents)
    {
        for (int i = 4; i <= tableOfContents.Contents.Count - 4; i += 4)
        {
            AdSpace.Add(random.Next(i, i + 5));
        }
    }
}