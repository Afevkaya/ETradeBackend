namespace ETradeBackend.Application.DTOs.Users;

public record CreateUser(string NameSurname,string Username, string Email, string Password, string PasswordConfirm);