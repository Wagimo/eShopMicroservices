

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.

builder.Services
    .AddApplicationServices ()
    .AddInfrastructureServices ( builder.Configuration )
    .AddPresentationServices ();

var app = builder.Build ();

//Configure the HTTP request pipeline.

app.Run ();
