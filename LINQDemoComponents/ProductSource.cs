namespace LINQDemoComponents;

public class ProductSource
{
    private readonly List<Product> FullProducts = new(31);
    public IReadOnlyList<Product> Products => FullProducts.ToList();

    public ProductSource()
    {
        FullProducts =
            new()
            {
                new Product
                {
                    Id = 1,
                    Name = "HL Road Frame",
                    Color = "Black",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 2,
                    Name = "HL Road Frame",
                    Color = "Red",
                    Price = 1000m,
                    Size = "58"
                }
            };
    }
}