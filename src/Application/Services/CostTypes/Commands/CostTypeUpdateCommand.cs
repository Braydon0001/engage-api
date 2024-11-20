namespace Engage.Application.Services.CostTypes.Commands;

public class CostTypeUpdateCommand : IMapTo<CostType>, IRequest<CostType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostTypeUpdateCommand, CostType>();
    }
}

public record CostTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostTypeUpdateCommand, CostType>
{
    public async Task<CostType> Handle(CostTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CostTypes.SingleOrDefaultAsync(e => e.CostTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCostTypeValidator : AbstractValidator<CostTypeUpdateCommand>
{
    public UpdateCostTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}