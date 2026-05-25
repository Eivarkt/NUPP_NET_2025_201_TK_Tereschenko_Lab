using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Models;

public abstract class Product : IHasId
{
    public static readonly string DefaultCurrency;
    public static readonly decimal VatRate;

    static Product()
    {
        DefaultCurrency = "UAH";
        VatRate = 0.20m;
    }

    protected Product()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    protected Product(string name, string size, string color, decimal price, string material) : this()
    {
        Name = name;
        Size = size;
        Color = color;
        Price = price;
        Material = material;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Material { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public abstract string Category { get; }

    public virtual string GetShortInfo()
    {
        return $"{Category}: {Name}, {Color}, size {Size}, {Price} {DefaultCurrency}";
    }

    public static decimal CalculatePriceWithVat(decimal price)
    {
        return price + price * VatRate;
    }
}
