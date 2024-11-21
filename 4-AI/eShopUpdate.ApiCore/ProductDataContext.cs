using eShopUpdate.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eShopUpdate.Api
{
    public class ProductDataContext : DbContext
    {
        public ProductDataContext(DbContextOptions<ProductDataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .OwnsMany(p => p.Reviews, a =>
                {
                    a.WithOwner().HasForeignKey("ProductId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                });
        }
    }

    public static class Extensions
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ProductDataContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
    }


    public static class DbInitializer
    {
        public static void Initialize(ProductDataContext context)
        {
            if (context.Products.Any())
                return;

            var productData =
                File.ReadAllText("products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productData);

            context.AddRange(products);

            context.SaveChanges();
        }
    }
}