namespace Engage.Application.Services.EntityContacts.Models;

public class EntityEmailOption
{
    public int? Id { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
}

public class StoreContactEmailOption : EntityEmailOption, IMapFrom<StoreContact>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreContact, StoreContactEmailOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.Label, opt => opt.MapFrom(s => $"{s.EmailAddress1} - {s.EntityContactType.Name}"))
            .ForMember(d => d.Value, opt => opt.MapFrom(s => s.EmailAddress1));
    }
}

public class EntityOption
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class StoreContactOption : EntityOption, IMapFrom<StoreContact>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreContact, StoreContactOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.FullName} - {s.EntityContactType.Name}"));
    }
}

public class SupplierContactOption : EntityOption, IMapFrom<SupplierContact>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContact, SupplierContactOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.FullName} - {s.EntityContactType.Name}"));
    }
}