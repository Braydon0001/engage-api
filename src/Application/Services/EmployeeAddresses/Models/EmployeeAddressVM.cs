namespace Engage.Application.Services.EmployeeAddresses.Models;

public class EmployeeAddressVm : IMapFrom<EmployeeAddress>
{
    public int Id { get; set; }
    public string UnitNumber { get; set; }
    public string ComplexName { get; set; }
    public string StreetNumber { get; set; }
    public string StreetName { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Code { get; set; }
    public OptionDto CountryId { get; set; }
    public OptionDto ProvinceId { get; set; }
    public bool IsSameAsPhysicalAddress { get; set; }
    public bool IsPostalAddressCareOfAddress { get; set; }
    public string CareOfIntermediary { get; set; }
    public string PostalUnitNumber { get; set; }
    public string PostalComplexName { get; set; }
    public string PostalStreetNumber { get; set; }
    public string PostalStreetName { get; set; }
    public string PostalSuburb { get; set; }
    public string PostalCity { get; set; }
    public string PostalCode { get; set; }
    public OptionDto PostalCountryId { get; set; }
    public OptionDto PostalProvinceId { get; set; }
    public int EmployeeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAddress, EmployeeAddressVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeAddressId))
            .ForMember(d => d.ProvinceId, opt => opt.MapFrom(s => s.ProvinceId.HasValue
                                                                 ? new OptionDto(s.ProvinceId.Value, s.Province.Name)
                                                                 : null))
            .ForMember(d => d.CountryId, opt => opt.MapFrom(s => s.CountryId.HasValue
                                                                 ? new OptionDto(s.CountryId.Value, s.Country.Name)
                                                                 : null))
            .ForMember(d => d.PostalProvinceId, opt => opt.MapFrom(s => s.PostalProvinceId.HasValue
                                                                 ? new OptionDto(s.PostalProvinceId.Value, s.PostalProvince.Name)
                                                                 : null))
            .ForMember(d => d.PostalCountryId, opt => opt.MapFrom(s => s.PostalCountryId.HasValue
                                                                 ? new OptionDto(s.PostalCountryId.Value, s.PostalCountry.Name)
                                                                 : null));
    }
}    
