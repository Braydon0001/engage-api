using Engage.Application.Services.EngageDepartments.Models;

namespace Engage.Application.Services.EngageDepartments.Queries;

public class EngageDepartmentsQuery : GetQuery, IRequest<ListResult<EngageDepartmentDto>>
{

}

public class EngageDepartmentsHandler : BaseQueryHandler, IRequestHandler<EngageDepartmentsQuery, ListResult<EngageDepartmentDto>>
{
    public EngageDepartmentsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageDepartmentDto>> Handle(EngageDepartmentsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.EngageDepartments.OrderBy(e => e.Name)
                                                     .ProjectTo<EngageDepartmentDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<EngageDepartmentDto>(entities);

    }
}
