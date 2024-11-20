namespace Engage.Application.Services.CostCenterDepartments.Queries;

public class CostCenterDepartmentOptionQuery : IRequest<List<CostCenterDepartmentOption>>
{
    public int? CostCenterId { get; set; }
    public string CostCenterIds { get; set; }
}

public record CostCenterDepartmentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterDepartmentOptionQuery, List<CostCenterDepartmentOption>>
{
    public async Task<List<CostCenterDepartmentOption>> Handle(CostCenterDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterDepartments.AsQueryable().AsNoTracking();

        //if (query.CostCenterIds.IsNotNullOrWhiteSpace())
        //{
        //}

        return await queryable.OrderBy(e => e.CostCenterDepartmentId)
                              .ProjectTo<CostCenterDepartmentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}