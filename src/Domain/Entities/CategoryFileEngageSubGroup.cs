namespace Engage.Domain.Entities
{
    public class CategoryFileEngageSubGroup : CategoryFileTarget
    {
        public int EngageSubGroupId { get; set; }
        public EngageSubGroup EngageSubGroup { get; set; }
    }
}
