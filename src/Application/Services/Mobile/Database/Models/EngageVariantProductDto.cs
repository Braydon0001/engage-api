namespace Engage.Application.Services.Mobile.Database.Models
{
    public class EngageVariantProductDto : IMapFrom<EngageVariantProduct>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitTypeId { get; set; }
        public float Size { get; set; }
        public float PackSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EngageVariantProduct, EngageVariantProductDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EngageVariantProductId));
        }
    }
}
