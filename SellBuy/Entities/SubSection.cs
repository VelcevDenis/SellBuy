using System.ComponentModel.DataAnnotations;

namespace SellBuy.Entities
{
    public class SubSection
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Json { get; set; }
        public string SubPayments { get; set; }
    }
}
