namespace Engage.Application.Services.ProjectComments.Queries;

public record ProjectCommentVmQuery(int Id) : IRequest<ProjectCommentVm>;

public record ProjectCommentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCommentVmQuery, ProjectCommentVm>
{
    public async Task<ProjectCommentVm> Handle(ProjectCommentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectComments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectCommentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectCommentVm>(entity);
    }
}