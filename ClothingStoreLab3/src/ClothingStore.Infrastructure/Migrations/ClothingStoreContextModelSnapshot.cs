using ClothingStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    [DbContext(typeof(ClothingStoreContext))]
    public class ClothingStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            // Snapshot is intentionally compact for the laboratory project.
            // The actual database schema is defined in ClothingStoreContext and InitialCreate migration.
        }
    }
}
