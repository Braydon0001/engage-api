namespace Engage.Application.Services.ProjectDcProducts.Queries;

public record ProjectDcProductVmQuery(int Id) : IRequest<ProjectDcProductVm>;

public record ProjectDcProductVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectDcProductVmQuery, ProjectDcProductVm>
{
    public async Task<ProjectDcProductVm> Handle(ProjectDcProductVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectDcProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.DcProduct);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectDcProductId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectDcProductVm>(entity);
    }
}