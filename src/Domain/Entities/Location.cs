using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class Location : BaseAuditableEntity
    {
        public int LocationId { get; set; }
        public int StakeholderId { get; set; }
        public int LocationTypeId { get; set; }
        public int EngageLocationId { get; set; }
        public int EngageRegionId { get; set; }
        public string BusinessUnit { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public float? Lat { get; set; }
        public float? Long { get; set; }
        public bool IsPrimaryAddress { get; set; }
        public string Note { get; set; }

        // Navigation Properties
        public Stakeholder Stakeholder { get; set; }
        public LocationType LocationType { get; set; }
        public EngageRegion EngageRegion { get; set; }
        public EngageLocation EngageLocation { get; set; }
    }
}
