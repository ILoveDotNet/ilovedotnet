namespace DesignPatternDemoComponents.Builder;

public class MiniBuilder : ICarBuilder
{
    private Car _car;

    public MiniBuilder()
    {
        _car = new Car("Mini");
    }

    public void BuildEngine()
    {
        _car.AddPart("Efficient Mini Engine");
    }

    public void BuildFrame()
    {
        _car.AddPart("Compact Mini Frame");
    }

    public Car GetCar()
    {
        return _car;
    }
}
