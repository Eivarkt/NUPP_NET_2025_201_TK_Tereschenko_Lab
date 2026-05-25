using ClothingStore.Common.Delegates;
using ClothingStore.Common.Enums;

namespace ClothingStore.Common.Models;

public class Order
{
    private readonly List<Product> _products = new();

    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Created;
    }

    public Order(Customer customer) : this()
    {
        Customer = customer;
    }

    public Guid Id { get; set; }
    public Customer? Customer { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public event OrderStatusChangedHandler? StatusChanged;

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalAmount()
    {
        return _products.Sum(product => product.Price);
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        OrderStatus oldStatus = Status;
        Status = newStatus;
        StatusChanged?.Invoke(this, oldStatus, newStatus);
    }
}
