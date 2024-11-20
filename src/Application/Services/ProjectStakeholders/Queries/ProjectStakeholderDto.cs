namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderDto : IMapFrom<ProjectStakeholder>, IMapFrom<ProjectStakeholderUser>, IMapFrom<ProjectStakeholderStoreContact>, IMapFrom<ProjectStakeholderSupplierContact>, IMapFrom<ProjectStakeholderEmployeeRegionContact>, IMapFrom<ProjectStakeholderExternalUser>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public int StakeholderType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStakeholderUser, ProjectStakeholderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStakeholderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.User.FullName))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "User"))
               .ForMember(d => d.StakeholderType, opt => opt.MapFrom(s => (int)ProjectStakeholderType.User));

        profile.CreateMap<ProjectStakeholderStoreContact, ProjectStakeholderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStakeholderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.StoreContact.FullName))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.StoreContact.EmailAddress1))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Store Contact"))
               .ForMember(d => d.StakeholderType, opt => opt.MapFrom(s => (int)ProjectStakeholderType.StoreContact));

        profile.CreateMap<ProjectStakeholderSupplierContact, ProjectStakeholderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStakeholderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.SupplierContact.FullName))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.SupplierContact.EmailAddress1))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Supplier Contact"))
               .ForMember(d => d.StakeholderType, opt => opt.MapFrom(s => (int)ProjectStakeholderType.SupplierContact));

        profile.CreateMap<ProjectStakeholderEmployeeRegionContact, ProjectStakeholderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStakeholderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.EmployeeRegionContact.Employee.FirstName + " " + s.EmployeeRegionContact.Employee.LastName))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.EmployeeRegionContact.Employee.EmailAddress1))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "Region Contact"))
               .ForMember(d => d.StakeholderType, opt => opt.MapFrom(s => (int)ProjectStakeholderType.EngageRegionContact));

        profile.CreateMap<ProjectStakeholderExternalUser, ProjectStakeholderDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStakeholderId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.ProjectExternalUser.FirstName} {s.ProjectExternalUser.LastName}"))
               .ForMember(d => d.Email, opt => opt.MapFrom(s => s.ProjectExternalUser.Email))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => "External User"))
               .ForMember(d => d.StakeholderType, opt => opt.MapFrom(s => (int)ProjectStakeholderType.ExternalUser));
    }
}
