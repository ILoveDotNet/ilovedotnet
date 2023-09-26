namespace SharedModels;

public class SocialUrl
{
    public SocialUrl(SocialLink type, Uri url)
    {
        Type = type;
        Url = url;
    }

    public SocialLink Type { get; set; }
    public Uri Url { get; set; }
}