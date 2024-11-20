// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryVmQuery : IRequest<ProductSubCategoryVm>
{
    public int Id { get; set; }
}

public class ProductSubCategoryVmHandler : VmQueryHandler, IRequestHandler<ProductSubCategoryVmQuery, ProductSubCategoryVm>
{
    public ProductSubCategoryVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSubCategoryVm> Handle(ProductSubCategoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubCategories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductCategory);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductSubCategoryId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductSubCategoryVm>(entity);
    }
}