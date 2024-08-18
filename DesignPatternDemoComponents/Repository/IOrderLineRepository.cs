namespace DesignPatternDemoComponents.Repository
{
    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        Task<IEnumerable<OrderLine>> GetAllByOrderId(int orderId);
    }
}