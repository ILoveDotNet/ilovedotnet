namespace LINQDemoComponents;

public class ProductSource
{
  private readonly List<Product> FullProducts = new(6);
  public IReadOnlyList<Product> Products => FullProducts.AsReadOnly();

  public ProductSource()
  {
    FullProducts =
        [
                new Product
                {
                    Id = 1,
                    Name = "Shirt",
                    Color = "Black",
                    Price = 1000m,
                    Quantity = 1,
                    Size = "18"
                },
                new Product
                {
                    Id = 2,
                    Name = "Shirt",
                    Color = "Red",
                    Price = 1500m,
                    Quantity = 2,
                    Size = "28"
                },
                new Product
                {
                    Id = 3,
                    Name = "Shirt",
                    Color = "Black",
                    Price = 2000m,
                    Quantity = 3,
                    Size = "38"
                },
                new Product
                {
                    Id = 4,
                    Name = "Shirt",
                    Color = "Red",
                    Price = 2500m,
                    Quantity = 4,
                    Size = "48"
                },
                new Product
                {
                    Id = 5,
                    Name = "Shirt",
                    Color = "Brown",
                    Price = 3000m,
                    Quantity = 5,
                    Size = "58"
                },
                new Product
                {
                    Id = 6,
                    Name = "Shirt",
                    Color = "White",
                    Price = 3500m,
                    Quantity = 6,
                    Size = "68"
                }
        ];
  }
}
