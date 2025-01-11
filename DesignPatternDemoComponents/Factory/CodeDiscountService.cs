namespace DesignPatternDemoComponents.Factory;

// Concrete Product 2
public class CodeDiscountService(string code) : DiscountService
{
  public override decimal DiscountPercentage => 0.15m;
}
