namespace SellBuy.DTOs
{
    public class FromToDateDto
    {
        public DateTime FromDate { get; set; } = DateTime.MinValue;
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
