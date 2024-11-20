
using Engage.Application.Services.CreditorStatuses.Queries;

namespace Engage.Application.Services.Creditors.Queries;

public class CreditorVm : IMapFrom<Creditor>
{
    public int Id { get; init; }
    public CreditorStatusOption CreditorStatusId { get; init; }
    public string Name { get; init; }
    public string TradingName { get; init; }
    public bool IsVatRegistered { get; init; }
    public string VatNumber { get; set; }
    public DateTime BankConfirmationDate { get; set; }
    public List<JsonFile> FilesVendorApplication { get; set; }
    public List<JsonFile> FilesCipcDocument { get; set; }
    public List<JsonFile> FilesVatDocument { get; set; }
    public List<JsonFile> FilesBankConfirmation { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Creditor, CreditorVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorId))
               .ForMember(d => d.CreditorStatusId, opt => opt.MapFrom(s => s.CreditorStatus))
               .ForMember(d => d.FilesVendorApplication, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "vendorapplication")))
               .ForMember(d => d.FilesCipcDocument, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "cipc")))
               .ForMember(d => d.FilesVatDocument, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "vat")))
               .ForMember(d => d.FilesBankConfirmation, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "bankconfirmation")));
    }
}
