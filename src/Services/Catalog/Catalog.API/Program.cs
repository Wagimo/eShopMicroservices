

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container

var assembly = typeof ( Program ).Assembly;
var connectionString = builder.Configuration.GetConnectionString ( "DefaultConnection" )!;
builder.Services.AddMediatR ( config =>
{
    config.RegisterServicesFromAssemblies ( assembly );
    config.AddOpenBehavior ( typeof ( ValidationBehavior<,> ) );
    config.AddOpenBehavior ( typeof ( LoggingBehavior<,> ) );
} );

builder.Services.AddValidatorsFromAssembly ( assembly );

builder.Services.AddCarter ();

builder.Services.AddMarten ( opt =>
{
    opt.Connection ( connectionString );
} ).UseLightweightSessions ();

if (builder.Environment.IsDevelopment ())
    builder.Services.InitializeMartenWith<CatalogInitialData> ();

builder.Services.AddExceptionHandler<CustomExceptionHandler> ();

builder.Services.AddHealthChecks ()
    .AddNpgSql ( connectionString );


var app = builder.Build ();

//Configure the HTTp pipelines

app.MapCarter ();

app.UseExceptionHandler ( op => { } );

app.UseHealthChecks ( "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    } );

app.Run ();
