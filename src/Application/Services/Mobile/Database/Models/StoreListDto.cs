namespace Engage.Application.Services.Mobile.Database.Models;



public class StoreListDto : IMapFrom<Store>
{
    public int Id { get; set; }
    public int StoreFormatId { get; set; }
    public string AccountNo { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
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
    public double? StorePerformancePercent { get; set; }
    public List<MobileStoreContactDto> Contacts { get; set; }
    public List<StoreConceptWithAttributesMobile> Concepts { get; set; }
    public List<OptionDto> DistributionCentres { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.StoreId))
            .ForMember(e => e.AccountNo, opt => opt.MapFrom(d => string.Join(",", d.DCAccounts.Select(e => e.AccountNumber))))
            .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.EngageRegion.Name))
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
            .ForMember(e => e.StoreFormatId, opt => opt.MapFrom(d => d.StoreFormatId))
            .ForMember(e => e.StorePerformancePercent, opt => opt.MapFrom(d => d.StoreStoreConceptPerformances));
    }
}

public class MobileStoreContactDto
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class StoreConceptLevelMobile
{
    public int StoreConceptId { get; set; }
    public string Name { get; set; }
    public int? Level { get; set; }
    public double? Score { get; set; }
    public int? Target { get; set; }
    public int? Actual { get; set; }
    public List<MobileStoreConceptAttributeValue> AttributeValues { get; set; }

}


public class MobileStoreConceptAttributeValue
{
    public string StoreConceptName { get; set; }

    public string Attribute { get; set; }
    public string Value { get; set; }
}

public class StoreConceptWithAttributesMobile : StoreConceptLevelMobile
{
    public List<StoreConceptAttributeMobile> Attributes { get; set; }
}

public class StoreStoreConceptPerformance
{
    public string StoreConceptName { get; set; }
    public int FormatTarget { get; set; }
    public int StoreSkuCount { get; set; }
    public int StoreSkuPercentDist { get; set; }
    public string KpiTier { get; set; }
}

public class StoreConceptAttributeMobile
{
    public int StoreConceptAttributeId { get; set; }
    public string Name { get; set; }
    public string Detail { get; set; }

}





