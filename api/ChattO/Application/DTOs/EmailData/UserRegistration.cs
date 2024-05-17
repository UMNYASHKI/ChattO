using Domain.Enums;

namespace Application.DTOs.EmailData;

public record UserRegistration(string DisplayName,
    string Email,
    AppUserRole Role,
    string GenaratedPassword);
