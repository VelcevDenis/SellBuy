using System.ComponentModel.DataAnnotations;

public class UpdateOrderDto
{
    public string Title { get; set; }
    public decimal Price { get; set; }    
    public string Description { get; set; }
    public int CategoryId { get; set; }   
    public DateTime? ExperiationAt { get; set; }
}