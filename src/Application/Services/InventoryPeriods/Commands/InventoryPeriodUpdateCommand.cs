namespace Engage.Application.Services.InventoryPeriods.Commands;

public class InventoryPeriodUpdateCommand : IMapTo<InventoryPeriod>, IRequest<InventoryPeriod>
{
    public int Id { get; init; }
    public int InventoryYearId { get; init; }
    public string Name { get; init; }
    public int Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryPeriodUpdateCommand, InventoryPeriod>();
    }
}

public record InventoryPeriodUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryPeriodUpdateCommand, InventoryPeriod>
{
    public async Task<InventoryPeriod> Handle(InventoryPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.InventoryPeriods.SingleOrDefaultAsync(e => e.InventoryPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryPeriodValidator : AbstractValidator<InventoryPeriodUpdateCommand>
{
    public UpdateInventoryPeriodValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}