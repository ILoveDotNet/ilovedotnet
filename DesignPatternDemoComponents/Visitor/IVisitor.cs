namespace DesignPatternDemoComponents.Visitor;

//Step 1: Define Element Interface
public interface IElement
{
  void Accept(IVisitor visitor);
}

//Step 2: Create Concrete Elements
public class FreeUser(string name) : IElement
{
  public string Name { get; set; } = name;
  public int ShippingCost { get; set; } = 40; // Default cost

  public void Accept(IVisitor visitor)
  {
    visitor.Visit(this);
  }
}

public class PrimeUser(string name) : IElement
{
  public string Name { get; set; } = name;
  public int ShippingCost { get; set; } = 0; // Free shipping for Prime users

  public void Accept(IVisitor visitor)
  {
    visitor.Visit(this);
  }
}

//Step 3: Define Visitor Interface
public interface IVisitor
{
  void Visit(FreeUser user);
  void Visit(PrimeUser user);
}

//Step 4: Create Concrete Visitor
public class ShippingCostVisitor : IVisitor
{
  public void Visit(FreeUser user)
  {
    Console.WriteLine($"Shipping cost for Free user {user.Name} is {user.ShippingCost}");
  }

  public void Visit(PrimeUser user)
  {
    Console.WriteLine($"Shipping cost for Prime user {user.Name} is {user.ShippingCost}");
  }
}

//Step 5: Implement Object Structure
public class UserContainer
{
  private readonly List<IElement> _users = new();

  public void AddUser(IElement user) => _users.Add(user);

  public void ApplyVisitor(IVisitor visitor)
  {
    foreach (var user in _users)
    {
      user.Accept(visitor);
    }
  }
}

//Step 6: Test the Implementation
public class VisitorDemo
{
  public static void Run()
  {
    var userContainer = new UserContainer();
    userContainer.AddUser(new FreeUser("John"));
    userContainer.AddUser(new PrimeUser("Doe"));

    var visitor = new ShippingCostVisitor();
    userContainer.ApplyVisitor(visitor);
  }
}
