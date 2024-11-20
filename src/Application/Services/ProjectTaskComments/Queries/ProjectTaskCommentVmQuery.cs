namespace Engage.Application.Services.ProjectTaskComments.Queries;

public record ProjectTaskCommentVmQuery(int Id) : IRequest<ProjectTaskCommentVm>;

public record ProjectTaskCommentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskCommentVmQuery, ProjectTaskCommentVm>
{
    public async Task<ProjectTaskCommentVm> Handle(ProjectTaskCommentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskComments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectTask);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskCommentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskCommentVm>(entity);
    }
}