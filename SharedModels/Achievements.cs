namespace SharedModels;

public class Achievements 
{
    public List<GoogleSearchImpact> GoogleSearchImpacts { get; set; } = [];
    public List<GoogleSearchImpression> GoogleSearchImpressions { get; set; } = [];

    public Achievements()
    {
        GoogleSearchImpacts =
        [
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
        ];

        GoogleSearchImpressions =
        [
            new() { Date = new DateOnly(2022, 11, 30), Impressions = 17300 },
            new() { Date = new DateOnly(2022, 12, 31), Impressions = 16900 },
            new() { Date = new DateOnly(2023, 1, 31), Impressions = 23600 },
            new() { Date = new DateOnly(2023, 2, 28), Impressions = 30300 },
            new() { Date = new DateOnly(2023, 3, 31), Impressions = 52900 },
            new() { Date = new DateOnly(2023, 4, 30), Impressions = 54600 },
            new() { Date = new DateOnly(2023, 5, 31), Impressions = 80800 },
            new() { Date = new DateOnly(2023, 6, 30), Impressions = 104000 },
            new() { Date = new DateOnly(2023, 7, 31), Impressions = 123000 },
            new() { Date = new DateOnly(2023, 8, 31), Impressions = 97400 },
            new() { Date = new DateOnly(2023, 9, 30), Impressions = 68900 },
            new() { Date = new DateOnly(2023, 10, 31), Impressions = 78900 },
            new() { Date = new DateOnly(2023, 11, 30), Impressions = 90500 },
            new() { Date = new DateOnly(2023, 12, 31), Impressions = 75900 },
            new() { Date = new DateOnly(2024, 1, 31), Impressions = 84600 },
            new() { Date = new DateOnly(2024, 2, 29), Impressions = 89600 },
            new() { Date = new DateOnly(2024, 3, 31), Impressions = 102000 },
            new() { Date = new DateOnly(2024, 4, 30), Impressions = 91300 },
            new() { Date = new DateOnly(2024, 5, 31), Impressions = 86300 },
            new() { Date = new DateOnly(2024, 6, 30), Impressions = 82400 },
            new() { Date = new DateOnly(2024, 7, 31), Impressions = 83000 },
            new() { Date = new DateOnly(2024, 8, 31), Impressions = 79800 },
            new() { Date = new DateOnly(2024, 9, 30), Impressions = 61900 },
            new() { Date = new DateOnly(2024, 10, 31), Impressions = 79700 },
            new() { Date = new DateOnly(2024, 11, 30), Impressions = 79200 },
        ];
    }    
}
