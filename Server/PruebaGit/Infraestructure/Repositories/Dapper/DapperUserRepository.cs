using Dapper;
using Microsoft.Data.Sqlite;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Infraestructure.Repositories.Dapper;

public class DapperUserRepository(SqliteConnection _connection) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Users (Email, Password) 
            VALUES (@Email, @Password);";

        await _connection.ExecuteAsync(sql, new
        {
            user.Email,
            user.Password
        });
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Email, Password 
            FROM Users 
            WHERE Email = @Email;";

        var result = await _connection.QueryFirstOrDefaultAsync(sql, new { Email = email });

        if (result == null) return null;

        return User.Build(
            id: result.Id,
            email: result.Email,
            password: result.Password
        );
    }
}