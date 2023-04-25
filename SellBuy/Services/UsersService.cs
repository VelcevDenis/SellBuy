using SellBuy.Repositories;
using SellBuy.Services.Helpers;

namespace SellBuy.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> AddUser(AddUserDto userDto)
        {
            var pbkdf2Hasher = new Pbkdf2Hasher();
            userDto.Password = pbkdf2Hasher.Generate(userDto.Password);

            var isAdded = await _usersRepository.Add(userDto);
            if (!isAdded)
                throw new Exception("user not found");

            var checkUser = await _usersRepository.GetByEmail(userDto.Email);           

            return new User()
            {
                Email = checkUser.Email,
                Id = checkUser.Id,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            };
        }             

        public async Task<IEnumerable<User>> ListUsers()
        {
            var users = new List<User>();
            foreach (var user in await _usersRepository.GetAll())
            {
                users.Add(new User()
                {
                    Email = user.Email,
                    Id = user.Id,
                    RegisteredAt = user.RegisteredAt,
                    Role = user.Role,
                    Status = user.Status
                });

            }
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var checkUser = await _usersRepository.GetById(id);

            if (checkUser == null)
                return null;

            return new User()
            {
                Email = checkUser.Email,
                Id = checkUser.Id,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            };
        }
        
        public async Task<User> Update(int id, UpdateUserDto updateUserDto)
        {
            if (!String.IsNullOrEmpty(updateUserDto.Password))
            {
                var pbkdf2Hasher = new Pbkdf2Hasher();
                updateUserDto.Password = pbkdf2Hasher.Generate(updateUserDto.Password);
            }

            var allUsers = await _usersRepository.GetAll();
            var user = allUsers.FirstOrDefault(x => x.Id == id);

            if (user?.Role == UserRole.Admin && updateUserDto.Role != UserRole.Admin)
            {
                if (updateUserDto.Status != UserStatus.Active)
                {
                    var countOfAdmins = allUsers.Where(x => x.Role == UserRole.Admin).Count();
                    if (countOfAdmins <= 1 && updateUserDto.Role == user.Role)
                        throw new Exception("Even one Admin should be in a system");
                }
            }

            var update = await _usersRepository.UpdateUser(id, updateUserDto);

            if (!update)
                throw new Exception("user not found");

            var checkUser = await _usersRepository.GetById(id);

            return new User()
            {
                Email = checkUser.Email,
                Id = checkUser.Id,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            var allUsers = await _usersRepository.GetAll();
            var user = allUsers.FirstOrDefault(x => x.Id == id);

            if (user?.Role == UserRole.Admin)
            {
                var countOfAdmins = allUsers.Where(x => x.Role == UserRole.Admin).Count();
                if (countOfAdmins <= 1)
                    throw new Exception("Even one Admin should be in a system");
            }

            var checkUser = await _usersRepository.Delete(id);

            if (checkUser == null)
                throw new Exception("user not found");

            return checkUser;
        }
    }
}
