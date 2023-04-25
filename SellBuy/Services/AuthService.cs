using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SellBuy.Services.Helpers;
using Microsoft.Extensions.Options;
using SellBuy.Repositories;
using Sprache;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using SellBuy.Entities;
using Result = CSharpFunctionalExtensions.Result;
using SellBuy.Services;

public class AuthService
{

    private readonly AuthOptions _config;
    private AuthRepository _authRepository;
    private ActivityLogService _activityLogService;

    public AuthService(AuthRepository authRepository, IOptions<AuthOptions> config, ActivityLogService activityLogService)
    {
        _authRepository = authRepository;
        _config = config.Value;
        _activityLogService = activityLogService;
    }

    public async Task<Result<AuthResponseDto>> Login(LoginDto loginDto, string ipAddress, string userAgent)
    {
        var checkUser = await _authRepository.GetUser(loginDto);

        if (checkUser == null)
            return Result.Failure<AuthResponseDto>("user not found");

        var pbkdf2Hasher = new Pbkdf2Hasher();

        if (!pbkdf2Hasher.Verify(loginDto.Password, checkUser.PasswordHash))
            return Result.Failure<AuthResponseDto>("wrong password");

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("id",  checkUser.Id.ToString())
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_config.JWT_SECRET);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_config.JWT_LIFETIME),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var loginInfo = new { IP = ipAddress, UserAgent = userAgent };
        _activityLogService.Add(new ActivityLog
        {
            ActorId = checkUser.Id,
            TargetId = checkUser.Id,
            ActivityType = ActivityType.UserLogin,
            Payload = JsonConvert.SerializeObject(loginInfo)
        });

        return Result.Success(new AuthResponseDto
        {
            AccessToken = tokenHandler.WriteToken(token)
        });
    }
}