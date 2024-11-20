using Engage.Application.Services.EmployeeBadges.Models;

namespace Engage.Application.Services.EmployeeBadges.Queries
{
    public class EmployeeBadgeQuery : IRequest<List<EmployeeBadgeDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeBadgeQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBadgeQuery, List<EmployeeBadgeDto>>
    {
        public EmployeeBadgeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeBadgeDto>> Handle(EmployeeBadgeQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeBadges.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeBadgeId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeBadgeDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeBadgeDto>(entities);
        }
    }
}