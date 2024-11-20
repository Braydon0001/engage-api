// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

public class SupplierBudgetVersionTypeDto : IMapFrom<SupplierBudgetVersionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersionType, SupplierBudgetVersionTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetVersionTypeId));
    }
}
