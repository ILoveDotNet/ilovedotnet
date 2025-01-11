namespace DesignPatternDemoComponents.Decorator;

// Concrete Component
public class DatabaseRepository : IRepository
{
  public string ReadData()
  {
    return "Data From Database";
  }
}
