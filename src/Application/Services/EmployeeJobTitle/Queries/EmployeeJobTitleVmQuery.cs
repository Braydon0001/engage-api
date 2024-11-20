using Engage.Application.Services.EmployeeJobTitles.Models;

namespace Engage.Application.Services.EmployeeJobTitles.Queries;

public class EmployeeJobTitleVmQuery : GetByIdQuery, IRequest<EmployeeJobTitleVm>
{
}

public class EmployeeJobTitleVmHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitleVmQuery, EmployeeJobTitleVm>
{
    public EmployeeJobTitleVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeJobTitleVm> Handle(EmployeeJobTitleVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeJobTitles.Include(e => e.Parent)
                                                     .SingleAsync(x => x.EmployeeJobTitleId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeJobTitle, EmployeeJobTitleVm>(entity);
    }
}
