using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Domain.Contracts;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken, bool editable = false);

    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task CreateAsync(Product product, CancellationToken cancellationToken);

    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    
    Task DeleteAsync(Product product, CancellationToken cancellationToken);
}