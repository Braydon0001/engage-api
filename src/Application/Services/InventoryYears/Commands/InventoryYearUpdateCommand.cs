namespace Engage.Application.Services.InventoryYears.Commands;

public class InventoryYearUpdateCommand : IMapTo<InventoryYear>, IRequest<InventoryYear>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryYearUpdateCommand, InventoryYear>();
    }
}

public record InventoryYearUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryYearUpdateCommand, InventoryYear>
{
    public async Task<InventoryYear> Handle(InventoryYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.InventoryYears.SingleOrDefaultAsync(e => e.InventoryYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryYearValidator : AbstractValidator<InventoryYearUpdateCommand>
{
    public UpdateInventoryYearValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}