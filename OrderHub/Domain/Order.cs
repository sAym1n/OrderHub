using System;
using System.Globalization;

namespace OrderHub.Domain;

public class Order
{
    public int Id { get; }
    public string CustomerName { get; }
    public string ProductName { get; }
    public decimal Price { get; }
    public DateTime CreatedAt { get; }

    public Order(int id, string customerName, string productName, decimal price, DateTime createdAt)
    {
        Id = id;
        CustomerName = customerName;
        ProductName = productName;
        Price = price;
        CreatedAt = createdAt;
    }

    public override string ToString()
    {
        var culture = CultureInfo.GetCultureInfo("ru-RU");
        string formattedPrice = Price.ToString("#,##0.00", culture) + " ₽";
        string formattedDate = CreatedAt.ToString("dd.MM.yyyy");

        return $"{Id} {CustomerName} — {ProductName} {formattedPrice} ({formattedDate})";
    }
}
