using Engage.Application.Services.EmployeeExpenseClaims.Models;

namespace Engage.Application.Services.EmployeeExpenseClaims.Queries;

public class EmployeeExpenseClaimsQuery : GetQuery, IRequest<ListResult<EmployeeExpenseClaimDto>>
{
    public int EmployeeId { get; set; }
}

public class EmployeeExpenseClaimsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeExpenseClaimsQuery, ListResult<EmployeeExpenseClaimDto>>
{
    public EmployeeExpenseClaimsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeExpenseClaimDto>> Handle(EmployeeExpenseClaimsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeExpenseClaims.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderBy(e => e.Description)
                                                           .ProjectTo<EmployeeExpenseClaimDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        return new ListResult<EmployeeExpenseClaimDto>(entities);
    }
}
