namespace Engage.Application.Services.ProjectFileTypes.Queries;

public class ProjectFileTypeOptionListQuery : IRequest<List<ProjectFileTypeOption>>
{

}

public class ProjectFileTypeOptionListHandler : ListQueryHandler, IRequestHandler<ProjectFileTypeOptionListQuery, List<ProjectFileTypeOption>>
{
    public ProjectFileTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProjectFileTypeOption>> Handle(ProjectFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProjectFileTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}