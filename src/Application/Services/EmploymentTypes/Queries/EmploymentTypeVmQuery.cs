using Engage.Application.Services.EmploymentTypes.Models;

namespace Engage.Application.Services.EmploymentTypes.Queries;

public class EmploymentTypeVmQuery : GetByIdQuery, IRequest<EmploymentTypeVm>
{
}

public class EmploymentTypeVmQueryHandler : BaseQueryHandler, IRequestHandler<EmploymentTypeVmQuery, EmploymentTypeVm>
{
    public EmploymentTypeVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmploymentTypeVm> Handle(EmploymentTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmploymentTypes.SingleAsync(x => x.Id == request.Id, cancellationToken);
        return _mapper.Map<EmploymentType, EmploymentTypeVm>(entity);
    }
}
