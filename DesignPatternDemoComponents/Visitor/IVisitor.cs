namespace DesignPatternDemoComponents.Visitor;

//Step 3: Define Visitor Interface
public interface IVisitor
{
  string Visit(FreeUser user);
  string Visit(PrimeUser user);
}
