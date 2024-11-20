using Engage.Application.Services.EmployeeDisciplinaryProcedures.Models;

namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Queries;

public class EmployeeDisciplinaryProcedureVmQuery : IRequest<EmployeeDisciplinaryProcedureVm>
{
    public int Id { get; set; }
}

public class EmployeeDisciplinaryProcedureVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeDisciplinaryProcedureVmQuery, EmployeeDisciplinaryProcedureVm>
{
    public EmployeeDisciplinaryProcedureVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeDisciplinaryProcedureVm> Handle(EmployeeDisciplinaryProcedureVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeDisciplinaryProcedures.Include(e => e.Employee)
                                                                  .SingleAsync(e => e.EmployeeDisciplinaryProcedureId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeDisciplinaryProcedure, EmployeeDisciplinaryProcedureVm>(entity);
    }
}
