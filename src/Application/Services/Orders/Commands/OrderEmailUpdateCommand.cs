namespace Engage.Application.Services.Orders.Commands;

public class OrderEmailUpdateCommand : IRequest<Order>
{
    public int Id { get; set; }
    public string Email { get; set; }
}

public class OrderEmailUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderEmailUpdateCommand, Order>
{
    public OrderEmailUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> Handle(OrderEmailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Email = command.Email;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderEmailUpdateValidator : AbstractValidator<OrderEmailUpdateCommand>
{
    public OrderEmailUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Email).MaximumLength(200);
    }
}