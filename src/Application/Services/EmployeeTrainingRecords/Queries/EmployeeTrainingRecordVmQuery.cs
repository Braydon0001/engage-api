using Engage.Application.Services.EmployeeTrainingRecords.Models;

namespace Engage.Application.Services.EmployeeTrainingRecords.Queries;

public class EmployeeTrainingRecordVmQuery : GetByIdQuery, IRequest<EmployeeTrainingRecordVm>
{
}

public class EmployeeTrainingRecordVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeTrainingRecordVmQuery, EmployeeTrainingRecordVm>
{
    public EmployeeTrainingRecordVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeTrainingRecordVm> Handle(EmployeeTrainingRecordVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTrainingRecords.Include(e => e.Employee)
                                                     .Include(e => e.EmployeeTrainingStatus)
                                                     .SingleAsync(e => e.EmployeeTrainingRecordId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeTrainingRecord, EmployeeTrainingRecordVm>(entity);
    }
}
