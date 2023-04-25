using SellBuy.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace SellBuy.Repositories
{
    public class UsersRepository
    {
        private readonly DapperHelper _dapperHelper;

        public UsersRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<bool> Add(AddUserDto userDto)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "INSERT INTO dbo.users (Email, PasswordHash, UserRole, Status, RegisteredAt) VALUES (@Email, @Password, @UserRole, @Status, @RegisteredAt)";

            return await connection.ExecuteAsync(queryRes, new
            {
                Email = userDto.Email,
                Username = userDto.Username,
                Password = userDto.Password,
                UserRole = userDto.UserRole,
                Status = UserStatus.Active,
                RegisteredAt = DateTime.UtcNow,
            }) > 0;
        }

        public async Task<User> GetById(int id)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.users where Id=@id";

            return await connection.QuerySingleOrDefaultAsync<User>(queryRes, new
            {
                id = id
            });
        }

        public async Task<User> GetByEmail(string email)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.users where Email=@Email";

            return await connection.QuerySingleOrDefaultAsync<User>(queryRes, new
            {
                Email = email
            });
        }

        public async Task<IEnumerable<User>> GetAll(FromToDateDto fromToDateDto)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.users where RegisteredAt BETWEEN @From AND @To";

            var transactions = await connection.Query<User>(queryRes, new
            {
                From = fromToDateDto.FromDate,
                To = fromToDateDto.ToDate
            });

            return transactions.ToList();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.users";

            var users = await connection.QueryAsync<User>(queryRes);
            return users.Distinct().ToList();
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var connection = _dapperHelper.GetConnection();
            var parameters = new DynamicParameters();
            var password = "";

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                password = ", PasswordHash=@Password";
                parameters.Add("Password", updateUserDto.Password, DbType.String);
            }

            var queryRes = "UPDATE dbo.users SET Status=@Status, UserRole=@Role " + password + " WHERE Id=@Id";

            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Status", updateUserDto.Status, DbType.String);
            parameters.Add("Role", updateUserDto.Role, DbType.String);

            return await connection.ExecuteAsync(queryRes, parameters) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "DELETE FROM dbo.users WHERE Id = @Id";

            return await await connection.ExecuteAsync(queryRes, new
            {
                Id = id
            }) > 0;
        }
    }
}
