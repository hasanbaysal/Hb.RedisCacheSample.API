using Hb.RedisCacheSample.API.Model;
using Hb.RedisCacheSample.API.Repository;
using Hb.RedisCacheSample.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Hb.RedisCacheSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repository;

      //  private readonly IDatabase database;
        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }


        //  private readonly RedisService redis;

        //public ProductsController(IProductRepository repository, RedisService redis)
        //{
        //    this.repository = repository;
        //    this.redis = redis;

        //    var db = redis.GetDatabase(0);
        //    db.StringSet("name", "hasan");
        //}


        //public ProductsController(IProductRepository repository, IDatabase database)
        //{
        //    this.repository = repository;
        //    this.database = database;
        //}


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            return Ok(await repository.GetAllAsync());
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            return Ok(await repository.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product p)
        {

            return Created(string.Empty, await repository.Create(p));

         }

    }
}
