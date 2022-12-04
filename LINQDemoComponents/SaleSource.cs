namespace LINQDemoComponents;

public class SaleSource
{
    private readonly List<Sale> FullSales = new(2);
    public IReadOnlyList<Sale> Sales => FullSales.ToList();

    public SaleSource()
    {
        FullSales =
            new()
            {
                new Sale
                {
                    ProductId = 1,
                    Price = 1000m
                },
                new Sale
                {
                    ProductId = 2,
                    Price = 1000m
                }
            };
    }
}