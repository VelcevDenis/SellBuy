using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using SellBuy.Entities;
using SellBuy.Repositories;
using SellBuy.Services.Helpers;

namespace SellBuy.Services
{
    public class OrdersService
    {
        private OrdersRepository _ordersRepository;
        private ActivityLogService _activityLogService;

        public OrdersService(OrdersRepository ordersRepository, ActivityLogService activityLogService)
        {
            _ordersRepository = ordersRepository;
            _activityLogService = activityLogService;
        }

        public async Task<Result<Order>> AddOrder(AddOrderDto OrderDto, string loginedUser)
        {
            var checkOrder = await _ordersRepository.Add(OrderDto);
            if (checkOrder==null)
                return Result.Failure<Order>("order not found");

            var newOrder = new Order()
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

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                TargetId = newOrder.Id,
                ActivityType = ActivityType.OrderAdded,
                Payload = JsonConvert.SerializeObject(newOrder)
            });

            return Result.Success(newOrder);
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
        public async Task<IEnumerable<Order>> GetMyListOfOrders(int id)
        {
            var orders = new List<Order>();
            foreach (var order in await _ordersRepository.GetAll(null, id))
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

        public async Task<Result<Order>> GetOrder(int id)
        {
            var checkOrder = await _ordersRepository.GetById(id);

            if (checkOrder == null)
                return Result.Failure<Order>("order not found");

            return Result.Success(new Order()
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
            });
        }
        
        public async Task<Result<Order>> Update(int id, UpdateOrderDto updateOrderDto, string loginedUser)
        {
            var orderBeforeUpdate = await _ordersRepository.GetById(id);
            var update = await _ordersRepository.UpdateOrder(id, updateOrderDto);

            if (!update)
                return Result.Failure<Order>("order not found");  

            var checkUser = await _ordersRepository.GetById(id);

            GenerateUpdateLog(orderBeforeUpdate, checkUser, loginedUser);

            return Result.Success(new Order()
            {
                Id = orderBeforeUpdate.Id,
                SubProductId = orderBeforeUpdate.SubProductId,
                Description = orderBeforeUpdate.Description,
                Title = orderBeforeUpdate.Title,
                Price = orderBeforeUpdate.Price,
                UserId = orderBeforeUpdate.UserId,
                CreateAt = orderBeforeUpdate.CreateAt,
                ExperiationAt = orderBeforeUpdate.ExperiationAt,
                UpdateAt = orderBeforeUpdate.UpdateAt
            });
        }

        private void GenerateUpdateLog(Order orderBeforeUpdate, Order order, string loginedUser)
        {
            var compResult = SharedComparer.DiffObjects(
                    orderBeforeUpdate,
                    order,
                    new List<string>() {
                    "Order.Id",
                    "Order.SubProductId",
                    "Order.UserId",
                    "Order.CreateAt",
                    "Order.UpdateAt",
                    }
                );

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                ActivityType = ActivityType.OrderUpdated,
                TargetId = order.Id,
                Payload = JsonConvert.SerializeObject(compResult.Differences.Select(s => new
                {
                    Name = s.PropertyName,
                    OldValue = s.Object1Value,
                    NewValue = s.Object2Value
                })),
            });
        }

        public async Task<Result<bool>> DeleteOrder(int id, string loginedUser)
        {         
            var checkOrder = await _ordersRepository.Delete(id);

            if (checkOrder == null)
                return Result.Failure<bool>("order not found");

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                TargetId = id,
                ActivityType = ActivityType.OrderDeleted,
                Payload = JsonConvert.SerializeObject(id)
            });

            return Result.Success(checkOrder);
        }
    }
}
