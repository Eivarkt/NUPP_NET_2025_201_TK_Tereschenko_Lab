namespace ClothingStore.Infrastructure.Models;

public class OrderModel : EntityBase
{
    public int CustomerId { get; set; }
    public string Status { get; set; } = "Created";
    public decimal TotalAmount { get; set; }

    public CustomerModel? Customer { get; set; }
    public ICollection<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();

    public void RecalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.UnitPrice * item.Quantity);
    }
}
