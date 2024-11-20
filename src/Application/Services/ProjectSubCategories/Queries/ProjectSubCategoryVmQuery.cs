namespace Engage.Application.Services.ProjectSubCategories.Queries;

public record ProjectSubCategoryVmQuery(int Id) : IRequest<ProjectSubCategoryVm>;

public record ProjectSubCategoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryVmQuery, ProjectSubCategoryVm>
{
    public async Task<ProjectSubCategoryVm> Handle(ProjectSubCategoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubCategories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectCategory).Include(e => e.EngageSubGroup);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectSubCategoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectSubCategoryVm>(entity);
    }
}