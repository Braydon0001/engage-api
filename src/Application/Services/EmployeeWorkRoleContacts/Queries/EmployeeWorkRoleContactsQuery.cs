using Engage.Application.Services.EmployeeWorkRoleContacts.Models;

namespace Engage.Application.Services.EmployeeWorkRoleContacts.Queries
{
    public class EmployeeWorkRoleContactsQuery : GetQuery, IRequest<ListResult<EmployeeWorkRoleContactDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeWorkRoleContactsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeWorkRoleContactsQuery, ListResult<EmployeeWorkRoleContactDto>>
    {
        public EmployeeWorkRoleContactsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeWorkRoleContactDto>> Handle(EmployeeWorkRoleContactsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeWorkRoleContacts.Where(e => e.EmployeeWorkRole.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeWorkRoleContactId)
                                                           .ProjectTo<EmployeeWorkRoleContactDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeWorkRoleContactDto>(entities);
        }
    }
}
