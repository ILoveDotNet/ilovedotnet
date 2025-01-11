namespace SharedModels;

public class AdSpaceService
{
  private readonly Random random = new();

  private readonly List<int> AdSpace = [];

  public AdSpaceService(TableOfContents tableOfContents)
  {
    for (int i = 2; i <= tableOfContents.Contents.Count - 2; i += 2)
    {
      AdSpace.Add(random.Next(i, i + 3));
    }
  }

  public bool CanDisplayAd(int index)
  {
    return AdSpace.Contains(index);
  }
}
