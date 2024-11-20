namespace Engage.Domain.Entities.Views
{
    public class StatsByEngageRegionView
    {
        public int EngageRegionId { get; set; }
        public string EngageRegionName { get; set; }
        public int StoresCount { get; set; }
        public int OverdueUnsubmittedOrdersCount { get; set; }
        public int UnsubmittedOrdersCount { get; set; }
        public int SubmittedOrdersCount { get; set; }    
    }
}
