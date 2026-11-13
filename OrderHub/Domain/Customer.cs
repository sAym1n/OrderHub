namespace OrderHub.Domain;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; }

    public Customer(int id, string name, string city)
    {
        Id = id;
        Name = name;
        City = city;
    }

    public override string ToString() => $"{Name} (ID: {Id}, г. {City})";
}
