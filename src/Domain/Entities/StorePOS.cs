using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class StorePOS : BaseAuditableEntity
    {
        public int StorePOSId { get; set; }
        public int StoreId { get; set; }
        public int StorePOSTypeId { get; set; }
        public int A0PosterQty { get; set; }
        public int A1PosterQty { get; set; }
        public int A2PosterQty { get; set; }
        public int A3BuntingQty { get; set; }
        public int AisleBladesQty { get; set; }
        public int HangingMobilesQty { get; set; }
        public int ShelfStripsQty { get; set; }
        public int ShelfTalkersQty { get; set; }
        public int TentCardsQty { get; set; }
        public int TableClothsQty { get; set; }
        public int WobblersQty { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StorePOSType StorePOSType { get; set; }
    }
}
