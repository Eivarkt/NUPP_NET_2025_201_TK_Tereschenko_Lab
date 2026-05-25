namespace ClothingStore.Infrastructure.Models;

public class CustomerProfileModel : EntityBase
{
    public int CustomerId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }

    public CustomerModel? Customer { get; set; }
}
