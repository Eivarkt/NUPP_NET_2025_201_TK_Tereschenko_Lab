namespace ClothingStore.Common.Models;

public class Sweatpants : Product
{
    private static int _generatedCounter;

    // Конструктор без параметрів
    public Sweatpants()
    {
    }

    // Конструктор з параметрами
    public Sweatpants(
        string name,
        string size,
        string color,
        decimal price,
        string material,
        string fitType,
        bool hasCuffs,
        string waistType) : base(name, size, color, price, material)
    {
        FitType = fitType;
        HasCuffs = hasCuffs;
        WaistType = waistType;
    }

    public string FitType { get; set; } = string.Empty;
    public bool HasCuffs { get; set; }
    public string WaistType { get; set; } = string.Empty;

    public override string Category => "Sweatpants";

    // Метод
    public override string GetShortInfo()
    {
        return $"{base.GetShortInfo()}, fit: {FitType}, waist: {WaistType}";
    }

    // Статичний метод для створення нового об'єкта зі згенерованими даними
    public static Sweatpants CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);
        string[] sizes = ["S", "M", "L", "XL"];
        string[] colors = ["Black", "Graphite", "Grey"];

        return new Sweatpants(
            name: $"Generated Sweatpants #{number}",
            size: sizes[number % sizes.Length],
            color: colors[number % colors.Length],
            price: 1200m + (number % 10) * 90m,
            material: "Cotton",
            fitType: number % 2 == 0 ? "Regular" : "Oversize",
            hasCuffs: number % 2 == 0,
            waistType: "Elastic waist")
        {
            StockQuantity = 1 + number % 25
        };
    }
}
