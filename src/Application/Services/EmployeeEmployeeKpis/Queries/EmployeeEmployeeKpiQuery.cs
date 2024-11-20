using Engage.Application.Services.EmployeeEmployeeKpis.Models;

namespace Engage.Application.Services.EmployeeEmployeeKpis.Queries
{
    public class EmployeeEmployeeKpiQuery : IRequest<List<EmployeeEmployeeKpiDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeEmployeeKpiQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeEmployeeKpiQuery, List<EmployeeEmployeeKpiDto>>
    {
        public EmployeeEmployeeKpiQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeEmployeeKpiDto>> Handle(EmployeeEmployeeKpiQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeEmployeeKpis.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeEmployeeKpiDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeEmployeeKpiDto>(entities);
        }
    }
}