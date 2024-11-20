namespace Engage.Domain.Entities
{
    public class Vacancy : BaseAuditableEntity
    {
        public int VacancyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
