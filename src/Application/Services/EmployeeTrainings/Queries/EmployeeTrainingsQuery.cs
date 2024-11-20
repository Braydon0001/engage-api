using Engage.Application.Services.EmployeeTrainings.Models;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.EmployeeTrainings.Queries
{
    public class EmployeeTrainingsQuery : GetQuery, IRequest<PaginatedListResult<EmployeeTrainingDto>>
    {
    }

    public class EmployeeTrainingsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeTrainingsQuery, PaginatedListResult<EmployeeTrainingDto>>
    {
        private readonly IMultiTenantContextAccessor _multiTenantContextAccessor;
        public EmployeeTrainingsQueryHandler(IAppDbContext context, IMapper mapper, IMultiTenantContextAccessor multiTenantContextAccessor) : base(context, mapper)
        {
            _multiTenantContextAccessor = multiTenantContextAccessor;
        }

        public async Task<PaginatedListResult<EmployeeTrainingDto>> Handle(EmployeeTrainingsQuery request, CancellationToken cancellationToken)
        {
            var (queryable, paginationResult) = _context.EmployeeTrainings.Paginate(request, _multiTenantContextAccessor);

            var entities = await queryable.ProjectTo<EmployeeTrainingDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new PaginatedListResult<EmployeeTrainingDto>(entities, paginationResult);
        }
    }
}
