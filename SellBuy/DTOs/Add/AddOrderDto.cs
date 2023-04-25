using System.ComponentModel.DataAnnotations;

public class AddOrderDto
{
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; } = 0;
    public DateTime? ExperiationAt { get; set; }
}