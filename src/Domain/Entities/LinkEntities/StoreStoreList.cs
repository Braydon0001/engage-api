namespace Engage.Domain.Entities.LinkEntities
{
    public class StoreStoreList
    {
        public int StoreId { get; set; }
        public int StoreListId { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StoreList StoreList { get; set; }
    }
}
