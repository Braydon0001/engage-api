namespace Engage.Application.Services.ProjectFiles.Queries;

public class ProjectFileVmQuery : IRequest<ProjectFileVm>
{
    public int Id { get; set; }
}

public class ProjectFileVmHandler : VmQueryHandler, IRequestHandler<ProjectFileVmQuery, ProjectFileVm>
{
    public ProjectFileVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFileVm> Handle(ProjectFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.ProjectFileType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectFileId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProjectFileVm>(entity);
    }
}