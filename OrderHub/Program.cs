using System;
using System.Linq;
using OrderHub.Collections;

var buffer = new CircularBuffer<string>(3);

buffer.Add("1");
buffer.Add("2");
buffer.Add("3");
buffer.Add("4");
buffer.Add("5");

foreach (var item in buffer)
{
    Console.WriteLine(item);
}

foreach (var item in buffer.Reverse())
{
    Console.WriteLine(item);
}

Console.WriteLine($"Count: {buffer.Count}, IsFull: {buffer.IsFull}");

foreach (var formatted in buffer.Select(x => $"[{x}]"))
{
    Console.WriteLine(formatted);
}
