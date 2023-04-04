using Hb.RedisCacheSample.API.Context;
using Hb.RedisCacheSample.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Hb.RedisCacheSample.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Product> Create(Product product)
        {
            await appDbContext.AddAsync(product);
            await appDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await appDbContext.Products.FindAsync(id);
        }
    }
}
