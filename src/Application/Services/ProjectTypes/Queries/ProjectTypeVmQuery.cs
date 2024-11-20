namespace Engage.Application.Services.ProjectTypes.Queries;

public record ProjectTypeVmQuery(int Id) : IRequest<ProjectTypeVm>;

public record ProjectTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeVmQuery, ProjectTypeVm>
{
    public async Task<ProjectTypeVm> Handle(ProjectTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTypeVm>(entity);
    }
}