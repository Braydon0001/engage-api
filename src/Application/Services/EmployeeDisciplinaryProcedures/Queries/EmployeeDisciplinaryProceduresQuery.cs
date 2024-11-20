using Engage.Application.Services.EmployeeDisciplinaryProcedures.Models;

namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Queries;

public class EmployeeDisciplinaryProceduresQuery : GetQuery, IRequest<ListResult<EmployeeDisciplinaryDto>>
{
    public int EmployeeId { get; set; }
}

public class EmployeeDisciplinaryProceduresQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeDisciplinaryProceduresQuery, ListResult<EmployeeDisciplinaryDto>>
{
    public EmployeeDisciplinaryProceduresQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeDisciplinaryDto>> Handle(EmployeeDisciplinaryProceduresQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeDisciplinaryProcedures.Where(e => e.EmployeeId == request.EmployeeId)
                                                                    .OrderBy(e => e.Description)
                                                                    .ProjectTo<EmployeeDisciplinaryDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

        return new ListResult<EmployeeDisciplinaryDto>(entities);
    }
}
