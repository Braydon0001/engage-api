// auto-generated
namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateInsertCommand : IMapTo<OrderTemplate>, IRequest<OrderTemplate>
{
    public int OrderTemplateStatusId { get; set; }
    public int DistributionCenterId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateInsertCommand, OrderTemplate>();
    }
}

public class OrderTemplateInsertHandler : InsertHandler, IRequestHandler<OrderTemplateInsertCommand, OrderTemplate>
{
    public OrderTemplateInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplate> Handle(OrderTemplateInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<OrderTemplateInsertCommand, OrderTemplate>(command);

        _context.OrderTemplates.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateInsertValidator : AbstractValidator<OrderTemplateInsertCommand>
{
    public OrderTemplateInsertValidator()
    {
        RuleFor(e => e.OrderTemplateStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DistributionCenterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.StartDate).NotNull();
    }
}