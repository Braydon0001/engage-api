using Engage.Application.Services.EmployeeQualifications.Models;

namespace Engage.Application.Services.EmployeeQualifications.Queries;

public class EmployeeQualificationVmQuery : GetByIdQuery, IRequest<EmployeeQualificationVm>
{
}

public class EmployeeQualificationVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeQualificationVmQuery, EmployeeQualificationVm>
{
    public EmployeeQualificationVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeQualificationVm> Handle(EmployeeQualificationVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeQualifications.Include(e => e.Employee)
                                                          .Include(e => e.EducationLevel)
                                                          .Include(e => e.InstitutionType)
                                                          .SingleAsync(e => e.EmployeeQualificationId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeQualification, EmployeeQualificationVm>(entity);
    }
}
