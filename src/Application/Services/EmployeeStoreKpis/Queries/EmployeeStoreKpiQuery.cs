using Engage.Application.Services.EmployeeStoreKpis.Models;

namespace Engage.Application.Services.EmployeeStoreKpis.Queries
{
    public class EmployeeStoreKpiQuery : IRequest<List<EmployeeStoreKpiDto>>
    {
        public int? Id { get; set; }
    }

    public class EmployeeStoreKpiQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreKpiQuery, List<EmployeeStoreKpiDto>>
    {
        public EmployeeStoreKpiQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EmployeeStoreKpiDto>> Handle(EmployeeStoreKpiQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeStoreKpis.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.EmployeeId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<EmployeeStoreKpiDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<EmployeeStoreKpiDto>(entities);
        }
    }
}