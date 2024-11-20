namespace Engage.Application.Services.ProjectCategories.Queries;

public class ProjectCategoryListQuery : IRequest<List<ProjectCategoryDto>>
{

}

public record ProjectCategoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategoryListQuery, List<ProjectCategoryDto>>
{
    public async Task<List<ProjectCategoryDto>> Handle(ProjectCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectCategoryId)
                              .ProjectTo<ProjectCategoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}