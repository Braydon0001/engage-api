namespace Engage.Application.Services.EngageBrands.Queries;

public class EngageBrandBySupplierOfflineQuery : IRequest<List<EngageBrandOption>>
{
    public int? UserId { get; set; }

}

public record EngageBrandBySupplierOfflineHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageBrandBySupplierOfflineQuery, List<EngageBrandOption>>
{
    public async Task<List<EngageBrandOption>> Handle(EngageBrandBySupplierOfflineQuery query, CancellationToken cancellationToken)
    {
        List<EngageBrandOption> result = [];

        var userStakeholderIds = await Context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true).Select(e => e.ProjectStakeholderId).ToListAsync(cancellationToken);





        var projectIds = await Context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await Context.ProjectStores.Where(e => e.OwnerId == query.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);

        var userSupplierIds = await Context.ProjectSuppliers.AsNoTracking()
                                                             .Where(e => projectIds.Contains(e.ProjectId))
                                                             .Select(e => e.SupplierId)
                                                             .ToListAsync(cancellationToken);

        var categorySupplierIds = Context.ProjectCategorySuppliers.AsQueryable().AsNoTracking()
                                                            .Where(e => userSupplierIds
                                                            .Contains(e.SupplierId))
                                                            .Select(e => e.SupplierId)
                                                            .Distinct();

        var supplierBrands = await Context.SupplierEngageBrands
                                              .Where(e => categorySupplierIds.Contains(e.SupplierId))
                                              .Include(e => e.EngageBrand)
                                              .Select(e => e.EngageBrand)
                                              .ProjectTo<EngageBrandOption>(Mapper.ConfigurationProvider)
                                              .ToListAsync();
        var mappedEntities = supplierBrands.DistinctBy(e => e.Id);
        result.AddRange(mappedEntities);


        return result;
    }
}
