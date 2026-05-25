namespace ClothingStore.Infrastructure.Models;

public abstract class EntityBase
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
