using ClothingStore.Common.Abstractions;
using ClothingStore.Common.Delegates;
using ClothingStore.Common.Enums;

namespace ClothingStore.Common.Models;

public class Order : IHasId
{
    private static int _generatedCounter;

    // Подія
    public event OrderStatusChangedHandler? StatusChanged;

    // Конструктор без параметрів
    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Created;
    }

    // Конструктор з параметрами
    public Order(Customer customer) : this()
    {
        Customer = customer;
    }

    public Guid Id { get; set; }
    public Customer Customer { get; set; } = new Customer();
    public List<Product> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }

    // Метод
    public void AddProduct(Product product)
    {
        Items.Add(product);
    }

    // Метод
    public decimal GetTotalAmount()
    {
        return Items.Sum(product => product.Price);
    }

    // Метод, який викликає подію
    public void ChangeStatus(OrderStatus newStatus)
    {
        OrderStatus oldStatus = Status;
        Status = newStatus;
        StatusChanged?.Invoke(this, oldStatus, newStatus);
    }

    // Статичний метод для створення нового об'єкта зі згенерованими даними
    public static Order CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);
        Customer customer = Customer.CreateNew();
        Order order = new(customer)
        {
            CreatedAt = DateTime.Now.AddMinutes(-number)
        };

        order.AddProduct(PoloShirt.CreateNew());
        return order;
    }
}
