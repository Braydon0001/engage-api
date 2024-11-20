using Engage.Application.Services.EmployeeDeductions.Models;

namespace Engage.Application.Services.EmployeeDeductions.Queries
{
    public class EmployeeDeductionsQuery : GetQuery, IRequest<ListResult<EmployeeDeductionDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeDeductionsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeDeductionsQuery, ListResult<EmployeeDeductionDto>>
    {
        public EmployeeDeductionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeDeductionDto>> Handle(EmployeeDeductionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeDeductions.Where(e => e.EmployeeId == request.EmployeeId)
                                                            .OrderBy(e => e.Note)
                                                            .ProjectTo<EmployeeDeductionDto>(_mapper.ConfigurationProvider)
                                                            .ToListAsync(cancellationToken);

            return new ListResult<EmployeeDeductionDto>(entities);
        }
    }
}
