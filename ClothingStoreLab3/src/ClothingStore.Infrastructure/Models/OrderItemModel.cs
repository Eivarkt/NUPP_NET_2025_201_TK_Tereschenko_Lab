namespace ClothingStore.Infrastructure.Models;

public class OrderItemModel : EntityBase
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public OrderModel? Order { get; set; }
    public ProductModel? Product { get; set; }
}
