using Engage.Application.Services.DCDepartments.Models;

namespace Engage.Application.Services.DCDepartments.Queries;

public class DCDepartmentVmQuery : GetByIdQuery, IRequest<DCDepartmentVm>
{
}

public class DCDepartmentVmQueryHandler : BaseQueryHandler, IRequestHandler<DCDepartmentVmQuery, DCDepartmentVm>
{
    public DCDepartmentVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<DCDepartmentVm> Handle(DCDepartmentVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DCDepartments.SingleAsync(x => x.DCDepartmentId == request.Id, cancellationToken);
        return _mapper.Map<DCDepartment, DCDepartmentVm>(entity);
    }
}
