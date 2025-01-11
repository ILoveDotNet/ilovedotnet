using DesignPatternDemoComponents.Repository;

namespace DesignPatternDemoComponents.UnitOfWork;

public class CreateOrderWithOrderLinesUnitOfWork(IRepository<Order> orderRepo, IOrderLineRepository orderLineRepo) : UnitOfWork()
{
  public IRepository<Order> OrderRepository { get; } = orderRepo;
  public IOrderLineRepository OrderLineRepository { get; } = orderLineRepo;
}
