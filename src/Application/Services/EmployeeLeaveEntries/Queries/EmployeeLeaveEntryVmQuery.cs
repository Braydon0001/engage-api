using Engage.Application.Services.EmployeeLeaveEntries.Models;

namespace Engage.Application.Services.EmployeeLeaveEntries.Queries
{
    public class EmployeeLeaveEntryVmQuery : IRequest<EmployeeLeaveEntryVm>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeLeaveEntryViewModelQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeLeaveEntryVmQuery, EmployeeLeaveEntryVm>
    {
        public GetEmployeeLeaveEntryViewModelQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<EmployeeLeaveEntryVm> Handle(EmployeeLeaveEntryVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeeLeaveEntries.SingleAsync(x => x.EmployeeLeaveEntryId == request.Id, cancellationToken);

            return _mapper.Map<EmployeeLeaveEntry, EmployeeLeaveEntryVm>(entity);
        }
    }
}
