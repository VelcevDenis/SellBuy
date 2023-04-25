using Microsoft.AspNetCore.Mvc;

namespace SellBuy.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        //private ActivityLogService _activityLogService;

        //public ActivityLogController(ActivityLogService activityLogService)
        //{
        //    _activityLogService = activityLogService;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLog()
        //{
        //    return Ok(await _activityLogService.GetAll());
        //}
    }
}
