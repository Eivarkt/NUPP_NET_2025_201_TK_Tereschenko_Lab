# ClothingStore - Laboratory Work 2

Тема: багатопотоковість, асинхронність, `IEnumerable`, LINQ.

## Проєкти

- `ClothingStore.Common` - моделі, інтерфейси, CRUD-сервіси, приклади синхронізації.
- `ClothingStore.Console` - консольна демонстрація лабораторної роботи.
- `ClothingStore.Tests` - unit-тести для `AsyncCrudService<T>`.

## Запуск

```bash
dotnet restore
dotnet build
dotnet run --project src/ClothingStore.Console/ClothingStore.Console.csproj
```

## Запуск тестів

```bash
dotnet test
```
