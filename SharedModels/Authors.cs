namespace SharedModels;

public class Authors
{
    public readonly List<AuthorMetaData> FullAuthors = new(1);

    public Authors()
    {
        FullAuthors =
        new(1)
        {
            new AuthorMetaData
            {
                Order = 1,
                Name = "Abdul Rahman",
                Description = "I'm the founder and solo developer and author of I ❤️ .NET",
                ImageUrl = "image/talks/blazor-spa-from-aspnet-family.png",
                IsMVP = true,
                SocialUrls = new List<SocialUrl>
                {
                    new(SocialLink.LinkedIn, new Uri("https://www.linkedin.com/in/thebhai")),
                    new(SocialLink.GitHub, new Uri("https://www.github.com/fingers10")),
                    new(SocialLink.StackOverflow, new Uri("https://www.stackoverflow.com/fingers10")),
                    new(SocialLink.Microsoft, new Uri("https://mvp.microsoft.com/fingers10")),
                    new(SocialLink.Blog, new Uri("https://ilovedotnet.org")),
                    new(SocialLink.YouTube, new Uri("https://www.youtube.com/channel/UC4xKdmAXFh4ACyhpiQ_3qBw")),
                    new(SocialLink.Instagram, new Uri("https://www.instagram.com/fingers10"))
                }
            },
            new AuthorMetaData
            {
                Order = 2,
                Name = "Regina Sharon",
                Description = "Graphic Designer",
                ImageUrl = "image/talks/blazor-spa-from-aspnet-family.png",
                IsMVP = false,
                SocialUrls = new List<SocialUrl>
                {
                    new(SocialLink.LinkedIn, new Uri("https://www.linkedin.com/in/thebhai"))
                }
            },
        };
    }
}