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
                },
                new Product
                {
                    Id = 3,
                    Name = "HL Road Frame",
                    Color = "Black",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 4,
                    Name = "HL Road Frame",
                    Color = "Red",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 5,
                    Name = "HL Road Frame",
                    Color = "Brown",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 6,
                    Name = "HL Road Frame",
                    Color = "White",
                    Price = 1000m,
                    Size = "58"
                }
            };
    }
}