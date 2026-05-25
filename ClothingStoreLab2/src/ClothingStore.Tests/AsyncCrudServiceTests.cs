using Xunit;
using ClothingStore.Common.Models;
using ClothingStore.Common.Services;

namespace ClothingStore.Tests;

public class AsyncCrudServiceTests
{
    [Fact]
    public async Task CreateAsync_And_ReadAsync_Should_Return_Created_Product()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);
        PoloShirt product = PoloShirt.CreateNew();

        bool created = await service.CreateAsync(product);
        PoloShirt found = await service.ReadAsync(product.Id);

        Assert.True(created);
        Assert.Equal(product.Id, found.Id);
        Assert.Equal(product.Name, found.Name);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_Product()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);
        PoloShirt product = PoloShirt.CreateNew();
        await service.CreateAsync(product);

        product.Price = 9999m;
        bool updated = await service.UpdateAsync(product);
        PoloShirt found = await service.ReadAsync(product.Id);

        Assert.True(updated);
        Assert.Equal(9999m, found.Price);
    }

    [Fact]
    public async Task RemoveAsync_Should_Remove_Existing_Product()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);
        PoloShirt product = PoloShirt.CreateNew();
        await service.CreateAsync(product);

        bool removed = await service.RemoveAsync(product);
        IEnumerable<PoloShirt> all = await service.ReadAllAsync();

        Assert.True(removed);
        Assert.Empty(all);
    }

    [Fact]
    public async Task ReadAllAsync_With_Pagination_Should_Return_Correct_Amount()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);

        for (int i = 0; i < 12; i++)
        {
            await service.CreateAsync(PoloShirt.CreateNew());
        }

        IEnumerable<PoloShirt> page = await service.ReadAllAsync(page: 2, amount: 5);

        Assert.Equal(5, page.Count());
    }

    [Fact]
    public async Task SaveAsync_Should_Create_File_With_Data()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);
        await service.CreateAsync(PoloShirt.CreateNew());

        bool saved = await service.SaveAsync();

        Assert.True(saved);
        Assert.True(File.Exists(filePath));
        Assert.True(new FileInfo(filePath).Length > 0);
    }

    [Fact]
    public async Task CreateAsync_Should_Be_Thread_Safe_When_Called_In_Parallel()
    {
        string filePath = GetTempFilePath();
        AsyncCrudService<PoloShirt> service = new(filePath);

        Parallel.For(0, 1000, _ =>
        {
            PoloShirt product = PoloShirt.CreateNew();
            service.CreateAsync(product).GetAwaiter().GetResult();
        });

        IEnumerable<PoloShirt> all = await service.ReadAllAsync();

        Assert.Equal(1000, all.Count());
    }

    private static string GetTempFilePath()
    {
        return Path.Combine(Path.GetTempPath(), $"clothing-store-test-{Guid.NewGuid()}.json");
    }
}
