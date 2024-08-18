namespace DesignPatternDemoComponents.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected UnitOfWork()
        {
            // Inject DbContext here
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            //context.ChangeTracker.Clear();
            throw new NotImplementedException();
        }
    }

}