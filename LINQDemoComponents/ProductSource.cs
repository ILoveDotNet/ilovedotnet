namespace LINQDemoComponents;

public class ProductSource
{
    private readonly List<Product> FullProducts = new(6);
    public IReadOnlyList<Product> Products => FullProducts.ToList();

    public ProductSource()
    {
        FullProducts =
            new()
            {
                new Product
                {
                    Id = 1,
                    Name = "Shirt",
                    Color = "Black",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 2,
                    Name = "Shirt",
                    Color = "Red",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 3,
                    Name = "Shirt",
                    Color = "Black",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 4,
                    Name = "Shirt",
                    Color = "Red",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 5,
                    Name = "Shirt",
                    Color = "Brown",
                    Price = 1000m,
                    Size = "58"
                },
                new Product
                {
                    Id = 6,
                    Name = "Shirt",
                    Color = "White",
                    Price = 1000m,
                    Size = "58"
                }
            };
    }
}