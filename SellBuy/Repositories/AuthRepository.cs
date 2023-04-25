using Dapper;

namespace SellBuy.Repositories
{
    public class AuthRepository
    {
        private readonly DapperHelper _dapperHelper;

        public AuthRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<User> GetUser(LoginDto loginDto)
        {
            var connection = _dapperHelper.GetConnection();
            var queryRes = "SELECT * FROM dbo.users where Email=@Email";

            return await connection.QuerySingleOrDefaultAsync<User>(queryRes, new
            {
                Email = loginDto.Email
            });
        }
    }
}
