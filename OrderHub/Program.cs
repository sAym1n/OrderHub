using System;
using OrderHub.Services;

namespace OrderHub;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var service = new OrderService();

        var order1 = service.CreateOrder("Иванов", "Клавиатура", 5490m);
        var order2 = service.CreateOrder("Петрова", "Монитор", 18990m);

        Console.WriteLine("1. Инициализация истории (Иванов, затем Петрова)");
        service.InitHistory(order1, order2);
        service.PrintHistory();

        Console.WriteLine("\n2. Вставка нового заказа ПОСЛЕ текущего (Текущий: Петрова)");
        var orderAfterCurrent = service.CreateOrder("Сидоров", "Мышь", 1500m);
        service.PlaceOrderAfterCurrent(orderAfterCurrent);
        service.PrintHistory();

        Console.WriteLine("\n3. Смена текущего узла назад и повторная вставка");
        service.InitHistory(order1);
        service.PrintHistory();

        Console.WriteLine("\nВставляем заказ Петровой сразу ПОСЛЕ Иванова");
        service.PlaceOrderAfterCurrent(order2);
        service.PrintHistory();

        Console.WriteLine("\n4. Проверка поиска заказов (TryFindOrder)");

        if (service.TryFindOrder("Иванов", out var foundOrder))
        {
            Console.WriteLine($"Успешно: найден заказ клиента Иванов: {foundOrder}");
        }
        else
        {
            Console.WriteLine("Ошибка: заказ Иванова не найден");
        }

        if (service.TryFindOrder("Нет такого", out var missingOrder))
        {
            Console.WriteLine($"Ошибка: найден несуществующий заказ: {missingOrder}");
        }
        else
        {
            Console.WriteLine("Успешно: метод вернул false для несуществующего клиента без исключений");
        }
    }
}
