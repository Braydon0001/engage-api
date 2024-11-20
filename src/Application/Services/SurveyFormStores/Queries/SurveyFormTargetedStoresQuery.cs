using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.SurveyFormStores.Queries;

public class SurveyFormTargetedStoresQuery : IRequest<List<StoreListDto>>
{
    public int Id { get; set; }
}

public record SurveyFormTargetedStoresHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTargetedStoresQuery, List<StoreListDto>>
{
    public async Task<List<StoreListDto>> Handle(SurveyFormTargetedStoresQuery query, CancellationToken cancellationToken)
    {
        var survey = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (survey == null)
        {
            return null;
        }

        var entities = await Context.SurveyFormTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyFormId == query.Id).ToListAsync(cancellationToken);

        var storeIds = entities.OfType<SurveyFormStore>().Select(e => e.StoreId).ToList();
        var storeEngageRegionIds = entities.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).ToList();
        var storeClusterIds = entities.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).ToList();
        var storeFormatIds = entities.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).ToList();
        var storeLSMIds = entities.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).ToList();
        var storeTypeIds = entities.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).ToList();

        var excludedStoreIds = entities.OfType<SurveyFormExcludedStore>().Select(e => e.ExcludedStoreId).ToList();

        var hasStoreTarget = storeIds.Count != 0;
        var hasRegionTarget = storeEngageRegionIds.Count != 0;
        var hasClusterTarget = storeClusterIds.Count != 0;
        var hasFormatTarget = storeFormatIds.Count != 0;
        var hasLSMTarget = storeLSMIds.Count != 0;
        var hasTypeTarget = storeTypeIds.Count != 0;

        var hasCriteriaTarget = hasRegionTarget || hasClusterTarget || hasFormatTarget || hasLSMTarget || hasTypeTarget;

        var queryable = Context.Stores.AsQueryable().AsNoTracking();

        var targetedStores = await queryable.Where(e => (hasStoreTarget ? storeIds.Contains(e.StoreId) : false)
                                                ||
                                                (hasCriteriaTarget ?
                                                (
                                                (hasRegionTarget ? storeEngageRegionIds.Contains(e.EngageRegionId) : true)
                                                && (hasClusterTarget ? storeClusterIds.Contains(e.StoreClusterId) : true)
                                                && (hasFormatTarget ? storeFormatIds.Contains(e.StoreFormatId) : true)
                                                && (hasLSMTarget ? storeLSMIds.Contains(e.StoreLSMId) : true)
                                                && (hasTypeTarget ? storeTypeIds.Contains(e.StoreTypeId) : true
                                                )) : false))
                                            .Where(e => !excludedStoreIds.Contains(e.StoreId) && !e.Deleted && !e.Disabled)
                                            .ProjectTo<StoreListDto>(Mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

        return targetedStores;
    }
}
