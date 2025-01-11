namespace DesignPatternDemoComponents.Builder;

// Product
public class Car(string type)
{
  private readonly string _type = type;
  private readonly List<string> _parts = [];

  public void AddPart(string part)
  {
    _parts.Add(part);
  }

  public override string ToString()
  {
    return $"{_type} - Parts: {string.Join(", ", _parts)}";
  }
}
