namespace SharedModels;

public class SocialUrl(SocialLink type, Uri url)
{
  public SocialLink Type { get; set; } = type;
  public Uri Url { get; set; } = url;
}
