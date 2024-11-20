using Engage.Application.Services.EmployeeBankDetails.Models;
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeBankDetails.Queries
{
    public class EmployeeBankDetailsQuery : GetQuery, IRequest<ListResult<EmployeeBankDetailDto>>
    {
        public int EmployeeId { get; set; }
        public bool? IsPrimary { get; set; }
    }

    public class EmployeeBankDetailsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeBankDetailsQuery, ListResult<EmployeeBankDetailDto>>
    {
        private readonly IMediator _mediator;
        public EmployeeBankDetailsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<ListResult<EmployeeBankDetailDto>> Handle(EmployeeBankDetailsQuery request, CancellationToken cancellationToken)
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            if (!engageRegionIds.Contains(7))
            {
                var isInRegion = await _context.EmployeeRegions.Where(e => e.EmployeeId == request.EmployeeId && engageRegionIds.Contains(e.EngageRegionId))
                                                               .AnyAsync(cancellationToken);

                if (!isInRegion)
                {
                    throw new Exception("This Employee is not in your Region");
                }
            }

            var queryable = _context.EmployeeBankDetails.AsQueryable();

            if (request.IsPrimary.HasValue)
            {
                queryable = queryable.Where(e => e.IsPrimary == request.IsPrimary.Value);
            }

            var entities = await queryable.Where(e => e.EmployeeId == request.EmployeeId)
                                          .OrderBy(e => e.BankName.Name)
                                          .ThenBy(e => e.AccountNumber)
                                          .ProjectTo<EmployeeBankDetailDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<EmployeeBankDetailDto>(entities);
        }
    }
}
