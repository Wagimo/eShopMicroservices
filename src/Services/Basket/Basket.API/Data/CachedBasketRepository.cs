


namespace Basket.API.Data;

public class CachedBasketRepository ( IBasketRepository repository, IDistributedCache cache ) : IBasketRepository
{

    public async Task<ShoppingCart> GetBasket ( string userName, CancellationToken cancellation = default )
    {
        var basketCache = await cache.GetStringAsync ( userName, cancellation );
        if (!string.IsNullOrEmpty ( basketCache ))
        {
            return JsonSerializer.Deserialize<ShoppingCart> ( basketCache )!;
        }

        var basket = await repository.GetBasket ( userName, cancellation );
        await cache.SetStringAsync ( userName, JsonSerializer.Serialize ( basket ), cancellation );

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket ( ShoppingCart basket, CancellationToken cancellation = default )
    {
        await repository.StoreBasket ( basket, cancellation );
        await cache.SetStringAsync ( basket.UserName, JsonSerializer.Serialize ( basket ), cancellation );
        return basket;
    }

    public async Task<bool> DeleteBasket ( string userName, CancellationToken cancellation = default )
    {
        await repository.DeleteBasket ( userName, cancellation );
        await cache.RemoveAsync ( userName, cancellation );
        return true;
    }
}
