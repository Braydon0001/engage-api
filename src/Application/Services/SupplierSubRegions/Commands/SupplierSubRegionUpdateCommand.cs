namespace Engage.Application.Services.SupplierSubRegions.Commands;

public class SupplierSubRegionUpdateCommand : IMapTo<SupplierSubRegion>, IRequest<SupplierSubRegion>
{
    public int Id { get; init; }
    public int SupplierRegionId { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubRegionUpdateCommand, SupplierSubRegion>();
    }
}

public record SupplierSubRegionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierSubRegionUpdateCommand, SupplierSubRegion>
{
    public async Task<SupplierSubRegion> Handle(SupplierSubRegionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SupplierSubRegions.SingleOrDefaultAsync(e => e.SupplierSubRegionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierSubRegionValidator : AbstractValidator<SupplierSubRegionUpdateCommand>
{
    public UpdateSupplierSubRegionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierRegionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}