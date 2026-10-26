using System;
using System.Collections.Generic;
using OrderHub.Domain;
using OrderHub.Services;

namespace OrderHub;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var reports = new ReportService();

        var products = new List<Product>
        {
            new Product("Мышь", 2990.00m, "Периферия"),
            new Product("Клавиатура механика", 5490.00m, "Периферия"),
            new Product("Коврик для мыши", 990.00m, "Периферия"),
            new Product("Игровой монитор 24", 14990.00m, "Мониторы"),
            new Product("Монитор 4K 32", 34990.00m, "Мониторы"),
            new Product("Игровое кресло", 19990.00m, "Мебель")
        };

        Console.WriteLine("СВОДКА ПО КАТЕГОРИЯМ (Группировка и агрегаты LINQ)");
        var summary = reports.SummarizeByCategory(products);
        foreach (var row in summary)
        {
            Console.WriteLine($"  {row.Category}: {row.Count} шт., средняя {row.AveragePrice:0.00} ₽");
        }

        Console.WriteLine($"\nСамая дорогая категория (по средней цене): {reports.MostExpensiveCategory(products)}");

        Console.WriteLine("\nКатегории, где товаров не меньше 2:");
        var popularCategories = reports.GetCategoriesWithMinProducts(products, 2);
        foreach (var cat in popularCategories)
        {
            Console.WriteLine($"  {cat}");
        }
    }
}
