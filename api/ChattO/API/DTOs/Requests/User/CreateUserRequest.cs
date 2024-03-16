using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.User;

public class CreateUserRequest
{
    [Required]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }  
}

