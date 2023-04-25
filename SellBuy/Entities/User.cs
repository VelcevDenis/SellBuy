using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; } = string.Empty;    
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Disabled;
}