using PruebaRaddarStudios.Common.Abstract;
using PruebaRaddarStudios.Domain.DTOs;

namespace PruebaRaddarStudios.Domain.Entities;

public class User : TEntity
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    protected User() { }
    
    public static User Build(int id, string email, string password)
    {
        return new User
        {
            Id = id,
            Email = email,
            Password = password
        };
    }

    public static User Create(string email, string password)
    {
        return new User
        {
            Email = email,
            Password = password
        };
    }

    public void UpdateSecuredPassword(string securedPassword)
    {
        Password = securedPassword;
    }

    public UserDataDto RetrieveData()
    {
        return new UserDataDto(Id, Email);
    }
}