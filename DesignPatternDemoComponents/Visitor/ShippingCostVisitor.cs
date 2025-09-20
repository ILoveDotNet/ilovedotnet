namespace DesignPatternDemoComponents.Visitor;

//Step 4: Create Concrete Visitor
public class ShippingCostVisitor : IVisitor
{
  public string Visit(FreeUser user)
  {
    return $"Shipping cost for Free user {user.Name} is {user.ShippingCost}";
  }

  public string Visit(PrimeUser user)
  {
    return $"Shipping cost for Prime user {user.Name} is {user.ShippingCost}";
  }
}
