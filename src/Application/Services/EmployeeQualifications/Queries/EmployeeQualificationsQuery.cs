using Engage.Application.Services.EmployeeQualifications.Models;

namespace Engage.Application.Services.EmployeeQualifications.Queries
{
    public class EmployeeQualificationsQuery : GetQuery, IRequest<ListResult<EmployeeQualificationDto>>
    {
        public int EmployeeId { get; set; }
    }

    public class EmployeeQualificationsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeQualificationsQuery, ListResult<EmployeeQualificationDto>>
    {
        public EmployeeQualificationsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<EmployeeQualificationDto>> Handle(EmployeeQualificationsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.EmployeeQualifications.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeQualificationId)
                                                           .ProjectTo<EmployeeQualificationDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

            return new ListResult<EmployeeQualificationDto>(entities);
        }
    }
}
