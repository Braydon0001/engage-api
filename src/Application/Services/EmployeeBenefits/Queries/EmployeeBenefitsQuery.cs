using Engage.Application.Services.EmployeeBenefits.Models;

namespace Engage.Application.Services.EmployeeBenefits.Queries
{
    public class EmployeeBenefitsQuery : GetQuery, IRequest<ListResult<EmployeeBenefitDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeBenefitsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBenefitsQuery, ListResult<EmployeeBenefitDto>>
    {
        public EmployeeBenefitsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<EmployeeBenefitDto>> Handle(EmployeeBenefitsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeBenefits.Where(e => e.EmployeeId == request.EmployeeId)
                                                          .OrderBy(e => e.Name)
                                                          .ProjectTo<EmployeeBenefitDto>(_mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

            return new ListResult<EmployeeBenefitDto>(entities);
        }
    }
}
