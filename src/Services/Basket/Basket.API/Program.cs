

using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder ( args );

//Add services into container
builder.Services.AddCarter ();
var assembly = typeof ( Program ).Assembly;
var connectionString = builder.Configuration.GetConnectionString ( "DefaultConnection" )!;

builder.Services.AddMediatR ( config =>
{
    config.RegisterServicesFromAssemblies ( assembly );
    config.AddOpenBehavior ( typeof ( ValidationBehavior<,> ) );
    config.AddOpenBehavior ( typeof ( LoggingBehavior<,> ) );
} );


builder.Services.AddMarten ( config =>
{
    config.Connection ( connectionString );
    //Configuramos la propiedad username como la llave primaria de la tabla shoppingcart
    config.Schema.For<ShoppingCart> ().Identity ( x => x.UserName );

} ).UseLightweightSessions ();

builder.Services.AddExceptionHandler<CustomExceptionHandler> ();

builder.Services.AddScoped<IBasketRepository, BasketRepository> ();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository> ();

builder.Services.AddStackExchangeRedisCache ( options =>
{
    options.Configuration = builder.Configuration.GetConnectionString ( "Redis" );
    //options.InstanceName = "Basket_";
} );


var app = builder.Build ();

//Configure the HTTP request pipeline
app.MapCarter ();

app.UseExceptionHandler ( op => { } );

app.Run ();
