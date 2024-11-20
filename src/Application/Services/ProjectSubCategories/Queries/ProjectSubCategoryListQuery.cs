namespace Engage.Application.Services.ProjectSubCategories.Queries;

public class ProjectSubCategoryListQuery : IRequest<List<ProjectSubCategoryDto>>
{

}

public record ProjectSubCategoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubCategoryListQuery, List<ProjectSubCategoryDto>>
{
    public async Task<List<ProjectSubCategoryDto>> Handle(ProjectSubCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectSubCategoryId)
                              .ProjectTo<ProjectSubCategoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}