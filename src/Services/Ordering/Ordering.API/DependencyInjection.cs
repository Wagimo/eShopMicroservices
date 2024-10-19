namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices ( this IServiceCollection services )
    {
        // services.Adcarter();
        return services;
    }

    public static WebApplication UseApiServices ( this WebApplication app )
    {
        //app.Mapcarter();



        return app;
    }

}

