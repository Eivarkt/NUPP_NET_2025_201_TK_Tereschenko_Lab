using System.Text;
using ClothingStore.Common.Enums;
using ClothingStore.Common.Extensions;
using ClothingStore.Common.Models;
using ClothingStore.Common.Services;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("=== ClothingStore Lab 1 Demo ===");
Console.WriteLine();

CrudService<PoloShirt> poloService = new();

PoloShirt whitePolo = new(
    "Classic Cotton Polo",
    "M",
    "White",
    1290m,
    "100% cotton",
    "Buttons",
    "Short sleeve",
    "Classic collar")
{
    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
    StockQuantity = 5,
    CreatedAt = new DateTime(2025, 9, 10, 10, 0, 0)
};

PoloShirt blackPolo = new(
    "Premium Zip Polo",
    "L",
    "Black",
    1590m,
    "100% cotton",
    "Zipper",
    "Short sleeve",
    "Stand collar")
{
    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
    StockQuantity = 3,
    CreatedAt = new DateTime(2025, 9, 10, 10, 5, 0)
};

Console.WriteLine("1. Create products");
poloService.Create(whitePolo);
poloService.Create(blackPolo);

Console.WriteLine();
Console.WriteLine("2. Read all products");
foreach (PoloShirt product in poloService.ReadAll())
{
    Console.WriteLine(product.ToCatalogLine());
}

Console.WriteLine();
Console.WriteLine("3. Read one product by id");
PoloShirt foundProduct = poloService.Read(Guid.Parse("22222222-2222-2222-2222-222222222222"));
Console.WriteLine(foundProduct.ToCatalogLine());

Console.WriteLine();
Console.WriteLine("4. Update product price");
blackPolo.Price = 1690m;
poloService.Update(blackPolo);
Console.WriteLine(poloService.Read(blackPolo.Id).ToCatalogLine());

Console.WriteLine();
Console.WriteLine("5. Extension method check");
Console.WriteLine($"Is premium price: {blackPolo.IsPremiumPrice()}");

Console.WriteLine();
Console.WriteLine("6. Static method check");
decimal priceWithVat = Product.CalculatePriceWithVat(blackPolo.Price);
Console.WriteLine($"Price with VAT: {priceWithVat} {Product.DefaultCurrency}");

Console.WriteLine();
Console.WriteLine("7. Remove one product");
poloService.Remove(whitePolo);
foreach (PoloShirt product in poloService.ReadAll())
{
    Console.WriteLine(product.ToCatalogLine());
}

Console.WriteLine();
Console.WriteLine("8. Save and load data");
string filePath = Path.Combine("data", "polos.json");
poloService.Save(filePath);

CrudService<PoloShirt> loadedService = new();
loadedService.Load(filePath);
Console.WriteLine("Loaded products from file:");
foreach (PoloShirt product in loadedService.ReadAll())
{
    Console.WriteLine(product.ToCatalogLine());
}

Console.WriteLine();
Console.WriteLine("9. Delegate and event demonstration");
Customer customer = new("Ivan Petrenko", "+380501112233", "@ivan_style", "Poltava")
{
    Id = Guid.Parse("33333333-3333-3333-3333-333333333333")
};

Order order = new(customer)
{
    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
    CreatedAt = new DateTime(2025, 9, 10, 11, 0, 0)
};

order.StatusChanged += (changedOrder, oldStatus, newStatus) =>
{
    Console.WriteLine($"Event: order {changedOrder.Id} status changed from {oldStatus} to {newStatus}");
};

order.AddProduct(blackPolo);
Console.WriteLine($"Customer: {customer.GetContactInfo()}");
Console.WriteLine($"Order total: {order.GetTotalAmount()} {Product.DefaultCurrency}");
order.ChangeStatus(OrderStatus.Confirmed);

Console.WriteLine();
Console.WriteLine("Demo finished successfully.");
