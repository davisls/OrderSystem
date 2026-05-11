namespace Application.DTOs;

public class CreateOrderDto
{
    public string Customer { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public DateTime OrderDate { get; set; }
}