namespace Engage.Application.Services.DCAccounts.Commands
{
    public class DCAccountCommand : IMapTo<DCAccount>
    {
        public int StoreId { get; set; }
        public int DistributionCenterId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<DCAccountCommand, DCAccount>();
        }
    }
}
