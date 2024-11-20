namespace Engage.Domain.Entities
{
    public class TrainingProvider : BaseAuditableEntity
    {
        public int TrainingProviderId { get; set; }
        public string Name { get; set; }
    }
}
