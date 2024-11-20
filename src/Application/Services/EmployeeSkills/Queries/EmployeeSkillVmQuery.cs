using Engage.Application.Services.EmployeeSkills.Models;

namespace Engage.Application.Services.EmployeeSkills.Queries;

public class EmployeeSkillVmQuery : GetByIdQuery, IRequest<EmployeeSkillVm>
{
}

public class EmployeeSkillVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeSkillVmQuery, EmployeeSkillVm>
{
    public EmployeeSkillVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeSkillVm> Handle(EmployeeSkillVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSkills.Include(e => e.Employee)
                                                     .Include(e => e.Experience)
                                                     .Include(e => e.Proficiency)
                                                     .Include(e => e.SkillCategory)
                                                     .SingleAsync(e => e.EmployeeSkillId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeSkill, EmployeeSkillVm>(entity);
    }
}
