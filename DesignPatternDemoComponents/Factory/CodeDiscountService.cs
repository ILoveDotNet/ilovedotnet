namespace DesignPatternDemoComponents.Factory;

// Concrete Product 2
public class CodeDiscountService(string code) : DiscountService
{
  public override decimal DiscountPercentage =>
      string.Equals(code, "INDIA15", StringComparison.OrdinalIgnoreCase) ? 0.15m : 0m;
}
