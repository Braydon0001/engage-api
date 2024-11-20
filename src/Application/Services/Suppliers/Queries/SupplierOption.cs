namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierOption : IMapFrom<Supplier>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, SupplierOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId));
    }
}
