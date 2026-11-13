using System;
using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderStream
{
    public static IEnumerable<Order> Orders()
    {
        string[] customers = { "Иванов", "Петрова", "Сидоров", "Козлова", "Смирнов" };
        string[] products = { "Клавиатура", "Монитор", "Мышь", "Наушники", "Веб-камера" };
        int n = 1;

        while (true)
        {
            string customer = customers[(n - 1) % customers.Length];
            string product = products[(n - 1) % products.Length];
            decimal price = 1500m + n * 1200m;

            Console.WriteLine($"Готовлю заказ №{n}");
            yield return new Order(n, customer, product, price, DateTime.Now);

            n++;
        }
    }

    public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    {
        string[] customers = { "Иванов", "Петрова", "Сидоров", "Козлова", "Смирнов" };
        string[] products = { "Клавиатура", "Монитор", "Мышь", "Наушники", "Веб-камера" };

        for (int n = 1; n <= limit; n++)
        {
            decimal price = 2000m * n;

            if (price <= threshold)
            {
                continue;
            }

            string customer = customers[(n - 1) % customers.Length];
            string product = products[(n - 1) % products.Length];

            Console.WriteLine($"Готовлю заказ №{n}");
            yield return new Order(n, customer, product, price, DateTime.Now);
        }
    }
}
