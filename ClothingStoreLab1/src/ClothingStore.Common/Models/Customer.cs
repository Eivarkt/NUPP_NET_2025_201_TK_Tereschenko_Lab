using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Models;

public class Customer : IHasId
{
    // Конструктор без параметрів
    public Customer()
    {
        Id = Guid.NewGuid();
    }

    // Конструктор з параметрами
    public Customer(string fullName, string phoneNumber, string instagramUsername, string city) : this()
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        InstagramUsername = instagramUsername;
        City = city;
    }

    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string InstagramUsername { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    // Метод
    public string GetContactInfo()
    {
        return $"{FullName}, {PhoneNumber}, Instagram: {InstagramUsername}, city: {City}";
    }
}
