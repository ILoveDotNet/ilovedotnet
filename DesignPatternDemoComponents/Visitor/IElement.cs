namespace DesignPatternDemoComponents.Visitor;

//Step 1: Define Element Interface
public interface IElement
{
  string Accept(IVisitor visitor);
}
