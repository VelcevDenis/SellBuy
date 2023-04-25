using System.ComponentModel.DataAnnotations;

namespace SellBuy.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public ActivityType ActivityType { get; set; }

        public int ActorId { get; set; }

        public int TargetId { get; set; }

        public string Payload { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
