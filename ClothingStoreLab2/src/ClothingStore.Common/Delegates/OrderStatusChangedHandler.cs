using ClothingStore.Common.Enums;
using ClothingStore.Common.Models;

namespace ClothingStore.Common.Delegates;

// Делегат
public delegate void OrderStatusChangedHandler(Order order, OrderStatus oldStatus, OrderStatus newStatus);
