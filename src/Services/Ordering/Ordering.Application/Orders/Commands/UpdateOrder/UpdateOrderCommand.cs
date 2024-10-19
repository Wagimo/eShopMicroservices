

namespace Ordering.Application.Orders.Commands.UpdateOrder;


public record UpdateOrderCommand ( OrderDto Order ) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult ( bool IsSuccess );

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator ( )
    {
        RuleFor ( x => x.Order ).NotNull ().WithMessage ( "The Order Object  is Null!!" );
        RuleFor ( x => x.Order.Id ).NotNull ().WithMessage ( "The Id  is Required" );
        RuleFor ( x => x.Order.OrderName ).NotNull ().WithMessage ( "The OrderName  is Required" );
        RuleFor ( x => x.Order.CustomerId ).NotNull ().WithMessage ( "The CustomerId is Required" );
        RuleFor ( x => x.Order.OrderItems ).NotEmpty ().WithMessage ( "OrderItems should not be empty!" );
    }

}
