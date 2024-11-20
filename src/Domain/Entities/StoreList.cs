using Engage.Domain.Common;
using Engage.Domain.Entities.LinkEntities;
using System.Collections.Generic;

namespace Engage.Domain.Entities
{
    public class StoreList : BaseAuditableEntity
    {
        public StoreList()
        {
            StoreStoreLists = new HashSet<StoreStoreList>();
        }

        public int StoreListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Properties       
        public ICollection<StoreStoreList> StoreStoreLists { get; set; }
    }
}
