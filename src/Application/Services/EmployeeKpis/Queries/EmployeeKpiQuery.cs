using Engage.Application.Services.EmployeeKpis.Models;

namespace Engage.Application.Services.EmployeeKpis.Queries
{
    public class EmployeeKpiQuery : IRequest<List<EmployeeKpiDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeKpiQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeKpiQuery, List<EmployeeKpiDto>>
    {
        public EmployeeKpiQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeKpiDto>> Handle(EmployeeKpiQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeKpis.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeKpiId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeKpiDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeKpiDto>(entities);
        }
    }
}