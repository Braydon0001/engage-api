namespace Engage.Application.Services.EngageSubRegions.Commands;

public class EngageSubRegionInsertCommand : IMapTo<EngageSubRegion>, IRequest<EngageSubRegion>
{
    public int EngageRegionId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubRegionInsertCommand, EngageSubRegion>();
    }
}

public record EngageSubRegionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubRegionInsertCommand, EngageSubRegion>
{
    public async Task<EngageSubRegion> Handle(EngageSubRegionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EngageSubRegionInsertCommand, EngageSubRegion>(command);

        Context.EngageSubRegions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EngageSubRegionInsertValidator : AbstractValidator<EngageSubRegionInsertCommand>
{
    public EngageSubRegionInsertValidator()
    {
        RuleFor(e => e.EngageRegionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}