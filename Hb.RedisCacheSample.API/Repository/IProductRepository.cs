using Hb.RedisCacheSample.API.Model;

namespace Hb.RedisCacheSample.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> Create(Product product);


    }
}
