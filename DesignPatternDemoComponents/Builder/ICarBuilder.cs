namespace DesignPatternDemoComponents.Builder;

public interface ICarBuilder
{
  void BuildEngine();
  void BuildFrame();
  Car GetCar();
}
