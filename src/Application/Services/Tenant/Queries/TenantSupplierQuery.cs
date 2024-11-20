using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.Tenant.Queries;

public record TenantSupplierQuery() : IRequest<OptionDto>;

public record TenantSupplierHandler(IAppDbContext Context, IMapper Mapper, IMultiTenantContextAccessor<TenantAndSupplierInfo> MultiTenantContextAccessor) : IRequestHandler<TenantSupplierQuery, OptionDto>
{
    public async Task<OptionDto> Handle(TenantSupplierQuery query, CancellationToken cancellationToken)
    {
        var supplierId = MultiTenantContextAccessor.MultiTenantContext.TenantInfo.SupplierId;
        var supplier = await Context.Suppliers.SingleOrDefaultAsync(e => e.SupplierId == supplierId, cancellationToken);
        var supplierOption = new OptionDto()
        {
            Id = supplier.SupplierId,
            Name = supplier.Name,
            Disabled = supplier.Disabled
        };

        return supplierOption;
    }
}