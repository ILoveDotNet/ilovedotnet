namespace DesignPatternDemoComponents.Visitor;

//Step 2: Create Concrete Elements
public class FreeUser(string name) : IElement
{
  public string Name { get; set; } = name;
  public int ShippingCost { get; set; } = 40; // Default cost

  public string Accept(IVisitor visitor)
  {
    return visitor.Visit(this);
  }
}
