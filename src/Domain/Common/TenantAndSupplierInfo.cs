using Finbuckle.MultiTenant;

namespace Engage.Domain.Common;

public class TenantAndSupplierInfo : TenantInfo
{
    public int? SupplierId { get; set; }
}

