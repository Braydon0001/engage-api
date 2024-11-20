using Engage.Application.Services.EmployeePayRates.Models;

namespace Engage.Application.Services.EmployeePayRates.Queries;

public class EmployeePayRateVmQuery : IRequest<EmployeePayRateVm>
{
    public int Id { get; set; }
}

public class EmployeePayRateVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeePayRateVmQuery, EmployeePayRateVm>
{
    public EmployeePayRateVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeePayRateVm> Handle(EmployeePayRateVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePayRates
                                        .Include(x => x.EmployeePayRateFrequency)
                                        .Include(x => x.EmployeePayRatePackage)
                                        .FirstOrDefaultAsync(x => x.EmployeePayRateId == request.Id, cancellationToken);

        return _mapper.Map<EmployeePayRate, EmployeePayRateVm>(entity);
    }
}
