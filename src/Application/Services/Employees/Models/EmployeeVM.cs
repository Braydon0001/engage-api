using Engage.Application.Services.CostCenters.Queries;
using Engage.Application.Services.EmployeeEmployeeBadges.Models;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.EmployeeJobTitleTimes.Queries;
using Engage.Application.Services.EmployeeJobTitleTypes.Queries;
using Engage.Application.Services.EmployeePopiConsents.Models;
using Engage.Application.Services.EngageSubRegions.Queries;

namespace Engage.Application.Services.Employees.Models;


public class EmployeeVm : IMapFrom<Employee>
{
    public int Id { get; set; }
    public OptionDto EmployeeStateId { get; set; }
    public OptionDto ManagerId { get; set; }
    public OptionDto LeaveManagerId { get; set; }
    public OptionDto CostCenterManagerId { get; set; }
    public OptionDto EmployeeIncentiveTierId { get; set; }
    public OptionDto EmployeeCitzenshipId { get; set; }
    public OptionDto EmployeeLanguageId { get; set; }
    public OptionDto EmployeeTitleId { get; set; }
    public OptionDto MaritalStatusId { get; set; }
    public OptionDto EmployeeNationalityId { get; set; }
    public OptionDto CitizenshipId { get; set; }
    public OptionDto EmployeeRaceId { get; set; }
    public OptionDto EmployeeGenderId { get; set; }
    public OptionDto UniformSizeId { get; set; }
    public OptionDto NextOfKinTypeId { get; set; }
    public OptionDto EmployeeDisabledTypeId { get; set; }
    public OptionDto EmployeeIdentificationTypeId { get; set; }
    public OptionDto EmployeePassportNationalityId { get; set; }
    public OptionDto EmployeeSDLExemptionId { get; set; }
    public OptionDto EmployeePersonTypeId { get; set; }
    public OptionDto EmployeeTaxStatusId { get; set; }
    public OptionDto EmployeeUIFExemptionId { get; set; }
    public OptionDto EmployeeStandardIndustryGroupCodeId { get; set; }
    public OptionDto EmployeeStandardIndustryCodeId { get; set; }
    public OptionDto EmployeeTerminationReasonId { get; set; }
    public EngageSubRegionOption EngageSubRegionId { get; set; }
    public EmployeeJobTitleTimeOption EmployeeJobTitleTimeId { get; set; }
    public EmployeeJobTitleTypeOption EmployeeJobTitleTypeId { get; set; }
    public string Code { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string Initials { get; set; }
    public string MaidenName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string PassportNumber { get; set; }
    public DateTime? PassportStartDate { get; set; }
    public DateTime? PassportEndDate { get; set; }
    public string PAYENumber { get; set; }
    public string SARSNumber { get; set; }
    public string UIFNumber { get; set; }
    public string RANumber { get; set; }
    public string MedicalAidNumber { get; set; }
    public DateTime GroupStartDate { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? LeaveCycleStartDate { get; set; }
    public float? LeaveAccumulationRate { get; set; }
    public int? AnnualLeave { get; set; }
    public int? SickLeave { get; set; }
    public int? FamilyLeave { get; set; }
    public DateTime? EndDate { get; set; }
    public string EmailAddress1 { get; set; }
    public string EmailAddress2 { get; set; }
    public string PhoneNumber { get; set; }
    public string HomeNumber { get; set; }
    public string WorkNumber { get; set; }
    public string WorkExtension { get; set; }
    public string NextOfKinName { get; set; }
    public string NextOfKinContactNumber { get; set; }
    public string NextOfKinAddess { get; set; }
    public string MobileAppVersion { get; set; }
    public bool IsCovidVaccinated { get; set; }
    public string Note { get; set; }
    public bool IsDefaultPayslip { get; set; }
    public bool IsRetired { get; set; }
    public bool IsForeignNational { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public int? PrimaryBankDetailId { get; set; }
    public int? EmployeeAddressId { get; set; }
    public int? EmployeePayRateId { get; set; }
    public int? EmployeePensionId { get; set; }
    public string BlobUrl { get; set; }
    public string BlobName { get; set; }
    public int? EmployeeTypeId { get; set; }
    public int? UserId { get; set; }
    public ICollection<OptionDto> EngageDepartmentIds { get; set; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
    public ICollection<OptionDto> EmployeeHealthConditionIds { get; set; }
    public ICollection<OptionDto> EmployeeDivisionIds { get; set; }
    public ICollection<OptionDto> EmployeeJobTitleIds { get; set; }
    public ICollection<OptionDto> EmployeeStoreStoreIds { get; set; }
    public List<EmployeeJobTitleDto> EmployeeJobTitles { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonFile> FilesId { get; set; }
    public List<JsonFile> FilesSars { get; set; }
    public List<JsonFile> FileEmploymentContract { get; set; }
    public List<JsonFile> FileEea1Form { get; set; }
    public List<JsonFile> FileItPolicy { get; set; }
    public List<JsonFile> FilesUniformPolicy { get; set; }
    public List<JsonFile> FileEthicsPolicy { get; set; }
    public List<EmployeeEmployeeBadgeDto> EmployeeBadges { get; set; }
    public List<EmployeePopiConsentDto> EmployeePopiConsents { get; set; }
    public List<CostCenterOption> CostCenterIds { get; set; }

    public bool HasCoolerBoxes { get; set; }

    public bool HasVehicles { get; set; }

    public bool HasAssets { get; set; }
    public string TerminationDate { get; set; }
    public string TerminationReasonName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
            //.ForMember(d => d.FilesId, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "id")))
            .ForMember(d => d.FilesId, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.ID).SelectMany(e => e.Files).ToList()))
            //.ForMember(d => d.FilesSars, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "sars")))
            .ForMember(d => d.FilesSars, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.SARS).SelectMany(e => e.Files).ToList()))
            .ForMember(d => d.FileEmploymentContract, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.EmploymentContract).SelectMany(e => e.Files).ToList()))
            //.ForMember(d => d.FileEea1Form, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "eea1Form")))
            .ForMember(d => d.FileEea1Form, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.EEA1).SelectMany(e => e.Files).ToList()))
            //.ForMember(d => d.FileItPolicy, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "itPolicy")))
            .ForMember(d => d.FileItPolicy, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.ItPolicy).SelectMany(e => e.Files).ToList()))
            .ForMember(d => d.FilesUniformPolicy, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.UniformPolicy).SelectMany(e => e.Files).ToList()))
            .ForMember(d => d.FileEthicsPolicy, opt => opt.MapFrom(s => s.EmployeeFiles.Where(f => f.EmployeeFileTypeId == (int)EmployeeFileTypeId.EthicsPolicy).SelectMany(e => e.Files).ToList()))
            .ForMember(d => d.EmployeeStateId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeStateId, s.EmployeeState.Name)))
            .ForMember(d => d.ManagerId, opt => opt.MapFrom(s => s.ManagerId.HasValue
                                                                 ? new OptionDto(s.ManagerId.Value, $"{s.Manager.FirstName} {s.Manager.LastName}")
                                                                 : null))
            .ForMember(d => d.LeaveManagerId, opt => opt.MapFrom(s => s.LeaveManagerId.HasValue
                                                                 ? new OptionDto(s.LeaveManagerId.Value, $"{s.LeaveManager.FirstName} {s.LeaveManager.LastName}")
                                                                 : null))
            .ForMember(d => d.CostCenterManagerId, opt => opt.MapFrom(s => s.CostCenterManagerId.HasValue
                                                                 ? new OptionDto(s.CostCenterManagerId.Value, $"{s.CostCenterManager.FirstName} {s.CostCenterManager.LastName}")
                                                                 : null))
            .ForMember(d => d.EmployeeIncentiveTierId, opt => opt.MapFrom(s => s.EmployeeIncentiveTierId.HasValue
                                                                               ? new OptionDto(s.EmployeeIncentiveTierId.Value, s.EmployeeIncentiveTier.Name)
                                                                               : null))
            .ForMember(d => d.EmployeeCitzenshipId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeCitzenshipId, s.EmployeeCitzenship.Name)))
            .ForMember(d => d.EmployeeLanguageId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeLanguageId, s.EmployeeLanguage.Name)))
            .ForMember(d => d.EmployeeTitleId, opt => opt.MapFrom(s => s.EmployeeTitleId.HasValue
                                                                          ? new OptionDto(s.EmployeeTitleId.Value, s.EmployeeTitle.Name)
                                                                          : null))
            .ForMember(d => d.MaritalStatusId, opt => opt.MapFrom(s => s.MaritalStatusId.HasValue
                                                                          ? new OptionDto(s.MaritalStatusId.Value, s.MaritalStatus.Name)
                                                                          : null))
            .ForMember(d => d.EmployeeNationalityId, opt => opt.MapFrom(s => s.EmployeeNationalityId.HasValue
                                                                          ? new OptionDto(s.EmployeeNationalityId.Value, s.EmployeeNationality.Name)
                                                                          : null))
            .ForMember(d => d.CitizenshipId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeCitzenshipId, s.EmployeeCitzenship.Name)))
            .ForMember(d => d.EmployeeRaceId, opt => opt.MapFrom(s => s.EmployeeRaceId.HasValue
                                                                          ? new OptionDto(s.EmployeeRaceId.Value, s.EmployeeRace.Name)
                                                                          : null))
            .ForMember(d => d.EmployeeGenderId, opt => opt.MapFrom(s => s.EmployeeGenderId.HasValue
                                                                          ? new OptionDto(s.EmployeeGenderId.Value, s.EmployeeGender.Name)
                                                                          : null))
            .ForMember(d => d.UniformSizeId, opt => opt.MapFrom(s => s.UniformSizeId.HasValue
                                                                          ? new OptionDto(s.UniformSizeId.Value, s.UniformSize.Name)
                                                                          : null))
            .ForMember(d => d.NextOfKinTypeId, opt => opt.MapFrom(s => s.NextOfKinTypeId.HasValue
                                                                          ? new OptionDto(s.NextOfKinTypeId.Value, s.NextOfKinType.Name)
                                                                          : null))


            .ForMember(d => d.EmployeeDisabledTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeDisabledTypeId, s.EmployeeDisabledType.Name)))
            .ForMember(d => d.EmployeeIdentificationTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeIdentificationTypeId, s.EmployeeIdentificationType.Name)))
            .ForMember(d => d.EmployeePassportNationalityId, opt => opt.MapFrom(s => new OptionDto(s.EmployeePassportNationalityId, s.EmployeePassportNationality.Name)))
            .ForMember(d => d.EmployeePersonTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeePersonTypeId, s.EmployeePersonType.Name)))
            .ForMember(d => d.EmployeeSDLExemptionId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeSDLExemptionId, s.EmployeeSDLExemption.Name)))
            .ForMember(d => d.EmployeeTaxStatusId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeTaxStatusId, s.EmployeeTaxStatus.Name)))
            .ForMember(d => d.EmployeeUIFExemptionId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeUIFExemptionId, s.EmployeeUIFExemption.Name)))
            .ForMember(d => d.EmployeeStandardIndustryGroupCodeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeStandardIndustryGroupCodeId, s.EmployeeStandardIndustryGroupCode.Name)))
            .ForMember(d => d.EmployeeStandardIndustryCodeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeStandardIndustryCodeId, s.EmployeeStandardIndustryCode.Name)))
            .ForMember(d => d.EmployeeTerminationReasonId, opt => opt.MapFrom(s => s.EmployeeTerminationReasonId.HasValue
                                                                               ? new OptionDto(s.EmployeeTerminationReasonId.Value, s.EmployeeTerminationReason.Name)
                                                                               : null))
            .ForMember(d => d.EngageDepartmentIds, opt => opt.MapFrom(s => s.EmployeeDepartments.Select(o => new OptionDto(o.EngageDepartmentId, o.EngageDepartment.Name))))
            .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.EmployeeRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))))
            .ForMember(d => d.EmployeeHealthConditionIds, opt => opt.MapFrom(s => s.EmployeeHealthConditions.Select(o => new OptionDto(o.EmployeeHealthConditionId, o.EmployeeHealthCondition.Name))))
            .ForMember(d => d.EmployeeDivisionIds, opt => opt.MapFrom(s => s.EmployeeDivisions.Select(o => new OptionDto(o.EmployeeDivisionId, o.EmployeeDivision.Name))))
            .ForMember(d => d.EmployeeJobTitleIds, opt => opt.MapFrom(s => s.EmployeeJobTitles.Select(o => new OptionDto(o.EmployeeJobTitleId, o.EmployeeJobTitle.Name))))
            .ForMember(d => d.EmployeeJobTitles, opt => opt.MapFrom(s => s.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Level == 3).Select(o => new EmployeeJobTitleDto
            {
                Name = o.EmployeeJobTitle.Name,
                Description = o.EmployeeJobTitle.Description,
            })))
            .ForMember(d => d.CostCenterIds, opt => opt.MapFrom(s => s.EmployeeCostCenters.Select(e => e.CostCenter)))
            .ForMember(d => d.TerminationDate, opt => opt.MapFrom(s => s.EndDate.HasValue ? s.EndDate.Value.ToShortDateString() : ""))
            .ForMember(d => d.TerminationReasonName, opt => opt.MapFrom(s => (s.Disabled && s.EmployeeTerminationHistories.Count != 0) ? s.EmployeeTerminationHistories.OrderBy(t => t.EmployeeTerminationHistoryId).LastOrDefault().EmployeeTerminationReason.Name : ""))
            .ForMember(d => d.EngageSubRegionId, opt => opt.MapFrom(s => s.EngageSubRegion))
            .ForMember(d => d.EmployeeJobTitleTimeId, opt => opt.MapFrom(s => s.EmployeeJobTitleTime))
            .ForMember(d => d.EmployeeJobTitleTypeId, opt => opt.MapFrom(s => s.EmployeeJobTitleType));
    }
}
