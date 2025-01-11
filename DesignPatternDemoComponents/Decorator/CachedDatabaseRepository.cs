namespace DesignPatternDemoComponents.Decorator;

// Concrete Decorator
public class CachedDatabaseRepository(IRepository repository) : RepositoryDecoratorBase(repository)
{
  public override string ReadData()
  {
    Random gen = new();
    int prob = gen.Next(100);

    if (prob < 20)
    {
      return "Data from Cache";
    }

    return base.ReadData();
  }
}
