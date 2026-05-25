namespace ClothingStore.Infrastructure.Models;

public class CustomerModel : EntityBase
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Instagram { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public CustomerProfileModel? Profile { get; set; }
    public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();

    public static CustomerModel CreateNew()
    {
        return new CustomerModel
        {
            PublicId = Guid.NewGuid(),
            FullName = "Ivan Petrenko",
            PhoneNumber = "+380501112233",
            Instagram = "@ivan_style",
            City = "Poltava",
            RegisteredAt = DateTime.UtcNow,
            Profile = new CustomerProfileModel
            {
                PublicId = Guid.NewGuid(),
                Address = "Poltava, Nova Poshta #1",
                Notes = "Prefers minimalist black clothes",
                LoyaltyPoints = 120
            }
        };
    }
}
