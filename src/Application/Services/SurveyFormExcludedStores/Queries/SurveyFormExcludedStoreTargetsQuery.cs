using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyFormExcludedStores.Queries;

public class SurveyFormExcludedStoreTargetsQuery : IRequest<List<StoreDto>>
{
    public int Id { get; set; }
}

public record SurveyFormExcludedStoreTargetsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormExcludedStoreTargetsQuery, List<StoreDto>>
{
    public async Task<List<StoreDto>> Handle(SurveyFormExcludedStoreTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await Context.SurveyFormTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyFormId == query.Id).ToListAsync(cancellationToken);

        var excludedStoreIds = entities.OfType<SurveyFormExcludedStore>().Select(e => e.ExcludedStoreId).ToList();

        var stores = await Context.Stores.Where(e => excludedStoreIds.Contains(e.StoreId)).ProjectTo<StoreDto>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return stores;
    }
}
