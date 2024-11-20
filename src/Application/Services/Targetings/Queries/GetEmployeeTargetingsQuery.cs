using Engage.Application.Services.Employees.Models;
using Engage.Application.Services.Targetings.Enums;
using Engage.Application.Services.Targetings.Models;

namespace Engage.Application.Services.Targetings.Queries;

public class GetEmployeeTargetingsQuery : IRequest<ListResult<EmployeeTargetingDto>>
{
    public TargetEntity TargetEntity { get; set; }
    public int TargetEntityid { get; set; }
}

public class GetEmployeeTargetingsQueryHandler : IRequestHandler<GetEmployeeTargetingsQuery, ListResult<EmployeeTargetingDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeTargetingsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<EmployeeTargetingDto>> Handle(GetEmployeeTargetingsQuery query, CancellationToken cancellationToken)
    {
        var targetings = new List<EmployeeTargetingDto>();

        switch (query.TargetEntity)
        {
            case (TargetEntity.Survey):
                {
                    var entities = await _context.SurveyEmployees.Where(e => e.SurveyId == query.TargetEntityid)
                                                                 .Include(e => e.Employee)
                                                                 .ToListAsync(cancellationToken);

                    var targetingIds = entities.Select(e => e.TargetingId)
                                               .Distinct()
                                               .ToList();

                    targetings = await _context.Targetings.Where(e => targetingIds.Contains(e.TargetingId))
                                                          .Select(e => new EmployeeTargetingDto(e.TargetingId, e.CreatedBy, e.Created, e.Criteria, query.TargetEntity))
                                                          .ToListAsync(cancellationToken);

                    targetings.ForEach(targeting => targeting.Employees = entities.Where(e => e.TargetingId == targeting.Id)
                                                                                  .Select(e => _mapper.Map<Employee, EmployeeListDto>(e.Employee))
                                                                                  .ToList());
                    break;
                }
            default:
                throw new UnknownTargetEntityException(query.TargetEntity);
        }

        return new ListResult<EmployeeTargetingDto>
        {
            Count = targetings.Count,
            Data = targetings
        };
    }

    private async Task<List<EmployeeTargetingDto>> GetTargetingsAsync(GetEmployeeTargetingsQuery query, List<EmployeeTargetingDto> targetings, List<int?> targetingIds, CancellationToken cancellationToken)
    {
        return await _context.Targetings.Where(e => targetingIds.Contains(e.TargetingId))
                                        .Select(e => new EmployeeTargetingDto(e.TargetingId, e.CreatedBy, e.Created, e.Criteria, query.TargetEntity))
                                        .ToListAsync(cancellationToken);
    }
}
