namespace ClothingStore.Common.Models;

public class PoloShirt : Product
{
    // Конструктор без параметрів
    public PoloShirt()
    {
    }

    // Конструктор з параметрами
    public PoloShirt(
        string name,
        string size,
        string color,
        decimal price,
        string material,
        string closureType,
        string sleeveLength,
        string collarType) : base(name, size, color, price, material)
    {
        ClosureType = closureType;
        SleeveLength = sleeveLength;
        CollarType = collarType;
    }

    public string ClosureType { get; set; } = string.Empty;
    public string SleeveLength { get; set; } = string.Empty;
    public string CollarType { get; set; } = string.Empty;

    public override string Category => "Polo Shirt";

    // Метод
    public override string GetShortInfo()
    {
        return $"{base.GetShortInfo()}, closure: {ClosureType}, collar: {CollarType}";
    }
}
