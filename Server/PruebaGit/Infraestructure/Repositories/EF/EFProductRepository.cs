using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaRaddarStudios.Domain.Contracts;
using PruebaRaddarStudios.Domain.Entities;
using PruebaRaddarStudios.Infraestructure.Persistence;

namespace PruebaRaddarStudios.Infraestructure.Repositories.EF;

public class EFProductRepository(DbWriteContext _writeContext, DbReadContext _readContext) : IProductRepository
{
    public Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        _writeContext.Products.Add(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _writeContext.Products.Remove(product);
        return Task.CompletedTask;
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readContext.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken, bool editable = false)
    {
        var product = await _readContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (product is not null && editable)
        {
            _writeContext.Products.Attach(product);
        }

        return product;
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _writeContext.Products.Update(product);
        return Task.CompletedTask;
    }
}
