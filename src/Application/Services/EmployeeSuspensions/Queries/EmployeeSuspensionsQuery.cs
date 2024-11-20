using Engage.Application.Services.EmployeeSuspensions.Models;

namespace Engage.Application.Services.EmployeeSuspensions.Queries
{
    public class EmployeeSuspensionsQuery : GetQuery, IRequest<ListResult<EmployeeSuspensionDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeSuspensionsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeSuspensionsQuery, ListResult<EmployeeSuspensionDto>>
    {
        public EmployeeSuspensionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeSuspensionDto>> Handle(EmployeeSuspensionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeSuspensions.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeSuspensionId)
                                                           .ProjectTo<EmployeeSuspensionDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeSuspensionDto>(entities);
        }
    }
}
