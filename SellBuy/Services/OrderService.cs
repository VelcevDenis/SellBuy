using SellBuy.Repositories;
using SellBuy.Services.Helpers;

namespace SellBuy.Services
{
    public class OrdersService
    {
        private OrdersRepository _ordersRepository;

        public OrdersService(OrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> AddOrder(AddOrderDto OrderDto)
        {
            var checkOrder = await _ordersRepository.Add(OrderDto);
            if (checkOrder==null)
                throw new Exception("Order not found");

            return new Order()
            {
                Id= checkOrder.Id,
                SubProductId= checkOrder.SubProductId,
                Description= checkOrder.Description,
                Title= checkOrder.Title,
                Price = checkOrder.Price,
                UserId= checkOrder.UserId,
                CreateAt= checkOrder.CreateAt,
                ExperiationAt= checkOrder.ExperiationAt,
                UpdateAt = checkOrder.UpdateAt
            };
        }             

        public async Task<IEnumerable<Order>> ListOrders()
        {
            var orders = new List<Order>();
            foreach (var order in await _ordersRepository.GetAll())
            {
                orders.Add(new Order()
                {
                    Id = order.Id,
                    SubProductId = order.SubProductId,
                    Description = order.Description,
                    Title = order.Title,
                    Price = order.Price,
                    UserId = order.UserId,
                    CreateAt = order.CreateAt,
                    ExperiationAt = order.ExperiationAt,
                    UpdateAt = order.UpdateAt
                });

            }
            return orders;
        }

        public async Task<Order> GetOrder(int id)
        {
            var checkOrder = await _ordersRepository.GetById(id);

            if (checkOrder == null)
                return null;

            return new Order()
            {
                Id = checkOrder.Id,
                SubProductId = checkOrder.SubProductId,
                Description = checkOrder.Description,
                Title = checkOrder.Title,
                Price = checkOrder.Price,
                UserId = checkOrder.UserId,
                CreateAt = checkOrder.CreateAt,
                ExperiationAt = checkOrder.ExperiationAt,
                UpdateAt = checkOrder.UpdateAt
            };
        }
        
        public async Task<Order> Update(int id, UpdateOrderDto updateOrderDto)
        {
            var update = await _ordersRepository.UpdateOrder(id, updateOrderDto);

            if (!update)
                throw new Exception("Order not found");

            var checkOrder = await _ordersRepository.GetById(id);

            return new Order()
            {
                Id = checkOrder.Id,
                SubProductId = checkOrder.SubProductId,
                Description = checkOrder.Description,
                Title = checkOrder.Title,
                Price = checkOrder.Price,
                UserId = checkOrder.UserId,
                CreateAt = checkOrder.CreateAt,
                ExperiationAt = checkOrder.ExperiationAt,
                UpdateAt = checkOrder.UpdateAt
            };
        }

        public async Task<bool> DeleteOrder(int id)
        {         
            var checkOrder = await _ordersRepository.Delete(id);

            if (checkOrder == null)
                throw new Exception("Order not found");

            return checkOrder;
        }
    }
}
