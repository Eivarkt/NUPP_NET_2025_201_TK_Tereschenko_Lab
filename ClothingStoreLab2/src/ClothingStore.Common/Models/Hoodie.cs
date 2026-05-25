namespace ClothingStore.Common.Models;

public class Hoodie : Product
{
    private static int _generatedCounter;

    // Конструктор без параметрів
    public Hoodie()
    {
    }

    // Конструктор з параметрами
    public Hoodie(
        string name,
        string size,
        string color,
        decimal price,
        string material,
        bool hasHood,
        string zipperType,
        int pocketCount) : base(name, size, color, price, material)
    {
        HasHood = hasHood;
        ZipperType = zipperType;
        PocketCount = pocketCount;
    }

    public bool HasHood { get; set; }
    public string ZipperType { get; set; } = string.Empty;
    public int PocketCount { get; set; }

    public override string Category => "Hoodie";

    // Метод
    public override string GetShortInfo()
    {
        return $"{base.GetShortInfo()}, zipper: {ZipperType}, pockets: {PocketCount}";
    }

    // Статичний метод для створення нового об'єкта зі згенерованими даними
    public static Hoodie CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);
        string[] sizes = ["S", "M", "L", "XL"];
        string[] colors = ["Black", "Graphite", "Navy"];

        return new Hoodie(
            name: $"Generated Hoodie #{number}",
            size: sizes[number % sizes.Length],
            color: colors[number % colors.Length],
            price: 1500m + (number % 15) * 100m,
            material: "Cotton fleece",
            hasHood: true,
            zipperType: number % 2 == 0 ? "Full zip" : "No zipper",
            pocketCount: number % 2 == 0 ? 2 : 1)
        {
            StockQuantity = 1 + number % 20
        };
    }
}
