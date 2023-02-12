namespace LINQDemoComponents;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Color { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; } = default!;
}