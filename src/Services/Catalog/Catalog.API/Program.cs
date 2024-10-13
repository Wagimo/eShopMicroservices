



var builder = WebApplication.CreateBuilder ( args );

// Add services to the container

var assembly = typeof ( Program ).Assembly;
builder.Services.AddMediatR ( config =>
{
    config.RegisterServicesFromAssemblies ( assembly );
    config.AddOpenBehavior ( typeof ( ValidationBehavior<,> ) );
} );

builder.Services.AddValidatorsFromAssembly ( assembly );

builder.Services.AddCarter ();

builder.Services.AddMarten ( opt =>
{
    opt.Connection ( builder.Configuration.GetConnectionString ( "DefaultConnection" )! );
} ).UseLightweightSessions ();

var app = builder.Build ();

//Configure the HTTp pipelines

app.MapCarter ();

app.Run ();
