using Hb.RedisCacheSample.API.Context;
using Hb.RedisCacheSample.API.Repository;
using Hb.RedisCacheSample.Cache;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("db");
});

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductRepository>(sp =>
{

    var appDbContext = sp.GetRequiredService<AppDbContext>();
    var productRespository = new ProductRepository(appDbContext);
    var redisService = sp.GetRequiredService<RedisService>();

    return new ProductRepositoryWithCache(productRespository,redisService);

});

builder.Services.AddSingleton<RedisService>();

builder.Services.AddScoped<IDatabase>(sp =>
{

    var redisService = sp.GetRequiredService<RedisService>();

    return redisService.GetDatabase(0);

});


var app = builder.Build();



 using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

}




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
