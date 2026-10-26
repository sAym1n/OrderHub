using System;
using OrderHub.Services;

namespace OrderHub;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var catalog = new CatalogService();

        catalog.AddProduct("Клавиатура механика", 5490.00m, "Периферия");
        catalog.AddProduct("Мышь", 2990.00m, "Периферия");
        catalog.AddProduct("Игровой ультраширокий монитор", 24990.00m, "Мониторы");

        catalog.PrintAll();
        Console.WriteLine();

        catalog.PrintSortedByNaturalOrder();
        Console.WriteLine();

        catalog.PrintSortedByName();
        Console.WriteLine();

        catalog.PrintSortedByNameLength();
    }
}
