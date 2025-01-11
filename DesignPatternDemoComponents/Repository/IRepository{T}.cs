namespace DesignPatternDemoComponents.Repository;

public interface IRepository<T>
{
  Task<T> GetByIdAsync(Guid id);
  Task<List<T>> GetAllAsync();
  T Add(T entity);
  void Update(T entity);
  void Delete(Guid id);
}
