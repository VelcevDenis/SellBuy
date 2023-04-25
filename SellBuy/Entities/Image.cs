using System.ComponentModel.DataAnnotations;

namespace SellBuy.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Values { get; set; }
    }
}
