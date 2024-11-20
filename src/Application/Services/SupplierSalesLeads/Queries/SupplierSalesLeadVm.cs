// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Queries;

public class SupplierSalesLeadVm : IMapFrom<SupplierSalesLead>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNumber { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSalesLead, SupplierSalesLeadVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSalesLeadId));
    }
}
