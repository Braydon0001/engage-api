namespace Engage.Application.Targetings
{
    public class StoreTargetingCommand
    {
        public List<int> EngageRegions { get; set; }
        public List<int> StoreClusters { get; set; }
        public List<int> StoreFormats { get; set; }
        public List<int> StoreLSMs { get; set; }
        public List<int> StoreTypes { get; set; }
    }
}
