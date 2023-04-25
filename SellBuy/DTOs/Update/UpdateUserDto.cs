public class UpdateUserDto
{
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }
    public string? Password { get; set; }
}