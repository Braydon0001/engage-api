using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.SurveyStores;

// Queries
public class SurveyStoresQuery : GetQuery, IRequest<ListResult<StoreListDto>>
{
    public int SurveyId { get; set; }
}

// Handlers
public class SurveyStoresQueryHandler : BaseQueryHandler, IRequestHandler<SurveyStoresQuery, ListResult<StoreListDto>>
{

    public SurveyStoresQueryHandler(IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<ListResult<StoreListDto>> Handle(SurveyStoresQuery query, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.Where(e => e.SurveyId == query.SurveyId)
                                           .FirstOrDefaultAsync(cancellationToken);
        if (survey == null)
        {
            throw new NotFoundException(nameof(Survey), query.SurveyId);
        }

        var storeIds = await _context.SurveyStores.Where(e => e.SurveyId == query.SurveyId)
                                                  .Select(e => e.StoreId)
                                                  .ToListAsync(cancellationToken);

        var entities = new List<StoreListDto>();
        if (storeIds.Count > 0)
        {
            entities = await _context.Stores.Where(e => storeIds.Contains(e.StoreId))
                                            .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                                            .OrderBy(e => e.Code)
                                            .ToListAsync(cancellationToken);
        }

        return new ListResult<StoreListDto>
        {
            Count = entities.Count,
            Data = entities
        };
    }
}
