namespace Engage.Application.Services.DistributionCenters.Models
{
    public class DistributionCenterVm : IMapFrom<DistributionCenter>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DistributionCenter, DistributionCenterVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DistributionCenterId));
        }
    }
}