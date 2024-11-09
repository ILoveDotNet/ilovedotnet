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
                RedirectUrl = "https://dometrain.com?ref=ilovedotnet&promotion=website"
            }
        ];
    }
}