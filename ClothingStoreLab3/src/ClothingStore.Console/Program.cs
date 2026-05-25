using System.Text;
using ClothingStore.Common.Concurrency;
using ClothingStore.Infrastructure.Data;
using ClothingStore.Infrastructure.Models;
using ClothingStore.Infrastructure.Repositories;
using ClothingStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("=== ClothingStore Lab 3 Demo ===");
Console.WriteLine("Entity Framework Core. SQLite. Repository. CRUD service.");
Console.WriteLine();

string databasePath = GetDatabasePath();
Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);

DbContextOptions<ClothingStoreContext> options = new DbContextOptionsBuilder<ClothingStoreContext>()
    .UseSqlite($"Data Source={databasePath};Cache=Shared;Default Timeout=30")
    .Options;

await using ClothingStoreContext context = new(options);

// For demonstration the database is recreated on every run.
await context.Database.EnsureDeletedAsync();
await context.Database.MigrateAsync();

Repository<PoloShirtModel> poloRepository = new(context);
Repository<CustomerModel> customerRepository = new(context);
AsyncCrudService<PoloShirtModel> poloService = new(poloRepository);
AsyncCrudService<CustomerModel> customerService = new(customerRepository);

const int totalCount = 1000;
Console.WriteLine($"1. Parallel creation of {totalCount} PoloShirtModel objects into SQLite database");

Parallel.For(0, totalCount, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, _ =>
{
    PoloShirtModel product = PoloShirtModel.CreateNew();
    poloService.CreateAsync(product).GetAwaiter().GetResult();
});

List<PoloShirtModel> products = (await poloService.ReadAllAsync()).ToList();
Console.WriteLine($"Created products in database: {products.Count}");
Console.WriteLine();

Console.WriteLine("2. LINQ statistics from database data");
Console.WriteLine($"Min price: {products.Min(product => product.Price)} UAH");
Console.WriteLine($"Max price: {products.Max(product => product.Price)} UAH");
Console.WriteLine($"Average price: {products.Average(product => product.Price):F2} UAH");
Console.WriteLine($"Min stock: {products.Min(product => product.StockQuantity)}");
Console.WriteLine($"Max stock: {products.Max(product => product.StockQuantity)}");
Console.WriteLine($"Average stock: {products.Average(product => product.StockQuantity):F2}");
Console.WriteLine();

Console.WriteLine("3. Pagination example: page 2, amount 5");
IEnumerable<PoloShirtModel> page = await poloService.ReadAllAsync(page: 2, amount: 5);
foreach (PoloShirtModel product in page)
{
    Console.WriteLine($"{product.Id}. {product.GetShortInfo()} | {product.ClosureType}");
}
Console.WriteLine();

Console.WriteLine("4. Read, update and remove through EF CRUD service");
PoloShirtModel firstProduct = products.First();
PoloShirtModel foundProduct = await poloService.ReadAsync(firstProduct.PublicId);
Console.WriteLine($"Read product: {foundProduct.GetShortInfo()}");

foundProduct.Price += 100m;
await poloService.UpdateAsync(foundProduct);
Console.WriteLine($"Updated product price: {(await poloService.ReadAsync(foundProduct.PublicId)).Price} UAH");

await poloService.RemoveAsync(foundProduct);
Console.WriteLine($"Products after remove: {(await poloService.ReadAllAsync()).Count()}");
Console.WriteLine();

Console.WriteLine("5. Relationships: one-to-one, one-to-many and many-to-many");
CustomerModel customer = CustomerModel.CreateNew();
await customerService.CreateAsync(customer);

ProductTagModel premiumTag = new()
{
    PublicId = Guid.NewGuid(),
    Name = "Premium basic",
    Description = "Minimalist premium everyday clothing"
};

List<PoloShirtModel> firstProducts = (await poloService.ReadAllAsync()).Take(3).ToList();
foreach (PoloShirtModel product in firstProducts)
{
    premiumTag.Products.Add(product);
}

await context.ProductTags.AddAsync(premiumTag);
await context.SaveChangesAsync();

OrderModel order = new()
{
    PublicId = Guid.NewGuid(),
    CustomerId = customer.Id,
    Status = "Confirmed",
    CreatedAt = DateTime.UtcNow
};

foreach (PoloShirtModel product in firstProducts.Take(2))
{
    order.Items.Add(new OrderItemModel
    {
        PublicId = Guid.NewGuid(),
        ProductId = product.Id,
        Quantity = 1,
        UnitPrice = product.Price,
        CreatedAt = DateTime.UtcNow
    });
}

order.RecalculateTotal();
await context.Orders.AddAsync(order);
await context.SaveChangesAsync();

Console.WriteLine($"Customer created: {customer.FullName}, profile address: {customer.Profile?.Address}");
Console.WriteLine($"Order created: #{order.Id}, items: {order.Items.Count}, total: {order.TotalAmount} UAH");
Console.WriteLine($"Tag created: {premiumTag.Name}, related products: {premiumTag.Products.Count}");
Console.WriteLine();

Console.WriteLine("6. Synchronization primitives from previous lab");
Console.WriteLine($"lock counter result: {SynchronizationExamples.LockExample()}");
Console.WriteLine($"Semaphore completed operations: {await SynchronizationExamples.SemaphoreExampleAsync()}");
Console.WriteLine($"AutoResetEvent result value: {SynchronizationExamples.AutoResetEventExample()}");
Console.WriteLine();

Console.WriteLine("7. Database tables count");
Console.WriteLine($"Products table: {await context.Products.CountAsync()}");
Console.WriteLine($"PoloShirts table: {await context.PoloShirts.CountAsync()}");
Console.WriteLine($"Customers table: {await context.Customers.CountAsync()}");
Console.WriteLine($"CustomerProfiles table: {await context.CustomerProfiles.CountAsync()}");
Console.WriteLine($"Orders table: {await context.Orders.CountAsync()}");
Console.WriteLine($"OrderItems table: {await context.OrderItems.CountAsync()}");
Console.WriteLine($"ProductTags table: {await context.ProductTags.CountAsync()}");
Console.WriteLine();

Console.WriteLine($"SQLite database file: {databasePath}");
Console.WriteLine("Demo finished successfully.");

static string GetDatabasePath()
{
    DirectoryInfo? currentDirectory = new(Directory.GetCurrentDirectory());

    while (currentDirectory is not null && !File.Exists(Path.Combine(currentDirectory.FullName, "ClothingStore.sln")))
    {
        currentDirectory = currentDirectory.Parent;
    }

    string root = currentDirectory?.FullName ?? Directory.GetCurrentDirectory();
    return Path.Combine(root, "database", "clothing_store_lab3.db");
}
