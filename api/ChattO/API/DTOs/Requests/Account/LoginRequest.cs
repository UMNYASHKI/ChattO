using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Account;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
