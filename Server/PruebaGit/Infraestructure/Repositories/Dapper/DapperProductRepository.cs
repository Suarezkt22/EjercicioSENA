using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using GitEjercicioSENA.Domain.Contracts;
using GitEjercicioSENA.Domain.Entities;
using GitEjercicioSENA.Domain.Entities.ValueObjects;

namespace GitEjercicioSENA.Infraestructure.Repositories.Dapper;

public class DapperProductRepository(SqlConnection _connection) : IProductRepository
{

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Products (Nombre, Descripcion, Precio, Stock, FechaCreacion) 
            VALUES (@Nombre, @Descripcion, @Precio, @Stock, @FechaCreacion);";

        var parameters = new
        {
            Nombre = product.Nombre.Value,
            Descripcion = product.Descripcion.Value,
            Precio = product.Precio.Value,
            Stock = product.Stock.Value,
            FechaCreacion = product.FechaCreacion
        };

        await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Products WHERE Id = @Id;";
        await _connection.ExecuteAsync(sql, new { Id = product.Id });
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Products;";
        var rows = await _connection.QueryAsync<(int Id, string Nombre, string Descripcion, decimal Precio, int Stock, DateTime FechaCreacion)>(sql);

        return [.. rows.Select(row => Product.Build(row.Id, row.Nombre, row.Descripcion, row.Precio, row.Stock, row.FechaCreacion))];
    }

    public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken, bool editable = false)
    {
        const string sql = "SELECT * FROM Products WHERE Id = @Id;";
        var row = await _connection.QueryFirstOrDefaultAsync<(int Id, string Nombre, string Descripcion, decimal Precio, int Stock, DateTime FechaCreacion)>
            (sql, new { Id = productId });

        if (row.Equals(default))
            return null;

        return Product.Build(row.Id, row.Nombre, row.Descripcion, row.Precio, row.Stock, row.FechaCreacion);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Products 
            SET Nombre = @Nombre, 
                Descripcion = @Descripcion, 
                Precio = @Precio, 
                Stock = @Stock
            WHERE Id = @Id;";

        var parameters = new
        {
            product.Id,
            Nombre = product.Nombre.Value,
            Descripcion = product.Descripcion.Value,
            Precio = product.Precio.Value,
            Stock = product.Stock.Value
        };

        await _connection.ExecuteAsync(sql, parameters);
    }
}
