namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public string Customer { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public DateTime OrderDate { get; set; }
}