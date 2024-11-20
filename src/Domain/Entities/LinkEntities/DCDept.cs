namespace Engage.Domain.Entities.LinkEntities
{
    public class DCDept
    {
        public int DistributionCenterId { get; set; }
        public int DCDepartmentId { get; set; }

        // Navigation Properties
        public DistributionCenter DistributionCenter { get; set; }
        public DCDepartment DCDepartment { get; set; }
    }
}
