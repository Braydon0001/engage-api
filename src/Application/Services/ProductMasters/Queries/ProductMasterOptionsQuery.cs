namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}
public class ProductMasterOptionsQueryHandler : ListQueryHandler, IRequestHandler<ProductMasterOptionsQuery, List<OptionDto>>
{
    public ProductMasterOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(ProductMasterOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasters.AsQueryable().AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.Name, $"%{request.Search}%"))
                                                );

        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ProductMasterId, Name = e.Code + " - " + e.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}