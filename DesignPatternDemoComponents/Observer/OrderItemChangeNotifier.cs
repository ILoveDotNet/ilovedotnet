namespace DesignPatternDemoComponents.Observer;

//Step - 1: Define Publisher
public abstract class OrderItemChangeNotifier()
{
    private readonly List<IOrderItemChangeListener> _observers = [];

    public void AddObserver(IOrderItemChangeListener observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IOrderItemChangeListener observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyOrderItemProcessed()
    {
        foreach (var observer in _observers)
        {
            observer.ReceiveOrderItemProcessedNotification();
        }
    }
}
