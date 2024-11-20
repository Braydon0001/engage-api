namespace Engage.Application.Services.Mobile.Stores.Models;

public class StoreListDtoDeprecate : IMapFrom<Store>
{
    public int Id { get; set; }
    public string AccountNo { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string StoreImageUrl { get; set; }
    public string AddressLineOne { get; set; }
    public string AddressLineTwo { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostCode { get; set; }
    public float? Lat { get; set; }
    public float? Long { get; set; }
    public double? Distance { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreListDtoDeprecate>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.StoreId))
            .ForMember(e => e.AccountNo, opt => opt.MapFrom(d => string.Join(",", d.DCAccounts.Select(e => e.AccountNumber))))
            .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.EngageRegion.Name))
            .ForMember(e => e.StoreImageUrl, opt => opt.MapFrom(d => d.StoreImageUrl))
            .ForMember(e => e.AddressLineOne, opt => opt.MapFrom(d => d.PrimaryLocation.AddressLineOne))
            .ForMember(e => e.AddressLineTwo, opt => opt.MapFrom(d => d.PrimaryLocation.AddressLineTwo))
            .ForMember(e => e.Suburb, opt => opt.MapFrom(d => d.PrimaryLocation.Suburb))
            .ForMember(e => e.City, opt => opt.MapFrom(d => d.PrimaryLocation.City))
            .ForMember(e => e.Province, opt => opt.MapFrom(d => d.PrimaryLocation.Province))
            .ForMember(e => e.PostCode, opt => opt.MapFrom(d => d.PrimaryLocation.PostCode))
            .ForMember(e => e.Lat, opt => opt.MapFrom(d => d.PrimaryLocation.Lat))
            .ForMember(e => e.Long, opt => opt.MapFrom(d => d.PrimaryLocation.Long))
            .ForMember(e => e.Email, opt => opt.MapFrom(d => d.PrimaryContact.PrimaryEmailContactItem.Value))
            .ForMember(e => e.Mobile, opt => opt.MapFrom(d => d.PrimaryContact.PrimaryMobileContactItem.Value));
    }
}
