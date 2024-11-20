using Engage.Application.Services.SupplierProducts.Models;

namespace Engage.Application.Services.SupplierProducts.Queries
{
    public class SupplierProductsQuery: IRequest<ListResult<SupplierProductDto>>
    {
        public int? SupplierId { get; set; }
        public int? EngageMasterProductId { get; set; }
    }

    public class SupplierProductsQueryHandler : BaseQueryHandler, IRequestHandler<SupplierProductsQuery, ListResult<SupplierProductDto>>
    {
        public SupplierProductsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<SupplierProductDto>> Handle(SupplierProductsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.SupplierProducts.AsQueryable();

            if (request.SupplierId.HasValue)
            {
                queryable = queryable.Where(e => e.SupplierId == request.SupplierId);
            }
            
            if (request.EngageMasterProductId.HasValue)
            {
                queryable = queryable.Where(e => e.EngageMasterProductId == request.EngageMasterProductId);
            }

            var entities = await queryable.OrderBy(e => e.Supplier.Name)
                                          .ThenBy(e => e.EngageMasterProduct.Name)
                                          .ProjectTo<SupplierProductDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<SupplierProductDto>(entities);
        }
    }
}
