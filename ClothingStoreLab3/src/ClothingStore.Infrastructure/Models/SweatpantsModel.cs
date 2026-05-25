namespace ClothingStore.Infrastructure.Models;

public class SweatpantsModel : ProductModel
{
    private static int _generatedCounter;

    public string FitType { get; set; } = string.Empty;
    public bool HasCuffs { get; set; }
    public string WaistType { get; set; } = string.Empty;

    public static SweatpantsModel CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);

        return new SweatpantsModel
        {
            PublicId = Guid.NewGuid(),
            Name = $"Generated Sweatpants #{number}",
            Size = number % 2 == 0 ? "M" : "L",
            Color = number % 2 == 0 ? "Black" : "Navy",
            Price = 1450m + (number % 10) * 60m,
            Material = "Cotton blend",
            StockQuantity = 1 + number % 25,
            FitType = "Oversize",
            HasCuffs = true,
            WaistType = "Elastic waistband",
            CreatedAt = DateTime.UtcNow
        };
    }
}
