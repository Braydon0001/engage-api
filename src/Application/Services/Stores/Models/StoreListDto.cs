namespace Engage.Application.Services.Stores.Models;

public class StoreListDto : IMapFrom<Store>
{
    public int Id { get; set; }
    public int? ParentStoreId { get; set; }
    public string AccountNo { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string EngageRegionName { get; set; }
    public string StoreClusterName { get; set; }
    public string StoreFormatName { get; set; }
    public string StoreGroupName { get; set; }
    public string StoreLSMName { get; set; }
    public string StoreMediaGroupName { get; set; }
    public string StoreSparRegionName { get; set; }
    public string StoreTypeName { get; set; }
    public string StoreImageUrl { get; set; }
    public string StoreTypeImageUrl { get; set; }
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
    public DateTime Created { get; set; }
    public bool Disabled { get; set; }
    public string VatNumber { get; set; }
    public bool IsHalaal { get; set; }
    public bool IsNotServiced { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.StoreId))
            .ForMember(e => e.AccountNo, opt => opt.MapFrom(d => string.Join(",", d.DCAccounts.Select(e => e.AccountNumber))))
            .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.EngageRegion.Name))
            .ForMember(e => e.StoreClusterName, opt => opt.MapFrom(d => d.StoreCluster.Name))
            .ForMember(e => e.StoreFormatName, opt => opt.MapFrom(d => d.StoreFormat.Name))
            .ForMember(e => e.StoreGroupName, opt => opt.MapFrom(d => d.StoreGroup.Name))
            .ForMember(e => e.StoreLSMName, opt => opt.MapFrom(d => d.StoreLSM.Name))
            .ForMember(e => e.StoreMediaGroupName, opt => opt.MapFrom(d => d.StoreMediaGroup.Name))
            .ForMember(e => e.StoreSparRegionName, opt => opt.MapFrom(d => d.StoreSparRegion.Name))
            .ForMember(e => e.StoreTypeName, opt => opt.MapFrom(d => d.StoreType.Name))
            .ForMember(e => e.StoreTypeImageUrl, opt => opt.MapFrom(d => d.StoreType.ImageUrl))
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
