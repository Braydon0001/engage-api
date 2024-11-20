using Engage.Application.Services.EmployeeDeductions.Models;

namespace Engage.Application.Services.EmployeeDeductions.Queries;

public class GetEmployeeDeductionVmQuery : IRequest<EmployeeDeductionVm>
{
    public int Id { get; set; }
}

public class GetEmployeeDeductionVmQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeDeductionVmQuery, EmployeeDeductionVm>
{
    public GetEmployeeDeductionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeDeductionVm> Handle(GetEmployeeDeductionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeDeductions.Include(e => e.Employee)
                                                      .Include(e => e.DeductionType)
                                                      .Include(e => e.DeductionCycleType)
                                                      .SingleAsync(e => e.EmployeeDeductionId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeDeduction, EmployeeDeductionVm>(entity);
    }
}
