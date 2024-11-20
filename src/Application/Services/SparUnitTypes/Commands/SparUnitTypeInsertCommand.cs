namespace Engage.Application.Services.SparUnitTypes.Commands;

public class SparUnitTypeInsertCommand : IMapTo<SparUnitType>, IRequest<SparUnitType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparUnitTypeInsertCommand, SparUnitType>();
    }
}

public record SparUnitTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparUnitTypeInsertCommand, SparUnitType>
{
    public async Task<SparUnitType> Handle(SparUnitTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparUnitTypeInsertCommand, SparUnitType>(command);
        
        Context.SparUnitTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparUnitTypeInsertValidator : AbstractValidator<SparUnitTypeInsertCommand>
{
    public SparUnitTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}