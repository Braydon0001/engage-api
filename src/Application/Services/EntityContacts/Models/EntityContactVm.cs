using Engage.Application.Services.CommunicationTypes.Queries;

namespace Engage.Application.Services.EntityContacts.Models;

public class EntityContactVm
{
    public int Id { get; set; }
    public OptionDto EntityContactTypeId { get; set; }
    public OptionDto UserId { get; set; }
    public string EmailAddress1 { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string MobilePhone { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> FileId { get; set; }
    public ICollection<CommunicationTypeOption> CommunicationTypeIds { get; set; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
}

public class EngageRegionContactVm : EntityContactVm, IMapFrom<EngageRegionContact>
{
    public OptionDto EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegionContact, EngageRegionContactVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeId, opt => opt.MapFrom(s => new OptionDto(s.EntityContactType.Id, s.EntityContactType.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.HasValue ? new OptionDto(s.UserId.Value, s.User.FirstName + " " + s.User.LastName) : null))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)));
    }
}

public class StoreContactVm : EntityContactVm, IMapFrom<StoreContact>
{
    public OptionDto StoreId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreContact, StoreContactVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeId, opt => opt.MapFrom(s => new OptionDto(s.EntityContactType.Id, s.EntityContactType.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.HasValue ? new OptionDto(s.UserId.Value, s.User.FirstName + " " + s.User.LastName) : null))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.FileId, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "id")))
            .ForMember(d => d.CommunicationTypeIds, opt => opt.Ignore())
            .ForMember(d => d.EngageRegionIds, opt => opt.Ignore());
    }
}

public class SupplierContactVm : EntityContactVm, IMapFrom<SupplierContact>
{
    public OptionDto SupplierId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContact, SupplierContactVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EntityContactId))
            .ForMember(d => d.EntityContactTypeId, opt => opt.MapFrom(s => new OptionDto(s.EntityContactType.Id, s.EntityContactType.Name)))
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.HasValue ? new OptionDto(s.UserId.Value, s.User.FirstName + " " + s.User.LastName) : null))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
            .ForMember(d => d.CommunicationTypeIds, opt => opt.Ignore())
            .ForMember(d => d.EngageRegionIds, opt => opt.Ignore());
    }
}
