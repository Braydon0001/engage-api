// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionOption : IMapFrom<SupplierBudgetVersion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersion, SupplierBudgetVersionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetVersionId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SupplierPeriod.Name + " - " + s.SupplierBudgetVersionType.Name));
    }
}