using Engage.Application.Services.EmployeeExpenseClaims.Models;

namespace Engage.Application.Services.EmployeeExpenseClaims.Queries
{
    public class GetEmployeeExpenseClaimVmQuery : IRequest<EmployeeExpenseClaimVm>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeExpenseClaimVmQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeExpenseClaimVmQuery, EmployeeExpenseClaimVm>
    {
        public GetEmployeeExpenseClaimVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeExpenseClaimVm> Handle(GetEmployeeExpenseClaimVmQuery request, CancellationToken cancellationToken)
        {
            var enitity = await _context.EmployeeExpenseClaims.Include(e => e.Employee)
                                                              .SingleAsync(e => e.EmployeeExpenseClaimId == request.Id, cancellationToken);

            return _mapper.Map<EmployeeExpenseClaim, EmployeeExpenseClaimVm>(enitity);
        }
    }
}
