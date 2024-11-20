// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

public class SupplierBudgetVersionTypeOption : IMapFrom<SupplierBudgetVersionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionType, SupplierBudgetVersionTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetVersionTypeId));
    }
}