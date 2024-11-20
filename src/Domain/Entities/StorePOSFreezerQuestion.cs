using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class StorePOSFreezerQuestion : BaseAuditableEntity
    {
        public int StorePOSFreezerQuestionId { get; set; }
        public int StoreId { get; set; }
        public int StorePOSTypeId { get; set; }
        public int StorePOSFreezerTypeId { get; set; }
        public bool IsWobblers { get; set; }
        public string WobblersComment { get; set; }
        public bool IsFreezerDecals { get; set; }
        public string FreezerDecalsComment { get; set; }
        public bool IsShelfTalker { get; set; }
        public string ShelfTalkerComment { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StorePOSType StorePOSType { get; set; }
        public StorePOSFreezerType StorePOSFreezerType { get; set; }
    }
}
