namespace ClothingStore.Infrastructure.Models;

public class PoloShirtModel : ProductModel
{
    private static int _generatedCounter;

    public string ClosureType { get; set; } = string.Empty;
    public string SleeveLength { get; set; } = string.Empty;
    public string CollarType { get; set; } = string.Empty;

    public static PoloShirtModel CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);

        string[] sizes = ["S", "M", "L", "XL"];
        string[] colors = ["White", "Black", "Navy", "Graphite"];
        string[] closureTypes = ["Buttons", "Zipper"];
        string[] collarTypes = ["Classic collar", "Stand collar"];

        return new PoloShirtModel
        {
            PublicId = Guid.NewGuid(),
            Name = $"Generated Polo #{number}",
            Size = sizes[number % sizes.Length],
            Color = colors[number % colors.Length],
            Price = 900m + (number % 20) * 50m,
            Material = "100% cotton",
            StockQuantity = 1 + number % 30,
            ClosureType = closureTypes[number % closureTypes.Length],
            SleeveLength = "Short sleeve",
            CollarType = collarTypes[number % collarTypes.Length],
            CreatedAt = DateTime.UtcNow
        };
    }
}
