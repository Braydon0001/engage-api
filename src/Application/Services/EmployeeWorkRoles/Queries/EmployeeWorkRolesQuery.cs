using Engage.Application.Services.EmployeeWorkRoles.Models;

namespace Engage.Application.Services.EmployeeWorkRoles.Queries
{
    public class EmployeeWorkRolesQuery : GetQuery, IRequest<ListResult<EmployeeWorkRoleDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeWorkRolesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeWorkRolesQuery, ListResult<EmployeeWorkRoleDto>>
    {
        public EmployeeWorkRolesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeWorkRoleDto>> Handle(EmployeeWorkRolesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeWorkRoles.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.StartDate)
                                                           .ProjectTo<EmployeeWorkRoleDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeWorkRoleDto>(entities);
        }
    }
}
