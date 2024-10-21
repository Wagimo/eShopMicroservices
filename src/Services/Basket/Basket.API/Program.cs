


var builder = WebApplication.CreateBuilder ( args );

//Add services into container
builder.Services.AddCarter ();
var assembly = typeof ( Program ).Assembly;
var connectionString = builder.Configuration.GetConnectionString ( "DefaultConnection" )!;
var redisConnectionString = builder.Configuration.GetConnectionString ( "Redis" )!;

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

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient> ( o =>
{
    o.Address = new Uri ( builder.Configuration["GrpcSettings:DiscountUrl"]! );
} ).ConfigurePrimaryHttpMessageHandler ( ( ) => //Omite el certificado SSL
{
    var handler = new HttpClientHandler
    {
        // Return `true` to allow certificates that are untrusted/invalid
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
} );

builder.Services.AddMessageBroker ( builder.Configuration );

builder.Services.AddHealthChecks ()
    .AddNpgSql ( connectionString )
    .AddRedis ( redisConnectionString );

var app = builder.Build ();

//Configure the HTTP request pipeline
app.MapCarter ();

app.UseExceptionHandler ( op => { } );

app.UseHealthChecks ( "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    } );

app.Run ();
