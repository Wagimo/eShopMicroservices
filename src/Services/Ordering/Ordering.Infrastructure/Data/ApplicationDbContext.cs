﻿

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : DbContext ( options ), IApplicationDbContext
{
    public DbSet<Order> Orders => Set<Order> ();
    public DbSet<OrderItem> OrderItems => Set<OrderItem> ();
    public DbSet<Product> Products => Set<Product> ();
    public DbSet<Customer> Customers => Set<Customer> ();



    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfigurationsFromAssembly ( Assembly.GetExecutingAssembly () );
        base.OnModelCreating ( modelBuilder );
    }
}
