namespace ClothingStore.Common.Models;

public class Hoodie : Product
{
    public Hoodie()
    {
    }

    public Hoodie(string name, string size, string color, decimal price, string material, string hoodType, bool hasPocket, int thicknessLevel)
        : base(name, size, color, price, material)
    {
        HoodType = hoodType;
        HasPocket = hasPocket;
        ThicknessLevel = thicknessLevel;
    }

    public string HoodType { get; set; } = string.Empty;
    public bool HasPocket { get; set; }
    public int ThicknessLevel { get; set; }

    public override string Category => "Hoodie";
}
