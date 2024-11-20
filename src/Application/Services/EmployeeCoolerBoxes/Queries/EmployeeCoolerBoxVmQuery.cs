using Engage.Application.Services.EmployeeCoolerBoxes.Models;

namespace Engage.Application.Services.EmployeeCoolerBoxes.Queries;

public class EmployeeCoolerBoxVmQuery : IRequest<EmployeeCoolerBoxVm>
{
    public int Id { get; set; }
}

public class EmployeeCoolerBoxVMQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeCoolerBoxVmQuery, EmployeeCoolerBoxVm>
{
    public EmployeeCoolerBoxVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeCoolerBoxVm> Handle(EmployeeCoolerBoxVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeCoolerBoxes.Include(e => e.Employee)
                                                       .Include(e => e.EmployeeCoolerBoxCondition)
                                                       .SingleAsync(x => x.EmployeeCoolerBoxId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeCoolerBox, EmployeeCoolerBoxVm>(entity);
    }
}
