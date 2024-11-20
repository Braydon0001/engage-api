using Engage.Application.Services.Stores.Models;
using Engage.Application.Services.Targetings.Enums;
using Engage.Application.Services.Targetings.Models;

namespace Engage.Application.Services.Targetings.Queries;

public class GetStoreTargetingsQuery : IRequest<ListResult<StoreTargetingDto>>
{
    public TargetEntity TargetEntity { get; set; }
    public int TargetEntityid { get; set; }
}

public class GetStoreTargetingsQueryHandler : IRequestHandler<GetStoreTargetingsQuery, ListResult<StoreTargetingDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetStoreTargetingsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<StoreTargetingDto>> Handle(GetStoreTargetingsQuery query, CancellationToken cancellationToken)
    {
        var targetings = new List<StoreTargetingDto>();

        switch (query.TargetEntity)
        {
            case (TargetEntity.Survey):
                {
                    var targetingIds = await _context.SurveyStores.Where(e => e.SurveyId == query.TargetEntityid)
                                                                  .Select(e => e.TargetingId)
                                                                  .Distinct()
                                                                  .ToListAsync(cancellationToken);

                    targetings = await _context.Targetings.Where(e => targetingIds.Contains(e.TargetingId))
                                                          .Select(e => new StoreTargetingDto(e.TargetingId, e.CreatedBy, e.Created, e.Criteria, query.TargetEntity))
                                                          .ToListAsync(cancellationToken);

                    foreach (var targeting in targetings)
                    {
                        targeting.Stores = await _context.SurveyStores.Where(e => e.SurveyId == query.TargetEntityid &&
                                                                                  e.TargetingId == targeting.Id)
                                                                      .Select(e => e.Store)
                                                                      .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                                                                      .ToListAsync(cancellationToken);
                    }
                    break;
                }
            default:
                throw new UnknownTargetEntityException(query.TargetEntity);
        }

        return new ListResult<StoreTargetingDto>
        {
            Count = targetings.Count,
            Data = targetings
        };
    }
}
