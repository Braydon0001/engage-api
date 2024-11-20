using Engage.Application.Services.EmployeeKpiTiers.Models;

namespace Engage.Application.Services.EmployeeKpiTiers.Queries
{
    public class EmployeeKpiTierVmQuery : IRequest<EmployeeKpiTierDto>
    {
        public int Id { get; set; }
    }

    public class EmployeeKpiTierVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeKpiTierVmQuery, EmployeeKpiTierDto>
    {
        public EmployeeKpiTierVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeKpiTierDto> Handle(EmployeeKpiTierVmQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeKpiTiers.AsQueryable();
                queryable = queryable.Where(e => e.EmployeeKpiTierId == request.Id);

            var entity = await queryable.ProjectTo<EmployeeKpiTierDto>(_mapper.ConfigurationProvider)
                                          .SingleAsync(cancellationToken);

            return entity;
        }
    }
}