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

        var customers = new List<Customer>
        {
            new Customer(101, "Иванов", "Москва"),
            new Customer(102, "Петров", "Тула"),
            new Customer(103, "Сидоров", "Казань"),
            new Customer(104, "Васильев", "Тверь")
        };

        var orders = new List<Order>
        {
            new Order(1, "Иванов", "Клавиатура", 5000m, new DateTime(2026, 3, 12)),
            new Order(2, "Иванов", "Коврик", 1500m, new DateTime(2026, 5, 20)),
            new Order(3, "Петров", "Монитор", 25000m, new DateTime(2026, 4, 15)),
            new Order(4, "Сидоров", "Мышь", 3000m, new DateTime(2026, 2, 10)),
            new Order(5, "Сидоров", "Ноутбук", 70000m, new DateTime(2026, 6, 1)),
            new Order(6, "Васильев", "Кабель", 500m, new DateTime(2025, 12, 25)),
            new Order(7, "Иванов", "Наушники", 4500m, new DateTime(2026, 7, 14))
        };

        Console.WriteLine("ТОП-3 КЛИЕНТА ПО СУММЕ ЗАКАЗОВ");
        var top = reports.TopCustomers(customers, orders, 3);
        foreach (var line in top)
        {
            Console.WriteLine($"  {line}");
        }

        Console.WriteLine($"\nОбщая выручка за 2026 год: {reports.TotalRevenueByYear(orders, 2026):0.00} ₽");

        Console.WriteLine($"Клиент с наибольшим числом заказов: {reports.MostActiveCustomer(orders)}");

        Console.WriteLine("\nЗаказы с суммой выше средней:");
        var expensiveOrders = reports.GetOrdersAboveAverage(orders);
        foreach (var order in expensiveOrders)
        {
            Console.WriteLine($"  {order}");
        }
    }
}
