namespace Engage.Application.Services.ProjectFileTypes.Queries;

public class ProjectFileTypeListQuery : IRequest<List<ProjectFileTypeDto>>
{

}

public class ProjectFileTypeListHandler : ListQueryHandler, IRequestHandler<ProjectFileTypeListQuery, List<ProjectFileTypeDto>>
{
    public ProjectFileTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProjectFileTypeDto>> Handle(ProjectFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProjectFileTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}