namespace DesignPatternDemoComponents.UnitOfWork;

public interface IUnitOfWork
{
  Task CommitAsync();
  Task RollbackAsync();
}
