using SellBuy.Services;
using SellBuy.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SellBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private UsersService _usersServicee;

        public UsersController(UsersService usersServicee)
        {
            _usersServicee = usersServicee;
        }

        [HttpPost]
        [Authorize(UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> AddUser(AddUserDto userDto)
        {
            var loginedUser = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            var res = await _usersServicee.AddUser(userDto, loginedUser);
            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpGet]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> ListUsers()
        {
            var res = await _usersServicee.ListUsers();

            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var res = await _usersServicee.GetUser(id);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpGet("/me")]
        [Authorize]
        public async Task<ActionResult<User>> GetMySelf()
        {
            var meUserId = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();

            if (meUserId == null)
                return NotFound();

            var res = await _usersServicee.GetUser(int.Parse(meUserId));

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpPatch("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<User>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var loginedUser = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            var res = await _usersServicee.Update(id, updateUserDto, loginedUser);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpDelete("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var loginedUser = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            var res = await _usersServicee.DeleteUser(id, loginedUser);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}
