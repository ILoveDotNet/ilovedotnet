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
                SocialUrls =
                [
                    new(SocialLink.LinkedIn, new Uri("https://www.linkedin.com/in/thebhai")),
                    new(SocialLink.WhatsApp, new Uri("https://whatsapp.com/channel/0029VaAGMV2LtOj5S5MHd23h")),
                    new(SocialLink.GitHub, new Uri("https://www.github.com/fingers10")),
                    new(SocialLink.StackOverflow, new Uri("https://stackoverflow.com/users/10851213/fingers10")),
                    new(SocialLink.Microsoft, new Uri("https://mvp.microsoft.com/en-US/MVP/profile/b177819f-95fb-ed11-8f6d-000d3a560942")),
                    new(SocialLink.Blog, new Uri("https://ilovedotnet.org")),
                    new(SocialLink.YouTube, new Uri("https://www.youtube.com/@ilovedotnet")),
                    new(SocialLink.Instagram, new Uri("https://www.instagram.com/abdulrahman.smsi")),
                    new(SocialLink.Mastodon, new Uri("https://mastodon.social/@thebhai")),
                    new(SocialLink.BuyMeACoffee, new Uri("https://www.buymeacoffee.com/thebhai")),
                ]
            },
            new AuthorMetaData
            {
                Order = 2,
                Name = "Regina Sharon",
                Description = "Graphic Designer",
                ImageUrl = "image/talks/blazor-spa-from-aspnet-family.png",
                IsMVP = false,
                SocialUrls =
                [
                    new(SocialLink.LinkedIn, new Uri("https://www.linkedin.com/in/regina-sharon"))
                ]
            },
    };
  }
}
