namespace DesignPatternDemoComponents.Factory;

// Concrete Creator 1
public class CountryDiscountFactory(string country) : DiscountFactory
{
  public override DiscountService CreateDiscountService()
  {
    return new CountryDiscountService(country);
  }
}
