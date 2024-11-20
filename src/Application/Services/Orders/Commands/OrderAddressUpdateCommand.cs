namespace Engage.Application.Services.Orders.Commands;

public class OrderAddressUpdateCommand : IRequest<Order>
{
    public int Id { get; set; }
    public string Address { get; set; }
}

public class OrderAddressUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderAddressUpdateCommand, Order>
{
    public OrderAddressUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> Handle(OrderAddressUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Address = command.Address;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderAddressUpdateValidator : AbstractValidator<OrderAddressUpdateCommand>
{
    public OrderAddressUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Address).MaximumLength(1000);
    }
}