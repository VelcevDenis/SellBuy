public class AuthOptions
{
    public string JWT_SECRET { get; set; } = string.Empty;
    public int JWT_LIFETIME { get; set; } = 0;
}