

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container
builder.Services.AddCarter ();

builder.Services.AddMediatR ( config =>
{
    config.RegisterServicesFromAssemblies ( typeof ( Program ).Assembly );
} );
builder.Services.AddMarten ( opt =>
{
    opt.Connection ( builder.Configuration.GetConnectionString ( "DefaultConnection" )! );
} ).UseLightweightSessions ();

var app = builder.Build ();

//Configure the HTTp pipelines

app.MapCarter ();

app.Run ();
