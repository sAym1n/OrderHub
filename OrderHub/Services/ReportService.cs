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
}
