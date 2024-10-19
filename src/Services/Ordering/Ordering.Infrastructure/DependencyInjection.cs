
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices ( this IServiceCollection services, IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString ( "DefaultConnection" );

        services.AddDbContext<ApplicationDbContext> ( options =>
        {
            options.AddInterceptors ( new AuditableEntityInterceptor () );
            options.UseSqlServer ( connectionString );

        } );

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext> ();

        return services;
    }
}
