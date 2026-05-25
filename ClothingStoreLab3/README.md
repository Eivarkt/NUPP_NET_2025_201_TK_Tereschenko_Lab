# ClothingStore - Laboratory Work 3

Тема: **Робота із базами даних у .NET. Entity Framework Core**.

## Що реалізовано

- Проєкт `ClothingStore.Infrastructure`.
- EF Core модель бази даних у папці `Models`.
- `ClothingStoreContext : DbContext`.
- Fluent API конфігурація зв'язків.
- SQLite база даних `database/clothing_store_lab3.db`.
- Міграція `InitialCreate`.
- Патерн Repository через `IRepository<T>` та `Repository<T>`.
- Оновлений async CRUD сервіс, який працює через репозиторій.
- Консольний застосунок, який працює з базою даних.
- ERD-діаграма та скріншоти таблиць у PDF-звіті.

## Зв'язки у базі даних

- One-to-one: `Customers` -> `CustomerProfiles`.
- One-to-many: `Customers` -> `Orders`.
- One-to-many: `Orders` -> `OrderItems`.
- One-to-many: `Products` -> `OrderItems`.
- Many-to-many: `Products` <-> `ProductTags` через `ProductProductTags`.
- Наслідування: TPT, тобто `Products` + таблиці `PoloShirts`, `Hoodies`, `Sweatpants`.

## Як запустити

```bash
dotnet restore
dotnet build
dotnet run --project src/ClothingStore.Console/ClothingStore.Console.csproj
```

## Команди для міграції

Якщо потрібно створити міграцію заново:

```bash
dotnet ef migrations add InitialCreate -p src/ClothingStore.Infrastructure -s src/ClothingStore.Console
dotnet ef database update -p src/ClothingStore.Infrastructure -s src/ClothingStore.Console
```

## Примітка

У консолі база даних видаляється та створюється заново при кожному запуску, щоб результат лабораторної був стабільним та легко перевірявся.
