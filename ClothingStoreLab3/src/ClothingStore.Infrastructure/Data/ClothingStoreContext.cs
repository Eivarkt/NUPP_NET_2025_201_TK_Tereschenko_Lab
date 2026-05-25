using ClothingStore.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data;

public class ClothingStoreContext : DbContext
{
    public ClothingStoreContext(DbContextOptions<ClothingStoreContext> options) : base(options)
    {
    }

    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<PoloShirtModel> PoloShirts => Set<PoloShirtModel>();
    public DbSet<HoodieModel> Hoodies => Set<HoodieModel>();
    public DbSet<SweatpantsModel> Sweatpants => Set<SweatpantsModel>();
    public DbSet<CustomerModel> Customers => Set<CustomerModel>();
    public DbSet<CustomerProfileModel> CustomerProfiles => Set<CustomerProfileModel>();
    public DbSet<OrderModel> Orders => Set<OrderModel>();
    public DbSet<OrderItemModel> OrderItems => Set<OrderItemModel>();
    public DbSet<ProductTagModel> ProductTags => Set<ProductTagModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.Id);
            entity.HasIndex(product => product.PublicId).IsUnique();
            entity.Property(product => product.Name).HasMaxLength(100).IsRequired();
            entity.Property(product => product.Size).HasMaxLength(10).IsRequired();
            entity.Property(product => product.Color).HasMaxLength(50).IsRequired();
            entity.Property(product => product.Material).HasMaxLength(100).IsRequired();
            entity.Property(product => product.Price).HasConversion<double>();
        });

        // TPT inheritance: each derived product type has its own table.
        modelBuilder.Entity<PoloShirtModel>(entity =>
        {
            entity.ToTable("PoloShirts");
            entity.Property(product => product.ClosureType).HasMaxLength(50).IsRequired();
            entity.Property(product => product.SleeveLength).HasMaxLength(50).IsRequired();
            entity.Property(product => product.CollarType).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<HoodieModel>(entity =>
        {
            entity.ToTable("Hoodies");
            entity.Property(product => product.HoodType).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<SweatpantsModel>(entity =>
        {
            entity.ToTable("Sweatpants");
            entity.Property(product => product.FitType).HasMaxLength(50).IsRequired();
            entity.Property(product => product.WaistType).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(customer => customer.Id);
            entity.HasIndex(customer => customer.PublicId).IsUnique();
            entity.Property(customer => customer.FullName).HasMaxLength(100).IsRequired();
            entity.Property(customer => customer.PhoneNumber).HasMaxLength(30).IsRequired();
            entity.Property(customer => customer.Instagram).HasMaxLength(50);
            entity.Property(customer => customer.City).HasMaxLength(50);

            // One-to-one: Customer -> CustomerProfile.
            entity.HasOne(customer => customer.Profile)
                .WithOne(profile => profile.Customer)
                .HasForeignKey<CustomerProfileModel>(profile => profile.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: Customer -> Orders.
            entity.HasMany(customer => customer.Orders)
                .WithOne(order => order.Customer)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CustomerProfileModel>(entity =>
        {
            entity.ToTable("CustomerProfiles");
            entity.HasKey(profile => profile.Id);
            entity.HasIndex(profile => profile.PublicId).IsUnique();
            entity.HasIndex(profile => profile.CustomerId).IsUnique();
            entity.Property(profile => profile.Address).HasMaxLength(150).IsRequired();
            entity.Property(profile => profile.Notes).HasMaxLength(300);
        });

        modelBuilder.Entity<OrderModel>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(order => order.Id);
            entity.HasIndex(order => order.PublicId).IsUnique();
            entity.Property(order => order.Status).HasMaxLength(30).IsRequired();
            entity.Property(order => order.TotalAmount).HasConversion<double>();

            // One-to-many: Order -> OrderItems.
            entity.HasMany(order => order.Items)
                .WithOne(item => item.Order)
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItemModel>(entity =>
        {
            entity.ToTable("OrderItems");
            entity.HasKey(item => item.Id);
            entity.HasIndex(item => item.PublicId).IsUnique();
            entity.Property(item => item.UnitPrice).HasConversion<double>();

            entity.HasOne(item => item.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProductTagModel>(entity =>
        {
            entity.ToTable("ProductTags");
            entity.HasKey(tag => tag.Id);
            entity.HasIndex(tag => tag.PublicId).IsUnique();
            entity.Property(tag => tag.Name).HasMaxLength(50).IsRequired();
            entity.Property(tag => tag.Description).HasMaxLength(200);
        });

        // Many-to-many: ProductModel <-> ProductTagModel.
        modelBuilder.Entity<ProductModel>()
            .HasMany(product => product.Tags)
            .WithMany(tag => tag.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductProductTag",
                right => right.HasOne<ProductTagModel>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<ProductModel>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.ToTable("ProductProductTags");
                    join.HasKey("ProductId", "TagId");
                });
    }
}
