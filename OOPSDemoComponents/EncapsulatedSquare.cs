namespace OOPSDemoComponents;

public class EncapsulatedSquare
{
    public EncapsulatedSquare(IEnumerable<Edge> edges)
    {
        if (edges.Count() != 4)
            throw new Exception("Square must have 4 edges");

        Edges = edges;
    }

    public IEnumerable<Edge> Edges { get; }
}
