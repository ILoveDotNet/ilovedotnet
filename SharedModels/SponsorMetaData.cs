namespace SharedModels;

public class SponsorMetaData
{
    public required string Name { get; set; }
    public required string RedirectUrl { get; set; }
    public required string LogoUrl { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }

    public bool IsActive => DateTime.UtcNow >= Start && DateTime.UtcNow <= End;
}