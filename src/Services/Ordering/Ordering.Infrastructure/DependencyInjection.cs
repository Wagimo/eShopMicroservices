
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices ( this IServiceCollection services, IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString ( "DefaultConnection" );

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor> ();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor> ();

        services.AddDbContext<ApplicationDbContext> ( ( serviceProvider, options ) =>
        {
            //options.AddInterceptors ( 
            //    new AuditableEntityInterceptor (),
            //    //DispatchDomainEventsInterceptor necesita un Imediator para publicar los eventos de dominio
            //    new DispatchDomainEventsInterceptor () 
            //  );

            options.AddInterceptors ( serviceProvider.GetServices<ISaveChangesInterceptor> () );
            options.UseSqlServer ( connectionString );

        } );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext> ();

        return services;
    }
}
