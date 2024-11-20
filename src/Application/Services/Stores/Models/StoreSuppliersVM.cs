using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.SupplierStores.Models;

namespace Engage.Application.Services.Stores.Models;

public class StoreSuppliersVM
{
    public StoreDto Store { get; set; }
    public ListResult<SupplierStoreDto> SupplierStores { get; set; }
}
