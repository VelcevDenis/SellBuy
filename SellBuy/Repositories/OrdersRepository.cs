using SellBuy.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace SellBuy.Repositories
{
    public class OrdersRepository
    {
        private readonly DapperHelper _dapperHelper;

        public OrdersRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        
        public async Task<Order> Add(AddOrderDto orderDto)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = @"INSERT INTO dbo.Orders (
                                Price,
                                Title, 
                                Description, 
                                CategoryId, 
                                UserId, 
                                CreateAt, 
                                ExperiationAt
                            ) VALUES (
                                @Price, 
                                @Title, 
                                @Description, 
                                @CategoryId, 
                                @UserId,
                                @CreateAt,
                                @ExperiationAt
                            )";

            return await connection.QuerySingleOrDefaultAsync<Order>(queryRes, new
            {
                Price = orderDto.Price,
                Title = orderDto.Title,
                Description = orderDto.Description,
                CategoryId = orderDto.CategoryId,
                UserId = orderDto.UserId,
                CreateAt = DateTime.UtcNow,
                ExperiationAt = orderDto.ExperiationAt
            });
        }

        public async Task<Order> GetById(int id)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.Orders where Id=@id";

            return await connection.QuerySingleOrDefaultAsync<Order>(queryRes, new
            {
                id = id
            });            
        }

        public async Task<IEnumerable<Order>> GetAll(FromToDateDto? fromToDateDto = null, int? id = null)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.Orders";

            var parameters = new DynamicParameters();

            if(fromToDateDto != null || id != null)
                queryRes = queryRes + " where";

            if (fromToDateDto != null)
            {
                queryRes = queryRes + " CreateAt BETWEEN @From AND @To";
                parameters.Add("From", fromToDateDto.FromDate, DbType.DateTime);
                parameters.Add("To", fromToDateDto.ToDate, DbType.DateTime);
            }
            if (id != null)
            {
                queryRes = queryRes + " AND @id";
                parameters.Add("id", id, DbType.Int32);
            }

            var transactions = await connection.QueryAsync<Order>(queryRes, parameters);
            return transactions.ToList();
        }

        public async Task<bool> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            var connection = _dapperHelper.GetConnection();

            var queryRes = @"INSERT INTO dbo.Orders (
                              Price,
                              Title, 
                              Description, 
                              CategoryId, 
                              UserId, 
                              UpdateAt, 
                              ExperiationAt 
                            ) VALUES (
                              @Price, 
                              @Title, 
                              @Description, 
                              @CategoryId, 
                              @UserId, 
                              @UpdateAt,
                              @ExperiationAt
                            )";

            return await connection.ExecuteAsync(queryRes, new
            {
                Id = id,
                Price = updateOrderDto.Price,
                Title = updateOrderDto.Title,
                Description = updateOrderDto.Description,
                CategoryId = updateOrderDto.CategoryId,
                UpdateAt = DateTime.UtcNow,
                ExperiationAt = updateOrderDto.ExperiationAt
            }) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "DELETE FROM dbo.Orders WHERE Id = @Id";

            return await connection.ExecuteAsync(queryRes, new
            {
                Id = id
            }) > 0;            
        }
    }
}
