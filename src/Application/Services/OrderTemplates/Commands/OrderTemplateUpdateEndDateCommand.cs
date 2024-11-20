namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUpdateEndDateCommand : IRequest<OrderTemplate>
{
    public int Id { get; set; }
    public DateTime? EndDate { get; set; }

}

public class OrderTemplateUpdateEndDatesHandler : IRequestHandler<OrderTemplateUpdateEndDateCommand, OrderTemplate>
{
    private readonly IAppDbContext _context;

    public OrderTemplateUpdateEndDatesHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplate> Handle(OrderTemplateUpdateEndDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.EndDate = command.EndDate;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateUpdateEndDateValidator : AbstractValidator<OrderTemplateUpdateEndDateCommand>
{
    public OrderTemplateUpdateEndDateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}