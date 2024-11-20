namespace Engage.Application.Services.EmployeeBankDetails.Models
{
    public class EmployeeBankDetailDto : IMapFrom<EmployeeBankDetail>
    {
        public int Id { get; set; }
        public int BankAccountOwnerId { get; set; }
        public string BankAccountOwnerName { get; set; }
        public int BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; }
        public int BankPaymentMethodId { get; set; }
        public string BankPaymentMethodName { get; set; }
        public int BankNameId { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string BeneficiaryReference { get; set; }
        public string SwiftCode { get; set; }
        public string RoutingCode { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }
        public bool Disabled { get; set; }
        public List<JsonFile> Files { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeBankDetail, EmployeeBankDetailDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmployeeBankDetailId))
                .ForMember(e => e.BankName, opt => opt.MapFrom(d => d.BankName.Name));

        }
    }
}
