namespace Engage.Domain.Entities
{
    public class EmployeeKpi : BaseAuditableEntity
    {
        public EmployeeKpi()
        {
            EmployeeKpis = new HashSet<EmployeeEmployeeKpi>();
            EmployeeStoreKpis = new HashSet<EmployeeStoreKpi>();
            EmployeeKpiScores = new HashSet<EmployeeKpiScore>();
            EmployeeStoreKpiScores = new HashSet<EmployeeStoreKpiScore>();
        }
        public int EmployeeKpiId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EmployeeKpiTypeId { get; set; }

        public ICollection<EmployeeEmployeeKpi> EmployeeKpis { get; private set; }
        public ICollection<EmployeeStoreKpi> EmployeeStoreKpis { get; private set; }
        public ICollection<EmployeeKpiScore> EmployeeKpiScores { get; private set; }
        public ICollection<EmployeeStoreKpiScore> EmployeeStoreKpiScores { get; private set; }
        public EmployeeKpiType EmployeeKpiType { get; set; }
    }
}
