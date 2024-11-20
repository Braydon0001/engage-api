namespace Engage.Domain.Common
{
    public class BaseMasterEntity : BaseAuditableEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
