using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Models;

public class Customer : IHasId
{
    private static int _generatedCounter;

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

    // Статичний метод для створення нового об'єкта зі згенерованими даними
    public static Customer CreateNew()
    {
        int number = Interlocked.Increment(ref _generatedCounter);
        string[] cities = ["Poltava", "Kyiv", "Lviv", "Dnipro"];

        return new Customer(
            fullName: $"Customer #{number}",
            phoneNumber: $"+38050{number:0000000}",
            instagramUsername: $"@customer_{number}",
            city: cities[number % cities.Length]);
    }
}
