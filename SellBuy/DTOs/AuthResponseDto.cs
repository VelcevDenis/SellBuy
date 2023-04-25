using System.ComponentModel.DataAnnotations;

public class AuthResponseDto
	{
		[Required]
		public string AccessToken { get; set; }

		[Required]
		public string RefreshToken { get; set; }
	}