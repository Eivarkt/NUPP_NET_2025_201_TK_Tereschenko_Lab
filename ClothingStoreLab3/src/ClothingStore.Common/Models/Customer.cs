using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Models;

public class Customer : IHasId
{
    public Customer()
    {
        Id = Guid.NewGuid();
    }

    public Customer(string fullName, string phoneNumber, string instagram, string city) : this()
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Instagram = instagram;
        City = city;
    }

    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Instagram { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public string GetContactInfo()
    {
        return $"{FullName}, {PhoneNumber}, {Instagram}, {City}";
    }
}
