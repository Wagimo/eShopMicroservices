using Microsoft.AspNetCore.Builder;


namespace Ordering.Infrastructure.Data.Extensions;

public static class DataBaseExtentions
{
    public static async Task InitializeDatabaseAsync ( this WebApplication app )
    {
        //Se ejecutan Las migraciones
        using var scope = app.Services.CreateScope ();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext> ();
        context.Database.MigrateAsync ().GetAwaiter ().GetResult ();

        //Se Siembra la base de datos
        await SeedAsync ( context );
    }

    private static async Task SeedAsync ( ApplicationDbContext context )
    {
        await SeedCustomerAsync ( context );
        await SeedProductsAsync ( context );
        await SeedOrderAndItemsAsync ( context );
    }


    private static async Task SeedCustomerAsync ( ApplicationDbContext context )
    {
        if (!await context.Customers.AnyAsync ())
        {
            context.Customers.AddRange ( InitialData.Customers );
            await context.SaveChangesAsync ();
        }
    }

    private static async Task SeedProductsAsync ( ApplicationDbContext context )
    {
        if (!await context.Products.AnyAsync ())
        {
            context.Products.AddRange ( InitialData.Products );
            await context.SaveChangesAsync ();
        }
    }


    private static async Task SeedOrderAndItemsAsync ( ApplicationDbContext context )
    {
        if (!await context.Orders.AnyAsync ())
        {
            context.Orders.AddRange ( InitialData.OrdersWithItems );
            await context.SaveChangesAsync ();
        }
    }


}
