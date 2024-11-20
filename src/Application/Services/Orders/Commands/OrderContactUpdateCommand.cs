namespace Engage.Application.Services.Orders.Commands;

public class OrderContactUpdateCommand : IRequest<Order>
{
    public int Id { get; set; }
    public string Contact { get; set; }
}

public class OrderContactUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderContactUpdateCommand, Order>
{
    public OrderContactUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> Handle(OrderContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Contact = command.Contact;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderContactUpdateValidator : AbstractValidator<OrderContactUpdateCommand>
{
    public OrderContactUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Contact).MaximumLength(200);
    }
}