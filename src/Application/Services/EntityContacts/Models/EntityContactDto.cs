namespace Engage.Application.Services.EntityContacts.Models;

public class EntityContactDto
{
    public int Id { get; set; }
    public int EntityContactTypeId { get; set; }
    public string EntityContactTypeName { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; }
    public string EmailAddress1 { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string FullName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
}

public class EngageRegionContactDto : EntityContactDto, IMapFrom<EngageRegionContact>
{

    public string EngageRegionName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegionContact, EngageRegionContactDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeName, opt => opt.MapFrom(s => s.EntityContactType.Name))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name));
    }
}

public class StoreContactDto : EntityContactDto, IMapFrom<StoreContact>
{

    public string StoreName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreContact, StoreContactDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeName, opt => opt.MapFrom(s => s.EntityContactType.Name))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name));
    }
}

public class SupplierContactDto : EntityContactDto, IMapFrom<SupplierContact>
{
    public string SupplierName { get; set; }
    public string EngageRegionNames { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContact, SupplierContactDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeName, opt => opt.MapFrom(s => s.EntityContactType.Name))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.EngageRegionNames, opt => opt.MapFrom(s =>
            string.Join(',', s.EntityContactRegions.Select(e => e.EngageRegion.Name).ToList())));
    }
}
