using System;
using System.Collections.Generic;
using System.Linq;
using OrderHub.Domain;

namespace OrderHub.Services;

public class CategorySummary
{
    public string Category { get; set; } = "";
    public int Count { get; set; }
    public decimal AveragePrice { get; set; }
}

public class ReportService
{
    public List<string> ExpensiveProducts(IEnumerable<Product> products, decimal threshold)
    {
        return products
            .Where(p => p.Price > threshold)
            .OrderBy(p => p.Price)
            .Select(p => $"{p.Name} — {p.Price:0.00} ₽")
            .ToList();
    }

    public int CountExpensive(IEnumerable<Product> products, decimal threshold)
    {
        return products.Count(p => p.Price > threshold);
    }

    public bool HasVeryExpensive(IEnumerable<Product> products, decimal threshold)
    {
        return products.Any(p => p.Price > threshold);
    }

    public bool AllInStock(IEnumerable<Product> products)
    {
        return products.All(p => p.Price > 0);
    }

    public Product? Cheapest(IEnumerable<Product> products)
    {
        return products.OrderBy(p => p.Price).FirstOrDefault();
    }

    public List<CategorySummary> SummarizeByCategory(IEnumerable<Product> products)
    {
        return products
            .GroupBy(p => p.Category)
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                Count = g.Count(),
                AveragePrice = g.Average(p => p.Price)
            })
            .OrderByDescending(s => s.Count)
            .ToList();
    }

    public string MostExpensiveCategory(IEnumerable<Product> products)
    {
        var summary = SummarizeByCategory(products);
        if (!summary.Any()) return "Нет данных";

        return summary.OrderByDescending(s => s.AveragePrice).First().Category;
    }

    public List<string> GetCategoriesWithMinProducts(IEnumerable<Product> products, int minCount)
    {
        return SummarizeByCategory(products)
            .Where(s => s.Count >= minCount)
            .Select(s => s.Category)
            .ToList();
    }

    public List<string> TopCustomers(IEnumerable<Customer> customers, IEnumerable<Order> orders, int topCount)
    {
        return customers
            .Join(
                orders,
                c => c.Name,
                o => o.CustomerName,
                (c, o) => new { CustomerName = c.Name, OrderPrice = o.Price }
            )
            .GroupBy(pair => pair.CustomerName)
            .Select(g => new
            {
                Name = g.Key,
                TotalSum = g.Sum(pair => pair.OrderPrice)
            })
            .OrderByDescending(res => res.TotalSum)
            .Take(topCount)
            .Select(res => $"{res.Name} — {res.TotalSum:0.00} ₽")
            .ToList();
    }

    public decimal TotalRevenueByYear(IEnumerable<Order> orders, int year)
    {
        return orders.Where(o => o.CreatedAt.Year == year).Sum(o => o.Price);
    }

    public string MostActiveCustomer(IEnumerable<Order> orders)
    {
        if (!orders.Any()) return "Нет данных";

        return orders
            .GroupBy(o => o.CustomerName)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault() ?? "Нет данных";
    }

    public List<Order> GetOrdersAboveAverage(IEnumerable<Order> orders)
    {
        if (!orders.Any()) return new List<Order>();

        decimal averagePrice = orders.Average(o => o.Price);
        return orders.Where(o => o.Price > averagePrice).ToList();
    }
}
