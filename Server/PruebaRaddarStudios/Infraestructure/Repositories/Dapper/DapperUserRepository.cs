using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;
using PruebaRaddarStudios.Domain.Entities.ValueObjects;

namespace PruebaRaddarStudios.Infraestructure.Repositories.Dapper;

public class DapperUserRepository(SqlConnection _connection) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Users (Email, Password) 
            VALUES (@Email, @Password);";

        var parameters = new
        {
            user.Email,
            user.Password
        };

        await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Email, Password 
            FROM Users 
            WHERE Email = @Email;";

        var result = await _connection.QueryFirstOrDefaultAsync(sql, new { Email = email });

        if (result == null)
        {
            return null;
        }

        return User.Build(
            id: result.Id,
            email: result.Email,
            password: result.Password
        );
    }
}
