using SellBuy.Entities;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int SubProductId { get; set; }
    public int UserId { get; set; }
    public ICollection<Image>? ImageIds { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; }
    public DateTime? ExperiationAt { get; set; }
}

