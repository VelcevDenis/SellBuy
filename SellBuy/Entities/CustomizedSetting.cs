using System.ComponentModel.DataAnnotations;

namespace SellBuy.Entities
{
    public class CustomizedSetting
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string JSONSettings { get; set; }
    }
}
