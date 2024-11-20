using Engage.Application.Services.EmployeeKpis.Models;

namespace Engage.Application.Services.EmployeeKpis.Queries
{
    public class EmployeeKpiVmQuery : IRequest<EmployeeKpiDto>
    {
        public int Id { get; set; }
    }

    public class EmployeeKpiVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeKpiVmQuery, EmployeeKpiDto>
    {
        public EmployeeKpiVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeKpiDto> Handle(EmployeeKpiVmQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeKpis.AsQueryable();

            queryable = queryable.Where(e => e.EmployeeKpiId == request.Id);

            var entity = await queryable.ProjectTo<EmployeeKpiDto>(_mapper.ConfigurationProvider)
                                          .SingleAsync(cancellationToken);

            return entity;
        }
    }
}