namespace Engage.Application.Services.Employees.Commands;

public class EmployeeCommand : IMapTo<Employee>
{
    public int? ManagerId { get; set; }
    public int? LeaveManagerId { get; set; }
    public int EmployeeStateId { get; set; }
    public int? EmployeeIncentiveTierId { get; set; }
    public string Code { get; set; }
    public int? EmployeeTitleId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string MaidenName { get; set; }
    public string Initials { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string PassportNumber { get; set; }
    public int? EmployeeNationalityId { get; set; }
    public int EmployeeCitzenshipId { get; set; }
    public int? EmployeeRaceId { get; set; }
    public int? EmployeeGenderId { get; set; }
    public int? EmploymentActionId { get; set; }
    public int? EmployeeReinstatementReasonId { get; set; }
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
    public DateTime? ReinstatementDate { get; set; }
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
    public bool IsEncashLeave { get; set; }
    public bool IsRetired { get; set; }
    public bool IsForeignNational { get; set; }
    public int EmployeeStandardIndustryGroupCodeId { get; set; } = 1;
    public int EmployeeStandardIndustryCodeId { get; set; } = 1;
    public int? EmployeeDisabledTypeId { get; set; }
    public int? EmployeeLanguageId { get; set; }
    public int? EmployeeTerminationReasonId { get; set; }
    public int? PayrollPeriodId { get; set; }
    public int? EngageSubRegionId { get; set; }
    public string Note { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
    public List<int> CostCenterIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public List<int> EmployeeHealthConditionIds { get; set; }
    public List<int> EmployeeJobTitleIds { get; set; }
    public List<int> EmployeeDivisionIds { get; set; }
    public bool? SkipUserCreation { get; set; } = false;
    public int? EmployeeTypeId { get; set; }
    public int? UserId { get; set; }
    public int? CostCenterManagerId { get; set; }
    public int? EmployeeJobTitleTimeId { get; set; }
    public int? EmployeeJobTitleTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeCommand, Employee>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName.ToUpper()))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName.ToUpper()))
            .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName.ToUpper()))
            .ForMember(d => d.KnownAs, opt => opt.MapFrom(s => s.KnownAs.ToUpper()))
            .ForMember(d => d.Initials, opt => opt.MapFrom(s => s.Initials.ToUpper()))
            .ForMember(d => d.MaidenName, opt => opt.MapFrom(s => s.MaidenName.ToUpper()))
            .ForMember(d => d.NextOfKinName, opt => opt.MapFrom(s => s.NextOfKinName.ToUpper()))
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(
                       src => src.ManagerId.HasValue && src.ManagerId == 0 ? null : src.ManagerId))
            .ForMember(dest => dest.LeaveManagerId, opt => opt.MapFrom(
                       src => src.LeaveManagerId.HasValue && src.LeaveManagerId == 0 ? null : src.LeaveManagerId))
            .ForMember(dest => dest.EmployeeIncentiveTierId, opt => opt.MapFrom(
                       src => src.EmployeeIncentiveTierId.HasValue && src.EmployeeIncentiveTierId == 0 ? null : src.EmployeeIncentiveTierId))
            .ForMember(dest => dest.EmployeeDisabledTypeId, opt => opt.MapFrom(
                       src => src.EmployeeDisabledTypeId.HasValue && src.EmployeeDisabledTypeId == 0 ? null : src.EmployeeDisabledTypeId))
            .ForMember(dest => dest.EmployeeLanguageId, opt => opt.MapFrom(
                       src => src.EmployeeLanguageId.HasValue && src.EmployeeLanguageId == 0 ? null : src.EmployeeLanguageId))
            .ForMember(dest => dest.EmployeeTerminationReasonId, opt => opt.MapFrom(
                       src => src.EmployeeTerminationReasonId.HasValue && src.EmployeeTerminationReasonId == 0 ? null : src.EmployeeTerminationReasonId))
            .ForMember(dest => dest.EmployeeTitleId, opt => opt.MapFrom(
                       src => src.EmployeeTitleId.HasValue && src.EmployeeTitleId == 0 ? null : src.EmployeeTitleId))
            .ForMember(dest => dest.EmployeeNationalityId, opt => opt.MapFrom(
                       src => src.EmployeeNationalityId.HasValue && src.EmployeeNationalityId == 0 ? null : src.EmployeeNationalityId))
            .ForMember(dest => dest.EmployeeRaceId, opt => opt.MapFrom(
                       src => src.EmployeeRaceId.HasValue && src.EmployeeRaceId == 0 ? null : src.EmployeeRaceId))
            .ForMember(dest => dest.EmployeeGenderId, opt => opt.MapFrom(
                       src => src.EmployeeGenderId.HasValue && src.EmployeeGenderId == 0 ? null : src.EmployeeGenderId))
            .ForMember(dest => dest.EmploymentActionId, opt => opt.MapFrom(
                       src => src.EmploymentActionId.HasValue && src.EmploymentActionId == 0 ? null : src.EmploymentActionId))
            .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(
                       src => src.MaritalStatusId.HasValue && src.MaritalStatusId == 0 ? null : src.MaritalStatusId))
            .ForMember(dest => dest.UniformSizeId, opt => opt.MapFrom(
                       src => src.UniformSizeId.HasValue && src.UniformSizeId == 0 ? null : src.UniformSizeId))
            .ForMember(dest => dest.PayrollPeriodId, opt => opt.MapFrom(
                       src => src.PayrollPeriodId.HasValue && src.PayrollPeriodId == 0 ? null : src.PayrollPeriodId))
            .ForMember(dest => dest.NextOfKinTypeId, opt => opt.MapFrom(
                       src => src.NextOfKinTypeId.HasValue && src.NextOfKinTypeId == 0 ? null : src.NextOfKinTypeId));
    }
}
