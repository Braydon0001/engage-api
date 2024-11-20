namespace Engage.Domain.Entities
{
    public class EmployeeStoreKpiScore : BaseAuditableEntity
    {
        public int EmployeeStoreKpiScoreId { get; set; }
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public int EmployeeKpiId { get; set; }
        public int? EmployeeKpiTierId { get; set; }
        public float Score { get; set; }

        public Employee Employee { get; set; }
        public Store Store { get; set; }
        public EmployeeKpi EmployeeKpi { get; set; }
        public EmployeeKpiTier EmployeeKpiTier { get; set; }
    }
}
