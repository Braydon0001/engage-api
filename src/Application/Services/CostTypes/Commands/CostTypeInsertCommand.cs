namespace Engage.Application.Services.CostTypes.Commands;

public class CostTypeInsertCommand : IMapTo<CostType>, IRequest<CostType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostTypeInsertCommand, CostType>();
    }
}

public record CostTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostTypeInsertCommand, CostType>
{
    public async Task<CostType> Handle(CostTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostTypeInsertCommand, CostType>(command);
        
        Context.CostTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostTypeInsertValidator : AbstractValidator<CostTypeInsertCommand>
{
    public CostTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}