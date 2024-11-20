namespace Engage.Application.Services.EngageSubRegions.Commands;

public class EngageSubRegionUpdateCommand : IMapTo<EngageSubRegion>, IRequest<EngageSubRegion>
{
    public int Id { get; set; }
    public int EngageRegionId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubRegionUpdateCommand, EngageSubRegion>();
    }
}

public record EngageSubRegionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubRegionUpdateCommand, EngageSubRegion>
{
    public async Task<EngageSubRegion> Handle(EngageSubRegionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EngageSubRegions.SingleOrDefaultAsync(e => e.EngageSubRegionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEngageSubRegionValidator : AbstractValidator<EngageSubRegionUpdateCommand>
{
    public UpdateEngageSubRegionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageRegionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}