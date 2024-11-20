using Engage.Application.Services.EmployeeRegionContacts.Models;

namespace Engage.Application.Services.EmployeeRegionContacts.Queries;

public class EmployeeRegionContactVmQuery : GetByIdQuery, IRequest<EmployeeRegionContactVm>
{
}

public class EmployeeRegionContactVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeRegionContactVmQuery, EmployeeRegionContactVm>
{
    public EmployeeRegionContactVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeRegionContactVm> Handle(EmployeeRegionContactVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeRegionContacts.Include(e => e.EngageRegion)
                                                          .Include(e => e.Employee)
                                                          .SingleAsync(e => e.EmployeeRegionContactId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeRegionContact, EmployeeRegionContactVm>(entity);
    }
}
