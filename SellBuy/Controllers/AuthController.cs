using Microsoft.AspNetCore.Mvc;

namespace SellBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var res = await _authService.Login(loginDto, ipAddress, userAgent);
            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}
