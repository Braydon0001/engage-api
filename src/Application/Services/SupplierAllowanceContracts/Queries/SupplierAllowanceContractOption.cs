namespace Engage.Application.Services.SupplierAllowanceContracts.Queries;

public class SupplierAllowanceContractOption : IMapFrom<SupplierAllowanceContract>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceContract, SupplierAllowanceContractOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierAllowanceContractId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}