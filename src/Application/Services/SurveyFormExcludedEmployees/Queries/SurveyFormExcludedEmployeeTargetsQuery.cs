using Engage.Application.Services.Employees.Queries;

namespace Engage.Application.Services.SurveyFormExcludedEmployees.Queries;

public class SurveyFormExcludedEmployeeTargetsQuery : IRequest<List<EmployeeDto>>
{
    public int Id { get; set; }
}

public record SurveyFormExcludedEmployeeTargetsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormExcludedEmployeeTargetsQuery, List<EmployeeDto>>
{
    public async Task<List<EmployeeDto>> Handle(SurveyFormExcludedEmployeeTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await Context.SurveyFormTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyFormId == query.Id).ToListAsync(cancellationToken);

        var excludedEmployeeIds = entities.OfType<SurveyFormExcludedEmployee>().Select(e => e.ExcludedEmployeeId).ToList();

        var employees = await Context.Employees.Where(e => excludedEmployeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return employees;
    }
}
