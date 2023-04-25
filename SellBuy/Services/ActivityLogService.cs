using SellBuy.Entities;
using SellBuy.Repositories;

namespace SellBuy.Services
{
    public class ActivityLogService
    {
        private ActivityLogRepository _activityLogRepository;

        public ActivityLogService(ActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }
        public async void Add(ActivityLog log)
           => _activityLogRepository.Add(log);

        public async Task<IEnumerable<ActivityLog>> GetAll()
        {
            var activityLogs = new List<ActivityLog>();
            foreach (var activLog in await _activityLogRepository.GetAll())
            {
                activityLogs.Add(new ActivityLog()
                {
                    Id = activLog.Id,
                    TargetId = activLog.TargetId,
                    ActorId = activLog.ActorId,
                    ActivityType = activLog.ActivityType,
                    Payload = activLog.Payload,
                    Timestamp = activLog.Timestamp,
                });
            }
            return activityLogs;
        }


    }
}
