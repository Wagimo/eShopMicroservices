

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices ( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddCarter ();
        services.AddExceptionHandler<CustomExceptionHandler> ();
        services.AddHealthChecks ()
            .AddSqlServer ( configuration.GetConnectionString ( "DefaultConnection" )! );
        return services;
    }

    public static WebApplication UseApiServices ( this WebApplication app )
    {
        app.MapCarter ();

        app.UseExceptionHandler ( options => { } );

        app.UseHealthChecks ( "/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            } );

        return app;
    }

}

