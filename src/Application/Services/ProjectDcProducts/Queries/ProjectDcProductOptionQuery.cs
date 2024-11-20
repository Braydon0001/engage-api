namespace Engage.Application.Services.ProjectDcProducts.Queries;

public class ProjectDcProductOptionQuery : IRequest<List<ProjectDcProductOption>>
{ 

}

public record ProjectDcProductOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectDcProductOptionQuery, List<ProjectDcProductOption>>
{
    public async Task<List<ProjectDcProductOption>> Handle(ProjectDcProductOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectDcProducts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectDcProductId)
                              .ProjectTo<ProjectDcProductOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}