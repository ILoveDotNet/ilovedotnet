using DesignPatternDemoComponents.Repository;

namespace DesignPatternDemoComponents.UnitOfWork
{
    public class CreateOrderWithOrderLinesUnitOfWork : UnitOfWork
    {
        public IRepository<Order> OrderRepository { get; }
        public IOrderLineRepository OrderLineRepository { get; }

        public CreateOrderWithOrderLinesUnitOfWork(IRepository<Order> orderRepo, IOrderLineRepository orderLineRepo)
            : base()
        {
            OrderRepository = orderRepo;
            OrderLineRepository = orderLineRepo;
        }
    }

}