using Engage.Application.Services.EmployeeSkills.Models;

namespace Engage.Application.Services.EmployeeSkills.Queries
{
    public class EmployeeSkillsQuery : GetQuery, IRequest<ListResult<EmployeeSkillDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeSkillsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeSkillsQuery, ListResult<EmployeeSkillDto>>
    {
        public EmployeeSkillsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeSkillDto>> Handle(EmployeeSkillsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeSkills.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeSkillId)
                                                           .ProjectTo<EmployeeSkillDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeSkillDto>(entities);
        }
    }
}
