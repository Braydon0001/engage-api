namespace Engage.Application.Services.InventoryPeriods.Commands;

public class InventoryPeriodInsertCommand : IMapTo<InventoryPeriod>, IRequest<InventoryPeriod>
{
    public int InventoryYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryPeriodInsertCommand, InventoryPeriod>();
    }
}

public record InventoryPeriodInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPeriodInsertCommand, InventoryPeriod>
{
    public async Task<InventoryPeriod> Handle(InventoryPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<InventoryPeriodInsertCommand, InventoryPeriod>(command);

        Context.InventoryPeriods.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryPeriodInsertValidator : AbstractValidator<InventoryPeriodInsertCommand>
{
    public InventoryPeriodInsertValidator()
    {
        RuleFor(e => e.InventoryYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}