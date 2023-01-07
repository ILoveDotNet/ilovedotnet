namespace LINQDemoComponents;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public string ProductColor { get; set; } = default!;
}
