namespace DesignPatternDemoComponents.Decorator;

// Decorator
public abstract class RepositoryDecoratorBase(IRepository repository) : IRepository
{
  public virtual string ReadData()
  {
    return repository.ReadData();
  }
}
