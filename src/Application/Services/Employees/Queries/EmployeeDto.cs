namespace Engage.Application.Services.Employees.Queries;

public class EmployeeDto : IMapFrom<Employee>
{
    public int Id { get; set; }
    public int StakeholderId { get; set; }
    public int ManagerId { get; set; }
    public int LeaveManagerId { get; set; }
    public int? CostCenterManagerId { get; set; }
    public int EmployeeStateId { get; set; }
    public int EmployeeIncentiveTierId { get; set; }
    public string Code { get; set; }
    public int? EmployeeTitleId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string Initials { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string PassportNumber { get; set; }
    public int? EmployeeNationalityId { get; set; }
    public int EmployeeCitzenshipId { get; set; }
    public int? EmployeeRaceId { get; set; }
    public int? EmployeeGenderId { get; set; }
    public int? MaritalStatusId { get; set; }
    public string PAYENumber { get; set; }
    public string SARSNumber { get; set; }
    public string MedicalAidNumber { get; set; }
    public string UIFNumber { get; set; }
    public string RANumber { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? LeaveCycleStartDate { get; set; }
    public float? LeaveAccumulationRate { get; set; }
    public int? AnnualLeave { get; set; }
    public int? SickLeave { get; set; }
    public int? FamilyLeave { get; set; }
    public int? UniformSizeId { get; set; }
    public DateTime? EndDate { get; set; }
    public string EmailAddress1 { get; set; }
    public string EmailAddress2 { get; set; }
    public string PhoneNumber { get; set; }
    public string HomeNumber { get; set; }
    public string WorkNumber { get; set; }
    public string WorkExtension { get; set; }
    public int? NextOfKinTypeId { get; set; }
    public string NextOfKinName { get; set; }
    public string NextOfKinContactNumber { get; set; }
    public string NextOfKinAddess { get; set; }
    public bool IsCovidVaccinated { get; set; }
    public bool IsRetired { get; set; }
    public int EmployeeDisabledTypeId { get; set; }
    public int EmployeeLanguageId { get; set; }
    public string Note { get; set; }
    public string BlobUrl { get; set; }
    public string BlobName { get; set; }
    public ICollection<OptionDto> EngageDepartments { get; set; }
    public ICollection<OptionDto> EngageRegions { get; set; }
    public ICollection<OptionDto> EmployeeJobTitles { get; set; }
    public int? EmployeeTerminationReasonId { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<string, string>()
            .ConvertUsing(str => str ?? string.Empty);

        profile.CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.EngageDepartments,
                opt => opt.MapFrom(s => s.EmployeeDepartments
                    .Select(s => s.EngageDepartment)
                    .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
            .ForMember(d => d.EngageRegions,
                opt => opt.MapFrom(s => s.EmployeeRegions
                    .Select(s => s.EngageRegion)
                    .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
            .ForMember(d => d.EmployeeJobTitles,
                opt => opt.MapFrom(s => s.EmployeeJobTitles
                    .Select(s => s.EmployeeJobTitle)
                    .Select(s => new OptionDto() { Id = s.EmployeeJobTitleId, Name = s.Name })));
    }
}
