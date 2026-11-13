using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderHub.Services;

public class WarehouseService
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>();

    public void Receive(string productName, int quantity)
    {
        if (_stock.TryGetValue(productName, out int existingQuantity))
        {
            _stock[productName] = existingQuantity + quantity;
        }
        else
        {
            _stock[productName] = quantity;
        }

        Console.WriteLine($"Поступление: {productName} +{quantity} шт. Всего на складе: {_stock[productName]} шт.");
    }

    public bool Sell(string productName, int quantity)
    {
        if (!_stock.TryGetValue(productName, out int existingQuantity))
        {
            Console.WriteLine($"Ошибка: Товар \"{productName}\" отсутствует на складе.");
            return false;
        }

        if (existingQuantity < quantity)
        {
            Console.WriteLine($"Предупреждение: Недостаточно товара \"{productName}\". Запрошено: {quantity} шт., в наличии: {existingQuantity} шт.");
            return false;
        }

        int newQuantity = existingQuantity - quantity;

        if (newQuantity == 0)
        {
            _stock.Remove(productName);
            Console.WriteLine($"Продажа: {quantity} шт. товара \"{productName}\". Товар полностью распродан и снят с учёта.");
        }
        else
        {
            _stock[productName] = newQuantity;
            Console.WriteLine($"Продажа: {quantity} шт. товара \"{productName}\". Остаток: {newQuantity} шт.");
        }

        return true;
    }

    public bool TryGetStock(string productName, out int quantity)
    {
        if (_stock.TryGetValue(productName, out quantity))
        {
            return true;
        }

        quantity = 0;
        return false;
    }

    public void PrintStock()
    {
        Console.WriteLine("\nТЕКУЩИЙ СОСТАВ СКЛАДА");

        var sortedStock = _stock.OrderBy(pair => pair.Key);

        foreach (KeyValuePair<string, int> pair in sortedStock)
        {
            Console.WriteLine($"  {pair.Key}: {pair.Value} шт.");
        }
    }
}
