namespace DesignPatternDemoComponents.Observer;

//Step -2: Implement Publisher

public class OrderItemViewModel : OrderItemChangeNotifier
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;
        NotifyOrderItemProcessed();
    }
    
    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        UnitPrice = newUnitPrice;
        NotifyOrderItemProcessed();
    }
    
    public decimal GetTotalPrice()
    {
        return Quantity * UnitPrice;
    }
}