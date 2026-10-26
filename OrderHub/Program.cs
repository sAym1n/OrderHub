using System;
using OrderHub.Services;

namespace OrderHub;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var service = new OrderService();

        Console.WriteLine("1. Создание заказов");
        var o1 = service.CreateOrder("Иванов", "Клавиатура", 5490.00m);
        var o2 = service.CreateOrder("Петров", "Мышь", 2990.00m);
        var o3 = service.CreateOrder("Сидоров", "Монитор", 24990.00m);

        PrintState(service);

        Console.WriteLine("\n2. Обработка первого заказа (FIFO)");
        var processed = service.ProcessNextOrder();
        Console.WriteLine($"Успешно обработан: {processed}");

        PrintState(service);

        Console.WriteLine("\n3. Отмена последних действий (LIFO)");
        Console.WriteLine(service.UndoLastAction());
        Console.WriteLine(service.UndoLastAction());

        PrintState(service);
    }

    private static void PrintState(OrderService service)
    {
        Console.WriteLine("\nТЕКУЩЕЕ СОСТОЯНИЕ СИСТЕМЫ");

        Console.WriteLine("[СПИСОК ВСЕХ ЗАКАЗОВ (List)]:");
        foreach (var order in service.GetAllOrders())
            Console.WriteLine($"  {order}");

        Console.WriteLine("[ОЧЕРЕДЬ НА ОБРАБОТКУ (Queue)]:");
        foreach (var order in service.GetPendingQueue())
            Console.WriteLine($"  {order}");

        Console.WriteLine("[ИСТОРИЯ ДЕЙСТВИЙ (Stack)]:");
        foreach (var action in service.GetUndoHistory())
            Console.WriteLine($"  {action}");
    }
}
