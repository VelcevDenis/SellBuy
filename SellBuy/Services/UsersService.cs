using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using SellBuy.Entities;
using SellBuy.Repositories;
using SellBuy.Services.Helpers;

namespace SellBuy.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository;
        private ActivityLogService _activityLogService;

        public UsersService(UsersRepository usersRepository, ActivityLogService activityLogService)
        {
            _usersRepository = usersRepository;
            _activityLogService = activityLogService;
        }

        public async Task<Result<User>> AddUser(AddUserDto userDto, string loginedUser)
        {
            var pbkdf2Hasher = new Pbkdf2Hasher();
            userDto.Password = pbkdf2Hasher.Generate(userDto.Password);

            var isAdded = await _usersRepository.Add(userDto);
            if (!isAdded)
                return Result.Failure<User>("user not found");

            var checkUser = await _usersRepository.GetByEmail(userDto.Email);

            var newUser = new User()
            {
                Id = checkUser.Id,
                Email = checkUser.Email,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            };

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                TargetId = checkUser.Id,
                ActivityType = ActivityType.UserAdded,
                Payload = JsonConvert.SerializeObject(newUser)
            });

            return Result.Success(newUser);
        }             

        public async Task<IEnumerable<User>> ListUsers()
        {
            var users = new List<User>();
            foreach (var user in await _usersRepository.GetAll())
            {
                users.Add(new User()
                {
                    Id = user.Id,
                    Email = user.Email,                   
                    RegisteredAt = user.RegisteredAt,
                    Role = user.Role,
                    Status = user.Status
                });

            }
            return users;
        }

        public async Task<Result<User>> GetUser(int id)
        {
            var checkUser = await _usersRepository.GetById(id);

            if (checkUser == null)
                return Result.Failure<User>("user not found");

            return Result.Success(new User()
            {
                Email = checkUser.Email,
                Id = checkUser.Id,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            });
        }
        
        public async Task<Result<User>> Update(int id, UpdateUserDto updateUserDto, string loginedUser)
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
                        return Result.Failure<User>("At least one admin should remain");
                }
            }
            var userBeforeUpdate = _usersRepository.GetById(id);

            var update = await _usersRepository.UpdateUser(id, updateUserDto);

            if (!update)
                return Result.Failure<User>("user not found");

            var checkUser = await _usersRepository.GetById(id);

            GenerateUpdateLog(userBeforeUpdate.Result, checkUser, loginedUser);

            return Result.Success(new User()
            {
                Email = checkUser.Email,
                Id = checkUser.Id,
                RegisteredAt = checkUser.RegisteredAt,
                Role = checkUser.Role,
                Status = checkUser.Status
            });
        }

        private void GenerateUpdateLog(User userBeforeUpdate, User user, string loginedUser)
        {
            var compResult = SharedComparer.DiffObjects(
                    userBeforeUpdate,
                    user,
                    new List<string>() {
                    "User.Id",
                    "User.Email",
                    "User.RegisteredAt",
                    }
                );

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                ActivityType = ActivityType.UserUpdated,
                TargetId = user.Id,
                Payload = JsonConvert.SerializeObject(compResult.Differences.Select(s => new
                {
                    Name = s.PropertyName,
                    OldValue = s.Object1Value,
                    NewValue = s.Object2Value
                })),
            });
        }

        public async Task<Result<bool>> DeleteUser(int id, string loginedUser)
        {
            var allUsers = await _usersRepository.GetAll();
            var user = allUsers.FirstOrDefault(x => x.Id == id);

            if (user?.Role == UserRole.Admin)
            {
                var countOfAdmins = allUsers.Where(x => x.Role == UserRole.Admin).Count();
                if (countOfAdmins <= 1)
                    return Result.Failure<bool>("At least one admin should remain");
            }

            var checkUser = await _usersRepository.Delete(id);

            if (checkUser == null)
                return Result.Failure<bool>("user not found");

            _activityLogService.Add(new ActivityLog
            {
                ActorId = Convert.ToInt32(loginedUser),
                TargetId = id,
                ActivityType = ActivityType.UserDeleted,
                Payload = JsonConvert.SerializeObject(id)
            });

            return Result.Success(checkUser);
        }
    }
}
