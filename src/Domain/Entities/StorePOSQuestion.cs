using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class StorePOSQuestion: BaseAuditableEntity
    {
        public int StorePOSQuestionId { get; set; }
        public int StoreId { get; set; }
        public int StorePOSTypeId { get; set; }
        public bool IsFridgeDecals { get; set; }
        public string FridgeDecalsComment { get; set; }
        public bool IsFloorDecals { get; set; }
        public string FloorDecalsComment { get; set; }
        public bool IsFSUDecals { get; set; }
        public string FSUDecalsComment { get; set; }
        public bool IsFSUDecalsPaid { get; set; }
        public string FSUDecalsPaidComment { get; set; }
        public bool IsShelfStrips { get; set; }
        public string ShelfStripsComment { get; set; }
        public bool IsAisleBlades { get; set; }
        public string AisleBladesComment { get; set; }
        public bool IsStandee { get; set; }
        public string StandeeComment { get; set; }
        public bool IsEntryBox { get; set; }
        public string EntryBoxComment { get; set; }
        public bool IsBaseWrap { get; set; }
        public string BaseWrapComment { get; set; }
        public bool IsGondolaHeader { get; set; }
        public string GondolaHeaderComment { get; set; }
        public bool IsHangingMobiles { get; set; }
        public string HangingMobilesComment { get; set; }
        public bool IsPollUpBanner { get; set; }
        public string PollUpBannerComment { get; set; }
        public bool IsParaciteUnits { get; set; }
        public string ParaciteUnitsComment { get; set; }
        public bool IsSensorSleaves { get; set; }
        public string SensorSleavesComment { get; set; }
        public bool IsNeckTags { get; set; }
        public string NeckTagsComment { get; set; }

        // Navigation Properties
        public Store Store { get; set; }
        public StorePOSType StorePOSType { get; set; }
    }
}
