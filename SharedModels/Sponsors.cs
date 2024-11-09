namespace SharedModels;

public class Sponsors
{
    public readonly List<SponsorMetaData> FullSponsors = new(1);

    public Sponsors()
    {
        FullSponsors =
        [
            new SponsorMetaData
            {
                Name = "Dometrain",
                LogoUrl = "image/sponsors/dometrain.jpg",
                RedirectUrl = "https://dometrain.com?ref=ilovedotnet&promotion=website",
                Start = new DateTime(2024, 11, 10, 0, 0, 0, DateTimeKind.Utc),
                End = new DateTime(2025, 11, 10, 0, 0, 0, DateTimeKind.Utc)
            }
        ];
    }
}