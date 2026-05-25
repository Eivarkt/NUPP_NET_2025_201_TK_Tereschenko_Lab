using System.Collections.Concurrent;
using System.Text;
using ClothingStore.Common.Concurrency;
using ClothingStore.Common.Extensions;
using ClothingStore.Common.Models;
using ClothingStore.Common.Services;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("=== ClothingStore Lab 2 Demo ===");
Console.WriteLine("Multithreading. Async. IEnumerable. LINQ.");
Console.WriteLine();

string filePath = Path.Combine("data", "polos_lab2.json");
AsyncCrudService<PoloShirt> service = new(filePath);
ConcurrentBag<PoloShirt> createdProducts = new();

const int totalCount = 1000;

Console.WriteLine($"1. Parallel creation of {totalCount} objects");

Parallel.For(0, totalCount, _ =>
{
    PoloShirt product = PoloShirt.CreateNew();
    bool created = service.CreateAsync(product).GetAwaiter().GetResult();

    if (created)
    {
        createdProducts.Add(product);
    }
});

IEnumerable<PoloShirt> allProducts = await service.ReadAllAsync();
List<PoloShirt> productList = allProducts.ToList();

Console.WriteLine($"Created products: {productList.Count}");
Console.WriteLine();

Console.WriteLine("2. LINQ statistics for numeric properties");
Console.WriteLine($"Min price: {productList.Min(product => product.Price)} {Product.DefaultCurrency}");
Console.WriteLine($"Max price: {productList.Max(product => product.Price)} {Product.DefaultCurrency}");
Console.WriteLine($"Average price: {productList.Average(product => product.Price):F2} {Product.DefaultCurrency}");
Console.WriteLine($"Min stock: {productList.Min(product => product.StockQuantity)}");
Console.WriteLine($"Max stock: {productList.Max(product => product.StockQuantity)}");
Console.WriteLine($"Average stock: {productList.Average(product => product.StockQuantity):F2}");
Console.WriteLine();

Console.WriteLine("3. Pagination example: page 2, amount 5");
IEnumerable<PoloShirt> page = await service.ReadAllAsync(page: 2, amount: 5);
foreach (PoloShirt product in page)
{
    Console.WriteLine(product.ToCatalogLine());
}
Console.WriteLine();

Console.WriteLine("4. Read, update and remove examples");
PoloShirt firstProduct = productList.First();
PoloShirt foundProduct = await service.ReadAsync(firstProduct.Id);
Console.WriteLine($"Read product: {foundProduct.ToCatalogLine()}");

foundProduct.Price += 100m;
bool updated = await service.UpdateAsync(foundProduct);
Console.WriteLine($"Updated: {updated}; new price: {(await service.ReadAsync(foundProduct.Id)).Price} {Product.DefaultCurrency}");

bool removed = await service.RemoveAsync(foundProduct);
Console.WriteLine($"Removed: {removed}");
Console.WriteLine($"Products after remove: {(await service.ReadAllAsync()).Count()}");
Console.WriteLine();

Console.WriteLine("5. Save collection to file");
bool saved = await service.SaveAsync();
Console.WriteLine($"Saved: {saved}");
Console.WriteLine($"File path: {filePath}");
Console.WriteLine();

Console.WriteLine("6. IEnumerable example");
decimal totalPriceOfFirstTen = service
    .OrderBy(product => product.Name)
    .Take(10)
    .Sum(product => product.Price);
Console.WriteLine($"Total price of first 10 products: {totalPriceOfFirstTen} {Product.DefaultCurrency}");
Console.WriteLine();

Console.WriteLine("7. Synchronization primitives examples");
int lockCounter = SynchronizationExamples.LockExample();
int semaphoreCompleted = await SynchronizationExamples.SemaphoreExampleAsync();
int autoResetEventValue = SynchronizationExamples.AutoResetEventExample();

Console.WriteLine($"lock result counter: {lockCounter}");
Console.WriteLine($"Semaphore result completed operations: {semaphoreCompleted}");
Console.WriteLine($"AutoResetEvent result value: {autoResetEventValue}");
Console.WriteLine();

Console.WriteLine("Demo finished successfully.");
