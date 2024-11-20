namespace Engage.Application.Services.SparUnitTypes.Commands;

public class SparUnitTypeUpdateCommand : IMapTo<SparUnitType>, IRequest<SparUnitType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparUnitTypeUpdateCommand, SparUnitType>();
    }
}

public record SparUnitTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparUnitTypeUpdateCommand, SparUnitType>
{
    public async Task<SparUnitType> Handle(SparUnitTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparUnitTypes.SingleOrDefaultAsync(e => e.SparUnitTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparUnitTypeValidator : AbstractValidator<SparUnitTypeUpdateCommand>
{
    public UpdateSparUnitTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}