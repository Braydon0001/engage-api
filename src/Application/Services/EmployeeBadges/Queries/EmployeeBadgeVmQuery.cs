using Engage.Application.Services.EmployeeBadges.Models;

namespace Engage.Application.Services.EmployeeBadges.Queries
{
    public class EmployeeBadgeVmQuery : IRequest<EmployeeBadgeDto>
    {
        public int Id { get; set; }
    }

    public class EmployeeBadgeVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBadgeVmQuery, EmployeeBadgeDto>
    {
        public EmployeeBadgeVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeBadgeDto> Handle(EmployeeBadgeVmQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EmployeeBadges.AsQueryable();
                queryable = queryable.Where(e => e.EmployeeBadgeId == request.Id);

            var entity = await queryable.ProjectTo<EmployeeBadgeDto>(_mapper.ConfigurationProvider)
                                          .SingleAsync(cancellationToken);

            return entity;
        }
    }
}