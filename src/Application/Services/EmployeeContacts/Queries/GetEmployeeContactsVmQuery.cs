using Engage.Application.Services.EmployeeContacts.Models;
using Engage.Application.Services.EmployeeRegionContacts.Models;

namespace Engage.Application.Services.EmployeeContacts.Queries;

public class GetEmployeeContactsVmQuery : GetQuery, IRequest<EmployeeContactVm>
{
    //Required
    public int EmployeeId { get; set; }
}

public class GetEmployeeContactsVmQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeContactsVmQuery, EmployeeContactVm>
{
    public GetEmployeeContactsVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeContactVm> Handle(GetEmployeeContactsVmQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
                                        .Include(e => e.EmployeeRegions)
                                        .Include(e => e.Manager)
                                        .ThenInclude(x => x.EmployeeJobTitles)
                                        .ThenInclude(p => p.EmployeeJobTitle)
                                        .Include(e => e.LeaveManager)
                                        .ThenInclude(x => x.EmployeeJobTitles)
                                        .ThenInclude(p => p.EmployeeJobTitle)
                                        .SingleOrDefaultAsync(e => e.EmployeeId == request.EmployeeId, cancellationToken);

        var regionIds = employee.EmployeeRegions.Select(r => r.EngageRegionId).ToList();

        var engageRegionContacts = await _context.EmployeeRegionContacts.Where(e => regionIds.Contains(e.EngageRegionId))
                                                           .OrderByDescending(e => e.EmployeeRegionContactId)
                                                           .ProjectTo<EmployeeRegionContactDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        var employeeManager = _mapper.Map<Employee, EmployeeManagerVm>(employee.Manager);
        var leaveManager = _mapper.Map<Employee, EmployeeManagerVm>(employee.LeaveManager);

        return new EmployeeContactVm
        {
            EmployeeRegionContacts = engageRegionContacts,
            EmployeeManager = employeeManager,
            EmployeeLeaveManager = leaveManager,
        };
    }
}
