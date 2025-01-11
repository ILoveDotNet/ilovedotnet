using System.Diagnostics.CodeAnalysis;

namespace LINQDemoComponents;

public class ProductComparer : EqualityComparer<Product>
{
  public override bool Equals(Product? product, Product? anotherProduct)
  {
    return product?.Id == anotherProduct?.Id &&
            product?.Name == anotherProduct?.Name &&
            product?.Price == anotherProduct?.Price &&
            product?.Color == anotherProduct?.Color &&
            product?.Size == anotherProduct?.Size;
  }

  public override int GetHashCode([DisallowNull] Product product)
  {
    return $"{product.Id}{product.Name}{product.Price}{product.Color}{product.Size}".GetHashCode();
  }
}
