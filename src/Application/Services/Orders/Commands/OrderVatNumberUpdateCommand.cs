namespace Engage.Application.Services.Orders.Commands;

public class OrderVatNumberUpdateCommand : IRequest<Order>
{
    public int Id { get; set; }
    public string VATNumber { get; set; }
}

public class OrderVatNumberUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderVatNumberUpdateCommand, Order>
{
    public OrderVatNumberUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> Handle(OrderVatNumberUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.VATNumber = command.VATNumber;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderVatNumberUpdateValidator : AbstractValidator<OrderVatNumberUpdateCommand>
{
    public OrderVatNumberUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VATNumber).MaximumLength(100);
    }
}