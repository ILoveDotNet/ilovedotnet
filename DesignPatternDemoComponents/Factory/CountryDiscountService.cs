namespace DesignPatternDemoComponents.Factory
{
    // Concrete Product 1
    public class CountryDiscountService(string country) : DiscountService
    {
        public override decimal DiscountPercentage =>
            country == "India" ? 0.20m : 0.10m;
    }
}