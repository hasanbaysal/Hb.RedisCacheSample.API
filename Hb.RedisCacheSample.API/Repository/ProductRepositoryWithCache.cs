using Hb.RedisCacheSample.API.Model;
using Hb.RedisCacheSample.Cache;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Hb.RedisCacheSample.API.Repository
{
    //decorator dp
    public class ProductRepositoryWithCache : IProductRepository
    {
        //product'ın ef core tarafı db işlemler için bana bu lazım
        private readonly IProductRepository _repository;
        private readonly IDatabase CacheDatabase;
        private readonly RedisService redis;

        private const string key = "productsCache";

        public ProductRepositoryWithCache(IProductRepository repository, RedisService redis)
        {
            _repository = repository;
            this.redis = redis;



            CacheDatabase = redis.GetDatabase(0);
        }

        public async Task<Product> Create(Product product)
        {

            var data = await _repository.Create(product);

            await CacheDatabase.HashSetAsync(key, data.Id, JsonSerializer.Serialize(data));

            return data;

        }

        public async Task<List<Product>> GetAllAsync()
        {
            if ( !await CacheDatabase.KeyExistsAsync(key))
            {
                return await LoadToCacheFromDbAsync();
            }

            var products = new List<Product>();

            var cacheProducts = await CacheDatabase.HashGetAllAsync(key); 

            foreach (var item in  cacheProducts.ToList())
            {
                products.Add(JsonSerializer.Deserialize<Product>(item.Value!)!);
            }
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            if (await CacheDatabase.KeyExistsAsync(key))
            {
                var product = CacheDatabase.HashGet(key, id);

                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }


            var data = await _repository.GetByIdAsync(id);

           await CacheDatabase.HashSetAsync(key,data.Id,JsonSerializer.Serialize(data));

            return data;

        }

        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products = await _repository.GetAllAsync();

            products.ForEach(p =>
            {
                CacheDatabase.HashSetAsync(key, p.Id, JsonSerializer.Serialize(p));
            });

            return products;
        }

    }
}
