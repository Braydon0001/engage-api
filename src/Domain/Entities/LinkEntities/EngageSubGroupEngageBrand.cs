namespace Engage.Domain.Entities.LinkEntities
{
    public class EngageSubGroupEngageBrand
    {
        public int EngageSubGroupId { get; set; }
        public int EngageBrandId { get; set; }

        public EngageSubGroup EngageSubGroup { get; set; }
        public EngageBrand EngageBrand { get; set; }
    }
}
