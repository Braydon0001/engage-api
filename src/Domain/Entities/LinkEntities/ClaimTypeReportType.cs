namespace Engage.Domain.Entities.LinkEntities
{
    public class ClaimTypeReportType
    {
        public int ClaimTypeId { get; set; }
        public int ClaimReportTypeId { get; set; }

        // Navigation Properties
        public ClaimType ClaimType { get; set; }      
        public ClaimReportType ClaimReportType { get; set; }
    }
}
