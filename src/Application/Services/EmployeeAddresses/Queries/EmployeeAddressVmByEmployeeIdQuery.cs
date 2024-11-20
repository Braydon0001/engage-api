using Engage.Application.Services.EmployeeAddresses.Models;

namespace Engage.Application.Services.EmployeeAddresses.Queries;

public class EmployeeAddressVmByEmployeeIdQuery : IRequest<EmployeeAddressVm>
{
    public int EmployeeId { get; set; }
}

public class EmployeeAddressVmByEmployeeIdQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAddressVmByEmployeeIdQuery, EmployeeAddressVm>
{
    public EmployeeAddressVmByEmployeeIdQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeAddressVm> Handle(EmployeeAddressVmByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAddresses.Include(x => x.Employee)
                                             .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, cancellationToken);

        return _mapper.Map<EmployeeAddress, EmployeeAddressVm>(entity);
    }
}
