// auto-generated
namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryVmQuery : IRequest<ProductCategoryVm>
{
    public int Id { get; set; }
}

public class ProductCategoryVmHandler : VmQueryHandler, IRequestHandler<ProductCategoryVmQuery, ProductCategoryVm>
{
    public ProductCategoryVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductCategoryVm> Handle(ProductCategoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductCategories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductSubGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductCategoryId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductCategoryVm>(entity);
    }
}