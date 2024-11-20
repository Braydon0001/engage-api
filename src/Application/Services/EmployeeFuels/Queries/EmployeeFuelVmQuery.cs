using Engage.Application.Services.EmployeeFuels.Models;

namespace Engage.Application.Services.EmployeeFuels.Queries;

public class EmployeeFuelVmQuery: IRequest<EmployeeFuelVm>
{
    public int Id { get; set; }
}

public class EmployeeFuelVmHandler : BaseQueryHandler, IRequestHandler<EmployeeFuelVmQuery, EmployeeFuelVm>
{
    public EmployeeFuelVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFuelVm> Handle(EmployeeFuelVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeFuels.Include(e => e.EmployeeVehicle)
                                                 .Include(e => e.EmployeePaymentType)
                                                 .Include(e => e.EmployeeFuelExpenseType)
                                                 .Include(e => e.Employee)
                                                 .SingleAsync(e => e.EmployeeFuelId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeFuel, EmployeeFuelVm>(entity);
    }
}
