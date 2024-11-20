
select * from `engage-prod`.opt_employeeterminationreasons;

INSERT INTO `engage-prod`.opt_employeeterminationreasons
    (`Name`, `Description`, `Disabled`, `Deleted`, `Order`, `TenantId`)
SELECT 
    `Name`, `Description`, `Disabled`, `Deleted`, `Order`, "encore"
FROM 
    `engage-prod`.opt_employeeterminationreasons
WHERE 
    TenantId = 'engage' ;
    
select * from `engage-prod`.opt_employeeterminationreasons;

/*
opt_BankAccountTypes
opt_BankPaymentMethods
opt_BankNames
opt_EmailTemplateTypes
opt_EmailTypes
opt_EmployeeAssetTypes
opt_EmployeeDisabledTypes
opt_EmployeeIdentificationTypes
opt_EmployeeLanguages
opt_EmployeeNatiemployeedivisionsonalities
opt_Genders
opt_LocationTypes
opt_MaritalStatuses
opt_Provinces
opt_Races
opt_Titles
opt_employmentactions
opt_employeereinstatementreasons
opt_employeeterminationreasons
*/