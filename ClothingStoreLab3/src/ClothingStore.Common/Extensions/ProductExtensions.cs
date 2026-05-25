using ClothingStore.Common.Models;

namespace ClothingStore.Common.Extensions;

public static class ProductExtensions
{
    public static bool IsPremiumPrice(this Product product)
    {
        return product.Price >= 1500m;
    }

    public static string ToCatalogLine(this Product product)
    {
        return $"{product.Name} | {product.Color} | {product.Size} | {product.Price} {Product.DefaultCurrency}";
    }
}
