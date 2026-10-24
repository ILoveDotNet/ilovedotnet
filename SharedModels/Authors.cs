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
                Role = "Creator of I ❤️ .NET",
                Introduction = "Creator of I ❤️ .NET and Microsoft MVP with 10+ years of .NET engineering experience. Shares practical lessons on architecture, performance, platform engineering, and modernization.",
                IndustryExperienceYears = 10,
                Description = "Abdul Rahman is the creator and solo developer of I ❤️ .NET. He has over a decade of experience as a .NET developer and engineering leader, working on performance tuning, solution architecture, platform engineering, legacy modernization, and end-to-end application delivery. He also contributes to open source and has experience consulting, mentoring, leading teams, and connecting technical work to business outcomes. Microsoft recognized his community contributions with an MVP award. Through this site, he shares practical .NET lessons from his work.",
                ImageUrl = "image/authors/abdul rahman.webp",
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
                ],
                Badges =
                [
                    "af85bef9-38af-40c2-afbc-68ca189c06aa",
                    "4abd7deb-67a1-47db-b16b-f9860ebdd765",
                    "2e6468ea-beae-4c9e-a032-67d2306933bb",
                    "d435946f-0a4c-4d9d-b4b8-f0e3bf302850"
                ]
            },
    };
  }
}
