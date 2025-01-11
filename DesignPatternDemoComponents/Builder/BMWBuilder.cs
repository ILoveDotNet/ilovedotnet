namespace DesignPatternDemoComponents.Builder;

// Concrete Builders
public class BMWBuilder : ICarBuilder
{
  private readonly Car _car;

  public BMWBuilder()
  {
    _car = new Car("BMW");
  }

  public void BuildEngine()
  {
    _car.AddPart("High-performance BMW Engine");
  }

  public void BuildFrame()
  {
    _car.AddPart("Sturdy BMW Frame");
  }

  public Car GetCar()
  {
    return _car;
  }
}
