namespace Engage.Application.Services.Options;

public static class OptionUtils
{
    public static async Task<BaseOption> FindAsync(int optionId, string optionType, IAppDbContext context, CancellationToken cancellationToken)
    {
        BaseOption option = optionType.ToUpper() switch
        {
            OptionDesc.ASSETOWNERS => await context.AssetOwners.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ASSETSTATUSES => await context.AssetStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.BANKACCOUNTOWNERS => await context.BankAccountOwners.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.BANKACCOUNTTYPES => await context.BankAccountTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.BANKPAYMENTMETHODS => await context.BankPaymentMethods.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.BANKNAMES => await context.BankNames.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.BUDGETVERSIONS => await context.BudgetVersions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CLAIMPENDINGREASONS => await context.ClaimPendingReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CLAIMREPORTTYPES => await context.ClaimReportTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CLAIMQUANTITYTYPES => await context.ClaimQuantityTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CLAIMREJECTEDREASONS => await context.ClaimRejectedReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CLIENTTYPES => await context.ClientTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.CONTACTTYPES => await context.ContactTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.DCPRODUCTCLASSES => await context.DCProductClasses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.DEDUCTIONCYCLETYPES => await context.DeductionCycleTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.DEDUCTIONTYPES => await context.DeductionTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EDUCATIONLEVELS => await context.EducationLevels.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMAILTEMPLATETYPES => await context.EmailTemplateTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMAILTYPES => await context.EmailTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEASSETBRANDS => await context.EmployeeAssetBrands.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEASSETTYPES => await context.EmployeeAssetTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEECOOLERBOXCONDITIONS => await context.EmployeeCoolerBoxConditions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEDISABLEDTYPES => await context.EmployeeDisabledTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEIDENTIFICATIONTYPES => await context.EmployeeIdentificationTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEINCENTIVETIERS => await context.EmployeeIncentiveTiers.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEELANGUAGES => await context.EmployeeLanguages.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPAYMENTTYPES => await context.EmployeePaymentTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEFUELEXPENSETYPES => await context.EmployeeFuelExpenseTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEENATIONALITIES => await context.EmployeeNationalities.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPAYRATEFREQUENCIES => await context.EmployeePayRateFrequencies.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPAYRATEPACKAGES => await context.EmployeePayRatePackages.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPENSIONSCHEMES => await context.EmployeePensionSchemes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPENSIONCATEGORIES => await context.EmployeePensionCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPENSIONCONTRIBUTIONPERCENTAGES => await context.EmployeePensionContributionPercentages.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEPERSONTYPES => await context.EmployeePersonTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEESDLEXEMPTIONS => await context.EmployeeSDLExemptions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEDEFAULTPAYSLIPS => await context.EmployeeDefaultPayslips.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEREINSTATEMENTREASONS => await context.EmployeeReinstatementReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEESUSPENSIONREASONS => await context.EmployeeSuspensionReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEETERMINATIONREASONS => await context.EmployeeTerminationReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEESTATES => await context.EmployeeStates.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEETAXSTATUSES => await context.EmployeeTaxStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEETRAININGSTATUSES => await context.EmployeeTrainingStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEUIFEXEMPTIONS => await context.EmployeeUIFExemptions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEESTANDARDINDUSTRYGROUPCODES => await context.EmployeeStandardIndustryGroupCodes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEESTANDARDINDUSTRYCODES => await context.EmployeeStandardIndustryCodes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEKPITYPES => await context.EmployeeKpiTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYEEBADGETYPES => await context.EmployeeBadgeTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYMENTTYPES => await context.EmploymentTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EMPLOYMENTACTIONS => await context.EmploymentActions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEBRANDS => await context.EngageBrands.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGECATEGORIES => await context.EngageCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEDEPARTMENTGROUPS => await context.EngageDepartmentGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEDEPARTMENTS => await context.EngageDepartments.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEDEPARTMENTCATEGORIES => await context.EngageDepartmentCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEGROUPS => await context.EngageGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGELOCATIONS => await context.EngageLocations.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGEREGIONS => await context.EngageRegions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGESUBCATEGORIES => await context.EngageSubCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGESUBGROUPS => await context.EngageSubGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ENGAGETAGS => await context.EngageTags.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EVENTTYPES => await context.EventTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EXPENSECLAIMSTATUSES => await context.ExpenseClaimStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.EXPERIENCES => await context.Experiences.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.FREQUENCYTYPES => await context.FrequencyTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.GLADJUSTMENTTYPES => await context.GLAdjustmentTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.GENDERS => await context.Genders.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.GRADES => await context.Grades.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.INSTITUTIONTYPES => await context.InstitutionTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.LEAVETYPES => await context.LeaveTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.LOCATIONTYPES => await context.LocationTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.MARITALSTATUSES => await context.MaritalStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.NOTIFICATIONCHANNELS => await context.NotificationChannels.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.NOTIFICATIONTYPES => await context.NotificationTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.NOTIFICATIONCATEGORIES => await context.NotificationCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.NEXTOFKINTYPES => await context.NextOfKinTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ORDERQUANTITYTYPES => await context.OrderQuantityTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ORDERSKUSTATUSES => await context.OrderSkuStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ORDERSKUTYPES => await context.OrderSkuTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ORDERSTATUSES => await context.OrderStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.ORDERTYPES => await context.OrderTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTACTIVESTATUSES => await context.ProductActiveStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTANALYSISDIVISIONS => await context.ProductAnalysisDivisions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTANALYSISGROUPS => await context.ProductAnalysisGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTCLASSIFICATIONS => await context.ProductClassifications.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTSTATUSES => await context.ProductStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PRODUCTWAREHOUSESTATUSES => await context.ProductWarehouseStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PROFICIENCIES => await context.Proficiencies.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PROMOTIONTYPES => await context.PromotionTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PROMOTIONPRODUCTTYPES => await context.PromotionProductTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.PROVINCES => await context.Provinces.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.QUESTIONFALSEREASONS => await context.QuestionFalseReasons.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.QUESTIONTYPES => await context.QuestionTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.RACES => await context.Races.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.SKILLCATEGORIES => await context.SkillCategories.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREASSETOWNERS => await context.StoreAssetOwners.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREASSETTYPES => await context.StoreAssetTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECLAIMTYPES => await context.StoreClaimTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECLUSTERS => await context.StoreClusters.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECONCEPTS => await context.StoreConcepts.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREOWNERTYPES => await context.StoreOwnerTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECONCEPTTYPES => await context.StoreConceptTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECONCEPTATTRIBUTETYPES => await context.StoreConceptAttributeTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORECYCLEOPERATIONS => await context.StoreCycleOperations.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREDEPARTMENTS => await context.StoreDepartments.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREFORMATS => await context.StoreFormats.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREGROUPS => await context.StoreGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORELSMS => await context.StoreLSMs.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STOREMEDIAGROUPS => await context.StoreMediaGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORESPARREGIONS => await context.StoreSparRegions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.STORETYPES => await context.StoreTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.SUPPLIERGROUPS => await context.SupplierGroups.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.SUPPLIERREGIONS => await context.SupplierRegions.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.SUPPLIERTYPES => await context.SupplierTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.SURVEYTYPES => await context.SurveyTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.TITLES => await context.Titles.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.UNIFORMSIZES => await context.UniformSizes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.UNITTYPES => await context.UnitTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.VEHICLEBRANDS => await context.VehicleBrands.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.VEHICLETYPES => await context.VehicleTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.VOUCHERTYPES => await context.VoucherTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.WAREHOUSETYPES => await context.WarehouseTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.WEBEVENTTYPES => await context.WebEventTypes.FindAsync(new object[] { optionId }, cancellationToken),
            OptionDesc.WORKROLESTATUSES => await context.WorkRoleStatuses.FindAsync(new object[] { optionId }, cancellationToken),
            _ => throw new UnknownOptionException(optionType),
        };

        if (option == null)
        {
            throw new NotFoundException(optionType, optionId);
        }

        return option;

    }

    public static IQueryable<OptionDto> Select(string optionType, string search, IAppDbContext context)
    {
        var query = optionType.ToUpper() switch
        {
            OptionDesc.ASSETOWNERS => context.AssetOwners.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ASSETSTATUSES => context.AssetStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ATTACHMENTTYPES => context.AttachmentTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BANKACCOUNTOWNERS => context.BankAccountOwners.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BANKACCOUNTTYPES => context.BankAccountTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BANKPAYMENTMETHODS => context.BankPaymentMethods.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BANKNAMES => context.BankNames.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BENEFITTYPES => context.BenefitTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BUDGETTYPES => context.BudgetTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.BUDGETVERSIONS => context.BudgetVersions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMPENDINGREASONS => context.ClaimPendingReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMREPORTTYPES => context.ClaimReportTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMQUANTITYTYPES => context.ClaimQuantityTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMREJECTEDREASONS => context.ClaimRejectedReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMSTATUSES => context.ClaimStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMSUPPLIERSTATUSES => context.ClaimSupplierStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLAIMSKUSTATUSES => context.ClaimSkuStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CLIENTTYPES => context.ClientTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.CONTACTTYPES => context.ContactTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.DCPRODUCTCLASSES => context.DCProductClasses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.DEDUCTIONCYCLETYPES => context.DeductionCycleTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.DEDUCTIONTYPES => context.DeductionTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EDUCATIONLEVELS => context.EducationLevels.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMAILTEMPLATETYPES => context.EmailTemplateTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMAILTYPES => context.EmailTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEASSETBRANDS => context.EmployeeAssetBrands.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEASSETTYPES => context.EmployeeAssetTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEECOOLERBOXCONDITIONS => context.EmployeeCoolerBoxConditions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEDISABLEDTYPES => context.EmployeeDisabledTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEIDENTIFICATIONTYPES => context.EmployeeIdentificationTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEINCENTIVETIERS => context.EmployeeIncentiveTiers.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEELANGUAGES => context.EmployeeLanguages.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPAYMENTTYPES => context.EmployeePaymentTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEFUELEXPENSETYPES => context.EmployeeFuelExpenseTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEENATIONALITIES => context.EmployeeNationalities.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPAYRATEFREQUENCIES => context.EmployeePayRateFrequencies.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPAYRATEPACKAGES => context.EmployeePayRatePackages.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPENSIONSCHEMES => context.EmployeePensionSchemes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPENSIONCATEGORIES => context.EmployeePensionCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPENSIONCONTRIBUTIONPERCENTAGES => context.EmployeePensionContributionPercentages.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEPERSONTYPES => context.EmployeePersonTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEESDLEXEMPTIONS => context.EmployeeSDLExemptions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEESTATES => context.EmployeeStates.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEETAXSTATUSES => context.EmployeeTaxStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEUIFEXEMPTIONS => context.EmployeeUIFExemptions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEDEFAULTPAYSLIPS => context.EmployeeDefaultPayslips.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEREINSTATEMENTREASONS => context.EmployeeReinstatementReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEESUSPENSIONREASONS => context.EmployeeSuspensionReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEETERMINATIONREASONS => context.EmployeeTerminationReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEESTANDARDINDUSTRYGROUPCODES => context.EmployeeStandardIndustryGroupCodes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEESTANDARDINDUSTRYCODES => context.EmployeeStandardIndustryCodes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEETRAININGSTATUSES => context.EmployeeTrainingStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEKPITYPES => context.EmployeeKpiTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYEEBADGETYPES => context.EmployeeBadgeTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYMENTTYPES => context.EmploymentTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EMPLOYMENTACTIONS => context.EmploymentActions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEBRANDS => context.EngageBrands.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGECATEGORIES => context.EngageCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEDEPARTMENTGROUPS => context.EngageDepartmentGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEDEPARTMENTS => context.EngageDepartments.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEDEPARTMENTCATEGORIES => context.EngageDepartmentCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEGROUPS => context.EngageGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGELOCATIONS => context.EngageLocations.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGEREGIONS => context.EngageRegions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGESUBCATEGORIES => context.EngageSubCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGESUBGROUPS => context.EngageSubGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENGAGETAGS => context.EngageTags.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ENTITYCONTACTTYPES => context.EntityContactTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EVENTTYPES => context.EventTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EXPENSECLAIMSTATUSES => context.ExpenseClaimStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.EXPERIENCES => context.Experiences.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.FREQUENCYTYPES => context.FrequencyTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.GLADJUSTMENTTYPES => context.GLAdjustmentTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.GENDERS => context.Genders.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.GRADES => context.Grades.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.INSTITUTIONTYPES => context.InstitutionTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.LEAVETYPES => context.LeaveTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.LOCATIONTYPES => context.LocationTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.MARITALSTATUSES => context.MaritalStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.NOTIFICATIONCHANNELS => context.NotificationChannels.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.NOTIFICATIONTYPES => context.NotificationTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.NOTIFICATIONCATEGORIES => context.NotificationCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.NEXTOFKINTYPES => context.NextOfKinTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.OPTIONTYPEGROUPS => context.OptionTypeGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.OPTIONTYPES => context.OptionTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ORDERQUANTITYTYPES => context.OrderQuantityTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ORDERSKUSTATUSES => context.OrderSkuStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ORDERSKUTYPES => context.OrderSkuTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ORDERSTATUSES => context.OrderStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.ORDERTYPES => context.OrderTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTACTIVESTATUSES => context.ProductActiveStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTANALYSISDIVISIONS => context.ProductAnalysisDivisions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTANALYSISGROUPS => context.ProductAnalysisGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTCLASSIFICATIONS => context.ProductClassifications.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTSTATUSES => context.ProductStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PRODUCTWAREHOUSESTATUSES => context.ProductWarehouseStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PROFICIENCIES => context.Proficiencies.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PROMOTIONTYPES => context.PromotionTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PROMOTIONPRODUCTTYPES => context.PromotionProductTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.PROVINCES => context.Provinces.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.QUESTIONFALSEREASONS => context.QuestionFalseReasons.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.QUESTIONTYPES => context.QuestionTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.RACES => context.Races.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.SKILLCATEGORIES => context.SkillCategories.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREASSETOWNERS => context.StoreAssetOwners.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREASSETTYPES => context.StoreAssetTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECLAIMTYPES => context.StoreClaimTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECLUSTERS => context.StoreClusters.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECONCEPTS => context.StoreConcepts.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECONCEPTTYPES => context.StoreConceptTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECONCEPTATTRIBUTETYPES => context.StoreConceptAttributeTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORECYCLEOPERATIONS => context.StoreCycleOperations.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREDEPARTMENTS => context.StoreDepartments.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREFORMATS => context.StoreFormats.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREGROUPS => context.StoreGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORELSMS => context.StoreLSMs.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREMEDIAGROUPS => context.StoreMediaGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREOWNERTYPES => context.StoreOwnerTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREPOSTYPES => context.StorePOSTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STOREPOSFREEZERTYPES => context.StorePOSFreezerTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORESPARREGIONS => context.StoreSparRegions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.STORETYPES => context.StoreTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.SUPPLIERGROUPS => context.SupplierGroups.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.SUPPLIERREGIONS => context.SupplierRegions.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.SUPPLIERTYPES => context.SupplierTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.SURVEYTYPES => context.SurveyTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.TITLES => context.Titles.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.UNIFORMSIZES => context.UniformSizes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.UNITTYPES => context.UnitTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.VEHICLEBRANDS => context.VehicleBrands.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.VEHICLETYPES => context.VehicleTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.VOUCHERDETAILSTATUSES => context.VoucherDetailStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.VOUCHERSTATUSES => context.VoucherStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.VOUCHERTYPES => context.VoucherTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.WAREHOUSETYPES => context.WarehouseTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.WEBEVENTTYPES => context.WebEventTypes.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            OptionDesc.WORKROLESTATUSES => context.WorkRoleStatuses.Select(o => new { o.Id, o.Name, o.Disabled, o.Deleted }),
            // Return other entities as Options
            "BUDGETYEARS" => context.BudgetYears.Select(o => new { Id = o.BudgetYearId, o.Name, o.Disabled, o.Deleted }),
            "CLAIMCLASSIFICATIONS" => context.ClaimClassifications.Select(o => new { Id = o.ClaimClassificationId, o.Name, o.Disabled, o.Deleted }),
            "CLAIMFLOATS" => context.ClaimFloats.Select(o => new { Id = o.ClaimFloatId, Name = o.Title, o.Disabled, o.Deleted }),
            "CLAIMTYPES" => context.ClaimTypes.Select(o => new { Id = o.ClaimTypeId, o.Name, o.Disabled, o.Deleted }),
            "CLAIMYEARS" => context.ClaimYears.Select(o => new { Id = o.ClaimYearId, o.Name, o.Disabled, o.Deleted }),
            "DCDEPARTMENTS" => context.DCDepartments.Select(o => new { Id = o.DCDepartmentId, o.Name, o.Disabled, o.Deleted }),
            "DISTRIBUTIONCENTERS" => context.DistributionCenters.Select(o => new { Id = o.DistributionCenterId, o.Name, o.Disabled, o.Deleted }),
            "EMPLOYEES" => context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee).Select(o => new { Id = o.EmployeeId, Name = o.FirstName + " " + o.LastName, o.Disabled, o.Deleted }),
            "EMPLOYEEJOBTITLES" => context.EmployeeJobTitles.Select(o => new { Id = o.EmployeeJobTitleId, o.Name, o.Disabled, o.Deleted }),
            "ENGAGEMASTERPRODUCTS" => context.EngageMasterProducts.Select(o => new { Id = o.EngageMasterProductId, o.Name, o.Disabled, o.Deleted }),
            "ENGAGEVARIANTPRODUCTS" => context.EngageVariantProducts.Select(o => new { Id = o.EngageVariantProductId, o.Name, o.Disabled, o.Deleted }),
            "FILECONTAINERS" => context.FileContainers.Select(o => new { Id = o.FileContainerId, o.Name, o.Disabled, o.Deleted }),
            "FILETYPES" => context.FileTypes.Select(o => new { Id = o.FileTypeId, o.Name, o.Disabled, o.Deleted }),
            "MANUFACTURERS" => context.Manufacturers.Select(o => new { Id = o.ManufacturerId, o.Name, o.Disabled, o.Deleted }),
            "PAYROLLYEARS" => context.PayrollYears.Select(o => new { Id = o.PayrollYearId, o.Name, o.Disabled, o.Deleted }),
            "SURVEYS" => context.Surveys.Select(o => new { Id = o.SurveyId, Name = o.Title, o.Disabled, o.Deleted }),
            "STORECONCEPTATTRIBUTES" => context.StoreConceptAttributes.Select(o => new { Id = o.StoreConceptAttributeId, o.Name, o.Disabled, o.Deleted }),
            "STORES" => context.Stores.Select(o => new { Id = o.StoreId, o.Name, o.Disabled, o.Deleted }),
            "SUBWAREHOUSES" => context.SubWarehouses.Select(o => new { Id = o.SubWarehouseId, o.Name, o.Disabled, o.Deleted }),
            "SUPPLIERS" => context.Suppliers.Select(o => new { Id = o.SupplierId, o.Name, o.Disabled, o.Deleted }),
            "TARGETSTRATEGIES" => context.TargetStrategies.Select(o => new { Id = o.TargetStrategyId, o.Name, o.Disabled, o.Deleted }),
            "TRAININGS" => context.Trainings.Include(t => t.EngageRegion).Select(o => new { Id = o.TrainingId, Name = o.Name + " - " + o.EngageRegion.Name, o.Disabled, o.Deleted }),
            "TRAININGYEARS" => context.TrainingYears.Select(o => new { Id = o.TrainingYearId, o.Name, o.Disabled, o.Deleted }),
            "USERS" => context.Users.Select(o => new { Id = o.UserId, Name = o.FirstName + " " + o.LastName, o.Disabled, o.Deleted }),
            "VAT" => context.Vat.Select(o => new { Id = o.VatId, o.Name, o.Disabled, o.Deleted }),
            "VENDORS" => context.Vendors.Select(o => new { Id = o.VendorId, o.Name, o.Disabled, o.Deleted }),
            "WEBFILEGROUPS" => context.WebFileGroups.Select(o => new { Id = o.WebFileGroupId, o.Name, o.Disabled, o.Deleted }),
            _ => throw new UnknownOptionException(optionType.ToUpper()),
        };

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(o => EF.Functions.Like(o.Name, $"%{search}%"));
        }

        return query.Where(o => o.Disabled == false && o.Deleted == false)
                    .Select(o => new OptionDto { Id = o.Id, Name = o.Name })
                    .OrderBy(o => o.Name);
    }

    public static void Add(string optionType, string name, string description, IAppDbContext context)
    {

        switch (optionType.ToUpper())
        {
            case OptionDesc.ASSETOWNERS:
                context.AssetOwners.Add(new AssetOwner() { Name = name, Description = description });
                break;
            case OptionDesc.ASSETSTATUSES:
                context.AssetStatuses.Add(new AssetStatus() { Name = name, Description = description });
                break;
            case OptionDesc.ATTACHMENTTYPES:
                context.AttachmentTypes.Add(new AttachmentType() { Name = name, Description = description });
                break;
            case OptionDesc.BANKACCOUNTOWNERS:
                context.BankAccountOwners.Add(new BankAccountOwner() { Name = name, Description = description });
                break;
            case OptionDesc.BANKACCOUNTTYPES:
                context.BankAccountTypes.Add(new BankAccountType() { Name = name, Description = description });
                break;
            case OptionDesc.BANKPAYMENTMETHODS:
                context.BankPaymentMethods.Add(new BankPaymentMethod() { Name = name, Description = description });
                break;
            case OptionDesc.BANKNAMES:
                context.BankNames.Add(new BankName() { Name = name, Description = description });
                break;
            case OptionDesc.BENEFITTYPES:
                context.BenefitTypes.Add(new BenefitType() { Name = name, Description = description });
                break;
            case OptionDesc.BUDGETTYPES:
                context.BudgetTypes.Add(new BudgetType() { Name = name, Description = description });
                break;
            case OptionDesc.BUDGETVERSIONS:
                context.BudgetVersions.Add(new BudgetVersion() { Name = name, Description = description });
                break;
            case OptionDesc.CLAIMPENDINGREASONS:
                context.ClaimPendingReasons.Add(new ClaimPendingReason() { Name = name, Description = description });
                break;
            case OptionDesc.CLAIMREPORTTYPES:
                context.ClaimReportTypes.Add(new ClaimReportType() { Name = name, Description = description });
                break;
            case OptionDesc.CLAIMQUANTITYTYPES:
                context.ClaimQuantityTypes.Add(new ClaimQuantityType() { Name = name, Description = description });
                break;
            case OptionDesc.CLAIMREJECTEDREASONS:
                context.ClaimRejectedReasons.Add(new ClaimRejectedReason() { Name = name, Description = description });
                break;
            case OptionDesc.CLIENTTYPES:
                context.ClientTypes.Add(new ClientType() { Name = name, Description = description });
                break;
            case OptionDesc.CONTACTTYPES:
                context.ContactTypes.Add(new ContactType() { Name = name, Description = description });
                break;
            case OptionDesc.DCPRODUCTCLASSES:
                context.DCProductClasses.Add(new DCProductClass() { Name = name, Description = description });
                break;
            case OptionDesc.DEDUCTIONCYCLETYPES:
                context.DeductionCycleTypes.Add(new DeductionCycleType() { Name = name, Description = description });
                break;
            case OptionDesc.DEDUCTIONTYPES:
                context.DeductionTypes.Add(new DeductionType() { Name = name, Description = description });
                break;
            case OptionDesc.EDUCATIONLEVELS:
                context.EducationLevels.Add(new EducationLevel() { Name = name, Description = description });
                break;
            case OptionDesc.EMAILTEMPLATETYPES:
                context.EmailTemplateTypes.Add(new EmailTemplateType() { Name = name, Description = description });
                break;
            case OptionDesc.EMAILTYPES:
                context.EmailTypes.Add(new EmailType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEASSETBRANDS:
                context.EmployeeAssetBrands.Add(new EmployeeAssetBrand() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEASSETTYPES:
                context.EmployeeAssetTypes.Add(new EmployeeAssetType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEECOOLERBOXCONDITIONS:
                context.EmployeeCoolerBoxConditions.Add(new EmployeeCoolerBoxCondition() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEDISABLEDTYPES:
                context.EmployeeDisabledTypes.Add(new EmployeeDisabledType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEIDENTIFICATIONTYPES:
                context.EmployeeIdentificationTypes.Add(new EmployeeIdentificationType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEINCENTIVETIERS:
                context.EmployeeIncentiveTiers.Add(new EmployeeIncentiveTier() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEELANGUAGES:
                context.EmployeeLanguages.Add(new EmployeeLanguage() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPAYMENTTYPES:
                context.EmployeePaymentTypes.Add(new EmployeePaymentType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEKPITYPES:
                context.EmployeeKpiTypes.Add(new EmployeeKpiType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEBADGETYPES:
                context.EmployeeBadgeTypes.Add(new EmployeeBadgeType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEFUELEXPENSETYPES:
                context.EmployeeFuelExpenseTypes.Add(new EmployeeFuelExpenseType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEENATIONALITIES:
                context.EmployeeNationalities.Add(new EmployeeNationality() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPAYRATEFREQUENCIES:
                context.EmployeePayRateFrequencies.Add(new EmployeePayRateFrequency() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPAYRATEPACKAGES:
                context.EmployeePayRatePackages.Add(new EmployeePayRatePackage() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPENSIONSCHEMES:
                context.EmployeePensionSchemes.Add(new EmployeePensionScheme() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPENSIONCATEGORIES:
                context.EmployeePensionCategories.Add(new EmployeePensionCategory() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPENSIONCONTRIBUTIONPERCENTAGES:
                context.EmployeePensionContributionPercentages.Add(new EmployeePensionContributionPercentage() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEPERSONTYPES:
                context.EmployeePersonTypes.Add(new EmployeePersonType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEESDLEXEMPTIONS:
                context.EmployeeSDLExemptions.Add(new EmployeeSDLExemption() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEESTATES:
                context.EmployeeStates.Add(new EmployeeState() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEETAXSTATUSES:
                context.EmployeeTaxStatuses.Add(new EmployeeTaxStatus() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEETRAININGSTATUSES:
                context.EmployeeTrainingStatuses.Add(new EmployeeTrainingStatus() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEUIFEXEMPTIONS:
                context.EmployeeUIFExemptions.Add(new EmployeeUIFExemption() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEDEFAULTPAYSLIPS:
                context.EmployeeDefaultPayslips.Add(new EmployeeDefaultPayslip() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEEREINSTATEMENTREASONS:
                context.EmployeeReinstatementReasons.Add(new EmployeeReinstatementReason() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEESUSPENSIONREASONS:
                context.EmployeeSuspensionReasons.Add(new EmployeeSuspensionReason() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEETERMINATIONREASONS:
                context.EmployeeTerminationReasons.Add(new EmployeeTerminationReason() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEESTANDARDINDUSTRYGROUPCODES:
                context.EmployeeStandardIndustryGroupCodes.Add(new EmployeeStandardIndustryGroupCode() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYEESTANDARDINDUSTRYCODES:
                context.EmployeeStandardIndustryCodes.Add(new EmployeeStandardIndustryCode() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYMENTTYPES:
                context.EmploymentTypes.Add(new EmploymentType() { Name = name, Description = description });
                break;
            case OptionDesc.EMPLOYMENTACTIONS:
                context.EmploymentActions.Add(new EmploymentAction() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGEBRANDS:
                context.EngageBrands.Add(new EngageBrand() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGECATEGORIES:
                context.EngageCategories.Add(new EngageCategory() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGEDEPARTMENTGROUPS:
                context.EngageDepartmentGroups.Add(new EngageDepartmentGroup() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGEDEPARTMENTS:
                context.EngageDepartments.Add(new EngageDepartment() { Name = name, Description = description, EngageDepartmentGroupId = 1 });
                break;
            case OptionDesc.ENGAGEDEPARTMENTCATEGORIES:
                context.EngageDepartmentCategories.Add(new EngageDepartmentCategory() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGEGROUPS:
                context.EngageGroups.Add(new EngageGroup() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGELOCATIONS:
                context.EngageLocations.Add(new EngageLocation() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGEREGIONS:
                context.EngageRegions.Add(new EngageRegion() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGESUBCATEGORIES:
                context.EngageSubCategories.Add(new EngageSubCategory() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGESUBGROUPS:
                context.EngageSubGroups.Add(new EngageSubGroup() { Name = name, Description = description });
                break;
            case OptionDesc.ENGAGETAGS:
                context.EngageTags.Add(new EngageTag() { Name = name, Description = description });
                break;
            case OptionDesc.ENTITYCONTACTTYPES:
                context.EntityContactTypes.Add(new EntityContactType() { Name = name, Description = description });
                break;
            case OptionDesc.EVENTTYPES:
                context.EventTypes.Add(new EventType() { Name = name, Description = description });
                break;
            case OptionDesc.EXPENSECLAIMSTATUSES:
                context.ExpenseClaimStatuses.Add(new ExpenseClaimStatus() { Name = name, Description = description });
                break;
            case OptionDesc.EXPERIENCES:
                context.Experiences.Add(new Experience() { Name = name, Description = description });
                break;
            case OptionDesc.FREQUENCYTYPES:
                context.FrequencyTypes.Add(new FrequencyType() { Name = name, Description = description });
                break;
            case OptionDesc.GLADJUSTMENTTYPES:
                context.GLAdjustmentTypes.Add(new GLAdjustmentType() { Name = name, Description = description });
                break;
            case OptionDesc.GENDERS:
                context.Genders.Add(new Gender() { Name = name, Description = description });
                break;
            case OptionDesc.GRADES:
                context.Grades.Add(new Grade() { Name = name, Description = description });
                break;
            case OptionDesc.INSTITUTIONTYPES:
                context.InstitutionTypes.Add(new InstitutionType() { Name = name, Description = description });
                break;
            case OptionDesc.LEAVETYPES:
                context.LeaveTypes.Add(new LeaveType() { Name = name, Description = description });
                break;
            case OptionDesc.LOCATIONTYPES:
                context.LocationTypes.Add(new LocationType() { Name = name, Description = description });
                break;
            case OptionDesc.MARITALSTATUSES:
                context.MaritalStatuses.Add(new MaritalStatus() { Name = name, Description = description });
                break;
            case OptionDesc.NOTIFICATIONCHANNELS:
                context.NotificationChannels.Add(new NotificationChannel() { Name = name, Description = description });
                break;
            case OptionDesc.NOTIFICATIONTYPES:
                context.NotificationTypes.Add(new NotificationType() { Name = name, Description = description });
                break;
            case OptionDesc.NOTIFICATIONCATEGORIES:
                context.NotificationCategories.Add(new NotificationCategory() { Name = name, Description = description });
                break;
            case OptionDesc.NEXTOFKINTYPES:
                context.NextOfKinTypes.Add(new NextOfKinType() { Name = name, Description = description });
                break;
            case OptionDesc.ORDERQUANTITYTYPES:
                context.OrderQuantityTypes.Add(new OrderQuantityType() { Name = name, Description = description });
                break;
            case OptionDesc.ORDERSKUSTATUSES:
                context.OrderSkuStatuses.Add(new OrderSkuStatus() { Name = name, Description = description });
                break;
            case OptionDesc.ORDERSKUTYPES:
                context.OrderSkuTypes.Add(new OrderSkuType() { Name = name, Description = description });
                break;
            case OptionDesc.ORDERSTATUSES:
                context.OrderStatuses.Add(new OrderStatus() { Name = name, Description = description });
                break;
            case OptionDesc.ORDERTYPES:
                context.OrderTypes.Add(new OrderType() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTACTIVESTATUSES:
                context.ProductActiveStatuses.Add(new ProductActiveStatus() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTANALYSISDIVISIONS:
                context.ProductAnalysisDivisions.Add(new ProductAnalysisDivision() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTANALYSISGROUPS:
                context.ProductAnalysisGroups.Add(new ProductAnalysisGroup() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTCLASSIFICATIONS:
                context.ProductClassifications.Add(new ProductClassification() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTSTATUSES:
                context.ProductStatuses.Add(new ProductStatus() { Name = name, Description = description });
                break;
            case OptionDesc.PRODUCTWAREHOUSESTATUSES:
                context.ProductWarehouseStatuses.Add(new ProductWarehouseStatus() { Name = name, Description = description });
                break;
            case OptionDesc.PROFICIENCIES:
                context.Proficiencies.Add(new Proficiency() { Name = name, Description = description });
                break;
            case OptionDesc.PROMOTIONTYPES:
                context.PromotionTypes.Add(new PromotionType() { Name = name, Description = description });
                break;
            case OptionDesc.PROMOTIONPRODUCTTYPES:
                context.PromotionProductTypes.Add(new PromotionProductType() { Name = name, Description = description });
                break;
            case OptionDesc.PROVINCES:
                context.Provinces.Add(new Province() { Name = name, Description = description });
                break;
            case OptionDesc.QUESTIONFALSEREASONS:
                context.QuestionFalseReasons.Add(new QuestionFalseReason() { Name = name, Description = description });
                break;
            case OptionDesc.QUESTIONTYPES:
                context.QuestionTypes.Add(new QuestionType() { Name = name, Description = description });
                break;
            case OptionDesc.RACES:
                context.Races.Add(new Race() { Name = name, Description = description });
                break;
            case OptionDesc.SKILLCATEGORIES:
                context.SkillCategories.Add(new SkillCategory() { Name = name, Description = description });
                break;
            case OptionDesc.STOREASSETOWNERS:
                context.StoreAssetOwners.Add(new StoreAssetOwner() { Name = name, Description = description });
                break;
            case OptionDesc.STOREASSETTYPES:
                context.StoreAssetTypes.Add(new StoreAssetType() { Name = name, Description = description });
                break;
            case OptionDesc.STORECLAIMTYPES:
                context.StoreClaimTypes.Add(new StoreClaimType() { Name = name, Description = description });
                break;
            case OptionDesc.STORECLUSTERS:
                context.StoreClusters.Add(new StoreCluster() { Name = name, Description = description });
                break;
            case OptionDesc.STORECONCEPTS:
                context.StoreConcepts.Add(new StoreConcept() { Name = name, Description = description });
                break;
            case OptionDesc.STORECONCEPTTYPES:
                context.StoreConceptTypes.Add(new StoreConceptType() { Name = name, Description = description });
                break;
            case OptionDesc.STORECONCEPTATTRIBUTETYPES:
                context.StoreConceptAttributeTypes.Add(new StoreConceptAttributeType() { Name = name, Description = description });
                break;
            case OptionDesc.STORECYCLEOPERATIONS:
                context.StoreCycleOperations.Add(new StoreCycleOperation() { Name = name, Description = description });
                break;
            case OptionDesc.STOREDEPARTMENTS:
                context.StoreDepartments.Add(new StoreDepartment() { Name = name, Description = description });
                break;
            case OptionDesc.STOREFORMATS:
                context.StoreFormats.Add(new StoreFormat() { Name = name, Description = description });
                break;
            case OptionDesc.STOREGROUPS:
                context.StoreGroups.Add(new StoreGroup() { Name = name, Description = description });
                break;
            case OptionDesc.STOREOWNERTYPES:
                context.StoreOwnerTypes.Add(new StoreOwnerType() { Name = name, Description = description });
                break;
            case OptionDesc.STORELSMS:
                context.StoreLSMs.Add(new StoreLSM() { Name = name, Description = description });
                break;
            case OptionDesc.STOREMEDIAGROUPS:
                context.StoreMediaGroups.Add(new StoreMediaGroup() { Name = name, Description = description });
                break;
            case OptionDesc.STOREPOSTYPES:
                context.StorePOSTypes.Add(new StorePOSType() { Name = name, Description = description });
                break;
            case OptionDesc.STOREPOSFREEZERTYPES:
                context.StorePOSFreezerTypes.Add(new StorePOSFreezerType() { Name = name, Description = description });
                break;
            case OptionDesc.STORESPARREGIONS:
                context.StoreSparRegions.Add(new StoreSparRegion() { Name = name, Description = description });
                break;
            case OptionDesc.STORETYPES:
                context.StoreTypes.Add(new StoreType() { Name = name, Description = description });
                break;
            case OptionDesc.SUPPLIERGROUPS:
                context.SupplierGroups.Add(new SupplierGroup() { Name = name, Description = description });
                break;
            case OptionDesc.SUPPLIERTYPES:
                context.SupplierTypes.Add(new SupplierType() { Name = name, Description = description });
                break;
            case OptionDesc.SURVEYTYPES:
                context.SurveyTypes.Add(new SurveyType() { Name = name, Description = description });
                break;
            case OptionDesc.TITLES:
                context.Titles.Add(new Title() { Name = name, Description = description });
                break;
            case OptionDesc.UNIFORMSIZES:
                context.UniformSizes.Add(new UniformSize() { Name = name, Description = description });
                break;
            case OptionDesc.UNITTYPES:
                context.UnitTypes.Add(new UnitType() { Name = name, Description = description });
                break;
            case OptionDesc.VEHICLEBRANDS:
                context.VehicleBrands.Add(new VehicleBrand() { Name = name, Description = description });
                break;
            case OptionDesc.VEHICLETYPES:
                context.VehicleTypes.Add(new VehicleType() { Name = name, Description = description });
                break;
            case OptionDesc.VOUCHERTYPES:
                context.VoucherTypes.Add(new VoucherType() { Name = name, Description = description });
                break;
            case OptionDesc.WAREHOUSETYPES:
                context.WarehouseTypes.Add(new WarehouseType() { Name = name, Description = description });
                break;
            case OptionDesc.WEBEVENTTYPES:
                context.WebEventTypes.Add(new WebEventType() { Name = name, Description = description });
                break;
            case OptionDesc.WORKROLESTATUSES:
                context.WorkRoleStatuses.Add(new WorkRoleStatus() { Name = name, Description = description });
                break;
        };
    }
}
