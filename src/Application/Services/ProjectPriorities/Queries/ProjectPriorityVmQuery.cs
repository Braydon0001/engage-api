namespace Engage.Application.Services.ProjectPriorities.Queries;

public record ProjectPriorityVmQuery(int Id) : IRequest<ProjectPriorityVm>;

public record ProjectPriorityVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPriorityVmQuery, ProjectPriorityVm>
{
    public async Task<ProjectPriorityVm> Handle(ProjectPriorityVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectPriorities.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectPriorityId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectPriorityVm>(entity);
    }
}