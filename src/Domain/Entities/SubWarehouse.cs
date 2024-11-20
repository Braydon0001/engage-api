using Engage.Domain.Common;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class SubWarehouse : BaseAuditableEntity
    {
        public SubWarehouse()
        {
            DCProducts = new HashSet<DCProduct>();  
        }

        public int SubWarehouseId { get; set; }
        public string Name { get; set; }

        public ICollection<DCProduct> DCProducts { get; set; }
    }
}
