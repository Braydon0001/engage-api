namespace Engage.Application.Services.InventoryYears.Commands;

public class InventoryYearInsertCommand : IMapTo<InventoryYear>, IRequest<InventoryYear>
{
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryYearInsertCommand, InventoryYear>();
    }
}

public record InventoryYearInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryYearInsertCommand, InventoryYear>
{
    public async Task<InventoryYear> Handle(InventoryYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<InventoryYearInsertCommand, InventoryYear>(command);
        
        Context.InventoryYears.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryYearInsertValidator : AbstractValidator<InventoryYearInsertCommand>
{
    public InventoryYearInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}