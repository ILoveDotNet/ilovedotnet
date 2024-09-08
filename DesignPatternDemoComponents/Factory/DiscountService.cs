namespace DesignPatternDemoComponents.Factory
{
    // Product
    public abstract class DiscountService
    {
        public abstract decimal DiscountPercentage { get; }
    }
}