namespace Engage.Application.Services.ProjectDcProducts.Queries;

public class ProjectDcProductListQuery : IRequest<List<ProjectDcProductDto>>
{

}

public record ProjectDcProductListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectDcProductListQuery, List<ProjectDcProductDto>>
{
    public async Task<List<ProjectDcProductDto>> Handle(ProjectDcProductListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectDcProducts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectDcProductId)
                              .ProjectTo<ProjectDcProductDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}