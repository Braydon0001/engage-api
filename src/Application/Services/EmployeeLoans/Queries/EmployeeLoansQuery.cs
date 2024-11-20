using Engage.Application.Services.EmployeeLoans.Models;

namespace Engage.Application.Services.EmployeeLoans.Queries;

public class EmployeeLoansQuery : GetQuery, IRequest<ListResult<EmployeeLoanDto>>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeLoanListQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeLoansQuery, ListResult<EmployeeLoanDto>>
{
    public GetEmployeeLoanListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeLoanDto>> Handle(EmployeeLoansQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeLoans.Where(e => e.EmployeeId == request.EmployeeId)
                                                   .OrderBy(e => e.Created)
                                                   .ProjectTo<EmployeeLoanDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<EmployeeLoanDto>(entities);
    }
}
