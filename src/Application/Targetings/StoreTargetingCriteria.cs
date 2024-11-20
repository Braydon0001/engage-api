namespace Engage.Application.Targetings
{
    public class StoreTargetingCriteria
    {
        public List<OptionDto> EngageRegions { get; set; }
        public List<OptionDto> StoreClusters { get; set; }
        public List<OptionDto> StoreFormats { get; set; }
        public List<OptionDto> StoreLSMs { get; set; }
        public List<OptionDto> StoreTypes { get; set; }
    }
}
