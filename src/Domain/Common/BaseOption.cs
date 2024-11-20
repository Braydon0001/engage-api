namespace Engage.Domain.Common
{
    public class BaseOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public int? Order { get; set; }
    }
}