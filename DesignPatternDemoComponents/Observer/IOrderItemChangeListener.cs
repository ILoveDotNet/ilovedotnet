namespace DesignPatternDemoComponents.Observer;

//Step - 3: Define Subscriber

public interface IOrderItemChangeListener
{
    void ReceiveOrderItemProcessedNotification();
}
