using Engage.Application.Services.EmployeeBenefits.Models;

namespace Engage.Application.Services.EmployeeBenefits.Queries
{
    public class EmployeeBenefitVmQuery : IRequest<EmployeeBenefitVm>
    {
        public int Id { get; set; }
    }

    public class EmployeeBenefitVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBenefitVmQuery, EmployeeBenefitVm>
    {
        public EmployeeBenefitVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeBenefitVm> Handle(EmployeeBenefitVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeeBenefits.Include(e => e.Employee)
                                                        .Include(e => e.BenefitType)
                                                        .SingleAsync(e => e.EmployeeBenefitId == request.Id, cancellationToken);

            return _mapper.Map<EmployeeBenefit, EmployeeBenefitVm>(entity);
        }
    }
}
