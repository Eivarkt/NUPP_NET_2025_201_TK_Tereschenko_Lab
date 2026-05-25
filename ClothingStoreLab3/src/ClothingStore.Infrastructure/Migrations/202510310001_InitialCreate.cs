using System;
using ClothingStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    [DbContext(typeof(ClothingStoreContext))]
    [Migration("202510310001_InitialCreate")]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Instagram = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Size = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Material = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfiles", x => x.Id);
                    table.ForeignKey("FK_CustomerProfiles_Customers_CustomerId", x => x.CustomerId, "Customers", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    TotalAmount = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey("FK_Orders_Customers_CustomerId", x => x.CustomerId, "Customers", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hoodies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    HoodType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HasPocket = table.Column<bool>(type: "INTEGER", nullable: false),
                    ThicknessLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoodies", x => x.Id);
                    table.ForeignKey("FK_Hoodies_Products_Id", x => x.Id, "Products", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoloShirts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ClosureType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SleeveLength = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CollarType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoloShirts", x => x.Id);
                    table.ForeignKey("FK_PoloShirts_Products_Id", x => x.Id, "Products", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sweatpants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FitType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HasCuffs = table.Column<bool>(type: "INTEGER", nullable: false),
                    WaistType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sweatpants", x => x.Id);
                    table.ForeignKey("FK_Sweatpants_Products_Id", x => x.Id, "Products", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductTags",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductTags", x => new { x.ProductId, x.TagId });
                    table.ForeignKey("FK_ProductProductTags_ProductTags_TagId", x => x.TagId, "ProductTags", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_ProductProductTags_Products_ProductId", x => x.ProductId, "Products", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey("FK_OrderItems_Orders_OrderId", x => x.OrderId, "Orders", "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_OrderItems_Products_ProductId", x => x.ProductId, "Products", "Id", onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex("IX_Customers_PublicId", "Customers", "PublicId", unique: true);
            migrationBuilder.CreateIndex("IX_CustomerProfiles_CustomerId", "CustomerProfiles", "CustomerId", unique: true);
            migrationBuilder.CreateIndex("IX_CustomerProfiles_PublicId", "CustomerProfiles", "PublicId", unique: true);
            migrationBuilder.CreateIndex("IX_Orders_CustomerId", "Orders", "CustomerId");
            migrationBuilder.CreateIndex("IX_Orders_PublicId", "Orders", "PublicId", unique: true);
            migrationBuilder.CreateIndex("IX_OrderItems_OrderId", "OrderItems", "OrderId");
            migrationBuilder.CreateIndex("IX_OrderItems_ProductId", "OrderItems", "ProductId");
            migrationBuilder.CreateIndex("IX_OrderItems_PublicId", "OrderItems", "PublicId", unique: true);
            migrationBuilder.CreateIndex("IX_ProductProductTags_TagId", "ProductProductTags", "TagId");
            migrationBuilder.CreateIndex("IX_Products_PublicId", "Products", "PublicId", unique: true);
            migrationBuilder.CreateIndex("IX_ProductTags_PublicId", "ProductTags", "PublicId", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CustomerProfiles");
            migrationBuilder.DropTable("Hoodies");
            migrationBuilder.DropTable("OrderItems");
            migrationBuilder.DropTable("PoloShirts");
            migrationBuilder.DropTable("ProductProductTags");
            migrationBuilder.DropTable("Sweatpants");
            migrationBuilder.DropTable("Orders");
            migrationBuilder.DropTable("ProductTags");
            migrationBuilder.DropTable("Products");
            migrationBuilder.DropTable("Customers");
        }
    }
}
