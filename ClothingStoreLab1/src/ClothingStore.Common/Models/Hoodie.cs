namespace ClothingStore.Common.Models;

public class Hoodie : Product
{
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
}
