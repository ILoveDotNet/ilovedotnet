namespace LINQDemoComponents;

public class ProductSales 
{
    public Product Product { get; set; } = default!;
    public IEnumerable<Sale> Sales { get; set; } = default!;
}