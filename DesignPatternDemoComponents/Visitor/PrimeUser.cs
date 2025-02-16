namespace DesignPatternDemoComponents.Visitor;

public class PrimeUser(string name) : IElement
{
  public string Name { get; set; } = name;
  public int ShippingCost { get; set; } = 0; // Free shipping for Prime users

  public string Accept(IVisitor visitor)
  {
    return visitor.Visit(this);
  }
}
