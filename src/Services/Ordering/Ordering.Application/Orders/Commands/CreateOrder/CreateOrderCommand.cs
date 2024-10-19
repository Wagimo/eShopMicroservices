

using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand ( OrderDto Order ) : ICommand<CreateOrderResult>;


public record CreateOrderResult ( Guid OrderId );

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator ( )
    {
        RuleFor ( x => x.Order ).NotNull ().WithMessage ( "The Order Object  is Null!!" );
        RuleFor ( x => x.Order.OrderName ).NotNull ().WithMessage ( "The OrderName  is Required" );
        RuleFor ( x => x.Order.CustomerId ).NotNull ().WithMessage ( "The CustomerId is Required" );
        RuleFor ( x => x.Order.OrderItems ).NotEmpty ().WithMessage ( "OrderItems should not be empty!" );

    }
}