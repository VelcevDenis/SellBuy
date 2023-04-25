using Dapper;
using SellBuy.Entities;

namespace SellBuy.Repositories
{
    public class ActivityLogRepository
    {
        private readonly DapperHelper _dapperHelper;

        public ActivityLogRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async void Add(ActivityLog log)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "INSERT INTO dbo.activityLogs (ActivityType, ActorId, TargetId, Payload, Timestamp) VALUES (@ActivityType, @ActorId, @TargetId, @Payload, @Timestamp)";

            await connection.ExecuteAsync(queryRes, new
            {
                log.ActivityType,
                log.ActorId,
                log.TargetId,
                log.Payload,
                Timestamp = DateTime.UtcNow,
            });
        }

        public async Task<IEnumerable<ActivityLog>> GetAll()
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.activityLogs ORDER BY Timestamp DESC";

            var clients = await connection.QueryAsync<ActivityLog>(queryRes);

            return clients.Distinct().ToList();
        }
    }
}
