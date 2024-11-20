namespace Engage.Application.Services.Targetings;

// Queries
public class SurveyStoresTargetingQuery : GetQuery, IRequest<ListResult<StoreTargetingListDto>>
{
    public int SurveyId { get; set; }
}

public class SurveyEmployeesTargetingQuery : GetQuery, IRequest<ListResult<EmployeeTargetingListDto>>
{
    public int SurveyId { get; set; }
}

// Handlers
public class SurveyStoresTargetingQueryHandler : BaseQueryHandler, IRequestHandler<SurveyStoresTargetingQuery, ListResult<StoreTargetingListDto>>
{
    private readonly ITargetingService _targeting;

    public SurveyStoresTargetingQueryHandler(IAppDbContext context, IMapper mapper, ITargetingService targeting) : base(context, mapper)
    {
        _targeting = targeting;
    }

    public async Task<ListResult<StoreTargetingListDto>> Handle(SurveyStoresTargetingQuery query, CancellationToken cancellationToken)
    {
        var targetingIds = await _context.SurveyStores.Where(e => e.SurveyId == query.SurveyId)
                                                      .Select(e => e.TargetingId)
                                                      .Distinct()
                                                      .ToListAsync(cancellationToken);
        if (targetingIds.Count > 0)
        {
            var targetings = await _context.Targetings.Where(e => targetingIds.Contains(e.TargetingId))
                                                      .ProjectTo<StoreTargetingListDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

            targetings.ForEach(targeting => targeting.Criteria = _targeting.DeserializeStoreCriteria(targeting.CriteriaString));

            return new ListResult<StoreTargetingListDto>()
            {
                Count = targetings.Count,
                Data = targetings
            };
        }

        return new ListResult<StoreTargetingListDto>();
    }
}

public class SurveyEmployeesTargetingQueryHandler : BaseQueryHandler, IRequestHandler<SurveyEmployeesTargetingQuery, ListResult<EmployeeTargetingListDto>>
{
    private readonly ITargetingService _targeting;

    public SurveyEmployeesTargetingQueryHandler(IAppDbContext context, IMapper mapper, ITargetingService targeting) : base(context, mapper)
    {
        _targeting = targeting;
    }

    public async Task<ListResult<EmployeeTargetingListDto>> Handle(SurveyEmployeesTargetingQuery query, CancellationToken cancellationToken)
    {
        var targetingIds = await _context.SurveyEmployees.Where(e => e.SurveyId == query.SurveyId)
                                                         .Select(e => e.TargetingId)
                                                         .Distinct()
                                                         .ToListAsync(cancellationToken);
        if (targetingIds.Count > 0)
        {
            var targetings = await _context.Targetings.Where(e => targetingIds.Contains(e.TargetingId))
                                                      .ProjectTo<EmployeeTargetingListDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

            targetings.ForEach(targeting => targeting.Criteria = _targeting.DeserializeEmployeeCriteria(targeting.CriteriaString));

            return new ListResult<EmployeeTargetingListDto>()
            {
                Count = targetings.Count,
                Data = targetings
            };
        }

        return new ListResult<EmployeeTargetingListDto>();
    }
}
