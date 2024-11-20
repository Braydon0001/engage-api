namespace Engage.Application.Services.Orders.Commands;

public class OrderAccountNumberUpdateCommand : IRequest<Order>
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
}

public class OrderAccountNumberUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderAccountNumberUpdateCommand, Order>
{
    public OrderAccountNumberUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> Handle(OrderAccountNumberUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.AccountNumber = command.AccountNumber;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderAccountNumberUpdateValidator : AbstractValidator<OrderAccountNumberUpdateCommand>
{
    public OrderAccountNumberUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.AccountNumber).MaximumLength(100);
    }
}