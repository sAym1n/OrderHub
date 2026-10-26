using System;
using System.Collections.Generic;
using System.Linq;
using OrderHub.Domain;

namespace OrderHub.Services;

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
}
