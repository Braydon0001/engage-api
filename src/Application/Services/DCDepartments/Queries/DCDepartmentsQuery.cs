using Engage.Application.Services.DCDepartments.Models;

namespace Engage.Application.Services.DCDepartments.Queries;

public class DCDepartmentsQuery : GetQuery, IRequest<ListResult<DCDepartmentDto>>
{
}

public class DCDepartmentsQueryHandler : BaseQueryHandler, IRequestHandler<DCDepartmentsQuery, ListResult<DCDepartmentDto>>
{
    public DCDepartmentsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<DCDepartmentDto>> Handle(DCDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.DCDepartments.OrderBy(w => w.Name)
                                                   .ProjectTo<DCDepartmentDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<DCDepartmentDto>(entities);
    }
}
