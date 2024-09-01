namespace DesignPatternDemoComponents.Observer;

//Step - 4: Implement Subscriber

public class OrderViewModel : IOrderItemChangeListener
{

    private List<OrderItemViewModel> _orderItems = [];
    public IReadOnlyList<OrderItemViewModel> OrderItems => _orderItems;
    public decimal Total { get; private set; }
    
    public void AddItem()
    {
        var orderItem = new OrderItemViewModel 
        {
            Quantity = 1,
            UnitPrice = 10
        };
        orderItem.AddObserver(this);
        _orderItems.Add(orderItem);
        CalculateTotalAmount();
    }

    public void RemoveItem(OrderItemViewModel item)
    {
        item.RemoveObserver(this);
        _orderItems.Remove(item);
        CalculateTotalAmount();
    }
    
    public void CalculateTotalAmount()
    {
        Total = _orderItems.Sum(x => x.GetTotalPrice());
    }
    
    public void ReceiveOrderItemProcessedNotification()
    {
        CalculateTotalAmount();
    }
}