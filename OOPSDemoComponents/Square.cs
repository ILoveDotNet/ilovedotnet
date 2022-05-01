namespace OOPSDemoComponents;

public class Square
{
    public Square(IEnumerable<Edge> edges)
    {
        Edges = edges;
    }

    public IEnumerable<Edge> Edges { get; }
}
