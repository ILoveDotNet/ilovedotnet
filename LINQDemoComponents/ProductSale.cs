namespace LINQDemoComponents;

public class ProductSale 
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string? Color { get; set; }
    public decimal? Price { get; set; }
    public int? SaleId { get; set; }
}