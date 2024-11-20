namespace Engage.Application.Services.EmployeeReports.Models;
public class EmployeeAddressDetailsDto : IMapFrom<EmployeeAddress>
{
    public string EmployeeNumber { get; set; }                     //A
    public string AddressType { get; set; }                        //B
    public string IsPostalSameAsPhysical { get; set; }             //C
    public string UnitNumber { get; set; }                         //D
    public string ComplexName { get; set; }                        //E
    public string StreetNumber { get; set; }                       //F
    public string StreetNamePostalAddressNumber { get; set; }      //G
    public string SuburbOrDistrictPostalAgency { get; set; }       //H
    public string CityOrTownPostalCity { get; set; }               //I
    public string Code { get; set; }                               //J
    public string Country { get; set; }                            //K
    public string Province { get; set; }                           //L
    public string SpecialServices { get; set; }                    //M
    public string IsThePostalAddressAcareOfAddress { get; set; }   //N
    public string CareOfIntermediary { get; set; }                 //O

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAddress, EmployeeAddressDetailsDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.AddressType, opt => opt.MapFrom(s => "Physical"))
                .ForMember(d => d.IsPostalSameAsPhysical, opt => opt.MapFrom(s => s.IsSameAsPhysicalAddress ? "True" : "False"))
                .ForMember(d => d.UnitNumber, opt => opt.MapFrom(s => s.UnitNumber))
                .ForMember(d => d.ComplexName, opt => opt.MapFrom(s => s.ComplexName))
                .ForMember(d => d.StreetNumber, opt => opt.MapFrom(s => s.StreetNumber))
                .ForMember(d => d.StreetNamePostalAddressNumber, opt => opt.MapFrom(s => s.StreetName))
                .ForMember(d => d.SuburbOrDistrictPostalAgency, opt => opt.MapFrom(s => s.Suburb))
                .ForMember(d => d.CityOrTownPostalCity, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country.Name))
                .ForMember(d => d.Province, opt => opt.MapFrom(s => s.Province.Name))
                .ForMember(d => d.SpecialServices, opt => opt.MapFrom(s => ""))
                .ForMember(d => d.IsThePostalAddressAcareOfAddress, opt => opt.MapFrom(s => s.IsPostalAddressCareOfAddress ? "True" : "-"))
                .ForMember(d => d.CareOfIntermediary, opt => opt.MapFrom(s => s.CareOfIntermediary));
    }
}
