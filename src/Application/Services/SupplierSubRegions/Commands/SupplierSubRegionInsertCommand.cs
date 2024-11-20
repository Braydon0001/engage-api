namespace Engage.Application.Services.SupplierSubRegions.Commands;

public class SupplierSubRegionInsertCommand : IMapTo<SupplierSubRegion>, IRequest<SupplierSubRegion>
{
    public int SupplierRegionId { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubRegionInsertCommand, SupplierSubRegion>();
    }
}

public record SupplierSubRegionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SupplierSubRegionInsertCommand, SupplierSubRegion>
{
    public async Task<SupplierSubRegion> Handle(SupplierSubRegionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SupplierSubRegionInsertCommand, SupplierSubRegion>(command);

        Context.SupplierSubRegions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierSubRegionInsertValidator : AbstractValidator<SupplierSubRegionInsertCommand>
{
    public SupplierSubRegionInsertValidator()
    {
        RuleFor(e => e.SupplierRegionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}