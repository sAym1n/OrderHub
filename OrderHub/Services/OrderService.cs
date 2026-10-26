using System;
using System.Collections.Generic;
using System.Linq;
using OrderHub.Domain;

namespace OrderHub.Services;

public enum ActionType
{
    Create,
    Process
}

public struct UndoAction
{
    public ActionType Type { get; }
    public Order Order { get; }

    public UndoAction(ActionType type, Order order)
    {
        Type = type;
        Order = order;
    }

    public override string ToString()
    {
        return Type == ActionType.Create
            ? $"Добавлен заказ {Order.Id}"
            : $"Обработан заказ {Order.Id}";
    }
}

public class OrderService
{
    private readonly List<Order> _allOrders = new();
    private readonly Queue<Order> _pendingQueue = new();
    private readonly Stack<UndoAction> _undoHistory = new();

    private int _nextId = 1;

    public Order CreateOrder(string customerName, string productName, decimal price)
    {
        var order = new Order(_nextId++, customerName, productName, price, DateTime.Now);

        _allOrders.Add(order);
        _pendingQueue.Enqueue(order);
        _undoHistory.Push(new UndoAction(ActionType.Create, order));

        return order;
    }

    public Order? ProcessNextOrder()
    {
        if (_pendingQueue.Count == 0)
            return null;

        var order = _pendingQueue.Dequeue();
        _undoHistory.Push(new UndoAction(ActionType.Process, order));
        return order;
    }

    public Order? PeekNextOrder()
    {
        return _pendingQueue.Count > 0 ? _pendingQueue.Peek() : null;
    }

    public string UndoLastAction()
    {
        if (_undoHistory.Count == 0)
            return "История действий пуста. Нечего отменять.";

        var lastAction = _undoHistory.Pop();
        var order = lastAction.Order;

        if (lastAction.Type == ActionType.Create)
        {
            _allOrders.Remove(order);

            var tempQueue = new Queue<Order>(_pendingQueue.Where(o => o.Id != order.Id));
            _pendingQueue.Clear();
            foreach (var o in tempQueue)
            {
                _pendingQueue.Enqueue(o);
            }

            _nextId--;
        }
        else if (lastAction.Type == ActionType.Process)
        {
            var tempQueue = new Queue<Order>();
            tempQueue.Enqueue(order);

            foreach (var o in _pendingQueue)
            {
                tempQueue.Enqueue(o);
            }

            _pendingQueue.Clear();
            foreach (var o in tempQueue)
            {
                _pendingQueue.Enqueue(o);
            }
        }

        return $"Отменено действие: \"{lastAction}\"";
    }

    public IEnumerable<Order> GetAllOrders() => _allOrders;
    public IEnumerable<Order> GetPendingQueue() => _pendingQueue;
    public IEnumerable<string> GetUndoHistory() => _undoHistory.Select(a => a.ToString());
}
