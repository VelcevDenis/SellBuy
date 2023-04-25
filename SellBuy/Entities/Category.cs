using System.ComponentModel.DataAnnotations;

namespace SellBuy.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }       
        public string Name { get; set; }
        public string IconName { get; set; }
        public bool IsEnabled { get; set; }
        public string JSONSubPayments { get; set; }
    }
}
