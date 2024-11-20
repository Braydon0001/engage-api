namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public class EmployeeJobTitleTimesQuery : IRequest<ListResult<EmployeeJobTitleTimeDto>>
{
    public int? EmployeeJobTitleId { get; set; }
}

public class EmployeeJobTitleTimesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitleTimesQuery, ListResult<EmployeeJobTitleTimeDto>>
{
    public EmployeeJobTitleTimesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeJobTitleTimeDto>> Handle(EmployeeJobTitleTimesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeJobTitleTimes.AsQueryable();

        if (request.EmployeeJobTitleId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeJobTitleId == request.EmployeeJobTitleId.Value);
        }

        var entities = await queryable.OrderByDescending(e => e.EmployeeJobTitleTimeId)
                                      .ProjectTo<EmployeeJobTitleTimeDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeJobTitleTimeDto>(entities);
    }
}
