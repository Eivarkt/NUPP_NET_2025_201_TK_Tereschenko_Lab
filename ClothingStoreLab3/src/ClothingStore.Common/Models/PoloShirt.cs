namespace ClothingStore.Common.Models;

public class PoloShirt : Product
{
    private static int _generatedCounter;

    public PoloShirt()
    {
    }

    public PoloShirt(string name, string size, string color, decimal price, string material, string closureType, string sleeveLength, string collarType)
        : base(name, size, color, price, material)
    {
        ClosureType = closureType;
        SleeveLength = sleeveLength;
        CollarType = collarType;
    }

    public string ClosureType { get; set; } = string.Empty;
    public string SleeveLength { get; set; } = string.Empty;
    public string CollarType { get; set; } = string.Empty;

    public override string Category => "Polo Shirt";

    public override string GetShortInfo()
    {
        return $"{base.GetShortInfo()}, closure: {ClosureType}, collar: {CollarType}";
    }

    public static PoloShirt CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);
        string[] sizes = ["S", "M", "L", "XL"];
        string[] colors = ["White", "Black", "Navy", "Graphite"];
        string[] closureTypes = ["Buttons", "Zipper"];
        string[] collarTypes = ["Classic collar", "Stand collar"];

        return new PoloShirt(
            $"Generated Polo #{number}",
            sizes[number % sizes.Length],
            colors[number % colors.Length],
            900m + (number % 20) * 50m,
            "100% cotton",
            closureTypes[number % closureTypes.Length],
            "Short sleeve",
            collarTypes[number % collarTypes.Length])
        {
            StockQuantity = 1 + number % 30
        };
    }
}
