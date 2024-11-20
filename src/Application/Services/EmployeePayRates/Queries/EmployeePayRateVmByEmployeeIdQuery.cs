using Engage.Application.Services.EmployeePayRates.Models;

namespace Engage.Application.Services.EmployeePayRates.Queries;

public class EmployeePayRateVmByEmployeeIdQuery : IRequest<EmployeePayRateVm>
{
    public int EmployeeId { get; set; }
}

public class EmployeePayRateVmByEmployeeIdQueryHandler : BaseQueryHandler, IRequestHandler<EmployeePayRateVmByEmployeeIdQuery, EmployeePayRateVm>
{
    public EmployeePayRateVmByEmployeeIdQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeePayRateVm> Handle(EmployeePayRateVmByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePayRates.Include(x => x.Employee)
                                             .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, cancellationToken);

        return _mapper.Map<EmployeePayRate, EmployeePayRateVm>(entity);
    }
}
