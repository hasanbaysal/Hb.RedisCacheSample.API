using Hb.RedisCacheSample.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Hb.RedisCacheSample.API.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>().HasData(new Product[]
            {

                new(){Id=1,Name="product 1",Price=1.2m},
                new(){Id=2,Name="product 2",Price=2.2m},
                new(){Id=3,Name="product 3",Price=3.2m},
                new(){Id=4,Name="product 4",Price=4.2m},
                new(){Id=5,Name="product 5",Price=5.2m},
                new(){Id=6,Name="product 6",Price=6.2m},
                new(){Id=7,Name="product 7",Price=123.2m},
                new(){Id=8,Name="product 8",Price=7.2m},

            });


            base.OnModelCreating(modelBuilder); 
        }

    }
}
