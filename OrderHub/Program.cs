using System;
using OrderHub.Services;

namespace OrderHub;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var warehouse = new WarehouseService();

        Console.WriteLine("1. Поступление товаров на склад");
        warehouse.Receive("Клавиатура", 10);
        warehouse.Receive("Клавиатура", 5);
        warehouse.Receive("Монитор", 3);

        warehouse.PrintStock();

        Console.WriteLine("\n2. Продажа товаров со склада");
        warehouse.Sell("Монитор", 1);
        warehouse.Sell("Монитор", 99);
        warehouse.Sell("Проектор", 1);

        warehouse.PrintStock();

        Console.WriteLine("\n3. Полное списание товара (до нуля)");
        warehouse.Sell("Монитор", 2);

        warehouse.PrintStock();
    }
}
