namespace Engage.Application.Services.ProjectFiles.Queries;

public class ProjectFileListQuery : IRequest<List<ProjectFileDto>>
{
    public int? ProjectId { get; set; }
}

public class ProjectFileListHandler : ListQueryHandler, IRequestHandler<ProjectFileListQuery, List<ProjectFileDto>>
{
    public ProjectFileListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProjectFileDto>> Handle(ProjectFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectFiles.AsQueryable().AsNoTracking();

        if (query.ProjectId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectId == query.ProjectId.Value);
        }

        return await queryable.OrderBy(e => e.ProjectFileId)
                              .ProjectTo<ProjectFileDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}