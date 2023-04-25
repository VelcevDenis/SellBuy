
using Microsoft.AspNetCore.Mvc;
using SellBuy.Services;
using SellBuy.Services.Helpers;

namespace SellBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase
    {
        private OrdersService _OrdersServicee;

        public OrdersController(OrdersService OrdersServicee)
        {
            _OrdersServicee = OrdersServicee;
        }

        [HttpPost]
        [Authorize(UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Order>> AddOrder(AddOrderDto orderDto)
        {
            var meOrderId = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            orderDto.UserId = int.Parse(meOrderId);
            var res = await _OrdersServicee.AddOrder(orderDto);
            return Ok(res);
        }

        [HttpGet]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<Order>>> ListOrders()
        {
            var res = await _OrdersServicee.ListOrders();

            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var res = await _OrdersServicee.GetOrder(id);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpGet("/my")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyListOfOrders()
        {
            var res = await _OrdersServicee.ListOrders();

            return Ok(res);
        }

        [HttpGet("/my")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            var meOrderId = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();

            if (meOrderId == null)
                return NotFound();

            var res = await _OrdersServicee.GetMyListOfOrders(int.Parse(meOrderId));

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpPatch("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<Order>> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            var res = await _OrdersServicee.Update(id, updateOrderDto);

            return res != null ? Ok(res) : BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(UserRole.Admin)]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var loginedUser = HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            var res = await _OrdersServicee.DeleteOrder(id, loginedUser);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}