namespace GitEjercicioSENA.Application.Features.Users.V1.DTOs;

public record struct LoginUserRequest(string Email, string Password);