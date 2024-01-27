namespace DesignPatternDemoComponents.Builder;

// Product
public class Car
{
    private string _type;
    private List<string> _parts = new List<string>();

    public Car(string type)
    {
        _type = type;
    }

    public void AddPart(string part)
    {
        _parts.Add(part);
    }

    public override string ToString()
    {
        return $"{_type} - Parts: {string.Join(", ", _parts)}";
    }
}
