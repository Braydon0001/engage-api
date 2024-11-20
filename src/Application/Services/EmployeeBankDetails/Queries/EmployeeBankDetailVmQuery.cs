using Engage.Application.Services.EmployeeBankDetails.Models;
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeBankDetails.Queries
{
    public class EmployeeBankDetailVmQuery : IRequest<EmployeeBankDetailVm>
    {
        public int Id { get; set; }
    }

    public class EmployeeBankDetailVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBankDetailVmQuery, EmployeeBankDetailVm>
    {
        private readonly IMediator _mediator;
        public EmployeeBankDetailVmQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<EmployeeBankDetailVm> Handle(EmployeeBankDetailVmQuery request, CancellationToken cancellationToken)
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            var entity = await _context.EmployeeBankDetails.Include(e => e.Employee)
                                                           .Include(e => e.BankAccountOwner)
                                                           .Include(e => e.BankAccountType)
                                                           .Include(e => e.BankName)
                                                           .Include(e => e.BankPaymentMethod)
                                                           .SingleAsync(e => e.EmployeeBankDetailId == request.Id, cancellationToken);

            if (!engageRegionIds.Contains(7))
            {
                var isInRegion = await _context.EmployeeRegions.Where(e => e.EmployeeId == entity.EmployeeId && engageRegionIds.Contains(e.EngageRegionId))
                                                               .AnyAsync(cancellationToken);

                if (!isInRegion)
                {
                    throw new Exception("This Employee is not in your Region");
                }
            }

            return _mapper.Map<EmployeeBankDetail, EmployeeBankDetailVm>(entity);
        }
    }
}
