namespace DesignPatternDemoComponents.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository()
        {
            // Inject DbContext here
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}