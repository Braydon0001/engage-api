using Engage.Application.Services.SupplierClaimAccountManagers.Models;

namespace Engage.Application.Services.SupplierClaimAccountManagers.Queries
{
    public class SupplierClaimAccountManagersQuery: IRequest<ListResult<SupplierClaimAccountManagerDto>>
    {
        public int? SupplierId { get; set; }
        public int? ClaimAccountManagerId { get; set; }
    }

    public class SupplierClaimAccountManagersQueryHandler : BaseQueryHandler, IRequestHandler<SupplierClaimAccountManagersQuery, ListResult<SupplierClaimAccountManagerDto>>
    {
        public SupplierClaimAccountManagersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<SupplierClaimAccountManagerDto>> Handle(SupplierClaimAccountManagersQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.SupplierClaimAccountManagers.AsQueryable();

            if (request.SupplierId.HasValue)
            {
                queryable = queryable.Where(e => e.SupplierId == request.SupplierId);
            }
            
            if (request.ClaimAccountManagerId.HasValue)
            {
                queryable = queryable.Where(e => e.UserId == request.ClaimAccountManagerId);
            }

            var entities = await queryable.OrderBy(e => e.Supplier.Name)
                                          .ThenBy(e => e.User.FullName)
                                          .ProjectTo<SupplierClaimAccountManagerDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<SupplierClaimAccountManagerDto>(entities);
        }
    }
}
