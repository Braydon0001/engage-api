namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public class EmployeeJobTitleTypesQuery : IRequest<ListResult<EmployeeJobTitleTypeDto>>
{
    public int? EmployeeJobTitleId { get; set; }
}

public class EmployeeJobTitleTypesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitleTypesQuery, ListResult<EmployeeJobTitleTypeDto>>
{
    public EmployeeJobTitleTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeJobTitleTypeDto>> Handle(EmployeeJobTitleTypesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeJobTitleTypes.AsQueryable();

        if (request.EmployeeJobTitleId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeJobTitleId == request.EmployeeJobTitleId.Value);
        }

        var entities = await queryable.OrderByDescending(e => e.EmployeeJobTitleTypeId)
                                      .ProjectTo<EmployeeJobTitleTypeDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeJobTitleTypeDto>(entities);
    }
}
