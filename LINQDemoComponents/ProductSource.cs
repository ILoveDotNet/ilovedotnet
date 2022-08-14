namespace LINQDemoComponents;

internal class ProductSource
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
                    Name = "IPhone",
                    Price = 100000
                },
                new Product
                {
                    Name = "Google",
                    Price = 70000
                }
            };
    }
}