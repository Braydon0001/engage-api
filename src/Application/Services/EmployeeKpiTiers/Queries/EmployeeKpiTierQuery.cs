using Engage.Application.Services.EmployeeKpiTiers.Models;

namespace Engage.Application.Services.EmployeeKpiTiers.Queries
{
    public class EmployeeKpiTierQuery : IRequest<List<EmployeeKpiTierDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeKpiTierQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeKpiTierQuery, List<EmployeeKpiTierDto>>
    {
        public EmployeeKpiTierQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeKpiTierDto>> Handle(EmployeeKpiTierQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeKpiTiers.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeKpiTierId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeKpiTierDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeKpiTierDto>(entities);
        }
    }
}