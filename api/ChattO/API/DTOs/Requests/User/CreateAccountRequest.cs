using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Requests.User;

public class CreateAccountRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }  
}

