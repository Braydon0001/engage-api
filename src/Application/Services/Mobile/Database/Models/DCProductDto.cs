namespace Engage.Application.Services.Mobile.Database.Models
{
    public class DCProductDto : IMapFrom<DCProduct>
    {
        public int DCProductId { get; set; }
        public int? EngageVariantProductId { get; set; }
        public int DistributionCenterId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public int UnitTypeId { get; set; }
        public float Size { get; set; }
        public float PackSize { get; set; }
        public string SubWarehouse { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DCProduct, DCProductDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Code + " / " + s.Name + " / " + s.Size + " " + s.UnitType.Name));
        }
    }
}
