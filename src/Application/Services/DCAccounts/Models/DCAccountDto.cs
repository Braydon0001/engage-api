namespace Engage.Application.Services.DCAccounts.Models
{
    public class DCAccountDto : IMapFrom<DCAccount>
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int DistributionCenterId { get; set; }
        public string DistributionCenterName { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public bool IsPrimary { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DCAccount, DCAccountDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.DCAccountId))
                .ForMember(e => e.DistributionCenterName, opt => opt.MapFrom(d => d.DistributionCenter.Name));
        }
    }
}
