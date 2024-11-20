using Engage.Application.Services.Mobile.ProjectStores.Queries;
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public record ProjectStoreMobileVmQuery(int Id) : IRequest<ProjectMobileVm>;

public record ProjectStoreMobileVmHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreMobileVmQuery, ProjectMobileVm>
{
    public async Task<ProjectMobileVm> Handle(ProjectStoreMobileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Store)
                                .ThenInclude(e => e.EngageRegion)
                             .Include(e => e.ProjectType)
                             .Include(e => e.ProjectStatus)
                             .Include(e => e.ProjectPriority)
                             .Include(e => e.ProjectCampaign);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectId == query.Id, cancellationToken);



        var dcProducts = await Context.ProjectProjectTagDCProducts.Where(p => p.ProjectId == entity.ProjectId)
                                                          .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        var claims = await Context.ProjectProjectTagClaims.Where(p => p.ProjectId == entity.ProjectId)
                                                          .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        var jobTitles = await Context.ProjectProjectTagEmployeeJobTitles.Where(p => p.ProjectId == entity.ProjectId)
                                                                       .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                                       .ToListAsync(cancellationToken);

        var storeAssets = await Context.ProjectProjectTagStoreAssets.Where(p => p.ProjectId == entity.ProjectId)
                                                                    .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);


        var mappedEntity = Mapper.Map<ProjectMobileVm>(entity);


        mappedEntity.DcProductTagIds = dcProducts;
        mappedEntity.ClaimTagIds = claims;
        mappedEntity.EmployeeJobTitleTagIds = jobTitles;
        mappedEntity.StoreAssetTagIds = storeAssets;


        mappedEntity.StoreId = new OptionDto(entity.StoreId, entity.Store.Name + " - " + entity.Store.EngageRegion.Name);

        return entity == null ? null : mappedEntity;
    }
}