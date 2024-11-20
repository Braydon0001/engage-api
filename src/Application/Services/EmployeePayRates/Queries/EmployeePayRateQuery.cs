using Engage.Application.Services.EmployeePayRates.Models;

namespace Engage.Application.Services.EmployeePayRates.Queries
{
    public class EmployeePayRateQuery : GetByIdQuery, IRequest<EmployeePayRateDto>
    {
    }

    public class EmployeePayRateQueryHandler : BaseQueryHandler, IRequestHandler<EmployeePayRateQuery, EmployeePayRateDto>
    {
        public EmployeePayRateQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<EmployeePayRateDto> Handle(EmployeePayRateQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeePayRates.Include(x => x.Employee)
                                                .Include(x => x.EmployeePayRateFrequency)
                                                .Include(x => x.EmployeePayRatePackage)
                                                .FirstOrDefaultAsync(x => x.EmployeePayRateId == request.Id, cancellationToken);

            return _mapper.Map<EmployeePayRate, EmployeePayRateDto>(entity);
        }
    }
}
