
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand ( Guid ProductId ) : ICommand<DeleteProductResult>;
public record DeleteProductResult ( bool IsSuccess );

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator ( )
    {
        RuleFor ( x => x.ProductId ).NotEmpty ().WithMessage ( "Product Id is required" );
    }
}
internal class DeleteProductHandler ( IDocumentSession session, ILogger<DeleteProductHandler> logger ) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle ( DeleteProductCommand command, CancellationToken cancellationToken )
    {
        logger.LogInformation ( "Deleting product with id {ProductId}", command.ProductId );
        session.Delete<Product> ( command.ProductId );
        await session.SaveChangesAsync ( cancellationToken );
        return new DeleteProductResult ( true );
    }
}
