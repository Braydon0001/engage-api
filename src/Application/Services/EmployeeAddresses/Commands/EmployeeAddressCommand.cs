namespace Engage.Application.Services.EmployeeAddresses.Commands
{
    public class EmployeeAddressCommand : IMapTo<EmployeeAddress>
    {
        public string UnitNumber { get; set; }
        public string ComplexName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public bool IsSameAsPhysicalAddress { get; set; }
        public string CareOfIntermediary { get; set; }
        public bool IsPostalAddressCareOfAddress { get; set; }
        public string PostalUnitNumber { get; set; }
        public string PostalComplexName { get; set; }
        public string PostalStreetNumber { get; set; }
        public string PostalStreetName { get; set; }
        public string PostalSuburb { get; set; }
        public string PostalCity { get; set; }
        public string PostalCode { get; set; }
        public int? PostalCountryId { get; set; }
        public int? PostalProvinceId { get; set; }
        public int EmployeeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeAddressCommand, EmployeeAddress>();
        }
    }
}
