// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Queries;

public class SupplierSalesLeadOption : IMapFrom<SupplierSalesLead>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSalesLead, SupplierSalesLeadOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSalesLeadId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FirstName));
    }
}