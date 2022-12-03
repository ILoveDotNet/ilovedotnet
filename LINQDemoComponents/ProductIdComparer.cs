using System.Diagnostics.CodeAnalysis;

namespace LINQDemoComponents;

public class ProductIdComparer : EqualityComparer<Product>
{
    public override bool Equals(Product? product, Product? anotherProduct)
    {
        return product?.Id == anotherProduct?.Id;
    }

    public override int GetHashCode([DisallowNull] Product product)
    {
        return product.Id.GetHashCode();
    }
}
