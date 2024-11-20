using Engage.Application.Services.EmployeeTrainingRecords.Models;

namespace Engage.Application.Services.EmployeeTrainingRecords.Queries
{
    public class EmployeeTrainingRecordsQuery : GetQuery, IRequest<ListResult<EmployeeTrainingRecordDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeTrainingRecordsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeTrainingRecordsQuery, ListResult<EmployeeTrainingRecordDto>>
    {
        public EmployeeTrainingRecordsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeTrainingRecordDto>> Handle(EmployeeTrainingRecordsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeTrainingRecords.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeTrainingRecordId)
                                                           .ProjectTo<EmployeeTrainingRecordDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeTrainingRecordDto>(entities);
        }
    }
}
