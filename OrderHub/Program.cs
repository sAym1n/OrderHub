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
            new Product("Игровой ультраширокий монитор", 24990.00m, "Мониторы")
        };

        Console.WriteLine("АНАЛИЗ ПРИ ПОРОГЕ ЦЕНЫ: 5000");

        var list1 = reports.ExpensiveProducts(products, 5000m);
        foreach (string line in list1)
        {
            Console.WriteLine($"  {line}");
        }

        Console.WriteLine($"Количество дорогих товаров: {reports.CountExpensive(products, 5000m)}");
        Console.WriteLine($"Есть товары дороже 50000: {reports.HasVeryExpensive(products, 50000m)}");
        Console.WriteLine($"У всех товаров цена больше нуля: {reports.AllInStock(products)}");
        Console.WriteLine($"Самый дешевый товар: {reports.Cheapest(products)}");

        Console.WriteLine("\nАНАЛИЗ ПРИ ПОРОГЕ ЦЕНЫ: 15000");

        var list2 = reports.ExpensiveProducts(products, 15000m);
        foreach (string line in list2)
        {
            Console.WriteLine($"  {line}");
        }

        Console.WriteLine($"Количество дорогих товаров: {reports.CountExpensive(products, 15000m)}");
        Console.WriteLine($"Есть товары дороже 50000: {reports.HasVeryExpensive(products, 50000m)}");
        Console.WriteLine($"У всех товаров цена больше нуля: {reports.AllInStock(products)}");
        Console.WriteLine($"Самый дешевый товар: {reports.Cheapest(products)}");
    }
}
