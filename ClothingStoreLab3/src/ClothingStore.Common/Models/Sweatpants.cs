namespace ClothingStore.Common.Models;

public class Sweatpants : Product
{
    public Sweatpants()
    {
    }

    public Sweatpants(string name, string size, string color, decimal price, string material, string fitType, bool hasCuffs, string waistType)
        : base(name, size, color, price, material)
    {
        FitType = fitType;
        HasCuffs = hasCuffs;
        WaistType = waistType;
    }

    public string FitType { get; set; } = string.Empty;
    public bool HasCuffs { get; set; }
    public string WaistType { get; set; } = string.Empty;

    public override string Category => "Sweatpants";
}
