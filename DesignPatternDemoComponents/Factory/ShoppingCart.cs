namespace DesignPatternDemoComponents.Factory;

public class ShoppingCart(DiscountFactory discountFactory)
{
  public decimal Total { get; private set; } = 100;

  public decimal CalculateTotal()
  {
    Total -= Total * discountFactory.CreateDiscountService().DiscountPercentage;

    return Total;
  }
}
