using Engage.Application.Services.EmployeeEmployeeBadges.Models;

namespace Engage.Application.Services.EmployeeEmployeeBadges.Queries
{
    public class EmployeeEmployeeBadgeQuery : IRequest<List<EmployeeEmployeeBadgeDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeEmployeeBadgeQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeEmployeeBadgeQuery, List<EmployeeEmployeeBadgeDto>>
    {
        public EmployeeEmployeeBadgeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeEmployeeBadgeDto>> Handle(EmployeeEmployeeBadgeQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeEmployeeBadges.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeEmployeeBadgeDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeEmployeeBadgeDto>(entities);
        }
    }
}