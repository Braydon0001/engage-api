using Engage.Application.Services.EmployeeLoans.Models;

namespace Engage.Application.Services.EmployeeLoans.Queries;

public class EmployeeLoanVmQuery : GetByIdQuery, IRequest<EmployeeLoanVm>
{
}

public class EmployeeLoanVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeLoanVmQuery, EmployeeLoanVm>
{
    public EmployeeLoanVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeLoanVm> Handle(EmployeeLoanVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeLoans.Include(e => e.Employee)
                                                 .SingleAsync(e => e.EmployeeLoanId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeLoan, EmployeeLoanVm>(entity);
    }
}
