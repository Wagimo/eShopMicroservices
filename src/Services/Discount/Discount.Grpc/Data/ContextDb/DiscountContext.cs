

namespace Discount.Grpc.Data.ContextDb;

public class DiscountContext ( DbContextOptions<DiscountContext> options ) : DbContext ( options )
{
    public DbSet<Cupon> Cupons { get; set; }

    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {


        modelBuilder.Entity<Cupon> ().HasData (
            new Cupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
            new Cupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 },
            new Cupon { Id = 3, ProductName = "Huawei P30", Description = "Huawei Discount", Amount = 50 },
            new Cupon { Id = 4, ProductName = "Xiaomi Mi 10", Description = "Xiaomi Discount", Amount = 75 },
            new Cupon { Id = 5, ProductName = "IPhone 11", Description = "IPhone Discount", Amount = 200 }
            );
    }
}
