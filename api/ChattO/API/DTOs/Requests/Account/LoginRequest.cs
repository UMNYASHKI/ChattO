using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.Account;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
