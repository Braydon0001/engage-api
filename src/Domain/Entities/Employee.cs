namespace Engage.Domain.Entities
{
    public class Employee : BaseAuditableEntity
    {
        public Employee()
        {
            BankDetails = new HashSet<EmployeeBankDetail>();
            Benefits = new HashSet<EmployeeBenefit>();
            Deductions = new HashSet<EmployeeDeduction>();
            DisciplinaryProcedures = new HashSet<EmployeeDisciplinaryProcedure>();
            ExpenseClaims = new HashSet<EmployeeExpenseClaim>();
            LeaveEntries = new HashSet<EmployeeLeaveEntry>();
            Loans = new HashSet<EmployeeLoan>();
            SkillsDevelopment = new HashSet<EmployeeSkillsDevelopment>();
            WorkRoles = new HashSet<EmployeeWorkRole>();
            EmployeeSkills = new HashSet<EmployeeSkill>();
            EmployeeQualifications = new HashSet<EmployeeQualification>();
            EmployeeSuspensions = new HashSet<EmployeeSuspension>();
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
            EmployeeRegions = new HashSet<EmployeeRegion>();
            EmployeeStores = new HashSet<EmployeeStore>();
            EmployeeStoreArchives = new HashSet<EmployeeStoreArchive>();
            EmployeeStoreCheckIns = new HashSet<EmployeeStoreCheckIn>();
            EmployeeNotifications = new HashSet<EmployeeNotification>();
            NotificationEmployees = new HashSet<NotificationEmployee>();
            SurveyInstances = new HashSet<SurveyInstance>();
            SurveyEmployees = new HashSet<SurveyEmployee>();
            Employees = new HashSet<Employee>();
            LeaveEmployees = new HashSet<Employee>();
            CostCenterEmployees = new HashSet<Employee>();
            EmployeeCostCenters = new HashSet<CostCenterEmployee>();
            EmployeeJobTitles = new HashSet<EmployeeEmployeeJobTitle>();
            EmployeeBadges = new HashSet<EmployeeEmployeeBadge>();
            EmployeeKpis = new HashSet<EmployeeEmployeeKpi>();
            EmployeeStoreKpis = new HashSet<EmployeeStoreKpi>();
            EmployeeKpiScores = new HashSet<EmployeeKpiScore>();
            EmployeeStoreKpiScores = new HashSet<EmployeeStoreKpiScore>();
            EmployeeTrainingRecords = new HashSet<EmployeeTrainingRecord>();
            EmployeeTrainings = new HashSet<EmployeeTraining>();
            TrainingFacilitators = new HashSet<TrainingFacilitator>();
            NotificationEmployeeReads = new HashSet<NotificationEmployeeRead>();
            EmployeePopiConsents = new HashSet<EmployeePopiConsent>();
            EmployeeHealthConditions = new HashSet<EmployeeEmployeeHealthCondition>();
            EmployeeReinstatementHistories = new HashSet<EmployeeReinstatementHistory>();
            EmployeeTerminationHistories = new HashSet<EmployeeTerminationHistory>();
            EmployeeDivisions = new HashSet<EmployeeEmployeeDivision>();
            EmployeeFiles = new HashSet<EmployeeFile>();
        }
        public int EmployeeDisabledTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int StakeholderId { get; set; }
        public int? ManagerId { get; set; }
        public int? LeaveManagerId { get; set; }
        public int? CostCenterManagerId { get; set; }
        public int? UserId { get; set; }
        public int EmployeeStateId { get; set; }
        public int? EmployeeIncentiveTierId { get; set; }
        public int? EmployeeJobTitleId { get; set; }
        public int? EmployeeJobTitleTimeId { get; set; }
        public int? EmployeeJobTitleTypeId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public int? EmploymentActionId { get; set; }
        public string Code { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MaidenName { get; set; }
        public string KnownAs { get; set; }
        public string Initials { get; set; }
        public bool IsEncashLeave { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? LeaveCycleStartDate { get; set; }
        public float? LeaveAccumulationRate { get; set; }
        public int? AnnualLeave { get; set; }
        public int? SickLeave { get; set; }
        public int? FamilyLeave { get; set; }

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
        public bool IsDefaultPayslip { get; set; }
        public bool IsRetired { get; set; }
        public bool IsForeignNational { get; set; }
        public string Note { get; set; }
        // Tax
        public int EmployeeIdentificationTypeId { get; set; }
        public int EmployeePassportNationalityId { get; set; }
        public int EmployeeCitzenshipId { get; set; }
        public int EmployeeLanguageId { get; set; }
        public int EmployeePersonTypeId { get; set; }
        public int EmployeeSDLExemptionId { get; set; }
        public int EmployeeTaxStatusId { get; set; }
        public int EmployeeUIFExemptionId { get; set; }
        public int? EmployeeDefaultPayslipId { get; set; }
        public int? EmployeeReinstatementReasonId { get; set; }
        public int? EmployeeTerminationReasonId { get; set; }
        public int EmployeeStandardIndustryGroupCodeId { get; set; }
        public int EmployeeStandardIndustryCodeId { get; set; }

        public int? MaritalStatusId { get; set; }
        public int? EmployeeNationalityId { get; set; }
        //public int? ProfileCitizenshipId { get; set; }
        public int? EmployeeRaceId { get; set; }
        public int? EmployeeGenderId { get; set; }
        public int? NextOfKinTypeId { get; set; }
        public int? EmployeeTitleId { get; set; }
        public int? UniformSizeId { get; set; }
        public int? PayrollPeriodId { get; set; }
        //Add EngageRegionId Not Null and Default to 1
        public int EngageRegionId { get; set; } = 1;
        public int? EngageSubRegionId { get; set; }
        public int? EmployeeTypeId { get; set; }

        public DateTime GroupStartDate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ReinstatementDate { get; set; }
        public bool IsNotReemployable { get; set; }
        public string IdNumber { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportStartDate { get; set; }
        public DateTime? PassportEndDate { get; set; }
        public string PAYENumber { get; set; }
        public string SARSNumber { get; set; }
        public bool IsVoluntaryOverDeduction { get; set; }
        public DateTime? StatutoryEmploymentDateOverride { get; set; }
        public bool IsApplyTaxForPublicServiceEmployee { get; set; }
        public bool IsEmploymentTaxIncentive { get; set; }
        public string UIFNumber { get; set; }
        public string RANumber { get; set; }
        public string MedicalAidNumber { get; set; }
        public string BlobUrl { get; set; }
        public string BlobName { get; set; }
        public List<JsonFile> Files { get; set; }
        // Navigation Properties
        public EmployeeDisabledType EmployeeDisabledType { get; set; }
        public EmployeeIdentificationType EmployeeIdentificationType { get; set; }
        public EmployeeNationality EmployeePassportNationality { get; set; }
        public EmployeeNationality EmployeeCitzenship { get; set; }
        public EmployeeLanguage EmployeeLanguage { get; set; }
        public EmployeePersonType EmployeePersonType { get; set; }
        public EmployeeSDLExemption EmployeeSDLExemption { get; set; }
        public EmployeeTaxStatus EmployeeTaxStatus { get; set; }
        public EmployeeUIFExemption EmployeeUIFExemption { get; set; }
        public EmployeeDefaultPayslip EmployeeDefaultPayslip { get; set; }
        public EmployeeReinstatementReason EmployeeReinstatementReason { get; set; }
        public EmployeeTerminationReason EmployeeTerminationReason { get; set; }
        public EmployeeStandardIndustryGroupCode EmployeeStandardIndustryGroupCode { get; set; }
        public EmployeeStandardIndustryCode EmployeeStandardIndustryCode { get; set; }
        public Stakeholder Stakeholder { get; set; }
        public Employee Manager { get; set; }
        public Employee LeaveManager { get; set; }
        public Employee CostCenterManager { get; set; }
        public User User { get; set; }
        public EmployeeState EmployeeState { get; set; }
        public EmployeeIncentiveTier EmployeeIncentiveTier { get; set; }
        public EmployeeJobTitle EmployeeJobTitle { get; set; }
        public EmployeeJobTitleTime EmployeeJobTitleTime { get; set; }
        public EmployeeJobTitleType EmployeeJobTitleType { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public EmploymentAction EmploymentAction { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
        public EmployeeNationality EmployeeNationality { get; set; }
        //public ProfileCitizenship ProfileCitizenship { get; set; }
        public Race EmployeeRace { get; set; }
        public Gender EmployeeGender { get; set; }
        public NextOfKinType NextOfKinType { get; set; }
        public Title EmployeeTitle { get; set; }
        public UniformSize UniformSize { get; set; }
        public PayrollPeriod PayrollPeriod { get; set; }
        public EngageRegion EngageRegion { get; set; }
        public EngageSubRegion EngageSubRegion { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public ICollection<EmployeeBankDetail> BankDetails { get; private set; }
        public ICollection<EmployeeBenefit> Benefits { get; private set; }
        public ICollection<EmployeeDeduction> Deductions { get; private set; }
        public ICollection<EmployeeDisciplinaryProcedure> DisciplinaryProcedures { get; private set; }
        public ICollection<EmployeeExpenseClaim> ExpenseClaims { get; private set; }
        public ICollection<EmployeeLeaveEntry> LeaveEntries { get; private set; }
        public ICollection<EmployeeLoan> Loans { get; private set; }
        public ICollection<EmployeeSkillsDevelopment> SkillsDevelopment { get; private set; }
        public ICollection<EmployeeWorkRole> WorkRoles { get; private set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; private set; }
        public ICollection<EmployeeQualification> EmployeeQualifications { get; private set; }
        public ICollection<EmployeeSuspension> EmployeeSuspensions { get; private set; }
        public ICollection<EmployeeDepartment> EmployeeDepartments { get; private set; }
        public ICollection<EmployeeNotification> EmployeeNotifications { get; private set; }
        public ICollection<NotificationEmployee> NotificationEmployees { get; private set; }
        public ICollection<SurveyInstance> SurveyInstances { get; private set; }
        public ICollection<SurveyEmployee> SurveyEmployees { get; private set; }
        public ICollection<EmployeeStore> EmployeeStores { get; private set; }
        public ICollection<EmployeeStoreArchive> EmployeeStoreArchives { get; private set; }
        public ICollection<EmployeeStoreCheckIn> EmployeeStoreCheckIns { get; private set; }
        public ICollection<Employee> Employees { get; private set; }
        public ICollection<Employee> LeaveEmployees { get; private set; }
        public ICollection<Employee> CostCenterEmployees { get; private set; }
        public ICollection<CostCenterEmployee> EmployeeCostCenters { get; private set; }
        public ICollection<EmployeeReport> EmployeeReports { get; set; }
        public ICollection<EmployeeManager> EmployeeManagers { get; set; }
        public ICollection<EmployeeManager> ManagerEmployees { get; set; }
        public ICollection<EmployeeTrainingRecord> EmployeeTrainingRecords { get; set; }
        public ICollection<EmployeePopiConsent> EmployeePopiConsents { get; set; }
        // Many to Many
        public ICollection<EmployeeRegion> EmployeeRegions { get; private set; }
        public ICollection<EmployeeEmployeeJobTitle> EmployeeJobTitles { get; private set; }
        public ICollection<EmployeeEmployeeBadge> EmployeeBadges { get; private set; }
        public ICollection<EmployeeEmployeeKpi> EmployeeKpis { get; private set; }
        public ICollection<EmployeeStoreKpi> EmployeeStoreKpis { get; private set; }
        public ICollection<EmployeeKpiScore> EmployeeKpiScores { get; private set; }
        public ICollection<EmployeeStoreKpiScore> EmployeeStoreKpiScores { get; private set; }
        public ICollection<EmployeeTraining> EmployeeTrainings { get; private set; }
        public ICollection<TrainingFacilitator> TrainingFacilitators { get; private set; }
        public ICollection<NotificationEmployeeRead> NotificationEmployeeReads { get; private set; }
        public ICollection<EmployeeEmployeeHealthCondition> EmployeeHealthConditions { get; private set; }
        public ICollection<EmployeeReinstatementHistory> EmployeeReinstatementHistories { get; private set; }
        public ICollection<EmployeeTerminationHistory> EmployeeTerminationHistories { get; private set; }
        public ICollection<EmployeeEmployeeDivision> EmployeeDivisions { get; set; }
        public ICollection<EmployeeFile> EmployeeFiles { get; set; }
    }
}
