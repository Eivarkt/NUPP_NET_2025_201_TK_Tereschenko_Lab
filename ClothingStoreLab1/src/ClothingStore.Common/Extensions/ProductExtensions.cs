using ClothingStore.Common.Models;

namespace ClothingStore.Common.Extensions;

public static class ProductExtensions
{
    // Метод розширення
    public static string ToCatalogLine(this Product product)
    {
        return $"[{product.Id}] {product.GetShortInfo()}, stock: {product.StockQuantity}";
    }

    // Метод розширення
    public static bool IsPremiumPrice(this Product product, decimal threshold = 1500m)
    {
        return product.Price >= threshold;
    }
}
