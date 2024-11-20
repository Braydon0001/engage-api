namespace Engage.Application.Services.EmployeeFiles.Queries;

public class EmployeeFileListQuery : IRequest<List<EmployeeFileDto>>
{
    public int Id { get; set; }
}

public class EmployeeFileListHandler : ListQueryHandler, IRequestHandler<EmployeeFileListQuery, List<EmployeeFileDto>>
{
    public EmployeeFileListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeFileDto>> Handle(EmployeeFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeFiles.Where(t => t.EmployeeId == query.Id).AsQueryable().AsNoTracking();

        var entities = await queryable.OrderBy(e => e.EmployeeFileId)
                              .ProjectTo<EmployeeFileDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        //To remove?
        entities = entities.Where(e => e.EmployeeFileTypeName != "Photo").ToList();

        //Exclude Contracts
        entities = entities.Where(e => e.EmployeeFileTypeName != "EmploymentContract").ToList();

        return entities;
    }
}