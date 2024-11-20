using Engage.Application.Services.EmploymentTypes.Models;

namespace Engage.Application.Services.EmploymentTypes.Queries;

public class EmploymentTypeQuery : GetQuery, IRequest<ListResult<EmploymentTypeDto>>
{
}

public class EmploymentTypeQueryHandler : BaseQueryHandler, IRequestHandler<EmploymentTypeQuery, ListResult<EmploymentTypeDto>>
{
    public EmploymentTypeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmploymentTypeDto>> Handle(EmploymentTypeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmploymentTypes.OrderBy(w => w.Name)
                                                     .ProjectTo<EmploymentTypeDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<EmploymentTypeDto>(entities);
    }
}
