namespace ClothingStore.Infrastructure.Models;

public class ProductTagModel : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
