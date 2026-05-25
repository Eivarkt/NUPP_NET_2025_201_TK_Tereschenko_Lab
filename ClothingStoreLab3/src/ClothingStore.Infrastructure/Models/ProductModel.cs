namespace ClothingStore.Infrastructure.Models;

public abstract class ProductModel : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Material { get; set; } = string.Empty;
    public int StockQuantity { get; set; }

    public ICollection<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
    public ICollection<ProductTagModel> Tags { get; set; } = new List<ProductTagModel>();

    public virtual string GetShortInfo()
    {
        return $"{Name} | {Color} | {Size} | {Price} UAH";
    }
}
