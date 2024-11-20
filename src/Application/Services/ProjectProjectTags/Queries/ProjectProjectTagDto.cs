namespace Engage.Application.Services.ProjectProjectTags.Queries;

public class ProjectProjectTagDto : IMapFrom<ProjectProjectTag>, IMapFrom<ProjectProjectTagClaim>, IMapFrom<ProjectProjectTagDCProduct>, IMapFrom<ProjectProjectTagEngageRegion>, IMapFrom<ProjectProjectTagEmployeeJobTitle>, IMapFrom<ProjectProjectTagStore>, IMapFrom<ProjectProjectTagStoreAsset>, IMapFrom<ProjectProjectTagSupplier>, IMapFrom<ProjectProjectTagUser>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Type { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectProjectTag, ProjectProjectTagDto>();

        profile.CreateMap<ProjectProjectTagClaim, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Claim.ClaimNumber + " " + s.Claim.Store.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Claim"));

        profile.CreateMap<ProjectProjectTagDCProduct, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.DCProduct.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "DC Product"));

        profile.CreateMap<ProjectProjectTagEmployeeJobTitle, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.EmployeeJobTitle.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Job Title"));

        profile.CreateMap<ProjectProjectTagEngageRegion, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.EngageRegion.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Region"));

        profile.CreateMap<ProjectProjectTagStore, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Store.Name + " - " + s.Store.EngageRegion.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Store"));

        profile.CreateMap<ProjectProjectTagStoreAsset, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.StoreAsset.StoreAssetType.Name + " - " + s.StoreAsset.SerialNumber + " : " + s.StoreAsset.Store.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Store Asset"));

        profile.CreateMap<ProjectProjectTagSupplier, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Supplier.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Supplier"));

        profile.CreateMap<ProjectProjectTagUser, ProjectProjectTagDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectProjectTagId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.User.FullName + " - " + s.User.Email))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "User"));
    }
}
