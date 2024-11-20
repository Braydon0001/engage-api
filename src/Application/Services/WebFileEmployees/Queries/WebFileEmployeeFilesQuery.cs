namespace Engage.Application.Services.WebFileEmployees.Queries;

public record WebFileEmployeeFilesQuery(int EmployeeId) : IRequest<Dictionary<string, List<JsonFile>>>
{
}

public class WebFileEmployeeFilesHandler : IRequestHandler<WebFileEmployeeFilesQuery, Dictionary<string, List<JsonFile>>>
{
    private readonly IAppDbContext _context;

    public WebFileEmployeeFilesHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, List<JsonFile>>> Handle(WebFileEmployeeFilesQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId && e.Disabled == false, cancellationToken);
        if (employee == null)
        {
            return null;
        }

        var employeeBankDetails = await _context.EmployeeBankDetails.Where(e => e.EmployeeId == query.EmployeeId && e.Disabled == false).ToListAsync(cancellationToken);
        var employeePensions = await _context.EmployeePensions.Where(e => e.EmployeeId == query.EmployeeId && e.Disabled == false).ToListAsync(cancellationToken);
        var employeeQualifications = await _context.EmployeeQualifications.Where(e => e.EmployeeId == query.EmployeeId && e.Disabled == false).ToListAsync(cancellationToken);

        var employeeFiles = new Dictionary<string, List<JsonFile>>
        {
            { nameof(Employee), employee.Files != null ? new List<JsonFile>(employee?.Files) : new List<JsonFile>()  },
            { nameof(EmployeeBankDetail), new List<JsonFile>(employeeBankDetails.Where(e => e.Files != null).SelectMany(e => e?.Files)) },
            { nameof(EmployeePension), new List<JsonFile>(employeePensions.Where(e => e.Files != null).SelectMany(e => e?.Files)) },
            { nameof(EmployeeQualification), new List<JsonFile>(employeeQualifications.Where(e => e.Files != null).SelectMany(e => e?.Files)) }
        };

        return employeeFiles;
    }
}
