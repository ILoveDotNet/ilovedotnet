namespace DesignPatternDemoComponents.Repository
{
    public class OrderLineRepository() : GenericRepository<OrderLine>(), IOrderLineRepository
    {
        public Task<IEnumerable<OrderLine>> GetAllByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}