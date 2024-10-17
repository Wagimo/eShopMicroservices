
namespace Discount.Grpc.Services;

public class DiscountServices ( DiscountContext contextDb, ILogger<DiscountServices> logger ) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CuponModel> GetDiscount ( GetDiscountRequest request, ServerCallContext context )
    {

        var cupon = await contextDb
            .Cupons
            .FirstOrDefaultAsync ( x => x.ProductName == request.ProductName );
        if (cupon is null)
        {
            cupon = new Cupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
        }

        logger.LogInformation ( "Cupon is retrieved for productName: {productName}, Amount: {amount}", cupon.ProductName, cupon.Amount );

        var cuponModel = cupon.Adapt<CuponModel> ();
        return cuponModel;
    }

    public override async Task<CuponModel> CreateDiscount ( CreateDiscountRequest request, ServerCallContext context )
    {
        var cupon = request.Cupon.Adapt<Cupon> () ?? throw new RpcException ( new Status ( StatusCode.InvalidArgument, "Invalid Request" ) );
        contextDb.Cupons.Add ( cupon );
        await contextDb.SaveChangesAsync ();
        var cuponModel = cupon.Adapt<CuponModel> ();
        logger.LogInformation ( "Discount is successfully created. ProductName: {productName}", cuponModel.ProductName );
        return cuponModel;
    }

    public async override Task<CuponModel> UpdateDiscount ( UpdateDiscountRequest request, ServerCallContext context )
    {

        var cuponFound = await contextDb
          .Cupons
          .FirstOrDefaultAsync ( x => x.ProductName == request.Cupon.ProductName ) ?? throw new RpcException ( new Status ( StatusCode.NotFound, "Cupon not found!" ) );

        cuponFound.Amount = (decimal)request.Cupon.Amount;
        cuponFound.Description = request.Cupon.Description;

        contextDb.Cupons.Update ( cuponFound );
        await contextDb.SaveChangesAsync ();
        var cuponModel = cuponFound.Adapt<CuponModel> ();
        logger.LogInformation ( "Discount is successfully updated. ProductName: {productName}", cuponModel.ProductName );
        return cuponModel;
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount ( DeleteDiscountRequest request, ServerCallContext context )
    {
        var cupon = await contextDb
          .Cupons
          .FirstOrDefaultAsync ( x => x.ProductName == request.ProductName ) ?? throw new RpcException ( new Status ( StatusCode.NotFound, "Cupon not found!" ) );
        contextDb.Cupons.Remove ( cupon );
        await contextDb.SaveChangesAsync ();
        logger.LogInformation ( "Discount is successfully deleted. ProductName: {productName}", request.ProductName );
        return new DeleteDiscountResponse
        {
            Success = true
        };

    }
}
