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
        
        //[HttpPost]
        //[Authorize(UserRole.Admin)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<ActionResult<User>> AddUser(AddUserDto userDto)
        //{
        //    var res = await _usersServicee.AddUser(userDto);
        //    return Ok(res);
        //}

        //[HttpGet]
        //[Authorize(UserRole.Admin)]
        //public async Task<ActionResult<IEnumerable<User>>> ListUsers()
        //{
        //    var res = await _usersServicee.ListUsers();

        //    return Ok(res);
        //}

        //[HttpGet("{id}")]
        //[Authorize(UserRole.Admin)]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var res = await _usersServicee.GetUser(id);

        //    return res != null ? Ok(res) : NotFound();
        //}

        //[HttpGet("/me")]
        //[Authorize]
        //public async Task<ActionResult<User>> GetMySelf()
        //{
        //    var meUserId = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            
        //    if (meUserId == null) 
        //        return NotFound();

        //    var res = await _usersServicee.GetUser(int.Parse(meUserId));

        //    return res != null ? Ok(res) : NotFound();
        //}

        //[HttpPatch("{id}")]
        //[Authorize(UserRole.Admin)]
        //public async Task<ActionResult<User>> UpdateUser(int id, UpdateUserDto updateUserDto)
        //{
        //    var res = await _usersServicee.Update(id, updateUserDto);

        //    return res != null ? Ok(res) : BadRequest();
        //}

        //[HttpDelete("{id}")]
        //[Authorize(UserRole.Admin)]
        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    var res = await _usersServicee.DeleteUser(id);

        //    return res ? Ok(res) : BadRequest();
        //}
    }
}
