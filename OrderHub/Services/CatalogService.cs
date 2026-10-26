using System;
using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class CatalogService
{
    private readonly List<Product> _products = new();

    public void AddProduct(string name, decimal price, string category)
    {
        _products.Add(new Product(name, price, category));
    }

    public void PrintSortedByNaturalOrder()
    {
        _products.Sort();
        Console.WriteLine("Сортировка по естественному порядку (возрастание цены):");
        foreach (var product in _products)
        {
            Console.WriteLine($"  {product}");
        }
    }

    public void PrintSortedByName()
    {
        _products.Sort(new NameComparer());
        Console.WriteLine("Сортировка по алфавиту названий (NameComparer):");
        foreach (var product in _products)
        {
            Console.WriteLine($"  {product}");
        }
    }

    public void PrintSortedByNameLength()
    {
        _products.Sort((a, b) => a.Name.Length.CompareTo(b.Name.Length));
        Console.WriteLine("Сортировка по длине названия (делегат Comparison):");
        foreach (var product in _products)
        {
            Console.WriteLine($"  {product}");
        }
        Console.WriteLine("Пояснение: Сортировка выполнена через лямбду, структура класса Product не изменялась.");
    }

    public void PrintAll()
    {
        Console.WriteLine("Текущий порядок элементов в каталоге:");
        foreach (var product in _products)
        {
            Console.WriteLine($"  {product}");
        }
    }
}
