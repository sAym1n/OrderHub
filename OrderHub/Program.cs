using System;
using System.Collections.Generic;
using System.Linq;
using OrderHub.Domain;
using OrderHub.Services;

IEnumerable<Order> orders = OrderStream.Orders();
Console.WriteLine("После вызова метода");

foreach (var order in orders.Take(5))
{
    Console.WriteLine(order);
}

IEnumerable<Order> expensiveOrders = OrderStream.Expensive(10, 10000m);
Console.WriteLine("После вызова Expensive");

foreach (var order in expensiveOrders)
{
    Console.WriteLine(order);
}

Console.WriteLine("Вызов Orders().ToList() без Take() попытается загрузить все элементы в память сразу. Поскольку в Orders() используется бесконечный цикл while (true), перебор никогда не закончится, что приведёт к зависанию программы и переполнению памяти.");
