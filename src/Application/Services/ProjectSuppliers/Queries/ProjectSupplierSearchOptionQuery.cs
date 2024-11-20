namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierSearchOptionQuery : IRequest<List<OptionDto>>
{
    public int? ProjectCategoryId { get; set; }
    public string Search { get; set; }
}

public record ProjectSupplierSearchOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierSearchOptionQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(ProjectSupplierSearchOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Suppliers.AsQueryable().AsNoTracking();

        //if (query.ProjectCategoryId.HasValue)
        //{
        //    var projectIds = await Context.ProjectCategorySuppliers.AsNoTracking()
        //                                                           .Where(e => e.ProjectCategoryId == query.ProjectCategoryId)
        //                                                           .Select(e => e.SupplierId)
        //                                                           .ToListAsync(cancellationToken);
        //    queryable = queryable.Where(e => e.)
        //}

        queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"{query.Search}"));

        return await queryable.OrderBy(e => e.SupplierId)
                              .Take(100)
                              .Select(o => new OptionDto { Id = o.SupplierId, Name = o.Name, Disabled = o.Disabled })
                              .ToListAsync(cancellationToken);
    }
}