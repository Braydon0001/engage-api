namespace Engage.Application.Services.ProjectSubTypes.Queries;

public record ProjectSubTypeVmQuery(int Id) : IRequest<ProjectSubTypeVm>;

public record ProjectSubTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeVmQuery, ProjectSubTypeVm>
{
    public async Task<ProjectSubTypeVm> Handle(ProjectSubTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectSubTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectSubTypeVm>(entity);
    }
}