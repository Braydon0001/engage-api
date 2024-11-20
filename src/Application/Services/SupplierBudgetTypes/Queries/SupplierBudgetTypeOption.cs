// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Queries;

public class SupplierBudgetTypeOption : IMapFrom<SupplierBudgetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetType, SupplierBudgetTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetTypeId));
    }
}