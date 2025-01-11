namespace DesignPatternDemoComponents.Builder;

// Director
public class Garage
{
  private ICarBuilder? _builder;

  public void Construct(ICarBuilder builder)
  {
    _builder = builder;
    _builder.BuildEngine();
    _builder.BuildFrame();
  }
}
