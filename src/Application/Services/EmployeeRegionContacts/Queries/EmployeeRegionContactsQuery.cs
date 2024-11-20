using Engage.Application.Services.EmployeeRegionContacts.Models;

namespace Engage.Application.Services.EmployeeRegionContacts.Queries
{
    public class EmployeeRegionContactsQuery : GetQuery, IRequest<ListResult<EmployeeRegionContactDto>>
    {
        public int EngageRegionId { get; set; }
    }

    public class EmployeeRegionContactsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeRegionContactsQuery, ListResult<EmployeeRegionContactDto>>
    {
        public EmployeeRegionContactsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeRegionContactDto>> Handle(EmployeeRegionContactsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeRegionContacts.Where(e => e.EngageRegionId == request.EngageRegionId)
                                                           .OrderByDescending(e => e.EmployeeRegionContactId)
                                                           .ProjectTo<EmployeeRegionContactDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeRegionContactDto>(entities);
        }
    }
}
