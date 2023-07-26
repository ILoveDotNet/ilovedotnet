namespace SharedModels;

public class Achievements 
{
    public List<GoogleSearchImpact> GoogleSearchImpacts { get; set; } = new();

    public Achievements()
    {
        GoogleSearchImpacts = new List<GoogleSearchImpact>
        {
            new() { Date = new DateOnly(2022, 9, 18), Clicks = 5 },
            new() { Date = new DateOnly(2022, 9, 26), Clicks = 50 },
            new() { Date = new DateOnly(2022, 10, 3), Clicks = 120 },
            new() { Date = new DateOnly(2022, 10, 14), Clicks = 250 },
            new() { Date = new DateOnly(2022, 10, 24), Clicks = 350 },
            new() { Date = new DateOnly(2022, 11, 1), Clicks = 400 },
            new() { Date = new DateOnly(2022, 11, 16), Clicks = 500 },
            new() { Date = new DateOnly(2023, 1, 17), Clicks = 600 },
            new() { Date = new DateOnly(2023, 1, 30), Clicks = 700 },
            new() { Date = new DateOnly(2023, 2, 20), Clicks = 800 },
            new() { Date = new DateOnly(2023, 2, 27), Clicks = 900 },
            new() { Date = new DateOnly(2023, 3, 12), Clicks = 1000 },
            new() { Date = new DateOnly(2023, 3, 26), Clicks = 1200 },
            new() { Date = new DateOnly(2023, 5, 7), Clicks = 1500 },
            new() { Date = new DateOnly(2023, 5, 31), Clicks = 1800 },
            new() { Date = new DateOnly(2023, 6, 8), Clicks = 2000 },
            new() { Date = new DateOnly(2023, 6, 16), Clicks = 2200 },
            new() { Date = new DateOnly(2023, 7, 7), Clicks = 2500 },
            new() { Date = new DateOnly(2023, 7, 25), Clicks = 3000 },
        };
    }    
}