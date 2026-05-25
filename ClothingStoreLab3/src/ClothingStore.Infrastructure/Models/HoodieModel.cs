namespace ClothingStore.Infrastructure.Models;

public class HoodieModel : ProductModel
{
    private static int _generatedCounter;

    public string HoodType { get; set; } = string.Empty;
    public bool HasPocket { get; set; }
    public int ThicknessLevel { get; set; }

    public static HoodieModel CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);

        return new HoodieModel
        {
            PublicId = Guid.NewGuid(),
            Name = $"Generated Hoodie #{number}",
            Size = number % 2 == 0 ? "M" : "L",
            Color = number % 2 == 0 ? "Black" : "Graphite",
            Price = 1800m + (number % 10) * 80m,
            Material = "Cotton fleece",
            StockQuantity = 1 + number % 20,
            HoodType = "Classic hood",
            HasPocket = true,
            ThicknessLevel = 3 + number % 3,
            CreatedAt = DateTime.UtcNow
        };
    }
}
