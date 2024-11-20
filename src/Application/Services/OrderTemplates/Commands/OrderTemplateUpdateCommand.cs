// auto-generated
namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUpdateCommand : IMapTo<OrderTemplate>, IRequest<OrderTemplate>
{
    public int Id { get; set; }
    public int OrderTemplateStatusId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateUpdateCommand, OrderTemplate>();
    }
}

public class OrderTemplateUpdateHandler : UpdateHandler, IRequestHandler<OrderTemplateUpdateCommand, OrderTemplate>
{
    public OrderTemplateUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplate> Handle(OrderTemplateUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrderTemplateValidator : AbstractValidator<OrderTemplateUpdateCommand>
{
    public UpdateOrderTemplateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderTemplateStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.StartDate).NotNull();
    }
}