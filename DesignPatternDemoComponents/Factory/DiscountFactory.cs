namespace DesignPatternDemoComponents.Factory;

// Creator
public abstract class DiscountFactory
{
  public abstract DiscountService CreateDiscountService();
}
