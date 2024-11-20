namespace Engage.Application.Services.ProjectFileTypes.Queries;

public class ProjectFileTypeVmQuery : IRequest<ProjectFileTypeVm>
{
    public int Id { get; set; }
}

public class ProjectFileTypeVmHandler : VmQueryHandler, IRequestHandler<ProjectFileTypeVmQuery, ProjectFileTypeVm>
{
    public ProjectFileTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProjectFileTypeVm> Handle(ProjectFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectFileTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProjectFileTypeVm>(entity);
    }
}