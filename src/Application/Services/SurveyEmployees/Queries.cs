using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.SurveyEmployees;

// Queries
public class SurveyEmployeesQuery : GetQuery, IRequest<ListResult<EmployeeListDto>>
{
    public int SurveyId { get; set; }
}

// Handlers
public class SurveyEmployeesQueryHandler : BaseQueryHandler, IRequestHandler<SurveyEmployeesQuery, ListResult<EmployeeListDto>>
{

    public SurveyEmployeesQueryHandler(IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<ListResult<EmployeeListDto>> Handle(SurveyEmployeesQuery query, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.Where(e => e.SurveyId == query.SurveyId)
                                           .FirstOrDefaultAsync(cancellationToken);
        if (survey == null)
        {
            throw new NotFoundException(nameof(Survey), query.SurveyId);
        }

        var employeeIds = await _context.SurveyEmployees.Where(e => e.SurveyId == query.SurveyId)
                                                        .Select(e => e.EmployeeId)
                                                        .ToListAsync(cancellationToken);

        var entities = new List<EmployeeListDto>();
        if (employeeIds.Count > 0)
        {
            entities = await _context.Employees
               .Where(e => employeeIds.Contains(e.EmployeeId))
               .OrderBy(e => e.FirstName)
               .ThenBy(e => e.LastName)
               .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
        }

        return new ListResult<EmployeeListDto>
        {
            Data = entities,
            Count = entities.Count
        };
    }
}
