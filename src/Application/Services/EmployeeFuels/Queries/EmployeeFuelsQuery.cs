using Engage.Application.Services.EmployeeFuels.Models;

namespace Engage.Application.Services.EmployeeFuels.Queries;

public class EmployeeFuelsQuery : GetQuery, IRequest<ListResult<EmployeeFuelDto>>
{
    public int EmployeeId { get; set; }
}

public class EmployeeFuelsHandler : BaseQueryHandler, IRequestHandler<EmployeeFuelsQuery, ListResult<EmployeeFuelDto>>
{
    public EmployeeFuelsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeFuelDto>> Handle(EmployeeFuelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeFuels.Where(e => e.EmployeeId == request.EmployeeId)
                                                   .OrderByDescending(e => e.EmployeeFuelId)
                                                   .ProjectTo<EmployeeFuelDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<EmployeeFuelDto>(entities);
    }
}
