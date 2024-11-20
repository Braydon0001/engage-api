namespace Engage.Application.Services.EntityContacts.Models;

public class EntityContactOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
}

public class SupplierOption : EntityContactOption, IMapFrom<SupplierContact>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContact, SupplierOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FullName));
    }
}