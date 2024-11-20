namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUpdateNoteCommand : IRequest<OrderTemplate>
{
    public int Id { get; set; }
    public string Note { get; set; }

}

public class OrderTemplateUpdateNotesHandler : IRequestHandler<OrderTemplateUpdateNoteCommand, OrderTemplate>
{
    private readonly IAppDbContext _context;

    public OrderTemplateUpdateNotesHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplate> Handle(OrderTemplateUpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Note = command.Note;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateUpdateNoteValidator : AbstractValidator<OrderTemplateUpdateNoteCommand>
{
    public OrderTemplateUpdateNoteValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}