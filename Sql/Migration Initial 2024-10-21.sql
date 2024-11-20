CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `AnalysisPillarGroups` (
    `AnalysisPillarGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_AnalysisPillarGroups` PRIMARY KEY (`AnalysisPillarGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ApiKeys` (
    `ApiKeyId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Value` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `AssignedTo` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ApiKeys` PRIMARY KEY (`ApiKeyId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AuditEntries` (
    `AuditEntryID` int NOT NULL AUTO_INCREMENT,
    `EntitySetName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `EntityTypeName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `State` int NOT NULL,
    `StateName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(255) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AuditEntries` PRIMARY KEY (`AuditEntryID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BudgetYears` (
    `BudgetYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_BudgetYears` PRIMARY KEY (`BudgetYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryFileTypes` (
    `CategoryFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryFileTypes` PRIMARY KEY (`CategoryFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryGroups` (
    `CategoryGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryGroups` PRIMARY KEY (`CategoryGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategorySubGroups` (
    `CategorySubGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategorySubGroups` PRIMARY KEY (`CategorySubGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryTargetTypes` (
    `CategoryTargetTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryTargetTypes` PRIMARY KEY (`CategoryTargetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimSkuTypes` (
    `ClaimSkuTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsVatInclusive` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimSkuTypes` PRIMARY KEY (`ClaimSkuTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimYears` (
    `ClaimYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimYears` PRIMARY KEY (`ClaimYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTemplateTypes` (
    `CommunicationTemplateTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CommunicationTemplateTypes` PRIMARY KEY (`CommunicationTemplateTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTypes` (
    `CommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CommunicationTypes` PRIMARY KEY (`CommunicationTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostDepartments` (
    `CostDepartmentId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostDepartments` PRIMARY KEY (`CostDepartmentId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostTypes` (
    `CostTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostTypes` PRIMARY KEY (`CostTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorCutOffSettings` (
    `CreditorCutOffSettingId` int NOT NULL AUTO_INCREMENT,
    `CreditorCutOff` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `PaymentCutOff` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorCutOffSettings` PRIMARY KEY (`CreditorCutOffSettingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorFileTypes` (
    `CreditorFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorFileTypes` PRIMARY KEY (`CreditorFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorStatuses` (
    `CreditorStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorStatuses` PRIMARY KEY (`CreditorStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DCDepartments` (
    `DCDepartmentId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_DCDepartments` PRIMARY KEY (`DCDepartmentId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DistributionCenters` (
    `DistributionCenterId` int NOT NULL AUTO_INCREMENT,
    `Code` varchar(20) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_DistributionCenters` PRIMARY KEY (`DistributionCenterId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeDivisions` (
    `EmployeeDivisionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
    `IsRihCallCycles` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeDivisions` PRIMARY KEY (`EmployeeDivisionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeFileTypes` (
    `EmployeeFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeFileTypes` PRIMARY KEY (`EmployeeFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeHealthConditions` (
    `EmployeeHealthConditionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeHealthConditions` PRIMARY KEY (`EmployeeHealthConditionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeJobTitles` (
    `EmployeeJobTitleId` int NOT NULL AUTO_INCREMENT,
    `ParentId` int NULL,
    `Level` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Order` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeJobTitles` PRIMARY KEY (`EmployeeJobTitleId`),
    CONSTRAINT `FK_EmployeeJobTitles_EmployeeJobTitles_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeRecurringTransactionStatuses` (
    `EmployeeRecurringTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeRecurringTransactionStatuses` PRIMARY KEY (`EmployeeRecurringTransactionStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarGroups` (
    `EmployeeStoreCalendarGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarGroups` PRIMARY KEY (`EmployeeStoreCalendarGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarStatuses` (
    `EmployeeStoreCalendarStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarStatuses` PRIMARY KEY (`EmployeeStoreCalendarStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarTypes` (
    `EmployeeStoreCalendarTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarTypes` PRIMARY KEY (`EmployeeStoreCalendarTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarYears` (
    `EmployeeStoreCalendarYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarYears` PRIMARY KEY (`EmployeeStoreCalendarYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTransactionRemunerationTypes` (
    `EmployeeTransactionRemunerationTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTransactionRemunerationTypes` PRIMARY KEY (`EmployeeTransactionRemunerationTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTransactionStatuses` (
    `EmployeeTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTransactionStatuses` PRIMARY KEY (`EmployeeTransactionStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTransactionTypeGroups` (
    `EmployeeTransactionTypeGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTransactionTypeGroups` PRIMARY KEY (`EmployeeTransactionTypeGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTypes` (
    `EmployeeTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTypes` PRIMARY KEY (`EmployeeTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ExpenseTypes` (
    `ExpenseTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ExpenseTypes` PRIMARY KEY (`ExpenseTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ExternalUserTypes` (
    `ExternalUserTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ExternalUserTypes` PRIMARY KEY (`ExternalUserTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FileContainers` (
    `FileContainerId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `ContainerName` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `PublicAccess` tinyint(1) NOT NULL,
    `FileNameStrategy` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_FileContainers` PRIMARY KEY (`FileContainerId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FileTypes` (
    `FileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Icon` varchar(120) CHARACTER SET utf8mb4 NULL,
    `CanView` tinyint(1) NOT NULL,
    `IsUrl` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_FileTypes` PRIMARY KEY (`FileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FileUploads` (
    `FileUploadId` int NOT NULL AUTO_INCREMENT,
    `FileName` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ImportDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_FileUploads` PRIMARY KEY (`FileUploadId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `GLAccountTypes` (
    `GLAccountTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_GLAccountTypes` PRIMARY KEY (`GLAccountTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ImportFiles` (
    `ImportFileId` int NOT NULL AUTO_INCREMENT,
    `FileName` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ConfirmedDate` datetime(6) NULL,
    `RejectedDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ImportFiles` PRIMARY KEY (`ImportFileId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncidentSkuStatuses` (
    `IncidentSkuStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_IncidentSkuStatuses` PRIMARY KEY (`IncidentSkuStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncidentSkuTypes` (
    `IncidentSkuTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_IncidentSkuTypes` PRIMARY KEY (`IncidentSkuTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncidentStatuses` (
    `IncidentStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_IncidentStatuses` PRIMARY KEY (`IncidentStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncidentTypes` (
    `IncidentTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_IncidentTypes` PRIMARY KEY (`IncidentTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryGroups` (
    `InventoryGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryGroups` PRIMARY KEY (`InventoryGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryStatuses` (
    `InventoryStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryStatuses` PRIMARY KEY (`InventoryStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryTransactionStatuses` (
    `InventoryTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryTransactionStatuses` PRIMARY KEY (`InventoryTransactionStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryTransactionTypes` (
    `InventoryTransactionTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsPositive` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryTransactionTypes` PRIMARY KEY (`InventoryTransactionTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryUnitTypes` (
    `InventoryUnitTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryUnitTypes` PRIMARY KEY (`InventoryUnitTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryWarehouses` (
    `InventoryWarehouseId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryWarehouses` PRIMARY KEY (`InventoryWarehouseId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryYears` (
    `InventoryYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryYears` PRIMARY KEY (`InventoryYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_AssetOwners` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_AssetOwners` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_AssetStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_AssetStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_AttachmentTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_AttachmentTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BankAccountOwners` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BankAccountOwners` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BankAccountTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BankAccountTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BankNames` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BankNames` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BankPaymentMethods` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BankPaymentMethods` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BenefitTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BenefitTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BudgetTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BudgetTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_BudgetVersions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_BudgetVersions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimPendingReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimPendingReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimQuantityTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimQuantityTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimRejectedReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimRejectedReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimReportTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimReportTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimSkuStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimSkuStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClaimSupplierStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClaimSupplierStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ClientTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ClientTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ContactTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ContactTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_DeductionCycleTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_DeductionCycleTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_DeductionTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_DeductionTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EducationLevels` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EducationLevels` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmailTemplateTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmailTemplateTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmailTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmailTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeAssetBrands` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeAssetBrands` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeAssetTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeAssetTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeBadgeTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeBadgeTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeCoolerBoxConditions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeCoolerBoxConditions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeDefaultPayslips` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeDefaultPayslips` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeDisabledTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeDisabledTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeFuelExpenseTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeFuelExpenseTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeIdentificationTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeIdentificationTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeIncentiveTiers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeIncentiveTiers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeKpiTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeKpiTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeLanguages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeLanguages` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeNationalities` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeNationalities` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePaymentTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePaymentTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePayRateFrequencies` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePayRateFrequencies` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePayRatePackages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePayRatePackages` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePensionCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePensionCategories` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePensionContributionPercentages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePensionContributionPercentages` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePensionSchemes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePensionSchemes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeePersonTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeePersonTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeReinstatementReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeReinstatementReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeSDLExemptions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeSDLExemptions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeStandardIndustryCodes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeStandardIndustryCodes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeStandardIndustryGroupCodes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeStandardIndustryGroupCodes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeStates` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeStates` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeSuspensionReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeSuspensionReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeTaxStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeTaxStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeTerminationReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeTerminationReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeTrainingStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeTrainingStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmployeeUIFExemptions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmployeeUIFExemptions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmploymentActions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmploymentActions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EmploymentTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EndDateReminderDays` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmploymentTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageBrands` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IsSparBrand` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageBrands` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageDepartmentGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageDepartmentGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageLocations` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageLocations` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageTags` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageTags` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EntityContactTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EntityContactTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EventTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EventTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ExpenseClaimStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ExpenseClaimStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Experiences` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Experiences` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_FrequencyTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_FrequencyTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Genders` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Genders` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_GLAdjustmentTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_GLAdjustmentTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Grades` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Grades` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_InstitutionTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_InstitutionTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_LeaveTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_LeaveTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_LocationTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_LocationTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_MaritalStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_MaritalStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_NextOfKinTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_NextOfKinTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_NotificationCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_NotificationCategories` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_NotificationChannels` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_NotificationChannels` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_NotificationTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_NotificationTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_OrderQuantityTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_OrderQuantityTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_OrderSkuStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_OrderSkuStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_OrderSkuTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_OrderSkuTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_OrderStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_OrderStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_OrderTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_OrderTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductActiveStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductActiveStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductAnalysisDivisions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductAnalysisDivisions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductAnalysisGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductAnalysisGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductClassifications` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductClassifications` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_ProductWarehouseStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_ProductWarehouseStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Proficiencies` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Proficiencies` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_PromotionProductTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_PromotionProductTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_PromotionTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_PromotionTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Provinces` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Provinces` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_QuestionFalseReasons` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_QuestionFalseReasons` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_QuestionTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_QuestionTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Races` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Races` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_SkillCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_SkillCategories` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreAssetOwners` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreAssetOwners` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreAssetTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreAssetTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreClaimTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreClaimTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreClusters` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreClusters` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreConceptAttributeTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreConceptAttributeTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreConceptTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreConceptTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreCycleOperations` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreCycleOperations` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreDepartments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreDepartments` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreFormats` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreFormats` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreLSMs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreLSMs` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreMediaGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreMediaGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreOwnerTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreOwnerTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StorePOSFreezerTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StorePOSFreezerTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StorePOSTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StorePOSTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreSparRegions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreSparRegions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ImageUrl` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_SupplierGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_SupplierGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_SupplierTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_SupplierTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_SurveyTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_SurveyTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_Titles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_Titles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_UniformSizes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_UniformSizes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_UnitTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_UnitTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_VehicleBrands` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_VehicleBrands` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_VehicleTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_VehicleTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_VoucherDetailStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_VoucherDetailStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_VoucherStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_VoucherStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_VoucherTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_VoucherTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_WarehouseTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_WarehouseTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_WebEventTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_WebEventTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_WorkRoleStatuses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_WorkRoleStatuses` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OptionTypeGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_OptionTypeGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderStagings` (
    `OrderStagingId` int NOT NULL AUTO_INCREMENT,
    `Region` varchar(120) CHARACTER SET utf8mb4 NULL,
    `StoreName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `AccountNumber` varchar(120) CHARACTER SET utf8mb4 NULL,
    `OrderNumber` varchar(120) CHARACTER SET utf8mb4 NULL,
    `OrderContactName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `OrderContactEmail` varchar(120) CHARACTER SET utf8mb4 NULL,
    `VatNumber` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Date` varchar(60) CHARACTER SET utf8mb4 NULL,
    `Reference` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderStagings` PRIMARY KEY (`OrderStagingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplateStatuses` (
    `OrderTemplateStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateStatuses` PRIMARY KEY (`OrderTemplateStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrganizationSettings` (
    `OrganizationSettingId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `FaviconUrl` longtext CHARACTER SET utf8mb4 NULL,
    `LogoUrl` longtext CHARACTER SET utf8mb4 NULL,
    `LogoDarkUrl` longtext CHARACTER SET utf8mb4 NULL,
    `OrganizationTheme` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrganizationSettings` PRIMARY KEY (`OrganizationSettingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentArchives` (
    `PaymentArchiveId` int NOT NULL AUTO_INCREMENT,
    `ArchiveDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentArchives` PRIMARY KEY (`PaymentArchiveId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentBatches` (
    `PaymentBatchId` int NOT NULL AUTO_INCREMENT,
    `BatchDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentBatches` PRIMARY KEY (`PaymentBatchId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineFileTypes` (
    `PaymentLineFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineFileTypes` PRIMARY KEY (`PaymentLineFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentProofs` (
    `PaymentProofId` int NOT NULL AUTO_INCREMENT,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentProofs` PRIMARY KEY (`PaymentProofId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentStatuses` (
    `PaymentStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentStatuses` PRIMARY KEY (`PaymentStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentYears` (
    `PaymentYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentYears` PRIMARY KEY (`PaymentYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PayrollYears` (
    `PayrollYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PayrollYears` PRIMARY KEY (`PayrollYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductBrands` (
    `ProductBrandId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `SparBrand` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductBrands` PRIMARY KEY (`ProductBrandId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductGroups` (
    `ProductGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductGroups` PRIMARY KEY (`ProductGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductMasterStatuses` (
    `ProductMasterStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductMasterStatuses` PRIMARY KEY (`ProductMasterStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductMasterSystemStatuses` (
    `ProductMasterSystemStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductMasterSystemStatuses` PRIMARY KEY (`ProductMasterSystemStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductModuleStatuses` (
    `ProductModuleStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductModuleStatuses` PRIMARY KEY (`ProductModuleStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderLineStatuses` (
    `ProductOrderLineStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderLineStatuses` PRIMARY KEY (`ProductOrderLineStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderLineTypes` (
    `ProductOrderLineTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderLineTypes` PRIMARY KEY (`ProductOrderLineTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderStatuses` (
    `ProductOrderStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderStatuses` PRIMARY KEY (`ProductOrderStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderTypes` (
    `ProductOrderTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderTypes` PRIMARY KEY (`ProductOrderTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductPackSizeTypes` (
    `ProductPackSizeTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductPackSizeTypes` PRIMARY KEY (`ProductPackSizeTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductReasons` (
    `ProductReasonId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductReasons` PRIMARY KEY (`ProductReasonId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductSizeTypes` (
    `ProductSizeTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductSizeTypes` PRIMARY KEY (`ProductSizeTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductSuppliers` (
    `ProductSupplierId` int NOT NULL AUTO_INCREMENT,
    `Code` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductSuppliers` PRIMARY KEY (`ProductSupplierId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductSystemStatuses` (
    `ProductSystemStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductSystemStatuses` PRIMARY KEY (`ProductSystemStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductTransactionStatuses` (
    `ProductTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductTransactionStatuses` PRIMARY KEY (`ProductTransactionStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductTransactionTypes` (
    `ProductTransactionTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsPositive` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductTransactionTypes` PRIMARY KEY (`ProductTransactionTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductVendors` (
    `ProductVendorId` int NOT NULL AUTO_INCREMENT,
    `Code` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductVendors` PRIMARY KEY (`ProductVendorId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductYears` (
    `ProductYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductYears` PRIMARY KEY (`ProductYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectCategories` (
    `ProjectCategoryId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectCategories` PRIMARY KEY (`ProjectCategoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectFileTypes` (
    `ProjectFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectFileTypes` PRIMARY KEY (`ProjectFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectPriorities` (
    `ProjectPriorityId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsEndDateRequired` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectPriorities` PRIMARY KEY (`ProjectPriorityId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectStatuses` (
    `ProjectStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectStatuses` PRIMARY KEY (`ProjectStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskPriorities` (
    `ProjectTaskPriorityId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskPriorities` PRIMARY KEY (`ProjectTaskPriorityId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskSeverities` (
    `ProjectTaskSeverityId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskSeverities` PRIMARY KEY (`ProjectTaskSeverityId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskStatuses` (
    `ProjectTaskStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskStatuses` PRIMARY KEY (`ProjectTaskStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskTypes` (
    `ProjectTaskTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskTypes` PRIMARY KEY (`ProjectTaskTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTypes` (
    `ProjectTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsDescriptionRequired` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTypes` PRIMARY KEY (`ProjectTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Report` (
    `ReportId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Report` PRIMARY KEY (`ReportId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Roles` (
    `RoleId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Roles` PRIMARY KEY (`RoleId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecurityOrganizations` (
    `SecurityOrganizationId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Slug` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `ExternalId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `OwnerId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SecurityOrganizations` PRIMARY KEY (`SecurityOrganizationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecurityPermissions` (
    `SecurityPermissionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SecurityPermissions` PRIMARY KEY (`SecurityPermissionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecurityRoles` (
    `SecurityRoleId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SecurityRoles` PRIMARY KEY (`SecurityRoleId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Settings` (
    `SettingId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Settings` PRIMARY KEY (`SettingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparAnalysisGroups` (
    `SparAnalysisGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparAnalysisGroups` PRIMARY KEY (`SparAnalysisGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparProductStatuses` (
    `SparProductStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparProductStatuses` PRIMARY KEY (`SparProductStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparSources` (
    `SparSourceId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparSources` PRIMARY KEY (`SparSourceId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparSubProductStatuses` (
    `SparSubProductStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparSubProductStatuses` PRIMARY KEY (`SparSubProductStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparSystemStatuses` (
    `SparSystemStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparSystemStatuses` PRIMARY KEY (`SparSystemStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparUnitTypes` (
    `SparUnitTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparUnitTypes` PRIMARY KEY (`SparUnitTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetConditions` (
    `StoreAssetConditionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetConditions` PRIMARY KEY (`StoreAssetConditionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetFileTypes` (
    `StoreAssetFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetFileTypes` PRIMARY KEY (`StoreAssetFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetStatuses` (
    `StoreAssetStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetStatuses` PRIMARY KEY (`StoreAssetStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetTypeContacts` (
    `StoreAssetTypeContactId` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `EmailAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MobilePhone` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetTypeContacts` PRIMARY KEY (`StoreAssetTypeContactId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreList` (
    `StoreListId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(220) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreList` PRIMARY KEY (`StoreListId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubWarehouses` (
    `SubWarehouseId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SubWarehouses` PRIMARY KEY (`SubWarehouseId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierBudgetTypes` (
    `SupplierBudgetTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierBudgetTypes` PRIMARY KEY (`SupplierBudgetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierBudgetVersionTypes` (
    `SupplierBudgetVersionTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierBudgetVersionTypes` PRIMARY KEY (`SupplierBudgetVersionTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractAmountTypes` (
    `SupplierContractAmountTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractAmountTypes` PRIMARY KEY (`SupplierContractAmountTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractDetailTypes` (
    `SupplierContractDetailTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractDetailTypes` PRIMARY KEY (`SupplierContractDetailTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractGroups` (
    `SupplierContractGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractGroups` PRIMARY KEY (`SupplierContractGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractSplits` (
    `SupplierContractSplitId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractSplits` PRIMARY KEY (`SupplierContractSplitId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractTypes` (
    `SupplierContractTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractTypes` PRIMARY KEY (`SupplierContractTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSalesLeads` (
    `SupplierSalesLeadId` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `KnownAs` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EmailAddress` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ContactNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSalesLeads` PRIMARY KEY (`SupplierSalesLeadId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSubContractDetailTypes` (
    `SupplierSubContractDetailTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubContractDetailTypes` PRIMARY KEY (`SupplierSubContractDetailTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSubContractTypes` (
    `SupplierSubContractTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubContractTypes` PRIMARY KEY (`SupplierSubContractTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierYears` (
    `SupplierYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierYears` PRIMARY KEY (`SupplierYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormOptions` (
    `SurveyFormOptionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `CompleteSurvey` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormOptions` PRIMARY KEY (`SurveyFormOptionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionTypes` (
    `SurveyFormQuestionTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormQuestionTypes` PRIMARY KEY (`SurveyFormQuestionTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionValueComparisonOperations` (
    `SurveyFormQuestionValueComparisonOperationId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SurveyFormQuestionValueComparisonOperations` PRIMARY KEY (`SurveyFormQuestionValueComparisonOperationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionValueComparisonTargetTypes` (
    `SurveyFormQuestionValueComparisonTargetTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SurveyFormQuestionValueComparisonTargetTypes` PRIMARY KEY (`SurveyFormQuestionValueComparisonTargetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormReasons` (
    `SurveyFormReasonId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `CompleteSurvey` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormReasons` PRIMARY KEY (`SurveyFormReasonId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormTypes` (
    `SurveyFormTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `HideEmployeeTargeting` tinyint(1) NOT NULL,
    `HideEngageSupplier` tinyint(1) NOT NULL,
    `HideEndDate` tinyint(1) NOT NULL,
    `HideRecurring` tinyint(1) NOT NULL,
    `HideStoreRecurring` tinyint(1) NOT NULL,
    `HideSurveyRequired` tinyint(1) NOT NULL,
    `HideAddQuestionGroup` tinyint(1) NOT NULL,
    `HideAddQuestion` tinyint(1) NOT NULL,
    `HideReorderGroup` tinyint(1) NOT NULL,
    `HideReorderQuestion` tinyint(1) NOT NULL,
    `HideDeleteQuestion` tinyint(1) NOT NULL,
    `HideDisableGroup` tinyint(1) NOT NULL,
    `HideDisableQuestion` tinyint(1) NOT NULL,
    `UseTemplate` tinyint(1) NOT NULL,
    `SurveyFormTemplateId` int NULL,
    `HideStoreTargeting` tinyint(1) NOT NULL,
    `HideGroupRules` tinyint(1) NOT NULL,
    `HideQuestionRules` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormTypes` PRIMARY KEY (`SurveyFormTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Targetings` (
    `TargetingId` int NOT NULL AUTO_INCREMENT,
    `Criteria` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Targetings` PRIMARY KEY (`TargetingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TargetStrategies` (
    `TargetStrategyId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TargetStrategies` PRIMARY KEY (`TargetStrategyId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingCategories` (
    `TrainingCategoryId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingCategories` PRIMARY KEY (`TrainingCategoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingDurations` (
    `TrainingDurationId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingDurations` PRIMARY KEY (`TrainingDurationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingFileTypes` (
    `TrainingFileTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingFileTypes` PRIMARY KEY (`TrainingFileTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingProviders` (
    `TrainingProviderId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingProviders` PRIMARY KEY (`TrainingProviderId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingTypes` (
    `TrainingTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingTypes` PRIMARY KEY (`TrainingTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingYears` (
    `TrainingYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingYears` PRIMARY KEY (`TrainingYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserPermissions` (
    `UserPermissionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserPermissions` PRIMARY KEY (`UserPermissionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserRoles` (
    `UserRoleId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserRoles` PRIMARY KEY (`UserRoleId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Vacancies` (
    `VacancyId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Vacancies` PRIMARY KEY (`VacancyId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Vat` (
    `VatId` int NOT NULL AUTO_INCREMENT,
    `Code` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Vat` PRIMARY KEY (`VatId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebFileGroups` (
    `WebFileGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebFileGroups` PRIMARY KEY (`WebFileGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebPages` (
    `WebPageId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebPages` PRIMARY KEY (`WebPageId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WhatsAppHistories` (
    `WhatsAppHistoryId` int NOT NULL AUTO_INCREMENT,
    `ToMobileNumber` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `FromMobileNumber` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `FromName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Message` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `ContentVariables` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `ExternalTemplateId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AttachmentUrls` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Error` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WhatsAppHistories` PRIMARY KEY (`WhatsAppHistoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AnalysisPillarSubGroups` (
    `AnalysisPillarSubGroupId` int NOT NULL AUTO_INCREMENT,
    `AnalysisPillarGroupId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_AnalysisPillarSubGroups` PRIMARY KEY (`AnalysisPillarSubGroupId`),
    CONSTRAINT `FK_AnalysisPillarSubGroups_AnalysisPillarGroups_AnalysisPillarG~` FOREIGN KEY (`AnalysisPillarGroupId`) REFERENCES `AnalysisPillarGroups` (`AnalysisPillarGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AuditEntryProperties` (
    `AuditEntryPropertyID` int NOT NULL AUTO_INCREMENT,
    `AuditEntryID` int NOT NULL,
    `RelationName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `PropertyName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `OldValue` longtext CHARACTER SET utf8mb4 NULL,
    `NewValue` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AuditEntryProperties` PRIMARY KEY (`AuditEntryPropertyID`),
    CONSTRAINT `FK_AuditEntryProperties_AuditEntries_AuditEntryID` FOREIGN KEY (`AuditEntryID`) REFERENCES `AuditEntries` (`AuditEntryID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `BudgetPeriods` (
    `BudgetPeriodId` int NOT NULL AUTO_INCREMENT,
    `BudgetYearId` int NOT NULL,
    `No` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_BudgetPeriods` PRIMARY KEY (`BudgetPeriodId`),
    CONSTRAINT `FK_BudgetPeriods_BudgetYears_BudgetYearId` FOREIGN KEY (`BudgetYearId`) REFERENCES `BudgetYears` (`BudgetYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimPeriods` (
    `ClaimPeriodId` int NOT NULL AUTO_INCREMENT,
    `Number` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `ClaimYearId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimPeriods` PRIMARY KEY (`ClaimPeriodId`),
    CONSTRAINT `FK_ClaimPeriods_ClaimYears_ClaimYearId` FOREIGN KEY (`ClaimYearId`) REFERENCES `ClaimYears` (`ClaimYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTemplates` (
    `CommunicationTemplateId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `ExternalTemplateId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FromName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FromEmailAddress` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FromMobileNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `Subject` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Body` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `CommunicationTemplateTypeId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CommunicationTemplates` PRIMARY KEY (`CommunicationTemplateId`),
    CONSTRAINT `FK_CommunicationTemplates_CommunicationTemplateTypes_Communicat~` FOREIGN KEY (`CommunicationTemplateTypeId`) REFERENCES `CommunicationTemplateTypes` (`CommunicationTemplateTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CommunicationTemplates_CommunicationTypes_CommunicationTypeId` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostSubDepartments` (
    `CostSubDepartmentId` int NOT NULL AUTO_INCREMENT,
    `CostDepartmentId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostSubDepartments` PRIMARY KEY (`CostSubDepartmentId`),
    CONSTRAINT `FK_CostSubDepartments_CostDepartments_CostDepartmentId` FOREIGN KEY (`CostDepartmentId`) REFERENCES `CostDepartments` (`CostDepartmentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostCenters` (
    `CostCenterId` int NOT NULL AUTO_INCREMENT,
    `CostTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostCenters` PRIMARY KEY (`CostCenterId`),
    CONSTRAINT `FK_CostCenters_CostTypes_CostTypeId` FOREIGN KEY (`CostTypeId`) REFERENCES `CostTypes` (`CostTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Creditors` (
    `CreditorId` int NOT NULL AUTO_INCREMENT,
    `CreditorStatusId` int NOT NULL,
    `Name` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `TradingName` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `IsVatRegistered` tinyint(1) NOT NULL,
    `VatNumber` varchar(200) CHARACTER SET utf8mb4 NULL,
    `BankConfirmationDate` datetime(6) NOT NULL,
    `EvolutionCreditorNumber` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Creditors` PRIMARY KEY (`CreditorId`),
    CONSTRAINT `FK_Creditors_CreditorStatuses_CreditorStatusId` FOREIGN KEY (`CreditorStatusId`) REFERENCES `CreditorStatuses` (`CreditorStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_DCProductClasses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DCDepartmentId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_DCProductClasses` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_DCProductClasses_DCDepartments_DCDepartmentId` FOREIGN KEY (`DCDepartmentId`) REFERENCES `DCDepartments` (`DCDepartmentId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DCDepts` (
    `DistributionCenterId` int NOT NULL,
    `DCDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_DCDepts` PRIMARY KEY (`DistributionCenterId`, `DCDepartmentId`),
    CONSTRAINT `FK_DCDepts_DCDepartments_DCDepartmentId` FOREIGN KEY (`DCDepartmentId`) REFERENCES `DCDepartments` (`DCDepartmentId`),
    CONSTRAINT `FK_DCDepts_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Warehouses` (
    `WarehouseId` int NOT NULL AUTO_INCREMENT,
    `DCId` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Warehouses` PRIMARY KEY (`WarehouseId`),
    CONSTRAINT `FK_Warehouses_DistributionCenters_DCId` FOREIGN KEY (`DCId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeJobTitleTimes` (
    `EmployeeJobTitleTimeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeJobTitleId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeJobTitleTimes` PRIMARY KEY (`EmployeeJobTitleTimeId`),
    CONSTRAINT `FK_EmployeeJobTitleTimes_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeJobTitleTypes` (
    `EmployeeJobTitleTypeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeJobTitleId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeJobTitleTypes` PRIMARY KEY (`EmployeeJobTitleTypeId`),
    CONSTRAINT `FK_EmployeeJobTitleTypes_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarPeriods` (
    `EmployeeStoreCalendarPeriodId` int NOT NULL AUTO_INCREMENT,
    `EmployeeStoreCalendarYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarPeriods` PRIMARY KEY (`EmployeeStoreCalendarPeriodId`),
    CONSTRAINT `FK_EmployeeStoreCalendarPeriods_EmployeeStoreCalendarYears_Empl~` FOREIGN KEY (`EmployeeStoreCalendarYearId`) REFERENCES `EmployeeStoreCalendarYears` (`EmployeeStoreCalendarYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTransactionTypes` (
    `EmployeeTransactionTypeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeTransactionTypeGroupId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsPositive` tinyint(1) NOT NULL,
    `IsRecurring` tinyint(1) NOT NULL,
    `Fields` json NULL,
    `OvertimeMultiple` float NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTransactionTypes` PRIMARY KEY (`EmployeeTransactionTypeId`),
    CONSTRAINT `FK_EmployeeTransactionTypes_EmployeeTransactionTypeGroups_Emplo~` FOREIGN KEY (`EmployeeTransactionTypeGroupId`) REFERENCES `EmployeeTransactionTypeGroups` (`EmployeeTransactionTypeGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectExternalUsers` (
    `ProjectExternalUserId` int NOT NULL AUTO_INCREMENT,
    `ExternalUserTypeId` int NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `CellNumber` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectExternalUsers` PRIMARY KEY (`ProjectExternalUserId`),
    CONSTRAINT `FK_ProjectExternalUsers_ExternalUserTypes_ExternalUserTypeId` FOREIGN KEY (`ExternalUserTypeId`) REFERENCES `ExternalUserTypes` (`ExternalUserTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductFilterUploads` (
    `ProductFilterUploadId` int NOT NULL AUTO_INCREMENT,
    `Filter` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Barcode` varchar(120) CHARACTER SET utf8mb4 NULL,
    `EngageVariantProductId` int NULL,
    `EngageVariantProductName` varchar(220) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileUploadId` int NOT NULL,
    `RowNo` int NOT NULL,
    `RowType` int NOT NULL,
    `RowMessage` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ProductFilterUploads` PRIMARY KEY (`ProductFilterUploadId`),
    CONSTRAINT `FK_ProductFilterUploads_FileUploads_FileUploadId` FOREIGN KEY (`FileUploadId`) REFERENCES `FileUploads` (`FileUploadId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreFilterUploads` (
    `StoreFilterUploadId` int NOT NULL AUTO_INCREMENT,
    `Filter` varchar(120) CHARACTER SET utf8mb4 NULL,
    `StoreId` int NULL,
    `StoreName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileUploadId` int NOT NULL,
    `RowNo` int NOT NULL,
    `RowType` int NOT NULL,
    `RowMessage` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_StoreFilterUploads` PRIMARY KEY (`StoreFilterUploadId`),
    CONSTRAINT `FK_StoreFilterUploads_FileUploads_FileUploadId` FOREIGN KEY (`FileUploadId`) REFERENCES `FileUploads` (`FileUploadId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Inventories` (
    `InventoryId` int NOT NULL AUTO_INCREMENT,
    `InventoryGroupId` int NOT NULL,
    `InventoryStatusId` int NOT NULL,
    `InventoryUnitTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `BarCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Inventories` PRIMARY KEY (`InventoryId`),
    CONSTRAINT `FK_Inventories_InventoryGroups_InventoryGroupId` FOREIGN KEY (`InventoryGroupId`) REFERENCES `InventoryGroups` (`InventoryGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Inventories_InventoryStatuses_InventoryStatusId` FOREIGN KEY (`InventoryStatusId`) REFERENCES `InventoryStatuses` (`InventoryStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Inventories_InventoryUnitTypes_InventoryUnitTypeId` FOREIGN KEY (`InventoryUnitTypeId`) REFERENCES `InventoryUnitTypes` (`InventoryUnitTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryPeriods` (
    `InventoryPeriodId` int NOT NULL AUTO_INCREMENT,
    `InventoryYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryPeriods` PRIMARY KEY (`InventoryPeriodId`),
    CONSTRAINT `FK_InventoryPeriods_InventoryYears_InventoryYearId` FOREIGN KEY (`InventoryYearId`) REFERENCES `InventoryYears` (`InventoryYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorBankAccounts` (
    `CreditorBankAccountId` int NOT NULL AUTO_INCREMENT,
    `BankNameId` int NOT NULL,
    `BankAccountTypeId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `AccountNumber` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `BranchCode` varchar(15) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorBankAccounts` PRIMARY KEY (`CreditorBankAccountId`),
    CONSTRAINT `FK_CreditorBankAccounts_opt_BankAccountTypes_BankAccountTypeId` FOREIGN KEY (`BankAccountTypeId`) REFERENCES `opt_BankAccountTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorBankAccounts_opt_BankNames_BankNameId` FOREIGN KEY (`BankNameId`) REFERENCES `opt_BankNames` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `BudgetYearVersions` (
    `BudgetYearId` int NOT NULL,
    `BudgetVersionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_BudgetYearVersions` PRIMARY KEY (`BudgetYearId`, `BudgetVersionId`),
    CONSTRAINT `FK_BudgetYearVersions_BudgetYears_BudgetYearId` FOREIGN KEY (`BudgetYearId`) REFERENCES `BudgetYears` (`BudgetYearId`),
    CONSTRAINT `FK_BudgetYearVersions_opt_BudgetVersions_BudgetVersionId` FOREIGN KEY (`BudgetVersionId`) REFERENCES `opt_BudgetVersions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailTemplates` (
    `EmailTemplateId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `ExternalTemplateId` longtext CHARACTER SET utf8mb4 NULL,
    `FromEmailName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FromEmailAddress` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `EmailTemplateTypeId` int NOT NULL,
    `EmailTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailTemplates` PRIMARY KEY (`EmailTemplateId`),
    CONSTRAINT `FK_EmailTemplates_opt_EmailTemplateTypes_EmailTemplateTypeId` FOREIGN KEY (`EmailTemplateTypeId`) REFERENCES `opt_EmailTemplateTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmailTemplates_opt_EmailTypes_EmailTypeId` FOREIGN KEY (`EmailTypeId`) REFERENCES `opt_EmailTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeBadges` (
    `EmployeeBadgeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeBadgeTypeId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Points` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeBadges` PRIMARY KEY (`EmployeeBadgeId`),
    CONSTRAINT `FK_EmployeeBadges_opt_EmployeeBadgeTypes_EmployeeBadgeTypeId` FOREIGN KEY (`EmployeeBadgeTypeId`) REFERENCES `opt_EmployeeBadgeTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeKpis` (
    `EmployeeKpiId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `EmployeeKpiTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeKpis` PRIMARY KEY (`EmployeeKpiId`),
    CONSTRAINT `FK_EmployeeKpis_opt_EmployeeKpiTypes_EmployeeKpiTypeId` FOREIGN KEY (`EmployeeKpiTypeId`) REFERENCES `opt_EmployeeKpiTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageDepartments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageDepartmentGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageDepartments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageDepartments_opt_EngageDepartmentGroups_EngageDepar~` FOREIGN KEY (`EngageDepartmentGroupId`) REFERENCES `opt_EngageDepartmentGroups` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Notifications` (
    `NotificationId` int NOT NULL AUTO_INCREMENT,
    `NotificationTypeId` int NOT NULL,
    `NotificationCategoryId` int NULL,
    `Title` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `Message` varchar(1500) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `IsNational` tinyint(1) NOT NULL,
    `Important` tinyint(1) NOT NULL,
    `Targeted` tinyint(1) NOT NULL,
    `Subject` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Notifications` PRIMARY KEY (`NotificationId`),
    CONSTRAINT `FK_Notifications_opt_NotificationCategories_NotificationCategor~` FOREIGN KEY (`NotificationCategoryId`) REFERENCES `opt_NotificationCategories` (`Id`),
    CONSTRAINT `FK_Notifications_opt_NotificationTypes_NotificationTypeId` FOREIGN KEY (`NotificationTypeId`) REFERENCES `opt_NotificationTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Promotions` (
    `PromotionId` int NOT NULL AUTO_INCREMENT,
    `PromotionTypeId` int NOT NULL,
    `Title` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `OrderStartDate` datetime(6) NOT NULL,
    `OrderEndDate` datetime(6) NULL,
    `Amount` decimal(65,30) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Promotions` PRIMARY KEY (`PromotionId`),
    CONSTRAINT `FK_Promotions_opt_PromotionTypes_PromotionTypeId` FOREIGN KEY (`PromotionTypeId`) REFERENCES `opt_PromotionTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetOwnerStoreAssetTypes` (
    `StoreAssetOwnerStoreAssetTypeId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetOwnerId` int NOT NULL,
    `StoreAssetTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetOwnerStoreAssetTypes` PRIMARY KEY (`StoreAssetOwnerStoreAssetTypeId`),
    CONSTRAINT `FK_StoreAssetOwnerStoreAssetTypes_opt_StoreAssetOwners_StoreAss~` FOREIGN KEY (`StoreAssetOwnerId`) REFERENCES `opt_StoreAssetOwners` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssetOwnerStoreAssetTypes_opt_StoreAssetTypes_StoreAsse~` FOREIGN KEY (`StoreAssetTypeId`) REFERENCES `opt_StoreAssetTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetSubTypes` (
    `StoreAssetSubTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StoreAssetTypeId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetSubTypes` PRIMARY KEY (`StoreAssetSubTypeId`),
    CONSTRAINT `FK_StoreAssetSubTypes_opt_StoreAssetTypes_StoreAssetTypeId` FOREIGN KEY (`StoreAssetTypeId`) REFERENCES `opt_StoreAssetTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageRegions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IsAllRegions` tinyint(1) NOT NULL,
    `IsApproveClaims` tinyint(1) NOT NULL,
    `IsClaimManager` tinyint(1) NOT NULL,
    `StoreSparRegionId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageRegions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageRegions_opt_StoreSparRegions_StoreSparRegionId` FOREIGN KEY (`StoreSparRegionId`) REFERENCES `opt_StoreSparRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebEvents` (
    `WebEventId` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Message` longtext CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `WebEventTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebEvents` PRIMARY KEY (`WebEventId`),
    CONSTRAINT `FK_WebEvents_opt_WebEventTypes_WebEventTypeId` FOREIGN KEY (`WebEventTypeId`) REFERENCES `opt_WebEventTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `OptionTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `OptionTypeGroupId` int NOT NULL,
    `IsSystemOption` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_OptionTypes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_OptionTypes_OptionTypeGroups_OptionTypeGroupId` FOREIGN KEY (`OptionTypeGroupId`) REFERENCES `OptionTypeGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderStagingSkus` (
    `OrderStagingSkuId` int NOT NULL AUTO_INCREMENT,
    `OrderStagingId` int NOT NULL,
    `ProductName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Barcode` varchar(120) CHARACTER SET utf8mb4 NULL,
    `UnitType` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Quantity` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderStagingSkus` PRIMARY KEY (`OrderStagingSkuId`),
    CONSTRAINT `FK_OrderStagingSkus_OrderStagings_OrderStagingId` FOREIGN KEY (`OrderStagingId`) REFERENCES `OrderStagings` (`OrderStagingId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplates` (
    `OrderTemplateId` int NOT NULL AUTO_INCREMENT,
    `OrderTemplateStatusId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplates` PRIMARY KEY (`OrderTemplateId`),
    CONSTRAINT `FK_OrderTemplates_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderTemplates_OrderTemplateStatuses_OrderTemplateStatusId` FOREIGN KEY (`OrderTemplateStatusId`) REFERENCES `OrderTemplateStatuses` (`OrderTemplateStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Organizations` (
    `OrganizationId` int NOT NULL AUTO_INCREMENT,
    `OrganizationSettingId` int NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `TenantIdentifier` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Settings` json NULL,
    `ThemeColor` longtext CHARACTER SET utf8mb4 NULL,
    `ThemeCustomColor` longtext CHARACTER SET utf8mb4 NULL,
    `JsonTheme` json NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Organizations` PRIMARY KEY (`OrganizationId`),
    CONSTRAINT `FK_Organizations_OrganizationSettings_OrganizationSettingId` FOREIGN KEY (`OrganizationSettingId`) REFERENCES `OrganizationSettings` (`OrganizationSettingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentPeriods` (
    `PaymentPeriodId` int NOT NULL AUTO_INCREMENT,
    `PaymentYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentPeriods` PRIMARY KEY (`PaymentPeriodId`),
    CONSTRAINT `FK_PaymentPeriods_PaymentYears_PaymentYearId` FOREIGN KEY (`PaymentYearId`) REFERENCES `PaymentYears` (`PaymentYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PayrollPeriods` (
    `PayrollPeriodId` int NOT NULL AUTO_INCREMENT,
    `PayrollYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PayrollPeriods` PRIMARY KEY (`PayrollPeriodId`),
    CONSTRAINT `FK_PayrollPeriods_PayrollYears_PayrollYearId` FOREIGN KEY (`PayrollYearId`) REFERENCES `PayrollYears` (`PayrollYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductSubGroups` (
    `ProductSubGroupId` int NOT NULL AUTO_INCREMENT,
    `ProductGroupId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductSubGroups` PRIMARY KEY (`ProductSubGroupId`),
    CONSTRAINT `FK_ProductSubGroups_ProductGroups_ProductGroupId` FOREIGN KEY (`ProductGroupId`) REFERENCES `ProductGroups` (`ProductGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductManufacturers` (
    `ProductManufacturerId` int NOT NULL AUTO_INCREMENT,
    `ProductSupplierId` int NOT NULL,
    `Code` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductManufacturers` PRIMARY KEY (`ProductManufacturerId`),
    CONSTRAINT `FK_ProductManufacturers_ProductSuppliers_ProductSupplierId` FOREIGN KEY (`ProductSupplierId`) REFERENCES `ProductSuppliers` (`ProductSupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductPeriods` (
    `ProductPeriodId` int NOT NULL AUTO_INCREMENT,
    `ProductYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductPeriods` PRIMARY KEY (`ProductPeriodId`),
    CONSTRAINT `FK_ProductPeriods_ProductYears_ProductYearId` FOREIGN KEY (`ProductYearId`) REFERENCES `ProductYears` (`ProductYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectSubTypes` (
    `ProjectSubTypeId` int NOT NULL AUTO_INCREMENT,
    `ProjectTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectSubTypes` PRIMARY KEY (`ProjectSubTypeId`),
    CONSTRAINT `FK_ProjectSubTypes_ProjectTypes_ProjectTypeId` FOREIGN KEY (`ProjectTypeId`) REFERENCES `ProjectTypes` (`ProjectTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecurityPermissionRoles` (
    `SecurityPermissionRoleId` int NOT NULL AUTO_INCREMENT,
    `SecurityRoleId` int NOT NULL,
    `SecurityPermissionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SecurityPermissionRoles` PRIMARY KEY (`SecurityPermissionRoleId`),
    CONSTRAINT `FK_SecurityPermissionRoles_SecurityPermissions_SecurityPermissi~` FOREIGN KEY (`SecurityPermissionId`) REFERENCES `SecurityPermissions` (`SecurityPermissionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SecurityPermissionRoles_SecurityRoles_SecurityRoleId` FOREIGN KEY (`SecurityRoleId`) REFERENCES `SecurityRoles` (`SecurityRoleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `TenantSettings` (
    `TenantSettingId` int NOT NULL AUTO_INCREMENT,
    `SettingId` int NOT NULL,
    `Value` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TenantSettings` PRIMARY KEY (`TenantSettingId`),
    CONSTRAINT `FK_TenantSettings_Settings_SettingId` FOREIGN KEY (`SettingId`) REFERENCES `Settings` (`SettingId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetTypeStoreAssetTypeContacts` (
    `StoreAssetTypeStoreAssetTypeContactId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetTypeId` int NOT NULL,
    `StoreAssetTypeContactId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetTypeStoreAssetTypeContacts` PRIMARY KEY (`StoreAssetTypeStoreAssetTypeContactId`),
    CONSTRAINT `FK_StoreAssetTypeStoreAssetTypeContacts_StoreAssetTypeContacts_~` FOREIGN KEY (`StoreAssetTypeContactId`) REFERENCES `StoreAssetTypeContacts` (`StoreAssetTypeContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssetTypeStoreAssetTypeContacts_opt_StoreAssetTypes_Sto~` FOREIGN KEY (`StoreAssetTypeId`) REFERENCES `opt_StoreAssetTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractSubGroups` (
    `SupplierContractSubGroupId` int NOT NULL AUTO_INCREMENT,
    `SupplierContractGroupId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractSubGroups` PRIMARY KEY (`SupplierContractSubGroupId`),
    CONSTRAINT `FK_SupplierContractSubGroups_SupplierContractGroups_SupplierCon~` FOREIGN KEY (`SupplierContractGroupId`) REFERENCES `SupplierContractGroups` (`SupplierContractGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSubContractDetails` (
    `SupplierSubContractDetailId` int NOT NULL AUTO_INCREMENT,
    `SupplierSubContractTypeId` int NOT NULL,
    `SupplierSubContractDetailTypeId` int NULL,
    `Detail` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubContractDetails` PRIMARY KEY (`SupplierSubContractDetailId`),
    CONSTRAINT `FK_SupplierSubContractDetails_SupplierSubContractDetailTypes_Su~` FOREIGN KEY (`SupplierSubContractDetailTypeId`) REFERENCES `SupplierSubContractDetailTypes` (`SupplierSubContractDetailTypeId`),
    CONSTRAINT `FK_SupplierSubContractDetails_SupplierSubContractTypes_Supplier~` FOREIGN KEY (`SupplierSubContractTypeId`) REFERENCES `SupplierSubContractTypes` (`SupplierSubContractTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierPeriods` (
    `SupplierPeriodId` int NOT NULL AUTO_INCREMENT,
    `SupplierYearId` int NOT NULL,
    `Number` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierPeriods` PRIMARY KEY (`SupplierPeriodId`),
    CONSTRAINT `FK_SupplierPeriods_SupplierYears_SupplierYearId` FOREIGN KEY (`SupplierYearId`) REFERENCES `SupplierYears` (`SupplierYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingPeriods` (
    `TrainingPeriodId` int NOT NULL AUTO_INCREMENT,
    `Number` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `TrainingYearId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingPeriods` PRIMARY KEY (`TrainingPeriodId`),
    CONSTRAINT `FK_TrainingPeriods_TrainingYears_TrainingYearId` FOREIGN KEY (`TrainingYearId`) REFERENCES `TrainingYears` (`TrainingYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserRolePermissions` (
    `UserRolePermissionId` int NOT NULL AUTO_INCREMENT,
    `UserRoleId` int NOT NULL,
    `UserPermissionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserRolePermissions` PRIMARY KEY (`UserRolePermissionId`),
    CONSTRAINT `FK_UserRolePermissions_UserPermissions_UserPermissionId` FOREIGN KEY (`UserPermissionId`) REFERENCES `UserPermissions` (`UserPermissionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserRolePermissions_UserRoles_UserRoleId` FOREIGN KEY (`UserRoleId`) REFERENCES `UserRoles` (`UserRoleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `VatPeriods` (
    `VatPeriodId` int NOT NULL AUTO_INCREMENT,
    `VatId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `Percent` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_VatPeriods` PRIMARY KEY (`VatPeriodId`),
    CONSTRAINT `FK_VatPeriods_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebFileCategories` (
    `WebFileCategoryId` int NOT NULL AUTO_INCREMENT,
    `WebFileGroupId` int NOT NULL,
    `Name` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `DisplayName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Order` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebFileCategories` PRIMARY KEY (`WebFileCategoryId`),
    CONSTRAINT `FK_WebFileCategories_WebFileGroups_WebFileGroupId` FOREIGN KEY (`WebFileGroupId`) REFERENCES `WebFileGroups` (`WebFileGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EvoLedgers` (
    `EvoLedgerId` int NOT NULL AUTO_INCREMENT,
    `LedgerCode` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `AnalysisPillarSubGroupId` int NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EvoLedgers` PRIMARY KEY (`EvoLedgerId`),
    CONSTRAINT `FK_EvoLedgers_AnalysisPillarSubGroups_AnalysisPillarSubGroupId` FOREIGN KEY (`AnalysisPillarSubGroupId`) REFERENCES `AnalysisPillarSubGroups` (`AnalysisPillarSubGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostCenterDepartments` (
    `CostCenterDepartmentId` int NOT NULL AUTO_INCREMENT,
    `CostCenterId` int NOT NULL,
    `CostDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostCenterDepartments` PRIMARY KEY (`CostCenterDepartmentId`),
    CONSTRAINT `FK_CostCenterDepartments_CostCenters_CostCenterId` FOREIGN KEY (`CostCenterId`) REFERENCES `CostCenters` (`CostCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CostCenterDepartments_CostDepartments_CostDepartmentId` FOREIGN KEY (`CostDepartmentId`) REFERENCES `CostDepartments` (`CostDepartmentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorFiles` (
    `CreditorFileId` int NOT NULL AUTO_INCREMENT,
    `CreditorId` int NOT NULL,
    `CreditorFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorFiles` PRIMARY KEY (`CreditorFileId`),
    CONSTRAINT `FK_CreditorFiles_CreditorFileTypes_CreditorFileTypeId` FOREIGN KEY (`CreditorFileTypeId`) REFERENCES `CreditorFileTypes` (`CreditorFileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorFiles_Creditors_CreditorId` FOREIGN KEY (`CreditorId`) REFERENCES `Creditors` (`CreditorId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorStatusHistories` (
    `CreditorStatusHistoryId` int NOT NULL AUTO_INCREMENT,
    `CreditorId` int NOT NULL,
    `CreditorStatusId` int NOT NULL,
    `Reason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CreditorStatusHistories` PRIMARY KEY (`CreditorStatusHistoryId`),
    CONSTRAINT `FK_CreditorStatusHistories_CreditorStatuses_CreditorStatusId` FOREIGN KEY (`CreditorStatusId`) REFERENCES `CreditorStatuses` (`CreditorStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorStatusHistories_Creditors_CreditorId` FOREIGN KEY (`CreditorId`) REFERENCES `Creditors` (`CreditorId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectExternalUserCommunicationTypes` (
    `ProjectExternalUserCommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `ProjectExternalUserId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectExternalUserCommunicationTypes` PRIMARY KEY (`ProjectExternalUserCommunicationTypeId`),
    CONSTRAINT `FK_ProjectExternalUserCommunicationTypes_CommunicationTypes_Com~` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectExternalUserCommunicationTypes_ProjectExternalUsers_P~` FOREIGN KEY (`ProjectExternalUserId`) REFERENCES `ProjectExternalUsers` (`ProjectExternalUserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `InventoryTransactions` (
    `InventoryTransactionId` int NOT NULL AUTO_INCREMENT,
    `InventoryTransactionTypeId` int NOT NULL,
    `InventoryTransactionStatusId` int NOT NULL,
    `InventoryId` int NOT NULL,
    `InventoryWarehouseId` int NOT NULL,
    `Quantity` float NOT NULL,
    `TransactionDate` datetime(6) NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_InventoryTransactions` PRIMARY KEY (`InventoryTransactionId`),
    CONSTRAINT `FK_InventoryTransactions_Inventories_InventoryId` FOREIGN KEY (`InventoryId`) REFERENCES `Inventories` (`InventoryId`) ON DELETE CASCADE,
    CONSTRAINT `FK_InventoryTransactions_InventoryTransactionStatuses_Inventory~` FOREIGN KEY (`InventoryTransactionStatusId`) REFERENCES `InventoryTransactionStatuses` (`InventoryTransactionStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_InventoryTransactions_InventoryTransactionTypes_InventoryTra~` FOREIGN KEY (`InventoryTransactionTypeId`) REFERENCES `InventoryTransactionTypes` (`InventoryTransactionTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_InventoryTransactions_InventoryWarehouses_InventoryWarehouse~` FOREIGN KEY (`InventoryWarehouseId`) REFERENCES `InventoryWarehouses` (`InventoryWarehouseId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailHistories` (
    `EmailHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmailTemplateId` int NOT NULL,
    `ToEmail` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Subject` varchar(300) CHARACTER SET utf8mb4 NULL,
    `Error` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailHistories` PRIMARY KEY (`EmailHistoryId`),
    CONSTRAINT `FK_EmailHistories_EmailTemplates_EmailTemplateId` FOREIGN KEY (`EmailTemplateId`) REFERENCES `EmailTemplates` (`EmailTemplateId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeKpiTiers` (
    `EmployeeKpiTierId` int NOT NULL AUTO_INCREMENT,
    `EmployeeKpiId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `No` int NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Points` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeKpiTiers` PRIMARY KEY (`EmployeeKpiTierId`),
    CONSTRAINT `FK_EmployeeKpiTiers_EmployeeKpis_EmployeeKpiId` FOREIGN KEY (`EmployeeKpiId`) REFERENCES `EmployeeKpis` (`EmployeeKpiId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageDepartmentCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageDepartmentCategories` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageDepartmentCategories_opt_EngageDepartments_EngageD~` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_StoreConcepts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageDepartmentId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreConcepts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_StoreConcepts_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `NotificationNotificationChannels` (
    `NotificationId` int NOT NULL,
    `NotificationChannelId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_NotificationNotificationChannels` PRIMARY KEY (`NotificationId`, `NotificationChannelId`),
    CONSTRAINT `FK_NotificationNotificationChannels_Notifications_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notifications` (`NotificationId`),
    CONSTRAINT `FK_NotificationNotificationChannels_opt_NotificationChannels_No~` FOREIGN KEY (`NotificationChannelId`) REFERENCES `opt_NotificationChannels` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetTypeStoreAssetSubTypes` (
    `StoreAssetTypeStoreAssetSubTypeId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetTypeId` int NOT NULL,
    `StoreAssetSubTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetTypeStoreAssetSubTypes` PRIMARY KEY (`StoreAssetTypeStoreAssetSubTypeId`),
    CONSTRAINT `FK_StoreAssetTypeStoreAssetSubTypes_StoreAssetSubTypes_StoreAss~` FOREIGN KEY (`StoreAssetSubTypeId`) REFERENCES `StoreAssetSubTypes` (`StoreAssetSubTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssetTypeStoreAssetSubTypes_opt_StoreAssetTypes_StoreAs~` FOREIGN KEY (`StoreAssetTypeId`) REFERENCES `opt_StoreAssetTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageSubRegions` (
    `EngageSubRegionId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EngageSubRegions` PRIMARY KEY (`EngageSubRegionId`),
    CONSTRAINT `FK_EngageSubRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `GLAccounts` (
    `GLAccountId` int NOT NULL AUTO_INCREMENT,
    `GLAccountTypeId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `Code` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_GLAccounts` PRIMARY KEY (`GLAccountId`),
    CONSTRAINT `FK_GLAccounts_GLAccountTypes_GLAccountTypeId` FOREIGN KEY (`GLAccountTypeId`) REFERENCES `GLAccountTypes` (`GLAccountTypeId`),
    CONSTRAINT `FK_GLAccounts_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `NotificationRegions` (
    `NotificationId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_NotificationRegions` PRIMARY KEY (`NotificationId`, `EngageRegionId`),
    CONSTRAINT `FK_NotificationRegions_Notifications_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notifications` (`NotificationId`),
    CONSTRAINT `FK_NotificationRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentBatchRegions` (
    `PaymentBatchRegionId` int NOT NULL AUTO_INCREMENT,
    `PaymentBatchId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentBatchRegions` PRIMARY KEY (`PaymentBatchRegionId`),
    CONSTRAINT `FK_PaymentBatchRegions_PaymentBatches_PaymentBatchId` FOREIGN KEY (`PaymentBatchId`) REFERENCES `PaymentBatches` (`PaymentBatchId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentBatchRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductWarehouses` (
    `ProductWarehouseId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NULL,
    `ParentId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductWarehouses` PRIMARY KEY (`ProductWarehouseId`),
    CONSTRAINT `FK_ProductWarehouses_ProductWarehouses_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`),
    CONSTRAINT `FK_ProductWarehouses_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectCampaigns` (
    `ProjectCampaignId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `EngageRegionId` int NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `Note` json NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectCampaigns` PRIMARY KEY (`ProjectCampaignId`),
    CONSTRAINT `FK_ProjectCampaigns_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StatsOrdersByRegions` (
    `StatsOrdersByRegionId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `OrdersLast1Day` int NOT NULL,
    `OrdersLast7Days` int NOT NULL,
    `OrdersAll` int NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StatsOrdersByRegions` PRIMARY KEY (`StatsOrdersByRegionId`),
    CONSTRAINT `FK_StatsOrdersByRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StatsStoresByRegions` (
    `StatsStoresByRegionId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `Stores` int NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StatsStoresByRegions` PRIMARY KEY (`StatsStoresByRegionId`),
    CONSTRAINT `FK_StatsStoresByRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserGroups` (
    `UserGroupId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `VendorId` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserGroups` PRIMARY KEY (`UserGroupId`),
    CONSTRAINT `FK_UserGroups_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplateGroups` (
    `OrderTemplateGroupId` int NOT NULL AUTO_INCREMENT,
    `OrderTemplateId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Order` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateGroups` PRIMARY KEY (`OrderTemplateGroupId`),
    CONSTRAINT `FK_OrderTemplateGroups_OrderTemplates_OrderTemplateId` FOREIGN KEY (`OrderTemplateId`) REFERENCES `OrderTemplates` (`OrderTemplateId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Payments` (
    `PaymentId` int NOT NULL AUTO_INCREMENT,
    `PaymentBatchId` int NOT NULL,
    `PaymentArchiveId` int NULL,
    `CreditorId` int NOT NULL,
    `PaymentStatusId` int NOT NULL,
    `VatId` int NULL,
    `PaymentPeriodId` int NOT NULL,
    `InvoiceNumber` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `InvoiceDate` datetime(6) NOT NULL,
    `IsClaimFromSupplier` tinyint(1) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Payments` PRIMARY KEY (`PaymentId`),
    CONSTRAINT `FK_Payments_Creditors_CreditorId` FOREIGN KEY (`CreditorId`) REFERENCES `Creditors` (`CreditorId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Payments_PaymentArchives_PaymentArchiveId` FOREIGN KEY (`PaymentArchiveId`) REFERENCES `PaymentArchives` (`PaymentArchiveId`),
    CONSTRAINT `FK_Payments_PaymentBatches_PaymentBatchId` FOREIGN KEY (`PaymentBatchId`) REFERENCES `PaymentBatches` (`PaymentBatchId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Payments_PaymentPeriods_PaymentPeriodId` FOREIGN KEY (`PaymentPeriodId`) REFERENCES `PaymentPeriods` (`PaymentPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Payments_PaymentStatuses_PaymentStatusId` FOREIGN KEY (`PaymentStatusId`) REFERENCES `PaymentStatuses` (`PaymentStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Payments_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductCategories` (
    `ProductCategoryId` int NOT NULL AUTO_INCREMENT,
    `ProductSubGroupId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductCategories` PRIMARY KEY (`ProductCategoryId`),
    CONSTRAINT `FK_ProductCategories_ProductSubGroups_ProductSubGroupId` FOREIGN KEY (`ProductSubGroupId`) REFERENCES `ProductSubGroups` (`ProductSubGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractAmounts` (
    `SupplierContractAmountId` int NOT NULL AUTO_INCREMENT,
    `SupplierSubContractDetailId` int NOT NULL,
    `SupplierContractAmountTypeId` int NOT NULL,
    `SupplierContractSplitId` int NULL,
    `Amount` float NOT NULL,
    `StartRangeAmount` float NULL,
    `EndRangeAmount` float NULL,
    `IsAmountPercent` tinyint(1) NOT NULL,
    `IsRangeAmountPercent` tinyint(1) NOT NULL,
    `Note` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractAmounts` PRIMARY KEY (`SupplierContractAmountId`),
    CONSTRAINT `FK_SupplierContractAmounts_SupplierContractAmountTypes_Supplier~` FOREIGN KEY (`SupplierContractAmountTypeId`) REFERENCES `SupplierContractAmountTypes` (`SupplierContractAmountTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContractAmounts_SupplierContractSplits_SupplierContr~` FOREIGN KEY (`SupplierContractSplitId`) REFERENCES `SupplierContractSplits` (`SupplierContractSplitId`),
    CONSTRAINT `FK_SupplierContractAmounts_SupplierSubContractDetails_SupplierS~` FOREIGN KEY (`SupplierSubContractDetailId`) REFERENCES `SupplierSubContractDetails` (`SupplierSubContractDetailId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierBudgetVersions` (
    `SupplierBudgetVersionId` int NOT NULL AUTO_INCREMENT,
    `SupplierPeriodId` int NOT NULL,
    `SupplierBudgetVersionTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierBudgetVersions` PRIMARY KEY (`SupplierBudgetVersionId`),
    CONSTRAINT `FK_SupplierBudgetVersions_SupplierBudgetVersionTypes_SupplierBu~` FOREIGN KEY (`SupplierBudgetVersionTypeId`) REFERENCES `SupplierBudgetVersionTypes` (`SupplierBudgetVersionTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierBudgetVersions_SupplierPeriods_SupplierPeriodId` FOREIGN KEY (`SupplierPeriodId`) REFERENCES `SupplierPeriods` (`SupplierPeriodId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Trainings` (
    `TrainingId` int NOT NULL AUTO_INCREMENT,
    `TrainingProviderId` int NULL,
    `TrainingTypeId` int NOT NULL,
    `EngageRegionId` int NULL,
    `TrainingCategoryId` int NULL,
    `TrainingPeriodId` int NULL,
    `TrainingDurationId` int NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `IsInternalTraining` tinyint(1) NOT NULL,
    `Site` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `NoOfParticipants` int NOT NULL,
    `Duration` longtext CHARACTER SET utf8mb4 NULL,
    `DirectCost` decimal(65,30) NOT NULL,
    `AdditionalCost` decimal(65,30) NOT NULL,
    `TotalCost` decimal(65,30) AS (DirectCost + AdditionalCost + AccommodationCost + CarHireCost + CateringCost + FlightsCost + FuelCost + StationeryCost + VenueCost + OtherCost),
    `AccommodationCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `CarHireCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `CateringCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `FlightsCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `FuelCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `StationeryCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `VenueCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `OtherCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Trainings` PRIMARY KEY (`TrainingId`),
    CONSTRAINT `FK_Trainings_TrainingCategories_TrainingCategoryId` FOREIGN KEY (`TrainingCategoryId`) REFERENCES `TrainingCategories` (`TrainingCategoryId`),
    CONSTRAINT `FK_Trainings_TrainingDurations_TrainingDurationId` FOREIGN KEY (`TrainingDurationId`) REFERENCES `TrainingDurations` (`TrainingDurationId`),
    CONSTRAINT `FK_Trainings_TrainingPeriods_TrainingPeriodId` FOREIGN KEY (`TrainingPeriodId`) REFERENCES `TrainingPeriods` (`TrainingPeriodId`),
    CONSTRAINT `FK_Trainings_TrainingProviders_TrainingProviderId` FOREIGN KEY (`TrainingProviderId`) REFERENCES `TrainingProviders` (`TrainingProviderId`),
    CONSTRAINT `FK_Trainings_TrainingTypes_TrainingTypeId` FOREIGN KEY (`TrainingTypeId`) REFERENCES `TrainingTypes` (`TrainingTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Trainings_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `NPrintingBatches` (
    `NPrintingBatchId` int NOT NULL AUTO_INCREMENT,
    `WebFileCategoryId` int NOT NULL,
    `FileTypeId` int NOT NULL,
    `Directory` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `Report` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `DisplayName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_NPrintingBatches` PRIMARY KEY (`NPrintingBatchId`),
    CONSTRAINT `FK_NPrintingBatches_FileTypes_FileTypeId` FOREIGN KEY (`FileTypeId`) REFERENCES `FileTypes` (`FileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_NPrintingBatches_WebFileCategories_WebFileCategoryId` FOREIGN KEY (`WebFileCategoryId`) REFERENCES `WebFileCategories` (`WebFileCategoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailHistoryCCEmails` (
    `EmailHistoryCCEmailId` int NOT NULL AUTO_INCREMENT,
    `EmailHistoryId` int NOT NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailHistoryCCEmails` PRIMARY KEY (`EmailHistoryCCEmailId`),
    CONSTRAINT `FK_EmailHistoryCCEmails_EmailHistories_EmailHistoryId` FOREIGN KEY (`EmailHistoryId`) REFERENCES `EmailHistories` (`EmailHistoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailHistoryTemplateVariables` (
    `EmailHistoryTemplateVariableId` int NOT NULL AUTO_INCREMENT,
    `EmailHistoryId` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ApproverName` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimNumber` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedReason` longtext CHARACTER SET utf8mb4 NULL,
    `DisputedReason` longtext CHARACTER SET utf8mb4 NULL,
    `CutOffDate` longtext CHARACTER SET utf8mb4 NULL,
    `TotalAmount` decimal(65,30) NULL,
    `StoreName` longtext CHARACTER SET utf8mb4 NULL,
    `EmployeeName` longtext CHARACTER SET utf8mb4 NULL,
    `TerminationDate` longtext CHARACTER SET utf8mb4 NULL,
    `TerminationReason` longtext CHARACTER SET utf8mb4 NULL,
    `TerminatorName` longtext CHARACTER SET utf8mb4 NULL,
    `EmployeeId` int NULL,
    `ReportDate` datetime(6) NULL,
    `SurveyInstanceId` int NULL,
    `OrderId` int NULL,
    `OrderDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailHistoryTemplateVariables` PRIMARY KEY (`EmailHistoryTemplateVariableId`),
    CONSTRAINT `FK_EmailHistoryTemplateVariables_EmailHistories_EmailHistoryId` FOREIGN KEY (`EmailHistoryId`) REFERENCES `EmailHistories` (`EmailHistoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageSubGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageGroupId` int NOT NULL,
    `EngageDepartmentCategoryId` int NOT NULL,
    `StoreDepartmentId` int NOT NULL,
    `EngageDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageSubGroups` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageSubGroups_opt_EngageDepartmentCategories_EngageDep~` FOREIGN KEY (`EngageDepartmentCategoryId`) REFERENCES `opt_EngageDepartmentCategories` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_opt_EngageSubGroups_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_opt_EngageSubGroups_opt_EngageGroups_EngageGroupId` FOREIGN KEY (`EngageGroupId`) REFERENCES `opt_EngageGroups` (`Id`),
    CONSTRAINT `FK_opt_EngageSubGroups_opt_StoreDepartments_StoreDepartmentId` FOREIGN KEY (`StoreDepartmentId`) REFERENCES `opt_StoreDepartments` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreConceptAttributes` (
    `StoreConceptAttributeId` int NOT NULL AUTO_INCREMENT,
    `StoreConceptId` int NOT NULL,
    `StoreConceptAttributeTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreConceptAttributes` PRIMARY KEY (`StoreConceptAttributeId`),
    CONSTRAINT `FK_StoreConceptAttributes_opt_StoreConceptAttributeTypes_StoreC~` FOREIGN KEY (`StoreConceptAttributeTypeId`) REFERENCES `opt_StoreConceptAttributeTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreConceptAttributes_opt_StoreConcepts_StoreConceptId` FOREIGN KEY (`StoreConceptId`) REFERENCES `opt_StoreConcepts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Budgets` (
    `BudgetId` int NOT NULL AUTO_INCREMENT,
    `GLAccountId` int NOT NULL,
    `BudgetTypeId` int NOT NULL,
    `BudgetYearId` int NOT NULL,
    `BudgetVersionId` int NOT NULL,
    `BudgetPeriodId` int NOT NULL,
    `Value` double NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Budgets` PRIMARY KEY (`BudgetId`),
    CONSTRAINT `FK_Budgets_BudgetPeriods_BudgetPeriodId` FOREIGN KEY (`BudgetPeriodId`) REFERENCES `BudgetPeriods` (`BudgetPeriodId`),
    CONSTRAINT `FK_Budgets_BudgetYears_BudgetYearId` FOREIGN KEY (`BudgetYearId`) REFERENCES `BudgetYears` (`BudgetYearId`),
    CONSTRAINT `FK_Budgets_GLAccounts_GLAccountId` FOREIGN KEY (`GLAccountId`) REFERENCES `GLAccounts` (`GLAccountId`),
    CONSTRAINT `FK_Budgets_opt_BudgetTypes_BudgetTypeId` FOREIGN KEY (`BudgetTypeId`) REFERENCES `opt_BudgetTypes` (`Id`),
    CONSTRAINT `FK_Budgets_opt_BudgetVersions_BudgetVersionId` FOREIGN KEY (`BudgetVersionId`) REFERENCES `opt_BudgetVersions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrders` (
    `ProductOrderId` int NOT NULL AUTO_INCREMENT,
    `OrderNumber` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ProductOrderStatusId` int NOT NULL,
    `ProductWarehouseId` int NOT NULL,
    `ProductWarehouseOutId` int NULL,
    `ProductOrderTypeId` int NOT NULL,
    `ProductPeriodId` int NOT NULL,
    `ProductSupplierId` int NULL,
    `OrderDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `Note` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrders` PRIMARY KEY (`ProductOrderId`),
    CONSTRAINT `FK_ProductOrders_ProductOrderStatuses_ProductOrderStatusId` FOREIGN KEY (`ProductOrderStatusId`) REFERENCES `ProductOrderStatuses` (`ProductOrderStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrders_ProductOrderTypes_ProductOrderTypeId` FOREIGN KEY (`ProductOrderTypeId`) REFERENCES `ProductOrderTypes` (`ProductOrderTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrders_ProductPeriods_ProductPeriodId` FOREIGN KEY (`ProductPeriodId`) REFERENCES `ProductPeriods` (`ProductPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrders_ProductSuppliers_ProductSupplierId` FOREIGN KEY (`ProductSupplierId`) REFERENCES `ProductSuppliers` (`ProductSupplierId`),
    CONSTRAINT `FK_ProductOrders_ProductWarehouses_ProductWarehouseId` FOREIGN KEY (`ProductWarehouseId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrders_ProductWarehouses_ProductWarehouseOutId` FOREIGN KEY (`ProductWarehouseOutId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductWarehouseRegions` (
    `ProductWarehouseId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ProductWarehouseRegions` PRIMARY KEY (`ProductWarehouseId`, `EngageRegionId`),
    CONSTRAINT `FK_ProductWarehouseRegions_ProductWarehouses_ProductWarehouseId` FOREIGN KEY (`ProductWarehouseId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`),
    CONSTRAINT `FK_ProductWarehouseRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeJobTitleUserGroups` (
    `EmployeeJobTitleUserGroupId` int NOT NULL AUTO_INCREMENT,
    `EmployeeJobTitleId` int NOT NULL,
    `UserGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeJobTitleUserGroups` PRIMARY KEY (`EmployeeJobTitleUserGroupId`),
    CONSTRAINT `FK_EmployeeJobTitleUserGroups_EmployeeJobTitles_EmployeeJobTitl~` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeJobTitleUserGroups_UserGroups_UserGroupId` FOREIGN KEY (`UserGroupId`) REFERENCES `UserGroups` (`UserGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `RoleUserGroups` (
    `RoleUserGroupId` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `UserGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_RoleUserGroups` PRIMARY KEY (`RoleUserGroupId`),
    CONSTRAINT `FK_RoleUserGroups_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`RoleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_RoleUserGroups_UserGroups_UserGroupId` FOREIGN KEY (`UserGroupId`) REFERENCES `UserGroups` (`UserGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLines` (
    `PaymentLineId` int NOT NULL AUTO_INCREMENT,
    `PaymentId` int NOT NULL,
    `ExpenseTypeId` int NOT NULL,
    `VatId` int NULL,
    `Amount` float NOT NULL,
    `VatAmount` float NOT NULL,
    `Quantity` int NULL,
    `IsVat` tinyint(1) NOT NULL,
    `IsSplitAmount` tinyint(1) NOT NULL,
    `HasQuote` tinyint(1) NOT NULL,
    `HasInvoice` tinyint(1) NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLines` PRIMARY KEY (`PaymentLineId`),
    CONSTRAINT `FK_PaymentLines_ExpenseTypes_ExpenseTypeId` FOREIGN KEY (`ExpenseTypeId`) REFERENCES `ExpenseTypes` (`ExpenseTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLines_Payments_PaymentId` FOREIGN KEY (`PaymentId`) REFERENCES `Payments` (`PaymentId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLines_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentProofPayments` (
    `PaymentProofPaymentId` int NOT NULL AUTO_INCREMENT,
    `PaymentId` int NOT NULL,
    `PaymentProofId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentProofPayments` PRIMARY KEY (`PaymentProofPaymentId`),
    CONSTRAINT `FK_PaymentProofPayments_PaymentProofs_PaymentProofId` FOREIGN KEY (`PaymentProofId`) REFERENCES `PaymentProofs` (`PaymentProofId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentProofPayments_Payments_PaymentId` FOREIGN KEY (`PaymentId`) REFERENCES `Payments` (`PaymentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentStatusHistories` (
    `PaymentStatusHistoryId` int NOT NULL AUTO_INCREMENT,
    `PaymentId` int NOT NULL,
    `PaymentStatusId` int NOT NULL,
    `Reason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentStatusHistories` PRIMARY KEY (`PaymentStatusHistoryId`),
    CONSTRAINT `FK_PaymentStatusHistories_PaymentStatuses_PaymentStatusId` FOREIGN KEY (`PaymentStatusId`) REFERENCES `PaymentStatuses` (`PaymentStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentStatusHistories_Payments_PaymentId` FOREIGN KEY (`PaymentId`) REFERENCES `Payments` (`PaymentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductSubCategories` (
    `ProductSubCategoryId` int NOT NULL AUTO_INCREMENT,
    `ProductCategoryId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductSubCategories` PRIMARY KEY (`ProductSubCategoryId`),
    CONSTRAINT `FK_ProductSubCategories_ProductCategories_ProductCategoryId` FOREIGN KEY (`ProductCategoryId`) REFERENCES `ProductCategories` (`ProductCategoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingFiles` (
    `TrainingFileId` int NOT NULL AUTO_INCREMENT,
    `TrainingId` int NOT NULL,
    `TrainingFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingFiles` PRIMARY KEY (`TrainingFileId`),
    CONSTRAINT `FK_TrainingFiles_TrainingFileTypes_TrainingFileTypeId` FOREIGN KEY (`TrainingFileTypeId`) REFERENCES `TrainingFileTypes` (`TrainingFileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_TrainingFiles_Trainings_TrainingId` FOREIGN KEY (`TrainingId`) REFERENCES `Trainings` (`TrainingId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `NPrintings` (
    `NPrintingId` int NOT NULL AUTO_INCREMENT,
    `NPrintingBatchId` int NOT NULL,
    `FileName` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ProcessedDate` datetime(6) NULL,
    `Error` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_NPrintings` PRIMARY KEY (`NPrintingId`),
    CONSTRAINT `FK_NPrintings_NPrintingBatches_NPrintingBatchId` FOREIGN KEY (`NPrintingBatchId`) REFERENCES `NPrintingBatches` (`NPrintingBatchId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailTemplateVariableClaimNumbers` (
    `EmailTemplateVariableClaimNumberId` int NOT NULL AUTO_INCREMENT,
    `EmailHistoryTemplateVariableId` int NOT NULL,
    `ClaimNo` longtext CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(65,30) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailTemplateVariableClaimNumbers` PRIMARY KEY (`EmailTemplateVariableClaimNumberId`),
    CONSTRAINT `FK_EmailTemplateVariableClaimNumbers_EmailHistoryTemplateVariab~` FOREIGN KEY (`EmailHistoryTemplateVariableId`) REFERENCES `EmailHistoryTemplateVariables` (`EmailHistoryTemplateVariableId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageSubGroupEngageBrands` (
    `EngageSubGroupId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageSubGroupEngageBrands` PRIMARY KEY (`EngageSubGroupId`, `EngageBrandId`),
    CONSTRAINT `FK_EngageSubGroupEngageBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`),
    CONSTRAINT `FK_EngageSubGroupEngageBrands_opt_EngageSubGroups_EngageSubGrou~` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageSubGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageCategories` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageCategories_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectSubCategories` (
    `ProjectSubCategoryId` int NOT NULL AUTO_INCREMENT,
    `ProjectCategoryId` int NOT NULL,
    `EngageSubGroupId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectSubCategories` PRIMARY KEY (`ProjectSubCategoryId`),
    CONSTRAINT `FK_ProjectSubCategories_ProjectCategories_ProjectCategoryId` FOREIGN KEY (`ProjectCategoryId`) REFERENCES `ProjectCategories` (`ProjectCategoryId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectSubCategories_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreConceptAttributeOptions` (
    `StoreConceptAttributeOptionId` int NOT NULL AUTO_INCREMENT,
    `StoreConceptAttributeId` int NOT NULL,
    `Option` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreConceptAttributeOptions` PRIMARY KEY (`StoreConceptAttributeOptionId`),
    CONSTRAINT `FK_StoreConceptAttributeOptions_StoreConceptAttributes_StoreCon~` FOREIGN KEY (`StoreConceptAttributeId`) REFERENCES `StoreConceptAttributes` (`StoreConceptAttributeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderHistories` (
    `ProductOrderHistoryId` int NOT NULL AUTO_INCREMENT,
    `ProductOrderId` int NOT NULL,
    `ProductOrderStatusId` int NOT NULL,
    `Reason` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderHistories` PRIMARY KEY (`ProductOrderHistoryId`),
    CONSTRAINT `FK_ProductOrderHistories_ProductOrderStatuses_ProductOrderStatu~` FOREIGN KEY (`ProductOrderStatusId`) REFERENCES `ProductOrderStatuses` (`ProductOrderStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrderHistories_ProductOrders_ProductOrderId` FOREIGN KEY (`ProductOrderId`) REFERENCES `ProductOrders` (`ProductOrderId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineCostCenters` (
    `PaymentLineCostCenterId` int NOT NULL AUTO_INCREMENT,
    `PaymentLineId` int NOT NULL,
    `CostCenterId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineCostCenters` PRIMARY KEY (`PaymentLineCostCenterId`),
    CONSTRAINT `FK_PaymentLineCostCenters_CostCenters_CostCenterId` FOREIGN KEY (`CostCenterId`) REFERENCES `CostCenters` (`CostCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLineCostCenters_PaymentLines_PaymentLineId` FOREIGN KEY (`PaymentLineId`) REFERENCES `PaymentLines` (`PaymentLineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineCostSubDepartments` (
    `PaymentLineCostSubDepartmentId` int NOT NULL AUTO_INCREMENT,
    `PaymentLineId` int NOT NULL,
    `CostSubDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineCostSubDepartments` PRIMARY KEY (`PaymentLineCostSubDepartmentId`),
    CONSTRAINT `FK_PaymentLineCostSubDepartments_CostSubDepartments_CostSubDepa~` FOREIGN KEY (`CostSubDepartmentId`) REFERENCES `CostSubDepartments` (`CostSubDepartmentId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLineCostSubDepartments_PaymentLines_PaymentLineId` FOREIGN KEY (`PaymentLineId`) REFERENCES `PaymentLines` (`PaymentLineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineDivisions` (
    `PaymentLineDivisionId` int NOT NULL AUTO_INCREMENT,
    `PaymentLineId` int NOT NULL,
    `EmployeeDivisionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineDivisions` PRIMARY KEY (`PaymentLineDivisionId`),
    CONSTRAINT `FK_PaymentLineDivisions_EmployeeDivisions_EmployeeDivisionId` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLineDivisions_PaymentLines_PaymentLineId` FOREIGN KEY (`PaymentLineId`) REFERENCES `PaymentLines` (`PaymentLineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineFiles` (
    `PaymentLineFileId` int NOT NULL AUTO_INCREMENT,
    `PaymentLineId` int NOT NULL,
    `PaymentLineFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineFiles` PRIMARY KEY (`PaymentLineFileId`),
    CONSTRAINT `FK_PaymentLineFiles_PaymentLineFileTypes_PaymentLineFileTypeId` FOREIGN KEY (`PaymentLineFileTypeId`) REFERENCES `PaymentLineFileTypes` (`PaymentLineFileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLineFiles_PaymentLines_PaymentLineId` FOREIGN KEY (`PaymentLineId`) REFERENCES `PaymentLines` (`PaymentLineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductMasters` (
    `ProductMasterId` int NOT NULL AUTO_INCREMENT,
    `ProductBrandId` int NOT NULL,
    `ProductReasonId` int NOT NULL,
    `ProductSubCategoryId` int NOT NULL,
    `ProductMasterStatusId` int NOT NULL,
    `ProductMasterSystemStatusId` int NOT NULL,
    `ProductVendorId` int NOT NULL,
    `ProductManufacturerId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `LedgerCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductMasters` PRIMARY KEY (`ProductMasterId`),
    CONSTRAINT `FK_ProductMasters_ProductBrands_ProductBrandId` FOREIGN KEY (`ProductBrandId`) REFERENCES `ProductBrands` (`ProductBrandId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductManufacturers_ProductManufacturerId` FOREIGN KEY (`ProductManufacturerId`) REFERENCES `ProductManufacturers` (`ProductManufacturerId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductMasterStatuses_ProductMasterStatusId` FOREIGN KEY (`ProductMasterStatusId`) REFERENCES `ProductMasterStatuses` (`ProductMasterStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductMasterSystemStatuses_ProductMasterSyst~` FOREIGN KEY (`ProductMasterSystemStatusId`) REFERENCES `ProductMasterSystemStatuses` (`ProductMasterSystemStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductReasons_ProductReasonId` FOREIGN KEY (`ProductReasonId`) REFERENCES `ProductReasons` (`ProductReasonId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductSubCategories_ProductSubCategoryId` FOREIGN KEY (`ProductSubCategoryId`) REFERENCES `ProductSubCategories` (`ProductSubCategoryId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductMasters_ProductVendors_ProductVendorId` FOREIGN KEY (`ProductVendorId`) REFERENCES `ProductVendors` (`ProductVendorId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_EngageSubCategories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EngageCategoryId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EngageSubCategories` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_opt_EngageSubCategories_opt_EngageCategories_EngageCategoryId` FOREIGN KEY (`EngageCategoryId`) REFERENCES `opt_EngageCategories` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductMasterColors` (
    `ProductMasterColorId` int NOT NULL AUTO_INCREMENT,
    `ProductMasterId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductMasterColors` PRIMARY KEY (`ProductMasterColorId`),
    CONSTRAINT `FK_ProductMasterColors_ProductMasters_ProductMasterId` FOREIGN KEY (`ProductMasterId`) REFERENCES `ProductMasters` (`ProductMasterId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductMasterSizes` (
    `ProductMasterSizeId` int NOT NULL AUTO_INCREMENT,
    `ProductMasterId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductMasterSizes` PRIMARY KEY (`ProductMasterSizeId`),
    CONSTRAINT `FK_ProductMasterSizes_ProductMasters_ProductMasterId` FOREIGN KEY (`ProductMasterId`) REFERENCES `ProductMasters` (`ProductMasterId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductAnalyses` (
    `ProductAnalysisId` int NOT NULL AUTO_INCREMENT,
    `ProductAnalysisGroupId` int NOT NULL,
    `ProductAnalysisDivisionId` int NOT NULL,
    `EngageGroupId` int NOT NULL,
    `EngageSubGroupId` int NOT NULL,
    `EngageCategoryId` int NOT NULL,
    `EngageSubCategoryId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `Supplier` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Vendor` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Manufacturer` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Product` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ProductDescription` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Size` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Key` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Barcode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `LedgerCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Listed` int NOT NULL,
    `New` int NOT NULL,
    `Sold` int NOT NULL,
    `IsButchery` tinyint(1) NOT NULL,
    `IsBakery` tinyint(1) NOT NULL,
    `IsFresh` tinyint(1) NOT NULL,
    `IsHmr` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductAnalyses` PRIMARY KEY (`ProductAnalysisId`),
    CONSTRAINT `FK_ProductAnalyses_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_EngageCategories_EngageCategoryId` FOREIGN KEY (`EngageCategoryId`) REFERENCES `opt_EngageCategories` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_EngageGroups_EngageGroupId` FOREIGN KEY (`EngageGroupId`) REFERENCES `opt_EngageGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_EngageSubCategories_EngageSubCategoryId` FOREIGN KEY (`EngageSubCategoryId`) REFERENCES `opt_EngageSubCategories` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_ProductAnalysisDivisions_ProductAnalysis~` FOREIGN KEY (`ProductAnalysisDivisionId`) REFERENCES `opt_ProductAnalysisDivisions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductAnalyses_opt_ProductAnalysisGroups_ProductAnalysisGro~` FOREIGN KEY (`ProductAnalysisGroupId`) REFERENCES `opt_ProductAnalysisGroups` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Products` (
    `ProductId` int NOT NULL AUTO_INCREMENT,
    `ProductMasterId` int NOT NULL,
    `RelatedProductId` int NULL,
    `ProductWarehouseId` int NOT NULL,
    `ProductSizeTypeId` int NOT NULL,
    `ProductPackSizeTypeId` int NOT NULL,
    `ProductModuleStatusId` int NOT NULL,
    `ProductSystemStatusId` int NOT NULL,
    `ProductMasterColorId` int NULL,
    `ProductMasterSizeId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `ProductSize` float NOT NULL,
    `ProductPackSize` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`ProductId`),
    CONSTRAINT `FK_Products_ProductMasterColors_ProductMasterColorId` FOREIGN KEY (`ProductMasterColorId`) REFERENCES `ProductMasterColors` (`ProductMasterColorId`),
    CONSTRAINT `FK_Products_ProductMasterSizes_ProductMasterSizeId` FOREIGN KEY (`ProductMasterSizeId`) REFERENCES `ProductMasterSizes` (`ProductMasterSizeId`),
    CONSTRAINT `FK_Products_ProductMasters_ProductMasterId` FOREIGN KEY (`ProductMasterId`) REFERENCES `ProductMasters` (`ProductMasterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductModuleStatuses_ProductModuleStatusId` FOREIGN KEY (`ProductModuleStatusId`) REFERENCES `ProductModuleStatuses` (`ProductModuleStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductPackSizeTypes_ProductPackSizeTypeId` FOREIGN KEY (`ProductPackSizeTypeId`) REFERENCES `ProductPackSizeTypes` (`ProductPackSizeTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductSizeTypes_ProductSizeTypeId` FOREIGN KEY (`ProductSizeTypeId`) REFERENCES `ProductSizeTypes` (`ProductSizeTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductSystemStatuses_ProductSystemStatusId` FOREIGN KEY (`ProductSystemStatusId`) REFERENCES `ProductSystemStatuses` (`ProductSystemStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_ProductWarehouses_ProductWarehouseId` FOREIGN KEY (`ProductWarehouseId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Products_Products_RelatedProductId` FOREIGN KEY (`RelatedProductId`) REFERENCES `Products` (`ProductId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductOrderLines` (
    `ProductOrderLineId` int NOT NULL AUTO_INCREMENT,
    `ProductOrderId` int NOT NULL,
    `ProductId` int NOT NULL,
    `ProductOrderLineStatusId` int NOT NULL,
    `ProductOrderLineTypeId` int NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `Quantity` float NOT NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductOrderLines` PRIMARY KEY (`ProductOrderLineId`),
    CONSTRAINT `FK_ProductOrderLines_ProductOrderLineStatuses_ProductOrderLineS~` FOREIGN KEY (`ProductOrderLineStatusId`) REFERENCES `ProductOrderLineStatuses` (`ProductOrderLineStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrderLines_ProductOrderLineTypes_ProductOrderLineType~` FOREIGN KEY (`ProductOrderLineTypeId`) REFERENCES `ProductOrderLineTypes` (`ProductOrderLineTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrderLines_ProductOrders_ProductOrderId` FOREIGN KEY (`ProductOrderId`) REFERENCES `ProductOrders` (`ProductOrderId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductOrderLines_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductPrices` (
    `ProductPriceId` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `Price` decimal(65,30) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductPrices` PRIMARY KEY (`ProductPriceId`),
    CONSTRAINT `FK_ProductPrices_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductWarehouseSummaries` (
    `ProductWarehouseSummaryId` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `ProductWarehouseId` int NOT NULL,
    `ProductPeriodId` int NOT NULL,
    `EngageRegionId` int NULL,
    `Quantity` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductWarehouseSummaries` PRIMARY KEY (`ProductWarehouseSummaryId`),
    CONSTRAINT `FK_ProductWarehouseSummaries_ProductPeriods_ProductPeriodId` FOREIGN KEY (`ProductPeriodId`) REFERENCES `ProductPeriods` (`ProductPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductWarehouseSummaries_ProductWarehouses_ProductWarehouse~` FOREIGN KEY (`ProductWarehouseId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductWarehouseSummaries_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductWarehouseSummaries_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryFiles` (
    `CategoryFileId` int NOT NULL AUTO_INCREMENT,
    `CategoryFileTypeId` int NOT NULL,
    `StoreId` int NULL,
    `CategoryGroupId` int NULL,
    `CategorySubGroupId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Files` json NULL,
    `TargetRule` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryFiles` PRIMARY KEY (`CategoryFileId`),
    CONSTRAINT `FK_CategoryFiles_CategoryFileTypes_CategoryFileTypeId` FOREIGN KEY (`CategoryFileTypeId`) REFERENCES `CategoryFileTypes` (`CategoryFileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFiles_CategoryGroups_CategoryGroupId` FOREIGN KEY (`CategoryGroupId`) REFERENCES `CategoryGroups` (`CategoryGroupId`),
    CONSTRAINT `FK_CategoryFiles_CategorySubGroups_CategorySubGroupId` FOREIGN KEY (`CategorySubGroupId`) REFERENCES `CategorySubGroups` (`CategorySubGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryFileTargets` (
    `CategoryFileTargetId` int NOT NULL AUTO_INCREMENT,
    `CategoryFileId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `CategoryGroupId` int NULL,
    `EmployeeId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EngageRegionId` int NULL,
    `EngageSubGroupId` int NULL,
    `StoreId` int NULL,
    `StoreFormatId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryFileTargets` PRIMARY KEY (`CategoryFileTargetId`),
    CONSTRAINT `FK_CategoryFileTargets_CategoryFiles_CategoryFileId` FOREIGN KEY (`CategoryFileId`) REFERENCES `CategoryFiles` (`CategoryFileId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFileTargets_CategoryGroups_CategoryGroupId` FOREIGN KEY (`CategoryGroupId`) REFERENCES `CategoryGroups` (`CategoryGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFileTargets_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFileTargets_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFileTargets_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CategoryFileTargets_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryStoreGroups` (
    `CategoryStoreGroupId` int NOT NULL AUTO_INCREMENT,
    `CategoryGroupId` int NOT NULL,
    `StoreId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryStoreGroups` PRIMARY KEY (`CategoryStoreGroupId`),
    CONSTRAINT `FK_CategoryStoreGroups_CategoryGroups_CategoryGroupId` FOREIGN KEY (`CategoryGroupId`) REFERENCES `CategoryGroups` (`CategoryGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryTargetAnswerHistories` (
    `CategoryTargetAnswerHistoryId` int NOT NULL AUTO_INCREMENT,
    `CategoryTargetAnswerId` int NOT NULL,
    `CategoryTargetId` int NOT NULL,
    `CategoryTargetStoreId` int NOT NULL,
    `EmployeeId` int NULL,
    `Target` float NULL,
    `Available` float NULL,
    `Occupied` float NULL,
    `LastUserVerifiedDate` datetime(6) NULL,
    `IsNotApplicable` tinyint(1) NOT NULL,
    `TextAnswer` longtext CHARACTER SET utf8mb4 NULL,
    `CategoryTargetTypeId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryTargetAnswerHistories` PRIMARY KEY (`CategoryTargetAnswerHistoryId`),
    CONSTRAINT `FK_CategoryTargetAnswerHistories_CategoryTargetTypes_CategoryTa~` FOREIGN KEY (`CategoryTargetTypeId`) REFERENCES `CategoryTargetTypes` (`CategoryTargetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryTargetAnswers` (
    `CategoryTargetAnswerId` int NOT NULL AUTO_INCREMENT,
    `CategoryTargetId` int NOT NULL,
    `CategoryTargetStoreId` int NOT NULL,
    `EmployeeId` int NULL,
    `Target` float NOT NULL,
    `Available` float NOT NULL,
    `Occupied` float NOT NULL,
    `LastUserVerifiedDate` datetime(6) NULL,
    `IsNotApplicable` tinyint(1) NOT NULL,
    `TextAnswer` longtext CHARACTER SET utf8mb4 NULL,
    `CategoryTargetTypeId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryTargetAnswers` PRIMARY KEY (`CategoryTargetAnswerId`),
    CONSTRAINT `FK_CategoryTargetAnswers_CategoryTargetTypes_CategoryTargetType~` FOREIGN KEY (`CategoryTargetTypeId`) REFERENCES `CategoryTargetTypes` (`CategoryTargetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryTargets` (
    `CategoryTargetId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `Target` float NOT NULL,
    `AvailableLabel` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `OccupiedLabel` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TextQuestion` longtext CHARACTER SET utf8mb4 NULL,
    `CategoryTargetTypeId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CategoryTargets` PRIMARY KEY (`CategoryTargetId`),
    CONSTRAINT `FK_CategoryTargets_CategoryTargetTypes_CategoryTargetTypeId` FOREIGN KEY (`CategoryTargetTypeId`) REFERENCES `CategoryTargetTypes` (`CategoryTargetTypeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryTargetStores` (
    `CategoryTargetStoreId` int NOT NULL AUTO_INCREMENT,
    `CategoryTargetId` int NOT NULL,
    `StoreId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_CategoryTargetStores` PRIMARY KEY (`CategoryTargetStoreId`),
    CONSTRAINT `FK_CategoryTargetStores_CategoryTargets_CategoryTargetId` FOREIGN KEY (`CategoryTargetId`) REFERENCES `CategoryTargets` (`CategoryTargetId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimBatchDetails` (
    `ClaimBatchDetailId` int NOT NULL AUTO_INCREMENT,
    `ClaimBatchId` int NOT NULL,
    `ClaimId` int NOT NULL,
    `Message` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimBatchDetails` PRIMARY KEY (`ClaimBatchDetailId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimBatches` (
    `ClaimBatchId` int NOT NULL AUTO_INCREMENT,
    `ClaimStatusId` int NULL,
    `ClaimSupplierStatusId` int NULL,
    `ClaimClassificationId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimBatches` PRIMARY KEY (`ClaimBatchId`),
    CONSTRAINT `FK_ClaimBatches_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`),
    CONSTRAINT `FK_ClaimBatches_opt_ClaimSupplierStatuses_ClaimSupplierStatusId` FOREIGN KEY (`ClaimSupplierStatusId`) REFERENCES `opt_ClaimSupplierStatuses` (`Id`),
    CONSTRAINT `FK_ClaimBatches_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimClassifications` (
    `ClaimClassificationId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsPayStore` tinyint(1) NOT NULL,
    `EditIsPayStore` tinyint(1) NOT NULL,
    `IsClaimFromSupplier` tinyint(1) NOT NULL,
    `EditIsClaimFromSupplier` tinyint(1) NOT NULL,
    `IsDairy` tinyint(1) NOT NULL,
    `IsSupplierProcess` tinyint(1) NOT NULL,
    `ClaimTypeId` int NULL,
    `SupplierId` int NULL,
    `IsAttachmentRequiredOnSubmit` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimClassifications` PRIMARY KEY (`ClaimClassificationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimHistoryHeaders` (
    `ClaimHistoryHeaderId` int NOT NULL AUTO_INCREMENT,
    `ClaimStatusId` int NULL,
    `ClaimSupplierStatusId` int NULL,
    `ClaimClassificationId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimHistoryHeaders` PRIMARY KEY (`ClaimHistoryHeaderId`),
    CONSTRAINT `FK_ClaimHistoryHeaders_ClaimClassifications_ClaimClassification~` FOREIGN KEY (`ClaimClassificationId`) REFERENCES `ClaimClassifications` (`ClaimClassificationId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimHistoryHeaders_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`),
    CONSTRAINT `FK_ClaimHistoryHeaders_opt_ClaimSupplierStatuses_ClaimSupplierS~` FOREIGN KEY (`ClaimSupplierStatusId`) REFERENCES `opt_ClaimSupplierStatuses` (`Id`),
    CONSTRAINT `FK_ClaimHistoryHeaders_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimClassificationTypes` (
    `ClaimClassificationId` int NOT NULL,
    `ClaimTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ClaimClassificationTypes` PRIMARY KEY (`ClaimClassificationId`, `ClaimTypeId`),
    CONSTRAINT `FK_ClaimClassificationTypes_ClaimClassifications_ClaimClassific~` FOREIGN KEY (`ClaimClassificationId`) REFERENCES `ClaimClassifications` (`ClaimClassificationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimFloatClaims` (
    `ClaimFloatId` int NOT NULL,
    `ClaimId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ClaimFloatClaims` PRIMARY KEY (`ClaimFloatId`, `ClaimId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimFloats` (
    `ClaimFloatId` int NOT NULL AUTO_INCREMENT,
    `ClaimTypeId` int NULL,
    `EngageRegionId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `MinimumAmount` decimal(65,30) NOT NULL,
    `Title` varchar(300) CHARACTER SET utf8mb4 NULL,
    `Reference` varchar(220) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `TopUpAmount` decimal(65,30) NULL,
    `LastToppedUp` datetime(6) NULL,
    `LastToppedUpBy` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimFloats` PRIMARY KEY (`ClaimFloatId`),
    CONSTRAINT `FK_ClaimFloats_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimFloatTopUpHistories` (
    `ClaimFloatTopUpHistoryId` int NOT NULL AUTO_INCREMENT,
    `ClaimFloatId` int NOT NULL,
    `TopUpAmount` decimal(65,30) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimFloatTopUpHistories` PRIMARY KEY (`ClaimFloatTopUpHistoryId`),
    CONSTRAINT `FK_ClaimFloatTopUpHistories_ClaimFloats_ClaimFloatId` FOREIGN KEY (`ClaimFloatId`) REFERENCES `ClaimFloats` (`ClaimFloatId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimHistories` (
    `ClaimHistoryId` int NOT NULL AUTO_INCREMENT,
    `ClaimHistoryHeaderId` int NULL,
    `ClaimId` int NOT NULL,
    `ClaimStatusId` int NOT NULL,
    `ClaimSupplierStatusId` int NULL,
    `ClaimPendingReasonId` int NULL,
    `PendingReason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `ClaimRejectedReasonId` int NULL,
    `RejectedReason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimHistories` PRIMARY KEY (`ClaimHistoryId`),
    CONSTRAINT `FK_ClaimHistories_ClaimHistoryHeaders_ClaimHistoryHeaderId` FOREIGN KEY (`ClaimHistoryHeaderId`) REFERENCES `ClaimHistoryHeaders` (`ClaimHistoryHeaderId`),
    CONSTRAINT `FK_ClaimHistories_opt_ClaimPendingReasons_ClaimPendingReasonId` FOREIGN KEY (`ClaimPendingReasonId`) REFERENCES `opt_ClaimPendingReasons` (`Id`),
    CONSTRAINT `FK_ClaimHistories_opt_ClaimRejectedReasons_ClaimRejectedReasonId` FOREIGN KEY (`ClaimRejectedReasonId`) REFERENCES `opt_ClaimRejectedReasons` (`Id`),
    CONSTRAINT `FK_ClaimHistories_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimHistories_opt_ClaimSupplierStatuses_ClaimSupplierStatus~` FOREIGN KEY (`ClaimSupplierStatusId`) REFERENCES `opt_ClaimSupplierStatuses` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimNotificationUsers` (
    `ClaimNotificationUserId` int NOT NULL AUTO_INCREMENT,
    `ClaimStatusId` int NOT NULL,
    `UserId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ClaimNotificationUsers` PRIMARY KEY (`ClaimNotificationUserId`),
    CONSTRAINT `FK_ClaimNotificationUsers_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimNotificationUsers_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Claims` (
    `ClaimId` int NOT NULL AUTO_INCREMENT,
    `ClaimNumber` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsPayStore` tinyint(1) NOT NULL,
    `IsClaimFromSupplier` tinyint(1) NOT NULL,
    `IsVatInclusive` tinyint(1) NOT NULL,
    `IsDairy` tinyint(1) NOT NULL,
    `ClaimDate` datetime(6) NOT NULL,
    `ClaimReference` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Comment` varchar(300) CHARACTER SET utf8mb4 NULL,
    `UnapprovedDate` datetime(6) NULL,
    `UnapprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ApprovedDate` datetime(6) NULL,
    `ApprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `SupplierApprovedDate` datetime(6) NULL,
    `SupplierApprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `PaidDate` datetime(6) NULL,
    `PaidBy` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedDate` datetime(6) NULL,
    `RejectedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimRejectedReasonId` int NULL,
    `RejectedReason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `PendingDate` datetime(6) NULL,
    `PendingBy` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimPendingReasonId` int NULL,
    `PendingReason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `ClientTypeId` int NOT NULL,
    `ClaimTypeId` int NOT NULL,
    `ClaimStatusId` int NOT NULL,
    `ClaimSupplierStatusId` int NOT NULL,
    `ClaimClassificationId` int NOT NULL,
    `VatId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `StoreId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `ClaimPeriodId` int NOT NULL,
    `ClaimAccountManagerId` int NULL,
    `ClaimManagerId` int NULL,
    `ClaimFloatId` int NULL,
    `EmployeeDivisionId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Claims` PRIMARY KEY (`ClaimId`),
    CONSTRAINT `FK_Claims_ClaimClassifications_ClaimClassificationId` FOREIGN KEY (`ClaimClassificationId`) REFERENCES `ClaimClassifications` (`ClaimClassificationId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_ClaimFloats_ClaimFloatId` FOREIGN KEY (`ClaimFloatId`) REFERENCES `ClaimFloats` (`ClaimFloatId`),
    CONSTRAINT `FK_Claims_ClaimPeriods_ClaimPeriodId` FOREIGN KEY (`ClaimPeriodId`) REFERENCES `ClaimPeriods` (`ClaimPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_EmployeeDivisions_EmployeeDivisionId` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`),
    CONSTRAINT `FK_Claims_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_opt_ClaimSupplierStatuses_ClaimSupplierStatusId` FOREIGN KEY (`ClaimSupplierStatusId`) REFERENCES `opt_ClaimSupplierStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Claims_opt_ClientTypes_ClientTypeId` FOREIGN KEY (`ClientTypeId`) REFERENCES `opt_ClientTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EntityBlobs` (
    `EntityBlobId` int NOT NULL AUTO_INCREMENT,
    `FolderName` varchar(2000) CHARACTER SET utf8mb4 NOT NULL,
    `OriginalFileName` varchar(2000) CHARACTER SET utf8mb4 NOT NULL,
    `FileName` varchar(2000) CHARACTER SET utf8mb4 NOT NULL,
    `Url` varchar(2000) CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(13) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EntityBlobs` PRIMARY KEY (`EntityBlobId`),
    CONSTRAINT `FK_EntityBlobs_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimSkus` (
    `ClaimSkuId` int NOT NULL AUTO_INCREMENT,
    `ClaimId` int NOT NULL,
    `ClaimSkuTypeId` int NOT NULL,
    `ClaimSkuStatusId` int NOT NULL,
    `ClaimQuantityTypeId` int NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `VatAmount` decimal(65,30) NOT NULL,
    `TotalAmount` decimal(65,30) AS (Amount + VatAmount),
    `Quantity` int NOT NULL,
    `DCProductId` int NOT NULL,
    `ApprovedDate` datetime(6) NULL,
    `ApprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimSkus` PRIMARY KEY (`ClaimSkuId`),
    CONSTRAINT `FK_ClaimSkus_ClaimSkuTypes_ClaimSkuTypeId` FOREIGN KEY (`ClaimSkuTypeId`) REFERENCES `ClaimSkuTypes` (`ClaimSkuTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimSkus_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimSkus_opt_ClaimQuantityTypes_ClaimQuantityTypeId` FOREIGN KEY (`ClaimQuantityTypeId`) REFERENCES `opt_ClaimQuantityTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ClaimSkus_opt_ClaimSkuStatuses_ClaimSkuStatusId` FOREIGN KEY (`ClaimSkuStatusId`) REFERENCES `opt_ClaimSkuStatuses` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimStatusUsers` (
    `ClaimStatusUserId` int NOT NULL AUTO_INCREMENT,
    `ClaimStatusId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ClaimStatusUsers` PRIMARY KEY (`ClaimStatusUserId`),
    CONSTRAINT `FK_ClaimStatusUsers_opt_ClaimStatuses_ClaimStatusId` FOREIGN KEY (`ClaimStatusId`) REFERENCES `opt_ClaimStatuses` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimTypeReportTypes` (
    `ClaimTypeId` int NOT NULL,
    `ClaimReportTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ClaimTypeReportTypes` PRIMARY KEY (`ClaimTypeId`, `ClaimReportTypeId`),
    CONSTRAINT `FK_ClaimTypeReportTypes_opt_ClaimReportTypes_ClaimReportTypeId` FOREIGN KEY (`ClaimReportTypeId`) REFERENCES `opt_ClaimReportTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ClaimTypes` (
    `ClaimTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `VatId` int NOT NULL,
    `IsVatInclusive` tinyint(1) NOT NULL,
    `IsDairy` tinyint(1) NOT NULL,
    `IsEmployeeClaim` tinyint(1) NOT NULL,
    `SupplierId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ClaimTypes` PRIMARY KEY (`ClaimTypeId`),
    CONSTRAINT `FK_ClaimTypes_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationHistories` (
    `CommunicationHistoryId` int NOT NULL AUTO_INCREMENT,
    `CommunicationTemplateId` int NOT NULL,
    `ToEmail` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `FromEmail` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `FromName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Subject` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Body` varchar(10000) CHARACTER SET utf8mb4 NOT NULL,
    `CcEmails` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `AttachmentUrls` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `HasMemoryStreamAttachment` tinyint(1) NOT NULL,
    `Error` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Discriminator` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimId` int NULL,
    `ClaimFloatId` int NULL,
    `EmployeeId` int NULL,
    `EmployeeStoreCalendarId` int NULL,
    `OrderId` int NULL,
    `ProjectId` int NULL,
    `StoreId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CommunicationHistories` PRIMARY KEY (`CommunicationHistoryId`),
    CONSTRAINT `FK_CommunicationHistories_ClaimFloats_ClaimFloatId` FOREIGN KEY (`ClaimFloatId`) REFERENCES `ClaimFloats` (`ClaimFloatId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CommunicationHistories_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CommunicationHistories_CommunicationTemplates_CommunicationT~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplates` (`CommunicationTemplateId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ContactEvents` (
    `ContactEventId` int NOT NULL AUTO_INCREMENT,
    `ContactId` int NOT NULL,
    `EventTypeId` int NOT NULL,
    `FrequencyId` int NOT NULL,
    `EventDate` datetime(6) NOT NULL,
    `IsRecurringEvent` tinyint(1) NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ContactEvents` PRIMARY KEY (`ContactEventId`),
    CONSTRAINT `FK_ContactEvents_opt_EventTypes_EventTypeId` FOREIGN KEY (`EventTypeId`) REFERENCES `opt_EventTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ContactEvents_opt_FrequencyTypes_FrequencyId` FOREIGN KEY (`FrequencyId`) REFERENCES `opt_FrequencyTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ContactItems` (
    `ContactItemId` int NOT NULL AUTO_INCREMENT,
    `ContactId` int NOT NULL,
    `ContactTypeId` int NOT NULL,
    `IsPrimary` tinyint(1) NOT NULL,
    `IsEmergency` tinyint(1) NOT NULL,
    `Value` varchar(260) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ContactItems` PRIMARY KEY (`ContactItemId`),
    CONSTRAINT `FK_ContactItems_opt_ContactTypes_ContactTypeId` FOREIGN KEY (`ContactTypeId`) REFERENCES `opt_ContactTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Contacts` (
    `ContactId` int NOT NULL AUTO_INCREMENT,
    `StakeholderId` int NOT NULL,
    `PrimaryEmailContactItemId` int NULL,
    `PrimaryMobileContactItemId` int NULL,
    `FullName` varchar(160) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `ContactType` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Contacts` PRIMARY KEY (`ContactId`),
    CONSTRAINT `FK_Contacts_ContactItems_PrimaryEmailContactItemId` FOREIGN KEY (`PrimaryEmailContactItemId`) REFERENCES `ContactItems` (`ContactItemId`),
    CONSTRAINT `FK_Contacts_ContactItems_PrimaryMobileContactItemId` FOREIGN KEY (`PrimaryMobileContactItemId`) REFERENCES `ContactItems` (`ContactItemId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CostCenterEmployees` (
    `CostCenterEmployeeId` int NOT NULL AUTO_INCREMENT,
    `CostCenterId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_CostCenterEmployees` PRIMARY KEY (`CostCenterEmployeeId`),
    CONSTRAINT `FK_CostCenterEmployees_CostCenters_CostCenterId` FOREIGN KEY (`CostCenterId`) REFERENCES `CostCenters` (`CostCenterId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditorNotificationStatusUsers` (
    `CreditorNotificationStatusUserId` int NOT NULL AUTO_INCREMENT,
    `CreditorStatusId` int NOT NULL,
    `EngageRegionId` int NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_CreditorNotificationStatusUsers` PRIMARY KEY (`CreditorNotificationStatusUserId`),
    CONSTRAINT `FK_CreditorNotificationStatusUsers_CreditorStatuses_CreditorSta~` FOREIGN KEY (`CreditorStatusId`) REFERENCES `CreditorStatuses` (`CreditorStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorNotificationStatusUsers_opt_EngageRegions_EngageRegi~` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DCAccounts` (
    `DCAccountId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `AccountNumber` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(220) CHARACTER SET utf8mb4 NULL,
    `IsPrimary` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_DCAccounts` PRIMARY KEY (`DCAccountId`),
    CONSTRAINT `FK_DCAccounts_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `DCProducts` (
    `DCProductId` int NOT NULL AUTO_INCREMENT,
    `EngageVariantProductId` int NULL,
    `DistributionCenterId` int NOT NULL,
    `VendorId` int NOT NULL,
    `ManufacturerId` int NULL,
    `ProductClassId` int NOT NULL,
    `UnitTypeId` int NOT NULL,
    `ProductActiveStatusId` int NOT NULL,
    `ProductStatusId` int NOT NULL,
    `ProductWarehouseStatusId` int NOT NULL,
    `ProductSubWarehouseId` int NOT NULL,
    `Size` float NOT NULL,
    `PackSize` float NOT NULL,
    `EANNumber` varchar(20) CHARACTER SET utf8mb4 NULL,
    `SubWarehouse` varchar(20) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_DCProducts` PRIMARY KEY (`DCProductId`),
    CONSTRAINT `FK_DCProducts_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_SubWarehouses_ProductSubWarehouseId` FOREIGN KEY (`ProductSubWarehouseId`) REFERENCES `SubWarehouses` (`SubWarehouseId`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_opt_DCProductClasses_ProductClassId` FOREIGN KEY (`ProductClassId`) REFERENCES `opt_DCProductClasses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_opt_ProductActiveStatuses_ProductActiveStatusId` FOREIGN KEY (`ProductActiveStatusId`) REFERENCES `opt_ProductActiveStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_opt_ProductStatuses_ProductStatusId` FOREIGN KEY (`ProductStatusId`) REFERENCES `opt_ProductStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_opt_ProductWarehouseStatuses_ProductWarehouseStat~` FOREIGN KEY (`ProductWarehouseStatusId`) REFERENCES `opt_ProductWarehouseStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_DCProducts_opt_UnitTypes_UnitTypeId` FOREIGN KEY (`UnitTypeId`) REFERENCES `opt_UnitTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `DCStockOnHands` (
    `DCStockOnHandId` int NOT NULL AUTO_INCREMENT,
    `DCProductId` int NOT NULL,
    `OnOrderQuantity` float NOT NULL,
    `StockDate` datetime(6) NOT NULL,
    `Value` float NOT NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_DCStockOnHands` PRIMARY KEY (`DCStockOnHandId`),
    CONSTRAINT `FK_DCStockOnHands_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplateProducts` (
    `OrderTemplateProductId` int NOT NULL AUTO_INCREMENT,
    `OrderTemplateGroupId` int NOT NULL,
    `DCProductId` int NOT NULL,
    `Order` int NOT NULL,
    `Quantity` int NOT NULL,
    `Price` decimal(65,30) NOT NULL,
    `PromotionPrice` decimal(65,30) NOT NULL,
    `RecommendedPrice` decimal(65,30) NOT NULL,
    `GrossProfitPercent` decimal(65,30) NOT NULL,
    `Suffix` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateProducts` PRIMARY KEY (`OrderTemplateProductId`),
    CONSTRAINT `FK_OrderTemplateProducts_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderTemplateProducts_OrderTemplateGroups_OrderTemplateGroup~` FOREIGN KEY (`OrderTemplateGroupId`) REFERENCES `OrderTemplateGroups` (`OrderTemplateGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmailTemplateHistories` (
    `EmailTemplateHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmailTemplateId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmailTemplateHistories` PRIMARY KEY (`EmailTemplateHistoryId`),
    CONSTRAINT `FK_EmailTemplateHistories_EmailTemplates_EmailTemplateId` FOREIGN KEY (`EmailTemplateId`) REFERENCES `EmailTemplates` (`EmailTemplateId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeAddresses` (
    `EmployeeAddressId` int NOT NULL AUTO_INCREMENT,
    `UnitNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `ComplexName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `StreetNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `StreetName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Suburb` varchar(120) CHARACTER SET utf8mb4 NULL,
    `City` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(15) CHARACTER SET utf8mb4 NULL,
    `IsSameAsPhysicalAddress` tinyint(1) NOT NULL,
    `IsPostalAddressCareOfAddress` tinyint(1) NOT NULL,
    `CareOfIntermediary` varchar(120) CHARACTER SET utf8mb4 NULL,
    `PostalUnitNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `PostalComplexName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `PostalStreetNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `PostalStreetName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `PostalSuburb` varchar(120) CHARACTER SET utf8mb4 NULL,
    `PostalCity` varchar(120) CHARACTER SET utf8mb4 NULL,
    `PostalCode` varchar(15) CHARACTER SET utf8mb4 NULL,
    `EmployeeId` int NOT NULL,
    `CountryId` int NULL,
    `ProvinceId` int NULL,
    `PostalCountryId` int NULL,
    `PostalProvinceId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeAddresses` PRIMARY KEY (`EmployeeAddressId`),
    CONSTRAINT `FK_EmployeeAddresses_opt_EmployeeNationalities_CountryId` FOREIGN KEY (`CountryId`) REFERENCES `opt_EmployeeNationalities` (`Id`),
    CONSTRAINT `FK_EmployeeAddresses_opt_EmployeeNationalities_PostalCountryId` FOREIGN KEY (`PostalCountryId`) REFERENCES `opt_EmployeeNationalities` (`Id`),
    CONSTRAINT `FK_EmployeeAddresses_opt_Provinces_PostalProvinceId` FOREIGN KEY (`PostalProvinceId`) REFERENCES `opt_Provinces` (`Id`),
    CONSTRAINT `FK_EmployeeAddresses_opt_Provinces_ProvinceId` FOREIGN KEY (`ProvinceId`) REFERENCES `opt_Provinces` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeAssetHistories` (
    `EmployeeAssetHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeAssetId` int NOT NULL,
    `OldEmployeeId` int NOT NULL,
    `NewEmployeeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeAssetHistories` PRIMARY KEY (`EmployeeAssetHistoryId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeAssets` (
    `EmployeeAssetId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeAssetTypeId` int NOT NULL,
    `EmployeeAssetBrandId` int NOT NULL,
    `AssetStatusId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Contract` varchar(100) CHARACTER SET utf8mb4 NULL,
    `MobileNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Sim` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Imei` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SerialNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `RecievedDate` datetime(6) NULL,
    `HandedBackDate` datetime(6) NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeAssets` PRIMARY KEY (`EmployeeAssetId`),
    CONSTRAINT `FK_EmployeeAssets_opt_AssetStatuses_AssetStatusId` FOREIGN KEY (`AssetStatusId`) REFERENCES `opt_AssetStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeAssets_opt_EmployeeAssetBrands_EmployeeAssetBrandId` FOREIGN KEY (`EmployeeAssetBrandId`) REFERENCES `opt_EmployeeAssetBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeAssets_opt_EmployeeAssetTypes_EmployeeAssetTypeId` FOREIGN KEY (`EmployeeAssetTypeId`) REFERENCES `opt_EmployeeAssetTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeBankDetailFiles` (
    `EmployeeBankDetailFileId` int NOT NULL AUTO_INCREMENT,
    `EmployeeBankDetailId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileContainerId` int NOT NULL,
    `Name` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Url` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Metadata` varchar(4000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EmployeeBankDetailFiles` PRIMARY KEY (`EmployeeBankDetailFileId`),
    CONSTRAINT `FK_EmployeeBankDetailFiles_FileContainers_FileContainerId` FOREIGN KEY (`FileContainerId`) REFERENCES `FileContainers` (`FileContainerId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeBankDetails` (
    `EmployeeBankDetailId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `BankAccountOwnerId` int NOT NULL,
    `BankAccountTypeId` int NOT NULL,
    `BankPaymentMethodId` int NOT NULL,
    `BankNameId` int NOT NULL,
    `BranchCode` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `AccountNumber` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `AccountHolder` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `BeneficiaryReference` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SwiftCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RoutingCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `IsPrimary` tinyint(1) NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeBankDetails` PRIMARY KEY (`EmployeeBankDetailId`),
    CONSTRAINT `FK_EmployeeBankDetails_opt_BankAccountOwners_BankAccountOwnerId` FOREIGN KEY (`BankAccountOwnerId`) REFERENCES `opt_BankAccountOwners` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeBankDetails_opt_BankAccountTypes_BankAccountTypeId` FOREIGN KEY (`BankAccountTypeId`) REFERENCES `opt_BankAccountTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeBankDetails_opt_BankNames_BankNameId` FOREIGN KEY (`BankNameId`) REFERENCES `opt_BankNames` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeBankDetails_opt_BankPaymentMethods_BankPaymentMethod~` FOREIGN KEY (`BankPaymentMethodId`) REFERENCES `opt_BankPaymentMethods` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeBenefits` (
    `EmployeeBenefitId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `BenefitTypeId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Value` float NOT NULL,
    `IssuedDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeBenefits` PRIMARY KEY (`EmployeeBenefitId`),
    CONSTRAINT `FK_EmployeeBenefits_opt_BenefitTypes_BenefitTypeId` FOREIGN KEY (`BenefitTypeId`) REFERENCES `opt_BenefitTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeCoolerBoxes` (
    `EmployeeCoolerBoxId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeCoolerBoxConditionId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `RecievedDate` datetime(6) NULL,
    `HandedBackDate` datetime(6) NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeCoolerBoxes` PRIMARY KEY (`EmployeeCoolerBoxId`),
    CONSTRAINT `FK_EmployeeCoolerBoxes_opt_EmployeeCoolerBoxConditions_Employee~` FOREIGN KEY (`EmployeeCoolerBoxConditionId`) REFERENCES `opt_EmployeeCoolerBoxConditions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeCoolerBoxFiles` (
    `EmployeeCoolerBoxFileId` int NOT NULL AUTO_INCREMENT,
    `EmployeeCoolerBoxId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileContainerId` int NOT NULL,
    `Name` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Url` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Metadata` varchar(4000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EmployeeCoolerBoxFiles` PRIMARY KEY (`EmployeeCoolerBoxFileId`),
    CONSTRAINT `FK_EmployeeCoolerBoxFiles_EmployeeCoolerBoxes_EmployeeCoolerBox~` FOREIGN KEY (`EmployeeCoolerBoxId`) REFERENCES `EmployeeCoolerBoxes` (`EmployeeCoolerBoxId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeCoolerBoxFiles_FileContainers_FileContainerId` FOREIGN KEY (`FileContainerId`) REFERENCES `FileContainers` (`FileContainerId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeCoolerBoxHistories` (
    `EmployeeCoolerBoxHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeCoolerBoxId` int NOT NULL,
    `OldEmployeeId` int NOT NULL,
    `NewEmployeeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeCoolerBoxHistories` PRIMARY KEY (`EmployeeCoolerBoxHistoryId`),
    CONSTRAINT `FK_EmployeeCoolerBoxHistories_EmployeeCoolerBoxes_EmployeeCoole~` FOREIGN KEY (`EmployeeCoolerBoxId`) REFERENCES `EmployeeCoolerBoxes` (`EmployeeCoolerBoxId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeDeductions` (
    `EmployeeDeductionId` int NOT NULL AUTO_INCREMENT,
    `DeductionTypeId` int NOT NULL,
    `DeductionCycleTypeId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `DeductionDate` datetime(6) NOT NULL,
    `Amount` float NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `Reference` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeDeductions` PRIMARY KEY (`EmployeeDeductionId`),
    CONSTRAINT `FK_EmployeeDeductions_opt_DeductionCycleTypes_DeductionCycleTyp~` FOREIGN KEY (`DeductionCycleTypeId`) REFERENCES `opt_DeductionCycleTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeDeductions_opt_DeductionTypes_DeductionTypeId` FOREIGN KEY (`DeductionTypeId`) REFERENCES `opt_DeductionTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeDepartments` (
    `EmployeeId` int NOT NULL,
    `EngageDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeDepartments` PRIMARY KEY (`EmployeeId`, `EngageDepartmentId`),
    CONSTRAINT `FK_EmployeeDepartments_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeDisciplinaryProcedures` (
    `EmployeeDisciplinaryProcedureId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `Description` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `DisciplinaryProcedureDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeDisciplinaryProcedures` PRIMARY KEY (`EmployeeDisciplinaryProcedureId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeEmployeeBadges` (
    `EmployeeId` int NOT NULL,
    `EmployeeBadgeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeBadges` PRIMARY KEY (`EmployeeId`, `EmployeeBadgeId`),
    CONSTRAINT `FK_EmployeeEmployeeBadges_EmployeeBadges_EmployeeBadgeId` FOREIGN KEY (`EmployeeBadgeId`) REFERENCES `EmployeeBadges` (`EmployeeBadgeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeEmployeeDivisions` (
    `EmployeeId` int NOT NULL,
    `EmployeeDivisionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeDivisions` PRIMARY KEY (`EmployeeId`, `EmployeeDivisionId`),
    CONSTRAINT `FK_EmployeeEmployeeDivisions_EmployeeDivisions_EmployeeDivision~` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeEmployeeHealthConditions` (
    `EmployeeId` int NOT NULL,
    `EmployeeHealthConditionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeHealthConditions` PRIMARY KEY (`EmployeeId`, `EmployeeHealthConditionId`),
    CONSTRAINT `FK_EmployeeEmployeeHealthConditions_EmployeeHealthConditions_Em~` FOREIGN KEY (`EmployeeHealthConditionId`) REFERENCES `EmployeeHealthConditions` (`EmployeeHealthConditionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeEmployeeJobTitles` (
    `EmployeeId` int NOT NULL,
    `EmployeeJobTitleId` int NOT NULL,
    `IsDisabled` tinyint(1) NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeJobTitles` PRIMARY KEY (`EmployeeId`, `EmployeeJobTitleId`),
    CONSTRAINT `FK_EmployeeEmployeeJobTitles_EmployeeJobTitles_EmployeeJobTitle~` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeEmployeeKpis` (
    `EmployeeId` int NOT NULL,
    `EmployeeKpiId` int NOT NULL,
    `EmployeeKpiTierId` int NULL,
    `Score` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeKpis` PRIMARY KEY (`EmployeeId`, `EmployeeKpiId`),
    CONSTRAINT `FK_EmployeeEmployeeKpis_EmployeeKpiTiers_EmployeeKpiTierId` FOREIGN KEY (`EmployeeKpiTierId`) REFERENCES `EmployeeKpiTiers` (`EmployeeKpiTierId`),
    CONSTRAINT `FK_EmployeeEmployeeKpis_EmployeeKpis_EmployeeKpiId` FOREIGN KEY (`EmployeeKpiId`) REFERENCES `EmployeeKpis` (`EmployeeKpiId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeExpenseClaims` (
    `EmployeeExpenseClaimId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `RecoverFrom` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Value` int NOT NULL,
    `KMDistanse` int NOT NULL,
    `ManagerComment` longtext CHARACTER SET utf8mb4 NULL,
    `Processed` tinyint(1) NOT NULL,
    `ClaimDate` datetime(6) NOT NULL,
    `SubmittedDate` datetime(6) NOT NULL,
    `ProcessedDate` datetime(6) NULL,
    `StatusId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeExpenseClaims` PRIMARY KEY (`EmployeeExpenseClaimId`),
    CONSTRAINT `FK_EmployeeExpenseClaims_opt_ExpenseClaimStatuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `opt_ExpenseClaimStatuses` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeFiles` (
    `EmployeeFileId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeFiles` PRIMARY KEY (`EmployeeFileId`),
    CONSTRAINT `FK_EmployeeFiles_EmployeeFileTypes_EmployeeFileTypeId` FOREIGN KEY (`EmployeeFileTypeId`) REFERENCES `EmployeeFileTypes` (`EmployeeFileTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeFuels` (
    `EmployeeFuelId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeVehicleId` int NOT NULL,
    `EmployeePaymentTypeId` int NULL,
    `EmployeeFuelExpenseTypeId` int NOT NULL,
    `FuelDate` datetime(6) NOT NULL,
    `Amount` decimal(65,30) NULL,
    `Litres` float NULL,
    `Odometer` int NULL,
    `TollgateName` longtext CHARACTER SET utf8mb4 NULL,
    `BlobUrl` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `BlobName` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeFuels` PRIMARY KEY (`EmployeeFuelId`),
    CONSTRAINT `FK_EmployeeFuels_opt_EmployeeFuelExpenseTypes_EmployeeFuelExpen~` FOREIGN KEY (`EmployeeFuelExpenseTypeId`) REFERENCES `opt_EmployeeFuelExpenseTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeFuels_opt_EmployeePaymentTypes_EmployeePaymentTypeId` FOREIGN KEY (`EmployeePaymentTypeId`) REFERENCES `opt_EmployeePaymentTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeKpiScores` (
    `EmployeeKpiScoreId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeKpiId` int NOT NULL,
    `EmployeeKpiTierId` int NULL,
    `Score` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeKpiScores` PRIMARY KEY (`EmployeeKpiScoreId`),
    CONSTRAINT `FK_EmployeeKpiScores_EmployeeKpiTiers_EmployeeKpiTierId` FOREIGN KEY (`EmployeeKpiTierId`) REFERENCES `EmployeeKpiTiers` (`EmployeeKpiTierId`),
    CONSTRAINT `FK_EmployeeKpiScores_EmployeeKpis_EmployeeKpiId` FOREIGN KEY (`EmployeeKpiId`) REFERENCES `EmployeeKpis` (`EmployeeKpiId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeLeaveEntries` (
    `EmployeeLeaveEntryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `LeaveTypeId` int NOT NULL,
    `FromDate` datetime(6) NOT NULL,
    `FromHalfDay` tinyint(1) NOT NULL,
    `ToDate` datetime(6) NOT NULL,
    `ToHalfDay` tinyint(1) NOT NULL,
    `Status` int NOT NULL,
    `Comment` longtext CHARACTER SET utf8mb4 NULL,
    `AdjustLeave` tinyint(1) NOT NULL,
    `ManagerComment` longtext CHARACTER SET utf8mb4 NULL,
    `Processed` tinyint(1) NOT NULL,
    `ProcessedDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeLeaveEntries` PRIMARY KEY (`EmployeeLeaveEntryId`),
    CONSTRAINT `FK_EmployeeLeaveEntries_opt_LeaveTypes_LeaveTypeId` FOREIGN KEY (`LeaveTypeId`) REFERENCES `opt_LeaveTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeLoans` (
    `EmployeeLoanId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `Amount` float NOT NULL,
    `RepayableAmount` float NOT NULL,
    `LoanTerm` int NOT NULL,
    `LoanDate` datetime(6) NOT NULL,
    `Installment` float NOT NULL,
    `Reason` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeLoans` PRIMARY KEY (`EmployeeLoanId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeManager` (
    `EmployeeId` int NOT NULL,
    `ManagerId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeManager` PRIMARY KEY (`EmployeeId`, `ManagerId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeNotifications` (
    `EmployeeId` int NOT NULL,
    `NotificationId` int NOT NULL,
    `Count` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeNotifications` PRIMARY KEY (`EmployeeId`, `NotificationId`),
    CONSTRAINT `FK_EmployeeNotifications_Notifications_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notifications` (`NotificationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeePayRates` (
    `EmployeePayRateId` int NOT NULL AUTO_INCREMENT,
    `EffectiveDate` datetime(6) NOT NULL,
    `EmployeeId` int NOT NULL,
    `EmployeePayRateFrequencyId` int NOT NULL,
    `EmployeePayRatePackageId` int NOT NULL,
    `IncreaseReason` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(65,30) NOT NULL,
    `IsPayPackageAutomatically` tinyint(1) NOT NULL,
    `HoursPerDay` decimal(65,30) NOT NULL,
    `DaysPerPeriod` int NOT NULL,
    `HoursPerMonth` decimal(65,30) NOT NULL,
    `HourlyRate` decimal(65,30) NOT NULL,
    `DailyRate` decimal(65,30) NOT NULL,
    `MonthlyRate` decimal(65,30) NOT NULL,
    `IsWorkMonday` tinyint(1) NOT NULL,
    `IsWorkTuesday` tinyint(1) NOT NULL,
    `IsWorkWednesday` tinyint(1) NOT NULL,
    `IsWorkThursday` tinyint(1) NOT NULL,
    `IsWorkFriday` tinyint(1) NOT NULL,
    `IsWorkSaturday` tinyint(1) NOT NULL,
    `IsWorkSunday` tinyint(1) NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeePayRates` PRIMARY KEY (`EmployeePayRateId`),
    CONSTRAINT `FK_EmployeePayRates_opt_EmployeePayRateFrequencies_EmployeePayR~` FOREIGN KEY (`EmployeePayRateFrequencyId`) REFERENCES `opt_EmployeePayRateFrequencies` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeePayRates_opt_EmployeePayRatePackages_EmployeePayRate~` FOREIGN KEY (`EmployeePayRatePackageId`) REFERENCES `opt_EmployeePayRatePackages` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeePensions` (
    `EmployeePensionId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeePensionSchemeId` int NOT NULL,
    `EmployeePensionCategoryId` int NOT NULL,
    `EmployeePensionContributionPercentageId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeePensions` PRIMARY KEY (`EmployeePensionId`),
    CONSTRAINT `FK_EmployeePensions_opt_EmployeePensionCategories_EmployeePensi~` FOREIGN KEY (`EmployeePensionCategoryId`) REFERENCES `opt_EmployeePensionCategories` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeePensions_opt_EmployeePensionContributionPercentages_~` FOREIGN KEY (`EmployeePensionContributionPercentageId`) REFERENCES `opt_EmployeePensionContributionPercentages` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeePensions_opt_EmployeePensionSchemes_EmployeePensionS~` FOREIGN KEY (`EmployeePensionSchemeId`) REFERENCES `opt_EmployeePensionSchemes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeePopiConsents` (
    `EmployeePopiConsentId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `DateOfConsent` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeePopiConsents` PRIMARY KEY (`EmployeePopiConsentId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeQualificationFiles` (
    `EmployeeQualificationFileId` int NOT NULL AUTO_INCREMENT,
    `EmployeeQualificationId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileContainerId` int NOT NULL,
    `Name` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Url` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Metadata` varchar(4000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EmployeeQualificationFiles` PRIMARY KEY (`EmployeeQualificationFileId`),
    CONSTRAINT `FK_EmployeeQualificationFiles_FileContainers_FileContainerId` FOREIGN KEY (`FileContainerId`) REFERENCES `FileContainers` (`FileContainerId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeQualifications` (
    `EmployeeQualificationId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EducationLevelId` int NOT NULL,
    `InstitutionTypeId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
    `InstitutionName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `FinalYearSubjects` varchar(250) CHARACTER SET utf8mb4 NULL,
    `IsHighestQualification` tinyint(1) NOT NULL,
    `CompletedDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeQualifications` PRIMARY KEY (`EmployeeQualificationId`),
    CONSTRAINT `FK_EmployeeQualifications_opt_EducationLevels_EducationLevelId` FOREIGN KEY (`EducationLevelId`) REFERENCES `opt_EducationLevels` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeQualifications_opt_InstitutionTypes_InstitutionTypeId` FOREIGN KEY (`InstitutionTypeId`) REFERENCES `opt_InstitutionTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeRecurringTransactions` (
    `EmployeeRecurringTransactionId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeTransactionTypeId` int NOT NULL,
    `EmployeeRecurringTransactionStatusId` int NOT NULL,
    `PayrollPeriodId` int NOT NULL,
    `CreditorBankAccountId` int NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `InitialAmount` decimal(65,30) NOT NULL,
    `InstallmentAmount` decimal(65,30) NOT NULL,
    `BaseInstallmentOnAmountOrComponent` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Reference` varchar(220) CHARACTER SET utf8mb4 NULL,
    `IsFringeBenefitLoan` tinyint(1) NOT NULL,
    `LeavePayPercentage` float NOT NULL,
    `ApprovedDate` datetime(6) NULL,
    `ApprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedDate` datetime(6) NULL,
    `RejectedBy` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedReason` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeRecurringTransactions` PRIMARY KEY (`EmployeeRecurringTransactionId`),
    CONSTRAINT `FK_EmployeeRecurringTransactions_CreditorBankAccounts_CreditorB~` FOREIGN KEY (`CreditorBankAccountId`) REFERENCES `CreditorBankAccounts` (`CreditorBankAccountId`),
    CONSTRAINT `FK_EmployeeRecurringTransactions_EmployeeRecurringTransactionSt~` FOREIGN KEY (`EmployeeRecurringTransactionStatusId`) REFERENCES `EmployeeRecurringTransactionStatuses` (`EmployeeRecurringTransactionStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeRecurringTransactions_EmployeeTransactionTypes_Emplo~` FOREIGN KEY (`EmployeeTransactionTypeId`) REFERENCES `EmployeeTransactionTypes` (`EmployeeTransactionTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeRecurringTransactions_PayrollPeriods_PayrollPeriodId` FOREIGN KEY (`PayrollPeriodId`) REFERENCES `PayrollPeriods` (`PayrollPeriodId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeRegionContacts` (
    `EmployeeRegionContactId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `MobilePhone` varchar(30) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeRegionContacts` PRIMARY KEY (`EmployeeRegionContactId`),
    CONSTRAINT `FK_EmployeeRegionContacts_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeRegions` (
    `EmployeeId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeRegions` PRIMARY KEY (`EmployeeId`, `EngageRegionId`),
    CONSTRAINT `FK_EmployeeRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeReinstatementHistories` (
    `EmployeeReinstatementHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeReinstatementReasonId` int NOT NULL,
    `ReinstatementDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeReinstatementHistories` PRIMARY KEY (`EmployeeReinstatementHistoryId`),
    CONSTRAINT `FK_EmployeeReinstatementHistories_opt_EmployeeReinstatementReas~` FOREIGN KEY (`EmployeeReinstatementReasonId`) REFERENCES `opt_EmployeeReinstatementReasons` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeReports` (
    `EmployeeId` int NOT NULL,
    `ReportId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeReports` PRIMARY KEY (`EmployeeId`, `ReportId`),
    CONSTRAINT `FK_EmployeeReports_Report_ReportId` FOREIGN KEY (`ReportId`) REFERENCES `Report` (`ReportId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Employees` (
    `EmployeeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeDisabledTypeId` int NOT NULL,
    `StakeholderId` int NOT NULL,
    `ManagerId` int NULL,
    `LeaveManagerId` int NULL,
    `CostCenterManagerId` int NULL,
    `UserId` int NULL,
    `EmployeeStateId` int NOT NULL,
    `EmployeeIncentiveTierId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EmployeeJobTitleTimeId` int NULL,
    `EmployeeJobTitleTypeId` int NULL,
    `EmploymentTypeId` int NULL,
    `EmploymentActionId` int NULL,
    `Code` varchar(15) CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `MiddleName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `MaidenName` longtext CHARACTER SET utf8mb4 NULL,
    `KnownAs` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Initials` varchar(10) CHARACTER SET utf8mb4 NULL,
    `IsEncashLeave` tinyint(1) NOT NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `LeaveCycleStartDate` datetime(6) NULL,
    `LeaveAccumulationRate` float NULL,
    `AnnualLeave` int NULL,
    `SickLeave` int NULL,
    `FamilyLeave` int NULL,
    `EmailAddress1` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EmailAddress2` varchar(100) CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `HomeNumber` longtext CHARACTER SET utf8mb4 NULL,
    `WorkNumber` longtext CHARACTER SET utf8mb4 NULL,
    `WorkExtension` longtext CHARACTER SET utf8mb4 NULL,
    `NextOfKinName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NextOfKinContactNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `NextOfKinAddess` longtext CHARACTER SET utf8mb4 NULL,
    `MobileAppVersion` longtext CHARACTER SET utf8mb4 NULL,
    `IsCovidVaccinated` tinyint(1) NOT NULL,
    `IsDefaultPayslip` tinyint(1) NOT NULL,
    `IsRetired` tinyint(1) NOT NULL,
    `IsForeignNational` tinyint(1) NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `EmployeeIdentificationTypeId` int NOT NULL,
    `EmployeePassportNationalityId` int NOT NULL,
    `EmployeeCitzenshipId` int NOT NULL,
    `EmployeeLanguageId` int NOT NULL,
    `EmployeePersonTypeId` int NOT NULL,
    `EmployeeSDLExemptionId` int NOT NULL,
    `EmployeeTaxStatusId` int NOT NULL,
    `EmployeeUIFExemptionId` int NOT NULL,
    `EmployeeDefaultPayslipId` int NULL,
    `EmployeeReinstatementReasonId` int NULL,
    `EmployeeTerminationReasonId` int NULL,
    `EmployeeStandardIndustryGroupCodeId` int NOT NULL,
    `EmployeeStandardIndustryCodeId` int NOT NULL,
    `MaritalStatusId` int NULL,
    `EmployeeNationalityId` int NULL,
    `EmployeeRaceId` int NULL,
    `EmployeeGenderId` int NULL,
    `NextOfKinTypeId` int NULL,
    `EmployeeTitleId` int NULL,
    `UniformSizeId` int NULL,
    `PayrollPeriodId` int NULL,
    `EngageRegionId` int NOT NULL,
    `EngageSubRegionId` int NULL,
    `EmployeeTypeId` int NULL,
    `GroupStartDate` datetime(6) NOT NULL,
    `StartingDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `ReinstatementDate` datetime(6) NULL,
    `IsNotReemployable` tinyint(1) NOT NULL,
    `IdNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `PassportNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `PassportStartDate` datetime(6) NULL,
    `PassportEndDate` datetime(6) NULL,
    `PAYENumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `SARSNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `IsVoluntaryOverDeduction` tinyint(1) NOT NULL,
    `StatutoryEmploymentDateOverride` datetime(6) NULL,
    `IsApplyTaxForPublicServiceEmployee` tinyint(1) NOT NULL,
    `IsEmploymentTaxIncentive` tinyint(1) NOT NULL,
    `UIFNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `RANumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `MedicalAidNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `BlobUrl` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `BlobName` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Employees` PRIMARY KEY (`EmployeeId`),
    CONSTRAINT `FK_Employees_EmployeeJobTitleTimes_EmployeeJobTitleTimeId` FOREIGN KEY (`EmployeeJobTitleTimeId`) REFERENCES `EmployeeJobTitleTimes` (`EmployeeJobTitleTimeId`),
    CONSTRAINT `FK_Employees_EmployeeJobTitleTypes_EmployeeJobTitleTypeId` FOREIGN KEY (`EmployeeJobTitleTypeId`) REFERENCES `EmployeeJobTitleTypes` (`EmployeeJobTitleTypeId`),
    CONSTRAINT `FK_Employees_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`),
    CONSTRAINT `FK_Employees_EmployeeTypes_EmployeeTypeId` FOREIGN KEY (`EmployeeTypeId`) REFERENCES `EmployeeTypes` (`EmployeeTypeId`),
    CONSTRAINT `FK_Employees_Employees_CostCenterManagerId` FOREIGN KEY (`CostCenterManagerId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_Employees_Employees_LeaveManagerId` FOREIGN KEY (`LeaveManagerId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_Employees_Employees_ManagerId` FOREIGN KEY (`ManagerId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_Employees_EngageSubRegions_EngageSubRegionId` FOREIGN KEY (`EngageSubRegionId`) REFERENCES `EngageSubRegions` (`EngageSubRegionId`),
    CONSTRAINT `FK_Employees_PayrollPeriods_PayrollPeriodId` FOREIGN KEY (`PayrollPeriodId`) REFERENCES `PayrollPeriods` (`PayrollPeriodId`),
    CONSTRAINT `FK_Employees_opt_EmployeeDefaultPayslips_EmployeeDefaultPayslip~` FOREIGN KEY (`EmployeeDefaultPayslipId`) REFERENCES `opt_EmployeeDefaultPayslips` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmployeeDisabledTypes_EmployeeDisabledTypeId` FOREIGN KEY (`EmployeeDisabledTypeId`) REFERENCES `opt_EmployeeDisabledTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeIdentificationTypes_EmployeeIdentifica~` FOREIGN KEY (`EmployeeIdentificationTypeId`) REFERENCES `opt_EmployeeIdentificationTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeIncentiveTiers_EmployeeIncentiveTierId` FOREIGN KEY (`EmployeeIncentiveTierId`) REFERENCES `opt_EmployeeIncentiveTiers` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmployeeLanguages_EmployeeLanguageId` FOREIGN KEY (`EmployeeLanguageId`) REFERENCES `opt_EmployeeLanguages` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeNationalities_EmployeeCitzenshipId` FOREIGN KEY (`EmployeeCitzenshipId`) REFERENCES `opt_EmployeeNationalities` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeNationalities_EmployeeNationalityId` FOREIGN KEY (`EmployeeNationalityId`) REFERENCES `opt_EmployeeNationalities` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmployeeNationalities_EmployeePassportNational~` FOREIGN KEY (`EmployeePassportNationalityId`) REFERENCES `opt_EmployeeNationalities` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeePersonTypes_EmployeePersonTypeId` FOREIGN KEY (`EmployeePersonTypeId`) REFERENCES `opt_EmployeePersonTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeReinstatementReasons_EmployeeReinstate~` FOREIGN KEY (`EmployeeReinstatementReasonId`) REFERENCES `opt_EmployeeReinstatementReasons` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmployeeSDLExemptions_EmployeeSDLExemptionId` FOREIGN KEY (`EmployeeSDLExemptionId`) REFERENCES `opt_EmployeeSDLExemptions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeStandardIndustryCodes_EmployeeStandard~` FOREIGN KEY (`EmployeeStandardIndustryCodeId`) REFERENCES `opt_EmployeeStandardIndustryCodes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeStandardIndustryGroupCodes_EmployeeSta~` FOREIGN KEY (`EmployeeStandardIndustryGroupCodeId`) REFERENCES `opt_EmployeeStandardIndustryGroupCodes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeStates_EmployeeStateId` FOREIGN KEY (`EmployeeStateId`) REFERENCES `opt_EmployeeStates` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeTaxStatuses_EmployeeTaxStatusId` FOREIGN KEY (`EmployeeTaxStatusId`) REFERENCES `opt_EmployeeTaxStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmployeeTerminationReasons_EmployeeTermination~` FOREIGN KEY (`EmployeeTerminationReasonId`) REFERENCES `opt_EmployeeTerminationReasons` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmployeeUIFExemptions_EmployeeUIFExemptionId` FOREIGN KEY (`EmployeeUIFExemptionId`) REFERENCES `opt_EmployeeUIFExemptions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_EmploymentActions_EmploymentActionId` FOREIGN KEY (`EmploymentActionId`) REFERENCES `opt_EmploymentActions` (`Id`),
    CONSTRAINT `FK_Employees_opt_EmploymentTypes_EmploymentTypeId` FOREIGN KEY (`EmploymentTypeId`) REFERENCES `opt_EmploymentTypes` (`Id`),
    CONSTRAINT `FK_Employees_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_opt_Genders_EmployeeGenderId` FOREIGN KEY (`EmployeeGenderId`) REFERENCES `opt_Genders` (`Id`),
    CONSTRAINT `FK_Employees_opt_MaritalStatuses_MaritalStatusId` FOREIGN KEY (`MaritalStatusId`) REFERENCES `opt_MaritalStatuses` (`Id`),
    CONSTRAINT `FK_Employees_opt_NextOfKinTypes_NextOfKinTypeId` FOREIGN KEY (`NextOfKinTypeId`) REFERENCES `opt_NextOfKinTypes` (`Id`),
    CONSTRAINT `FK_Employees_opt_Races_EmployeeRaceId` FOREIGN KEY (`EmployeeRaceId`) REFERENCES `opt_Races` (`Id`),
    CONSTRAINT `FK_Employees_opt_Titles_EmployeeTitleId` FOREIGN KEY (`EmployeeTitleId`) REFERENCES `opt_Titles` (`Id`),
    CONSTRAINT `FK_Employees_opt_UniformSizes_UniformSizeId` FOREIGN KEY (`UniformSizeId`) REFERENCES `opt_UniformSizes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeSkills` (
    `EmployeeSkillId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `SkillCategoryId` int NOT NULL,
    `ProficiencyId` int NOT NULL,
    `ExperienceId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeSkills` PRIMARY KEY (`EmployeeSkillId`),
    CONSTRAINT `FK_EmployeeSkills_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeSkills_opt_Experiences_ExperienceId` FOREIGN KEY (`ExperienceId`) REFERENCES `opt_Experiences` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeSkills_opt_Proficiencies_ProficiencyId` FOREIGN KEY (`ProficiencyId`) REFERENCES `opt_Proficiencies` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeSkills_opt_SkillCategories_SkillCategoryId` FOREIGN KEY (`SkillCategoryId`) REFERENCES `opt_SkillCategories` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeSkillsDevelopment` (
    `EmployeeSkillsDevelopmentId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `CompletedDate` datetime(6) NULL,
    `Status` int NOT NULL,
    `Cost` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeSkillsDevelopment` PRIMARY KEY (`EmployeeSkillsDevelopmentId`),
    CONSTRAINT `FK_EmployeeSkillsDevelopment_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarBlockDays` (
    `EmployeeStoreCalendarBlockDayId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeStoreCalendarTypeId` int NOT NULL,
    `EmployeeStoreCalendarStatusId` int NOT NULL,
    `CalendarDate` datetime(6) NOT NULL,
    `IsManagerCreated` tinyint(1) NOT NULL,
    `EmployeeStoreCalendarPeriodId` int NOT NULL,
    `Note` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarBlockDays` PRIMARY KEY (`EmployeeStoreCalendarBlockDayId`),
    CONSTRAINT `FK_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarPeriods_~` FOREIGN KEY (`EmployeeStoreCalendarPeriodId`) REFERENCES `EmployeeStoreCalendarPeriods` (`EmployeeStoreCalendarPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarStatuses~` FOREIGN KEY (`EmployeeStoreCalendarStatusId`) REFERENCES `EmployeeStoreCalendarStatuses` (`EmployeeStoreCalendarStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarTypes_Em~` FOREIGN KEY (`EmployeeStoreCalendarTypeId`) REFERENCES `EmployeeStoreCalendarTypes` (`EmployeeStoreCalendarTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendarBlockDays_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeSuspensions` (
    `EmployeeSuspensionId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeSuspensionReasonId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeSuspensions` PRIMARY KEY (`EmployeeSuspensionId`),
    CONSTRAINT `FK_EmployeeSuspensions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeSuspensions_opt_EmployeeSuspensionReasons_EmployeeSu~` FOREIGN KEY (`EmployeeSuspensionReasonId`) REFERENCES `opt_EmployeeSuspensionReasons` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTerminationHistories` (
    `EmployeeTerminationHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeTerminationReasonId` int NOT NULL,
    `TerminationDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTerminationHistories` PRIMARY KEY (`EmployeeTerminationHistoryId`),
    CONSTRAINT `FK_EmployeeTerminationHistories_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeTerminationHistories_opt_EmployeeTerminationReasons_~` FOREIGN KEY (`EmployeeTerminationReasonId`) REFERENCES `opt_EmployeeTerminationReasons` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTrainingRecords` (
    `EmployeeTrainingRecordId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeTrainingStatusId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `CourseName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `CertificateNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CertificateExpiryDate` datetime(6) NULL,
    `IsAddReminder` tinyint(1) NOT NULL,
    `CourseResult` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DirectCourseCost` decimal(65,30) NOT NULL,
    `InvoiceNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EmployeeRate` decimal(65,30) NOT NULL,
    `Facilitator` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TravelCost` decimal(65,30) NOT NULL,
    `Assessor` varchar(120) CHARACTER SET utf8mb4 NULL,
    `AccommodationCost` decimal(65,30) NOT NULL,
    `FacilitatorCost` decimal(65,30) NOT NULL,
    `FoodAndBeverageCost` decimal(65,30) NOT NULL,
    `Additional5Cost` decimal(65,30) NOT NULL,
    `Additional6Cost` decimal(65,30) NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTrainingRecords` PRIMARY KEY (`EmployeeTrainingRecordId`),
    CONSTRAINT `FK_EmployeeTrainingRecords_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeTrainingRecords_opt_EmployeeTrainingStatuses_Employe~` FOREIGN KEY (`EmployeeTrainingStatusId`) REFERENCES `opt_EmployeeTrainingStatuses` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTrainings` (
    `EmployeeId` int NOT NULL,
    `TrainingId` int NOT NULL,
    `DirectCost` decimal(65,30) NOT NULL,
    `AdditionalCost` decimal(65,30) NOT NULL,
    `TotalCost` decimal(65,30) AS (DirectCost + AdditionalCost + AccommodationCost + CarHireCost + CateringCost + FlightsCost + FuelCost + StationeryCost + VenueCost + OtherCost),
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `AccommodationCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `CarHireCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `CateringCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `FlightsCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `FuelCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `StationeryCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `VenueCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `OtherCost` decimal(65,30) NOT NULL DEFAULT 0.0,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTrainings` PRIMARY KEY (`EmployeeId`, `TrainingId`),
    CONSTRAINT `FK_EmployeeTrainings_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeTrainings_Trainings_TrainingId` FOREIGN KEY (`TrainingId`) REFERENCES `Trainings` (`TrainingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeTransactions` (
    `EmployeeTransactionId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `EmployeeTransactionTypeId` int NOT NULL,
    `EmployeeRecurringTransactionId` int NULL,
    `EmployeeTransactionStatusId` int NULL,
    `EmployeeRecurringTransactionStatusId` int NULL,
    `PayrollPeriodId` int NOT NULL,
    `EmployeeRecurringTransactionCount` int NOT NULL,
    `TransactionDate` datetime(6) NULL,
    `Amount` decimal(65,30) NOT NULL,
    `Rate` decimal(65,30) NOT NULL,
    `Days` float NOT NULL,
    `Hours` float NOT NULL,
    `UnpaidDays` float NOT NULL,
    `UnpaidHours` float NOT NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `ApprovedDate` datetime(6) NULL,
    `ApprovedBy` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedDate` datetime(6) NULL,
    `RejectedBy` longtext CHARACTER SET utf8mb4 NULL,
    `RejectedReason` longtext CHARACTER SET utf8mb4 NULL,
    `EmployeeTransactionRemunerationTypeId` int NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeTransactions` PRIMARY KEY (`EmployeeTransactionId`),
    CONSTRAINT `FK_EmployeeTransactions_EmployeeRecurringTransactionStatuses_Em~` FOREIGN KEY (`EmployeeRecurringTransactionStatusId`) REFERENCES `EmployeeRecurringTransactionStatuses` (`EmployeeRecurringTransactionStatusId`),
    CONSTRAINT `FK_EmployeeTransactions_EmployeeRecurringTransactions_EmployeeR~` FOREIGN KEY (`EmployeeRecurringTransactionId`) REFERENCES `EmployeeRecurringTransactions` (`EmployeeRecurringTransactionId`),
    CONSTRAINT `FK_EmployeeTransactions_EmployeeTransactionRemunerationTypes_Em~` FOREIGN KEY (`EmployeeTransactionRemunerationTypeId`) REFERENCES `EmployeeTransactionRemunerationTypes` (`EmployeeTransactionRemunerationTypeId`),
    CONSTRAINT `FK_EmployeeTransactions_EmployeeTransactionStatuses_EmployeeTra~` FOREIGN KEY (`EmployeeTransactionStatusId`) REFERENCES `EmployeeTransactionStatuses` (`EmployeeTransactionStatusId`),
    CONSTRAINT `FK_EmployeeTransactions_EmployeeTransactionTypes_EmployeeTransa~` FOREIGN KEY (`EmployeeTransactionTypeId`) REFERENCES `EmployeeTransactionTypes` (`EmployeeTransactionTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeTransactions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeTransactions_PayrollPeriods_PayrollPeriodId` FOREIGN KEY (`PayrollPeriodId`) REFERENCES `PayrollPeriods` (`PayrollPeriodId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeVehicles` (
    `EmployeeVehicleId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `VehicleTypeId` int NOT NULL,
    `VehicleBrandId` int NOT NULL,
    `AssetStatusId` int NOT NULL,
    `AssetOwnerId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Tracker` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Year` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RegistrationNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Vin` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `RecievedDate` datetime(6) NULL,
    `HandedBackDate` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeVehicles` PRIMARY KEY (`EmployeeVehicleId`),
    CONSTRAINT `FK_EmployeeVehicles_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicles_opt_AssetOwners_AssetOwnerId` FOREIGN KEY (`AssetOwnerId`) REFERENCES `opt_AssetOwners` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicles_opt_AssetStatuses_AssetStatusId` FOREIGN KEY (`AssetStatusId`) REFERENCES `opt_AssetStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicles_opt_VehicleBrands_VehicleBrandId` FOREIGN KEY (`VehicleBrandId`) REFERENCES `opt_VehicleBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicles_opt_VehicleTypes_VehicleTypeId` FOREIGN KEY (`VehicleTypeId`) REFERENCES `opt_VehicleTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeWorkRoles` (
    `EmployeeWorkRoleId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `ManagerId` int NOT NULL,
    `EmploymentTypeId` int NOT NULL,
    `Title` varchar(300) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `ExitReason` longtext CHARACTER SET utf8mb4 NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `GradeLevel` int NOT NULL,
    `GradeId` int NULL,
    `VacancyId` int NULL,
    `StatusId` int NULL,
    `IsPromotion` tinyint(1) NOT NULL,
    `IsCurrentRole` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeWorkRoles` PRIMARY KEY (`EmployeeWorkRoleId`),
    CONSTRAINT `FK_EmployeeWorkRoles_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeWorkRoles_Employees_ManagerId` FOREIGN KEY (`ManagerId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeWorkRoles_Vacancies_VacancyId` FOREIGN KEY (`VacancyId`) REFERENCES `Vacancies` (`VacancyId`),
    CONSTRAINT `FK_EmployeeWorkRoles_opt_EmploymentTypes_EmploymentTypeId` FOREIGN KEY (`EmploymentTypeId`) REFERENCES `opt_EmploymentTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeWorkRoles_opt_Grades_GradeId` FOREIGN KEY (`GradeId`) REFERENCES `opt_Grades` (`Id`),
    CONSTRAINT `FK_EmployeeWorkRoles_opt_WorkRoleStatuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `opt_WorkRoleStatuses` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `NotificationEmployeeReads` (
    `NotificationId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `ReadDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_NotificationEmployeeReads` PRIMARY KEY (`NotificationId`, `EmployeeId`),
    CONSTRAINT `FK_NotificationEmployeeReads_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_NotificationEmployeeReads_Notifications_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notifications` (`NotificationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentLineEmployees` (
    `PaymentLineEmployeeId` int NOT NULL AUTO_INCREMENT,
    `PaymentLineId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentLineEmployees` PRIMARY KEY (`PaymentLineEmployeeId`),
    CONSTRAINT `FK_PaymentLineEmployees_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentLineEmployees_PaymentLines_PaymentLineId` FOREIGN KEY (`PaymentLineId`) REFERENCES `PaymentLines` (`PaymentLineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductTransactions` (
    `ProductTransactionId` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `ProductTransactionTypeId` int NOT NULL,
    `ProductTransactionStatusId` int NOT NULL,
    `ProductPeriodId` int NOT NULL,
    `ProductWarehouseId` int NOT NULL,
    `EmployeeId` int NULL,
    `EngageRegionId` int NULL,
    `Quantity` float NOT NULL DEFAULT 0,
    `Price` decimal(65,30) NOT NULL DEFAULT 0.0,
    `TransactionDate` datetime(6) NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductTransactions` PRIMARY KEY (`ProductTransactionId`),
    CONSTRAINT `FK_ProductTransactions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_ProductTransactions_ProductPeriods_ProductPeriodId` FOREIGN KEY (`ProductPeriodId`) REFERENCES `ProductPeriods` (`ProductPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductTransactions_ProductTransactionStatuses_ProductTransa~` FOREIGN KEY (`ProductTransactionStatusId`) REFERENCES `ProductTransactionStatuses` (`ProductTransactionStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductTransactions_ProductTransactionTypes_ProductTransacti~` FOREIGN KEY (`ProductTransactionTypeId`) REFERENCES `ProductTransactionTypes` (`ProductTransactionTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductTransactions_ProductWarehouses_ProductWarehouseId` FOREIGN KEY (`ProductWarehouseId`) REFERENCES `ProductWarehouses` (`ProductWarehouseId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductTransactions_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`ProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductTransactions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrainingFacilitators` (
    `TrainingId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `DirectCost` decimal(65,30) NOT NULL,
    `AdditionalCost` decimal(65,30) NOT NULL,
    `TotalCost` decimal(65,30) AS (DirectCost + AdditionalCost),
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_TrainingFacilitators` PRIMARY KEY (`EmployeeId`, `TrainingId`),
    CONSTRAINT `FK_TrainingFacilitators_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_TrainingFacilitators_Trainings_TrainingId` FOREIGN KEY (`TrainingId`) REFERENCES `Trainings` (`TrainingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebPageEmployees` (
    `WebPageEmployeeId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `WebPageId` int NOT NULL,
    `ViewDate` datetime(6) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebPageEmployees` PRIMARY KEY (`WebPageEmployeeId`),
    CONSTRAINT `FK_WebPageEmployees_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebPageEmployees_WebPages_WebPageId` FOREIGN KEY (`WebPageId`) REFERENCES `WebPages` (`WebPageId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeSkillFiles` (
    `EmployeeSkillFileId` int NOT NULL AUTO_INCREMENT,
    `EmployeeSkillId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `FileContainerId` int NOT NULL,
    `Name` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Url` varchar(1024) CHARACTER SET utf8mb4 NOT NULL,
    `Metadata` varchar(4000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EmployeeSkillFiles` PRIMARY KEY (`EmployeeSkillFileId`),
    CONSTRAINT `FK_EmployeeSkillFiles_EmployeeSkills_EmployeeSkillId` FOREIGN KEY (`EmployeeSkillId`) REFERENCES `EmployeeSkills` (`EmployeeSkillId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeSkillFiles_FileContainers_FileContainerId` FOREIGN KEY (`FileContainerId`) REFERENCES `FileContainers` (`FileContainerId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeVehicleHistories` (
    `EmployeeVehicleHistoryId` int NOT NULL AUTO_INCREMENT,
    `EmployeeVehicleId` int NOT NULL,
    `OldEmployeeId` int NOT NULL,
    `NewEmployeeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeVehicleHistories` PRIMARY KEY (`EmployeeVehicleHistoryId`),
    CONSTRAINT `FK_EmployeeVehicleHistories_EmployeeVehicles_EmployeeVehicleId` FOREIGN KEY (`EmployeeVehicleId`) REFERENCES `EmployeeVehicles` (`EmployeeVehicleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicleHistories_Employees_NewEmployeeId` FOREIGN KEY (`NewEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeVehicleHistories_Employees_OldEmployeeId` FOREIGN KEY (`OldEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeWorkRoleContacts` (
    `EmployeeWorkRoleContactId` int NOT NULL AUTO_INCREMENT,
    `EmployeeWorkRoleId` int NOT NULL,
    `Title` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `EmailAddress` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `FullName` longtext CHARACTER SET utf8mb4 AS (concat(FirstName,' ',LastName)) NULL,
    `MiddleName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `MobilePhone` varchar(30) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EmployeeWorkRoleContacts` PRIMARY KEY (`EmployeeWorkRoleContactId`),
    CONSTRAINT `FK_EmployeeWorkRoleContacts_EmployeeWorkRoles_EmployeeWorkRoleId` FOREIGN KEY (`EmployeeWorkRoleId`) REFERENCES `EmployeeWorkRoles` (`EmployeeWorkRoleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreArchives` (
    `EmployeeStoreArchiveId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EngageDepartmentCategoryId` int NULL,
    `EngageSubGroupId` int NOT NULL,
    `FrequencyTypeId` int NOT NULL,
    `Frequency` int NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreArchives` PRIMARY KEY (`EmployeeStoreArchiveId`),
    CONSTRAINT `FK_EmployeeStoreArchives_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeStoreArchives_opt_EngageDepartmentCategories_EngageD~` FOREIGN KEY (`EngageDepartmentCategoryId`) REFERENCES `opt_EngageDepartmentCategories` (`Id`),
    CONSTRAINT `FK_EmployeeStoreArchives_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreArchives_opt_FrequencyTypes_FrequencyTypeId` FOREIGN KEY (`FrequencyTypeId`) REFERENCES `opt_FrequencyTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendars` (
    `EmployeeStoreCalendarId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EmployeeStoreCalendarTypeId` int NULL,
    `EmployeeStoreCalendarStatusId` int NULL,
    `IsManagerCreated` tinyint(1) NOT NULL,
    `CalendarDate` datetime(6) NOT NULL,
    `Order` int NULL,
    `EmployeeStoreCalendarPeriodId` int NOT NULL,
    `EmployeeStoreCalendarGroupId` int NOT NULL,
    `SurveyInstanceId` int NULL,
    `CompletionDate` datetime(6) NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `EmailedTo` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendars` PRIMARY KEY (`EmployeeStoreCalendarId`),
    CONSTRAINT `FK_EmployeeStoreCalendars_EmployeeStoreCalendarGroups_EmployeeS~` FOREIGN KEY (`EmployeeStoreCalendarGroupId`) REFERENCES `EmployeeStoreCalendarGroups` (`EmployeeStoreCalendarGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendars_EmployeeStoreCalendarPeriods_Employee~` FOREIGN KEY (`EmployeeStoreCalendarPeriodId`) REFERENCES `EmployeeStoreCalendarPeriods` (`EmployeeStoreCalendarPeriodId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendars_EmployeeStoreCalendarStatuses_Employe~` FOREIGN KEY (`EmployeeStoreCalendarStatusId`) REFERENCES `EmployeeStoreCalendarStatuses` (`EmployeeStoreCalendarStatusId`),
    CONSTRAINT `FK_EmployeeStoreCalendars_EmployeeStoreCalendarTypes_EmployeeSt~` FOREIGN KEY (`EmployeeStoreCalendarTypeId`) REFERENCES `EmployeeStoreCalendarTypes` (`EmployeeStoreCalendarTypeId`),
    CONSTRAINT `FK_EmployeeStoreCalendars_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarSurveyFormSubmissions` (
    `EmployeeStoreCalendarSurveyFormSubmissionId` int NOT NULL AUTO_INCREMENT,
    `EmployeeStoreCalendarId` int NOT NULL,
    `SurveyFormSubmissionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCalendarSurveyFormSubmissions` PRIMARY KEY (`EmployeeStoreCalendarSurveyFormSubmissionId`),
    CONSTRAINT `FK_EmployeeStoreCalendarSurveyFormSubmissions_EmployeeStoreCale~` FOREIGN KEY (`EmployeeStoreCalendarId`) REFERENCES `EmployeeStoreCalendars` (`EmployeeStoreCalendarId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCheckIns` (
    `EmployeeStoreCheckInId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `CheckInTimezoneDate` datetime(6) NOT NULL,
    `CheckInUTCDate` datetime(6) NOT NULL,
    `CheckInLat` float NOT NULL,
    `CheckInLong` float NOT NULL,
    `CheckInDistance` float NOT NULL,
    `CheckOutTimezoneDate` datetime(6) NULL,
    `CheckOutUTCDate` datetime(6) NULL,
    `CheckOutLat` float NULL,
    `CheckOutLong` float NULL,
    `CheckOutDistance` float NULL,
    `CheckInUuid` varchar(255) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreCheckIns` PRIMARY KEY (`EmployeeStoreCheckInId`),
    CONSTRAINT `FK_EmployeeStoreCheckIns_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreKpis` (
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EmployeeKpiId` int NOT NULL,
    `EmployeeKpiTierId` int NULL,
    `Score` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EmployeeStoreKpis` PRIMARY KEY (`EmployeeId`, `StoreId`, `EmployeeKpiId`),
    CONSTRAINT `FK_EmployeeStoreKpis_EmployeeKpiTiers_EmployeeKpiTierId` FOREIGN KEY (`EmployeeKpiTierId`) REFERENCES `EmployeeKpiTiers` (`EmployeeKpiTierId`),
    CONSTRAINT `FK_EmployeeStoreKpis_EmployeeKpis_EmployeeKpiId` FOREIGN KEY (`EmployeeKpiId`) REFERENCES `EmployeeKpis` (`EmployeeKpiId`),
    CONSTRAINT `FK_EmployeeStoreKpis_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreKpiScores` (
    `EmployeeStoreKpiScoreId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EmployeeKpiId` int NOT NULL,
    `EmployeeKpiTierId` int NULL,
    `Score` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStoreKpiScores` PRIMARY KEY (`EmployeeStoreKpiScoreId`),
    CONSTRAINT `FK_EmployeeStoreKpiScores_EmployeeKpiTiers_EmployeeKpiTierId` FOREIGN KEY (`EmployeeKpiTierId`) REFERENCES `EmployeeKpiTiers` (`EmployeeKpiTierId`),
    CONSTRAINT `FK_EmployeeStoreKpiScores_EmployeeKpis_EmployeeKpiId` FOREIGN KEY (`EmployeeKpiId`) REFERENCES `EmployeeKpis` (`EmployeeKpiId`),
    CONSTRAINT `FK_EmployeeStoreKpiScores_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStores` (
    `EmployeeStoreId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EngageDepartmentCategoryId` int NULL,
    `EngageSubGroupId` int NOT NULL,
    `FrequencyTypeId` int NOT NULL,
    `Frequency` int NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EmployeeStores` PRIMARY KEY (`EmployeeStoreId`),
    CONSTRAINT `FK_EmployeeStores_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_EmployeeStores_opt_EngageDepartmentCategories_EngageDepartme~` FOREIGN KEY (`EngageDepartmentCategoryId`) REFERENCES `opt_EngageDepartmentCategories` (`Id`),
    CONSTRAINT `FK_EmployeeStores_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStores_opt_FrequencyTypes_FrequencyTypeId` FOREIGN KEY (`FrequencyTypeId`) REFERENCES `opt_FrequencyTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageMasterProducts` (
    `EngageMasterProductId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `ProductClassificationId` int NOT NULL,
    `EngageDepartmentId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `EngageSubCategoryId` int NOT NULL,
    `VatId` int NOT NULL,
    `IsVATProduct` tinyint(1) NOT NULL,
    `IsDairyProduct` tinyint(1) NOT NULL,
    `IsAllSuppliersProduct` tinyint(1) NOT NULL,
    `IsFreshProduct` tinyint(1) NOT NULL,
    `IsDropShipment` tinyint(1) NOT NULL,
    `IsCatalogue` tinyint(1) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageMasterProducts` PRIMARY KEY (`EngageMasterProductId`),
    CONSTRAINT `FK_EngageMasterProducts_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EngageMasterProducts_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EngageMasterProducts_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EngageMasterProducts_opt_EngageSubCategories_EngageSubCatego~` FOREIGN KEY (`EngageSubCategoryId`) REFERENCES `opt_EngageSubCategories` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EngageMasterProducts_opt_ProductClassifications_ProductClass~` FOREIGN KEY (`ProductClassificationId`) REFERENCES `opt_ProductClassifications` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageProductTags` (
    `EngageMasterProductId` int NOT NULL,
    `EngageTagId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageProductTags` PRIMARY KEY (`EngageMasterProductId`, `EngageTagId`),
    CONSTRAINT `FK_EngageProductTags_EngageMasterProducts_EngageMasterProductId` FOREIGN KEY (`EngageMasterProductId`) REFERENCES `EngageMasterProducts` (`EngageMasterProductId`),
    CONSTRAINT `FK_EngageProductTags_opt_EngageTags_EngageTagId` FOREIGN KEY (`EngageTagId`) REFERENCES `opt_EngageTags` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageVariantProducts` (
    `EngageVariantProductId` int NOT NULL AUTO_INCREMENT,
    `EngageMasterProductId` int NOT NULL,
    `UnitTypeId` int NOT NULL,
    `Size` float NOT NULL,
    `PackSize` float NOT NULL,
    `EANNumber` varchar(20) CHARACTER SET utf8mb4 NULL,
    `UnitBarcode` longtext CHARACTER SET utf8mb4 NULL,
    `CaseBarcode` longtext CHARACTER SET utf8mb4 NULL,
    `ShrinkBarcode` longtext CHARACTER SET utf8mb4 NULL,
    `IsMaster` tinyint(1) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageVariantProducts` PRIMARY KEY (`EngageVariantProductId`),
    CONSTRAINT `FK_EngageVariantProducts_EngageMasterProducts_EngageMasterProdu~` FOREIGN KEY (`EngageMasterProductId`) REFERENCES `EngageMasterProducts` (`EngageMasterProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EngageVariantProducts_opt_UnitTypes_UnitTypeId` FOREIGN KEY (`UnitTypeId`) REFERENCES `opt_UnitTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProductFilters` (
    `ProductFilterId` int NOT NULL AUTO_INCREMENT,
    `EngageVariantProductId` int NULL,
    `Barcode` longtext CHARACTER SET utf8mb4 NULL,
    `FileUploadId` int NULL,
    `Filter` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProductFilters` PRIMARY KEY (`ProductFilterId`),
    CONSTRAINT `FK_ProductFilters_EngageVariantProducts_EngageVariantProductId` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`),
    CONSTRAINT `FK_ProductFilters_FileUploads_FileUploadId` FOREIGN KEY (`FileUploadId`) REFERENCES `FileUploads` (`FileUploadId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PromotionProducts` (
    `PromotionProductId` int NOT NULL AUTO_INCREMENT,
    `PromotionProductTypeId` int NOT NULL,
    `EngageVariantProductId` int NOT NULL,
    `PromotionId` int NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PromotionProducts` PRIMARY KEY (`PromotionProductId`),
    CONSTRAINT `FK_PromotionProducts_EngageVariantProducts_EngageVariantProduct~` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PromotionProducts_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `Promotions` (`PromotionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PromotionProducts_opt_PromotionProductTypes_PromotionProduct~` FOREIGN KEY (`PromotionProductTypeId`) REFERENCES `opt_PromotionProductTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageRegionClaimManagers` (
    `EngageRegionId` int NOT NULL,
    `UserId` int NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageRegionClaimManagers` PRIMARY KEY (`EngageRegionId`, `UserId`),
    CONSTRAINT `FK_EngageRegionClaimManagers_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EngageSubGroupSuppliers` (
    `EngageSubGroupId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_EngageSubGroupSuppliers` PRIMARY KEY (`EngageSubGroupId`, `SupplierId`),
    CONSTRAINT `FK_EngageSubGroupSuppliers_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EntityContactCommunicationTypes` (
    `EntityContactCommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `EntityContactId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EntityContactCommunicationTypes` PRIMARY KEY (`EntityContactCommunicationTypeId`),
    CONSTRAINT `FK_EntityContactCommunicationTypes_CommunicationTypes_Communica~` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EntityContactRegions` (
    `EntityContactRegionId` int NOT NULL AUTO_INCREMENT,
    `EntityContactId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EntityContactRegions` PRIMARY KEY (`EntityContactRegionId`),
    CONSTRAINT `FK_EntityContactRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `EntityContacts` (
    `EntityContactId` int NOT NULL AUTO_INCREMENT,
    `EntityContactTypeId` int NOT NULL,
    `UserId` int NULL,
    `EmailAddress1` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `FullName` longtext CHARACTER SET utf8mb4 AS (concat(FirstName,' ',LastName)) NULL,
    `MiddleName` varchar(120) CHARACTER SET utf8mb4 NULL,
    `MobilePhone` varchar(30) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `Discriminator` varchar(21) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EngageRegionId` int NULL,
    `StoreId` int NULL,
    `IsSupplier` tinyint(1) NULL,
    `SupplierId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EntityContacts` PRIMARY KEY (`EntityContactId`),
    CONSTRAINT `FK_EntityContacts_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EntityContacts_opt_EntityContactTypes_EntityContactTypeId` FOREIGN KEY (`EntityContactTypeId`) REFERENCES `opt_EntityContactTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `GLAdjustments` (
    `GLAdjustmentId` int NOT NULL AUTO_INCREMENT,
    `Type` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `GLCode` int NOT NULL,
    `GLDescription` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TransactionDate` datetime(6) NOT NULL,
    `DocumentNo` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `DebitValue` double NOT NULL,
    `CreditValue` double NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Invoice` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `Account` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `SupplierId` int NOT NULL,
    `GLAdjustmentTypeId` int NOT NULL,
    `BudgetYearId` int NULL,
    `BudgetPeriodId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_GLAdjustments` PRIMARY KEY (`GLAdjustmentId`),
    CONSTRAINT `FK_GLAdjustments_BudgetPeriods_BudgetPeriodId` FOREIGN KEY (`BudgetPeriodId`) REFERENCES `BudgetPeriods` (`BudgetPeriodId`),
    CONSTRAINT `FK_GLAdjustments_BudgetYears_BudgetYearId` FOREIGN KEY (`BudgetYearId`) REFERENCES `BudgetYears` (`BudgetYearId`),
    CONSTRAINT `FK_GLAdjustments_opt_GLAdjustmentTypes_GLAdjustmentTypeId` FOREIGN KEY (`GLAdjustmentTypeId`) REFERENCES `opt_GLAdjustmentTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ImportPromotionStores` (
    `ImportPromotionStoreId` int NOT NULL AUTO_INCREMENT,
    `ImportFileId` int NOT NULL,
    `ImportRowType` int NOT NULL,
    `ImportRowMessage` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `RowNo` int NOT NULL,
    `PromotionId` int NOT NULL,
    `StoreId` int NULL,
    `AccountNumber` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ImportPromotionStores` PRIMARY KEY (`ImportPromotionStoreId`),
    CONSTRAINT `FK_ImportPromotionStores_ImportFiles_ImportFileId` FOREIGN KEY (`ImportFileId`) REFERENCES `ImportFiles` (`ImportFileId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ImportPromotionStores_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `Promotions` (`PromotionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ImportSurveyStores` (
    `ImportSurveyStoreId` int NOT NULL AUTO_INCREMENT,
    `ImportFileId` int NOT NULL,
    `ImportRowType` int NOT NULL,
    `ImportRowMessage` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `RowNo` int NOT NULL,
    `SurveyId` int NOT NULL,
    `StoreId` int NULL,
    `AccountNumber` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ImportSurveyStores` PRIMARY KEY (`ImportSurveyStoreId`),
    CONSTRAINT `FK_ImportSurveyStores_ImportFiles_ImportFileId` FOREIGN KEY (`ImportFileId`) REFERENCES `ImportFiles` (`ImportFileId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Incidents` (
    `IncidentId` int NOT NULL AUTO_INCREMENT,
    `ClientTypeId` int NOT NULL,
    `IncidentTypeId` int NOT NULL,
    `IncidentStatusId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `StoreId` int NOT NULL,
    `IncidentNumber` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `IncidentDate` datetime(6) NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Incidents` PRIMARY KEY (`IncidentId`),
    CONSTRAINT `FK_Incidents_IncidentStatuses_IncidentStatusId` FOREIGN KEY (`IncidentStatusId`) REFERENCES `IncidentStatuses` (`IncidentStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Incidents_IncidentTypes_IncidentTypeId` FOREIGN KEY (`IncidentTypeId`) REFERENCES `IncidentTypes` (`IncidentTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Incidents_opt_ClientTypes_ClientTypeId` FOREIGN KEY (`ClientTypeId`) REFERENCES `opt_ClientTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncidentSkus` (
    `IncidentSkuId` int NOT NULL AUTO_INCREMENT,
    `IncidentId` int NOT NULL,
    `IncidentSkuTypeId` int NOT NULL,
    `IncidentSkuStatusId` int NOT NULL,
    `DCProductId` int NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_IncidentSkus` PRIMARY KEY (`IncidentSkuId`),
    CONSTRAINT `FK_IncidentSkus_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_IncidentSkus_IncidentSkuStatuses_IncidentSkuStatusId` FOREIGN KEY (`IncidentSkuStatusId`) REFERENCES `IncidentSkuStatuses` (`IncidentSkuStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_IncidentSkus_IncidentSkuTypes_IncidentSkuTypeId` FOREIGN KEY (`IncidentSkuTypeId`) REFERENCES `IncidentSkuTypes` (`IncidentSkuTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_IncidentSkus_Incidents_IncidentId` FOREIGN KEY (`IncidentId`) REFERENCES `Incidents` (`IncidentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Locations` (
    `LocationId` int NOT NULL AUTO_INCREMENT,
    `StakeholderId` int NOT NULL,
    `LocationTypeId` int NOT NULL,
    `EngageLocationId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `BusinessUnit` varchar(120) CHARACTER SET utf8mb4 NULL,
    `AddressLineOne` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `AddressLineTwo` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `Suburb` varchar(120) CHARACTER SET utf8mb4 NULL,
    `City` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Province` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `PostCode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `Lat` float NULL,
    `Long` float NULL,
    `IsPrimaryAddress` tinyint(1) NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Locations` PRIMARY KEY (`LocationId`),
    CONSTRAINT `FK_Locations_opt_EngageLocations_EngageLocationId` FOREIGN KEY (`EngageLocationId`) REFERENCES `opt_EngageLocations` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Locations_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Locations_opt_LocationTypes_LocationTypeId` FOREIGN KEY (`LocationTypeId`) REFERENCES `opt_LocationTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Manufacturers` (
    `ManufacturerId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `AccountNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Manufacturers` PRIMARY KEY (`ManufacturerId`),
    CONSTRAINT `FK_Manufacturers_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `NotificationTargets` (
    `NotificationTargetId` int NOT NULL AUTO_INCREMENT,
    `NotificationId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EmployeeId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EngageRegionId` int NULL,
    `StoreId` int NULL,
    `StoreFormatId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_NotificationTargets` PRIMARY KEY (`NotificationTargetId`),
    CONSTRAINT `FK_NotificationTargets_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_NotificationTargets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_NotificationTargets_Notifications_NotificationId` FOREIGN KEY (`NotificationId`) REFERENCES `Notifications` (`NotificationId`) ON DELETE CASCADE,
    CONSTRAINT `FK_NotificationTargets_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_NotificationTargets_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `opt_SupplierRegions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_SupplierRegions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSubRegions` (
    `SupplierSubRegionId` int NOT NULL AUTO_INCREMENT,
    `SupplierRegionId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubRegions` PRIMARY KEY (`SupplierSubRegionId`),
    CONSTRAINT `FK_SupplierSubRegions_opt_SupplierRegions_SupplierRegionId` FOREIGN KEY (`SupplierRegionId`) REFERENCES `opt_SupplierRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderEngageDepartments` (
    `OrderId` int NOT NULL,
    `EngageDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_OrderEngageDepartments` PRIMARY KEY (`OrderId`, `EngageDepartmentId`),
    CONSTRAINT `FK_OrderEngageDepartments_opt_EngageDepartments_EngageDepartmen~` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Orders` (
    `OrderId` int NOT NULL AUTO_INCREMENT,
    `OrderTypeId` int NOT NULL,
    `OrderStatusId` int NOT NULL,
    `StoreId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `SupplierId` int NULL,
    `OrderTemplateId` int NULL,
    `OrderDate` datetime(6) NOT NULL,
    `DeliveryDate` datetime(6) NULL,
    `SubmittedDate` datetime(6) NULL,
    `ProcessedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ProcessedDate` datetime(6) NULL,
    `DCAccountNo` varchar(120) CHARACTER SET utf8mb4 NULL,
    `OrderNo` varchar(30) CHARACTER SET utf8mb4 NULL,
    `OrderReference` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Comment` varchar(300) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `VATNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AccountNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Contact` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Address` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `EmailedTo` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`OrderId`),
    CONSTRAINT `FK_Orders_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Orders_OrderTemplates_OrderTemplateId` FOREIGN KEY (`OrderTemplateId`) REFERENCES `OrderTemplates` (`OrderTemplateId`),
    CONSTRAINT `FK_Orders_opt_OrderStatuses_OrderStatusId` FOREIGN KEY (`OrderStatusId`) REFERENCES `opt_OrderStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Orders_opt_OrderTypes_OrderTypeId` FOREIGN KEY (`OrderTypeId`) REFERENCES `opt_OrderTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderSkus` (
    `OrderSkuId` int NOT NULL AUTO_INCREMENT,
    `OrderId` int NOT NULL,
    `OrderSkuTypeId` int NOT NULL,
    `OrderSkuStatusId` int NOT NULL,
    `DCProductId` int NOT NULL,
    `OrderQuantityTypeId` int NOT NULL,
    `OrderTemplateProductId` int NULL,
    `Quantity` int NOT NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Price` decimal(65,30) NOT NULL,
    `PromotionPrice` decimal(65,30) NOT NULL,
    `RecommendedPrice` decimal(65,30) NOT NULL,
    `GrossProfitPercent` decimal(65,30) NOT NULL,
    `Suffix` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `DeliveryDate` datetime(6) NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderSkus` PRIMARY KEY (`OrderSkuId`),
    CONSTRAINT `FK_OrderSkus_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderSkus_OrderTemplateProducts_OrderTemplateProductId` FOREIGN KEY (`OrderTemplateProductId`) REFERENCES `OrderTemplateProducts` (`OrderTemplateProductId`),
    CONSTRAINT `FK_OrderSkus_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`OrderId`),
    CONSTRAINT `FK_OrderSkus_opt_OrderQuantityTypes_OrderQuantityTypeId` FOREIGN KEY (`OrderQuantityTypeId`) REFERENCES `opt_OrderQuantityTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderSkus_opt_OrderSkuStatuses_OrderSkuStatusId` FOREIGN KEY (`OrderSkuStatusId`) REFERENCES `opt_OrderSkuStatuses` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderSkus_opt_OrderSkuTypes_OrderSkuTypeId` FOREIGN KEY (`OrderSkuTypeId`) REFERENCES `opt_OrderSkuTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentNotificationStatusUsers` (
    `PaymentNotificationStatusUserId` int NOT NULL AUTO_INCREMENT,
    `PaymentStatusId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_PaymentNotificationStatusUsers` PRIMARY KEY (`PaymentNotificationStatusUserId`),
    CONSTRAINT `FK_PaymentNotificationStatusUsers_PaymentStatuses_PaymentStatus~` FOREIGN KEY (`PaymentStatusId`) REFERENCES `PaymentStatuses` (`PaymentStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentNotificationStatusUsers_opt_EngageRegions_EngageRegio~` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectAssignees` (
    `ProjectAssigneeId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `ProjectStakeholderId` int NOT NULL,
    `ProjectStatusId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectAssignees` PRIMARY KEY (`ProjectAssigneeId`),
    CONSTRAINT `FK_ProjectAssignees_ProjectStatuses_ProjectStatusId` FOREIGN KEY (`ProjectStatusId`) REFERENCES `ProjectStatuses` (`ProjectStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectCategorySuppliers` (
    `ProjectCategorySupplierId` int NOT NULL AUTO_INCREMENT,
    `ProjectCategoryId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectCategorySuppliers` PRIMARY KEY (`ProjectCategorySupplierId`),
    CONSTRAINT `FK_ProjectCategorySuppliers_ProjectCategories_ProjectCategoryId` FOREIGN KEY (`ProjectCategoryId`) REFERENCES `ProjectCategories` (`ProjectCategoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectComments` (
    `ProjectCommentId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `Comment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ProjectStatusId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectComments` PRIMARY KEY (`ProjectCommentId`),
    CONSTRAINT `FK_ProjectComments_ProjectStatuses_ProjectStatusId` FOREIGN KEY (`ProjectStatusId`) REFERENCES `ProjectStatuses` (`ProjectStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectDcProducts` (
    `ProjectDcProductId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `DcProductId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectDcProducts` PRIMARY KEY (`ProjectDcProductId`),
    CONSTRAINT `FK_ProjectDcProducts_DCProducts_DcProductId` FOREIGN KEY (`DcProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectEngageBrands` (
    `ProjectEngageBrandId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectEngageBrands` PRIMARY KEY (`ProjectEngageBrandId`),
    CONSTRAINT `FK_ProjectEngageBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectFiles` (
    `ProjectFileId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `ProjectFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectFiles` PRIMARY KEY (`ProjectFileId`),
    CONSTRAINT `FK_ProjectFiles_ProjectFileTypes_ProjectFileTypeId` FOREIGN KEY (`ProjectFileTypeId`) REFERENCES `ProjectFileTypes` (`ProjectFileTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectNotes` (
    `ProjectNoteId` int NOT NULL AUTO_INCREMENT,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ProjectId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectNotes` PRIMARY KEY (`ProjectNoteId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectProjectTags` (
    `ProjectProjectTagId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimId` int NULL,
    `DCProductId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EngageRegionId` int NULL,
    `OrderId` int NULL,
    `StoreId` int NULL,
    `StoreAssetId` int NULL,
    `SupplierId` int NULL,
    `UserId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectProjectTags` PRIMARY KEY (`ProjectProjectTagId`),
    CONSTRAINT `FK_ProjectProjectTags_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectProjectTags_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectProjectTags_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectProjectTags_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`OrderId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectProjectTags_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Projects` (
    `ProjectId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` json NULL,
    `ProjectTypeId` int NOT NULL,
    `ProjectSubTypeId` int NULL,
    `ProjectStatusId` int NOT NULL,
    `ProjectPriorityId` int NOT NULL,
    `EngageRegionId` int NULL,
    `ProjectCampaignId` int NULL,
    `ProjectCategoryId` int NULL,
    `ProjectSubCategoryId` int NULL,
    `OwnerId` int NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `EstimatedHours` float NULL,
    `RemainingHours` float NULL,
    `OpenedDate` datetime(6) NULL,
    `OpenedBy` longtext CHARACTER SET utf8mb4 NULL,
    `AssignedDate` datetime(6) NULL,
    `AssignedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ClosedDate` datetime(6) NULL,
    `ClosedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Emails` longtext CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `Discriminator` varchar(13) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `StoreId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Projects` PRIMARY KEY (`ProjectId`),
    CONSTRAINT `FK_Projects_ProjectCampaigns_ProjectCampaignId` FOREIGN KEY (`ProjectCampaignId`) REFERENCES `ProjectCampaigns` (`ProjectCampaignId`),
    CONSTRAINT `FK_Projects_ProjectCategories_ProjectCategoryId` FOREIGN KEY (`ProjectCategoryId`) REFERENCES `ProjectCategories` (`ProjectCategoryId`),
    CONSTRAINT `FK_Projects_ProjectPriorities_ProjectPriorityId` FOREIGN KEY (`ProjectPriorityId`) REFERENCES `ProjectPriorities` (`ProjectPriorityId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Projects_ProjectStatuses_ProjectStatusId` FOREIGN KEY (`ProjectStatusId`) REFERENCES `ProjectStatuses` (`ProjectStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Projects_ProjectSubCategories_ProjectSubCategoryId` FOREIGN KEY (`ProjectSubCategoryId`) REFERENCES `ProjectSubCategories` (`ProjectSubCategoryId`),
    CONSTRAINT `FK_Projects_ProjectSubTypes_ProjectSubTypeId` FOREIGN KEY (`ProjectSubTypeId`) REFERENCES `ProjectSubTypes` (`ProjectSubTypeId`),
    CONSTRAINT `FK_Projects_ProjectTypes_ProjectTypeId` FOREIGN KEY (`ProjectTypeId`) REFERENCES `ProjectTypes` (`ProjectTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Projects_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `projectStatusHistories` (
    `ProjectStatusHistoryId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `ProjectStatusId` int NOT NULL,
    `Reason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_projectStatusHistories` PRIMARY KEY (`ProjectStatusHistoryId`),
    CONSTRAINT `FK_projectStatusHistories_ProjectStatuses_ProjectStatusId` FOREIGN KEY (`ProjectStatusId`) REFERENCES `ProjectStatuses` (`ProjectStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_projectStatusHistories_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectStakeholders` (
    `ProjectStakeholderId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `Discriminator` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EmployeeRegionContactId` int NULL,
    `ProjectExternalUserId` int NULL,
    `StoreContactId` int NULL,
    `SupplierContactId` int NULL,
    `UserId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectStakeholders` PRIMARY KEY (`ProjectStakeholderId`),
    CONSTRAINT `FK_ProjectStakeholders_EmployeeRegionContacts_EmployeeRegionCon~` FOREIGN KEY (`EmployeeRegionContactId`) REFERENCES `EmployeeRegionContacts` (`EmployeeRegionContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectStakeholders_EntityContacts_StoreContactId` FOREIGN KEY (`StoreContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectStakeholders_EntityContacts_SupplierContactId` FOREIGN KEY (`SupplierContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectStakeholders_ProjectExternalUsers_ProjectExternalUser~` FOREIGN KEY (`ProjectExternalUserId`) REFERENCES `ProjectExternalUsers` (`ProjectExternalUserId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectStakeholders_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectStoreAssets` (
    `ProjectStoreAssetId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `StoreAssetId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectStoreAssets` PRIMARY KEY (`ProjectStoreAssetId`),
    CONSTRAINT `FK_ProjectStoreAssets_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectSuppliers` (
    `ProjectSupplierId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectSuppliers` PRIMARY KEY (`ProjectSupplierId`),
    CONSTRAINT `FK_ProjectSuppliers_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTacOpRegions` (
    `ProjectTacOpId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTacOpRegions` PRIMARY KEY (`ProjectTacOpId`, `EngageRegionId`),
    CONSTRAINT `FK_ProjectTacOpRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTacOps` (
    `ProjectTacOpId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `MobilePhone` varchar(20) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTacOps` PRIMARY KEY (`ProjectTacOpId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskAssignees` (
    `ProjectTaskAssigneeId` int NOT NULL AUTO_INCREMENT,
    `ProjectTaskId` int NOT NULL,
    `ProjectStakeholderId` int NOT NULL,
    `ProjectTaskStatusId` int NULL,
    `ProjectId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskAssignees` PRIMARY KEY (`ProjectTaskAssigneeId`),
    CONSTRAINT `FK_ProjectTaskAssignees_ProjectTaskStatuses_ProjectTaskStatusId` FOREIGN KEY (`ProjectTaskStatusId`) REFERENCES `ProjectTaskStatuses` (`ProjectTaskStatusId`),
    CONSTRAINT `FK_ProjectTaskAssignees_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskComments` (
    `ProjectTaskCommentId` int NOT NULL AUTO_INCREMENT,
    `ProjectTaskId` int NOT NULL,
    `Comment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ProjectStatusId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskComments` PRIMARY KEY (`ProjectTaskCommentId`),
    CONSTRAINT `FK_ProjectTaskComments_ProjectStatuses_ProjectStatusId` FOREIGN KEY (`ProjectStatusId`) REFERENCES `ProjectStatuses` (`ProjectStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskNotes` (
    `ProjectTaskNoteId` int NOT NULL AUTO_INCREMENT,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `ProjectTaskId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskNotes` PRIMARY KEY (`ProjectTaskNoteId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskProjectStakeholderUsers` (
    `ProjectTaskProjectStakeholderUserId` int NOT NULL AUTO_INCREMENT,
    `ProjectTaskId` int NOT NULL,
    `ProjectStakeholderId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskProjectStakeholderUsers` PRIMARY KEY (`ProjectTaskProjectStakeholderUserId`),
    CONSTRAINT `FK_ProjectTaskProjectStakeholderUsers_ProjectStakeholders_Proje~` FOREIGN KEY (`ProjectStakeholderId`) REFERENCES `ProjectStakeholders` (`ProjectStakeholderId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTasks` (
    `ProjectTaskId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `ProjectId` int NOT NULL,
    `ProjectTaskTypeId` int NULL,
    `ProjectTaskStatusId` int NOT NULL,
    `ProjectTaskPriorityId` int NULL,
    `ProjectTaskSeverityId` int NULL,
    `UserId` int NULL,
    `ProjectStakeholderId` int NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `EstimatedHours` float NULL,
    `RemainingHours` float NULL,
    `OpenedDate` datetime(6) NULL,
    `OpenedBy` longtext CHARACTER SET utf8mb4 NULL,
    `AssignedDate` datetime(6) NULL,
    `AssignedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ClosedDate` datetime(6) NULL,
    `ClosedBy` longtext CHARACTER SET utf8mb4 NULL,
    `IsClosed` tinyint(1) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTasks` PRIMARY KEY (`ProjectTaskId`),
    CONSTRAINT `FK_ProjectTasks_ProjectStakeholders_ProjectStakeholderId` FOREIGN KEY (`ProjectStakeholderId`) REFERENCES `ProjectStakeholders` (`ProjectStakeholderId`),
    CONSTRAINT `FK_ProjectTasks_ProjectTaskPriorities_ProjectTaskPriorityId` FOREIGN KEY (`ProjectTaskPriorityId`) REFERENCES `ProjectTaskPriorities` (`ProjectTaskPriorityId`),
    CONSTRAINT `FK_ProjectTasks_ProjectTaskSeverities_ProjectTaskSeverityId` FOREIGN KEY (`ProjectTaskSeverityId`) REFERENCES `ProjectTaskSeverities` (`ProjectTaskSeverityId`),
    CONSTRAINT `FK_ProjectTasks_ProjectTaskStatuses_ProjectTaskStatusId` FOREIGN KEY (`ProjectTaskStatusId`) REFERENCES `ProjectTaskStatuses` (`ProjectTaskStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectTasks_ProjectTaskTypes_ProjectTaskTypeId` FOREIGN KEY (`ProjectTaskTypeId`) REFERENCES `ProjectTaskTypes` (`ProjectTaskTypeId`),
    CONSTRAINT `FK_ProjectTasks_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskStakeholders` (
    `ProjectTaskStakeholderId` int NOT NULL AUTO_INCREMENT,
    `ProjectTaskId` int NOT NULL,
    `ProjectStakeholderId` int NOT NULL,
    `ProjectTaskStatusId` int NOT NULL,
    `Emails` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskStakeholders` PRIMARY KEY (`ProjectTaskStakeholderId`),
    CONSTRAINT `FK_ProjectTaskStakeholders_ProjectStakeholders_ProjectStakehold~` FOREIGN KEY (`ProjectStakeholderId`) REFERENCES `ProjectStakeholders` (`ProjectStakeholderId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectTaskStakeholders_ProjectTaskStatuses_ProjectTaskStatu~` FOREIGN KEY (`ProjectTaskStatusId`) REFERENCES `ProjectTaskStatuses` (`ProjectTaskStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectTaskStakeholders_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectTaskStatusHistories` (
    `ProjectTaskStatusHistoryId` int NOT NULL AUTO_INCREMENT,
    `ProjectTaskId` int NOT NULL,
    `ProjectTaskStatusId` int NOT NULL,
    `Reason` varchar(300) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectTaskStatusHistories` PRIMARY KEY (`ProjectTaskStatusHistoryId`),
    CONSTRAINT `FK_ProjectTaskStatusHistories_ProjectTaskStatuses_ProjectTaskSt~` FOREIGN KEY (`ProjectTaskStatusId`) REFERENCES `ProjectTaskStatuses` (`ProjectTaskStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectTaskStatusHistories_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ProjectUsers` (
    `ProjectId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectUsers` PRIMARY KEY (`ProjectId`, `UserId`),
    CONSTRAINT `FK_ProjectUsers_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PromotionStores` (
    `PromotionId` int NOT NULL,
    `StoreId` int NOT NULL,
    `TargetingId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_PromotionStores` PRIMARY KEY (`PromotionId`, `StoreId`),
    CONSTRAINT `FK_PromotionStores_Promotions_PromotionId` FOREIGN KEY (`PromotionId`) REFERENCES `Promotions` (`PromotionId`),
    CONSTRAINT `FK_PromotionStores_Targetings_TargetingId` FOREIGN KEY (`TargetingId`) REFERENCES `Targetings` (`TargetingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecurityRoleUsers` (
    `SecurityRoleUserId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `SecurityRoleId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SecurityRoleUsers` PRIMARY KEY (`SecurityRoleUserId`),
    CONSTRAINT `FK_SecurityRoleUsers_SecurityRoles_SecurityRoleId` FOREIGN KEY (`SecurityRoleId`) REFERENCES `SecurityRoles` (`SecurityRoleId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparProducts` (
    `SparProductId` int NOT NULL AUTO_INCREMENT,
    `ItemCode` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `UnitSize` float NULL,
    `SparUnitTypeId` int NULL,
    `Barcode` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `EngageBrandId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `EngageSubCategoryId` int NOT NULL,
    `SparProductStatusId` int NOT NULL,
    `SparAnalysisGroupId` int NOT NULL,
    `SparSystemStatusId` int NOT NULL,
    `EvoLedgerId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparProducts` PRIMARY KEY (`SparProductId`),
    CONSTRAINT `FK_SparProducts_EvoLedgers_EvoLedgerId` FOREIGN KEY (`EvoLedgerId`) REFERENCES `EvoLedgers` (`EvoLedgerId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparProducts_SparAnalysisGroups_SparAnalysisGroupId` FOREIGN KEY (`SparAnalysisGroupId`) REFERENCES `SparAnalysisGroups` (`SparAnalysisGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparProducts_SparProductStatuses_SparProductStatusId` FOREIGN KEY (`SparProductStatusId`) REFERENCES `SparProductStatuses` (`SparProductStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparProducts_SparSystemStatuses_SparSystemStatusId` FOREIGN KEY (`SparSystemStatusId`) REFERENCES `SparSystemStatuses` (`SparSystemStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparProducts_SparUnitTypes_SparUnitTypeId` FOREIGN KEY (`SparUnitTypeId`) REFERENCES `SparUnitTypes` (`SparUnitTypeId`),
    CONSTRAINT `FK_SparProducts_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparProducts_opt_EngageSubCategories_EngageSubCategoryId` FOREIGN KEY (`EngageSubCategoryId`) REFERENCES `opt_EngageSubCategories` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SparSubProducts` (
    `SparSubProductId` int NOT NULL AUTO_INCREMENT,
    `SparProductId` int NOT NULL,
    `DCCode` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Barcode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `CaseBarcode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `ShrinkBarcode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `PalletBarcode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `IsPrimary` tinyint(1) NOT NULL,
    `SparSubProductStatusId` int NOT NULL,
    `SparSourceId` int NULL,
    `DistributionCenterId` int NOT NULL,
    `Warehouse` varchar(30) CHARACTER SET utf8mb4 NULL,
    `StockOnHand` float NULL,
    `StockOnOrder` float NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SparSubProducts` PRIMARY KEY (`SparSubProductId`),
    CONSTRAINT `FK_SparSubProducts_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparSubProducts_SparProducts_SparProductId` FOREIGN KEY (`SparProductId`) REFERENCES `SparProducts` (`SparProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SparSubProducts_SparSources_SparSourceId` FOREIGN KEY (`SparSourceId`) REFERENCES `SparSources` (`SparSourceId`),
    CONSTRAINT `FK_SparSubProducts_SparSubProductStatuses_SparSubProductStatusId` FOREIGN KEY (`SparSubProductStatusId`) REFERENCES `SparSubProductStatuses` (`SparSubProductStatusId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stakeholders` (
    `StakeholderId` int NOT NULL AUTO_INCREMENT,
    `VendorId` int NULL,
    `SupplierId` int NULL,
    `StoreId` int NULL,
    `EmployeeId` int NULL,
    `StakeholderType` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Stakeholders` PRIMARY KEY (`StakeholderId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stores` (
    `StoreId` int NOT NULL AUTO_INCREMENT,
    `ParentStoreId` int NULL,
    `StakeholderId` int NOT NULL,
    `StoreClusterId` int NOT NULL,
    `StoreFormatId` int NOT NULL,
    `StoreGroupId` int NOT NULL,
    `StoreLSMId` int NOT NULL,
    `StoreMediaGroupId` int NOT NULL,
    `StoreTypeId` int NOT NULL,
    `StoreLocationTypeId` int NOT NULL,
    `StoreSparRegionId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `EngageSubRegionId` int NULL,
    `EngageLocationId` int NULL,
    `PrimaryLocationId` int NULL,
    `PrimaryContactId` int NULL,
    `CatManStoreCode` varchar(30) CHARACTER SET utf8mb4 NULL,
    `StoreImageUrl` varchar(300) CHARACTER SET utf8mb4 NULL,
    `VatNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `IsHalaal` tinyint(1) NOT NULL,
    `IsNotServiced` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Stores` PRIMARY KEY (`StoreId`),
    CONSTRAINT `FK_Stores_Contacts_PrimaryContactId` FOREIGN KEY (`PrimaryContactId`) REFERENCES `Contacts` (`ContactId`),
    CONSTRAINT `FK_Stores_EngageSubRegions_EngageSubRegionId` FOREIGN KEY (`EngageSubRegionId`) REFERENCES `EngageSubRegions` (`EngageSubRegionId`),
    CONSTRAINT `FK_Stores_Locations_PrimaryLocationId` FOREIGN KEY (`PrimaryLocationId`) REFERENCES `Locations` (`LocationId`),
    CONSTRAINT `FK_Stores_Stakeholders_StakeholderId` FOREIGN KEY (`StakeholderId`) REFERENCES `Stakeholders` (`StakeholderId`),
    CONSTRAINT `FK_Stores_Stores_ParentStoreId` FOREIGN KEY (`ParentStoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_Stores_opt_EngageLocations_EngageLocationId` FOREIGN KEY (`EngageLocationId`) REFERENCES `opt_EngageLocations` (`Id`),
    CONSTRAINT `FK_Stores_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_LocationTypes_StoreLocationTypeId` FOREIGN KEY (`StoreLocationTypeId`) REFERENCES `opt_LocationTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreClusters_StoreClusterId` FOREIGN KEY (`StoreClusterId`) REFERENCES `opt_StoreClusters` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreGroups_StoreGroupId` FOREIGN KEY (`StoreGroupId`) REFERENCES `opt_StoreGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreLSMs_StoreLSMId` FOREIGN KEY (`StoreLSMId`) REFERENCES `opt_StoreLSMs` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreMediaGroups_StoreMediaGroupId` FOREIGN KEY (`StoreMediaGroupId`) REFERENCES `opt_StoreMediaGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreSparRegions_StoreSparRegionId` FOREIGN KEY (`StoreSparRegionId`) REFERENCES `opt_StoreSparRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Stores_opt_StoreTypes_StoreTypeId` FOREIGN KEY (`StoreTypeId`) REFERENCES `opt_StoreTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Suppliers` (
    `SupplierId` int NOT NULL AUTO_INCREMENT,
    `StakeholderId` int NOT NULL,
    `SupplierGroupId` int NOT NULL,
    `PrimaryLocationId` int NULL,
    `PrimaryContactId` int NULL,
    `ShortName` varchar(30) CHARACTER SET utf8mb4 NULL,
    `VATNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `StringSettings` json NULL,
    `NumberSettings` json NULL,
    `BooleanSettings` json NULL,
    `Files` json NULL,
    `Settings` json NULL,
    `OrderModuleEnabled` tinyint(1) NOT NULL,
    `IsSupplierProductsOnly` tinyint(1) NOT NULL,
    `ClaimModuleEnabled` tinyint(1) NOT NULL,
    `IsDairy` tinyint(1) NOT NULL,
    `IsClaimAccountManager` tinyint(1) NOT NULL,
    `IsClaimManager` tinyint(1) NOT NULL,
    `ClaimReportTitle` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ClaimReportAccountNumber` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsClaimAccountManagerRequired` tinyint(1) NOT NULL,
    `IsClaimFloatRequired` tinyint(1) NOT NULL,
    `IsEmployeeClaim` tinyint(1) NOT NULL,
    `IsSubContractor` tinyint(1) NOT NULL,
    `ThemeColor` longtext CHARACTER SET utf8mb4 NULL,
    `ThemeCustomColor` longtext CHARACTER SET utf8mb4 NULL,
    `JsonTheme` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Suppliers` PRIMARY KEY (`SupplierId`),
    CONSTRAINT `FK_Suppliers_Contacts_PrimaryContactId` FOREIGN KEY (`PrimaryContactId`) REFERENCES `Contacts` (`ContactId`),
    CONSTRAINT `FK_Suppliers_Locations_PrimaryLocationId` FOREIGN KEY (`PrimaryLocationId`) REFERENCES `Locations` (`LocationId`),
    CONSTRAINT `FK_Suppliers_Stakeholders_StakeholderId` FOREIGN KEY (`StakeholderId`) REFERENCES `Stakeholders` (`StakeholderId`),
    CONSTRAINT `FK_Suppliers_opt_SupplierGroups_SupplierGroupId` FOREIGN KEY (`SupplierGroupId`) REFERENCES `opt_SupplierGroups` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssets` (
    `StoreAssetId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StoreAssetOwnerId` int NULL,
    `StoreAssetTypeId` int NOT NULL,
    `StoreAssetSubTypeId` int NULL,
    `StoreAssetConditionId` int NULL,
    `StoreAssetStatusId` int NULL,
    `AssetStatusId` int NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SerialNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EmailAddress` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `InstallDate` datetime(6) NULL,
    `UpliftDate` datetime(6) NULL,
    `HasContract` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssets` PRIMARY KEY (`StoreAssetId`),
    CONSTRAINT `FK_StoreAssets_StoreAssetConditions_StoreAssetConditionId` FOREIGN KEY (`StoreAssetConditionId`) REFERENCES `StoreAssetConditions` (`StoreAssetConditionId`),
    CONSTRAINT `FK_StoreAssets_StoreAssetStatuses_StoreAssetStatusId` FOREIGN KEY (`StoreAssetStatusId`) REFERENCES `StoreAssetStatuses` (`StoreAssetStatusId`),
    CONSTRAINT `FK_StoreAssets_StoreAssetSubTypes_StoreAssetSubTypeId` FOREIGN KEY (`StoreAssetSubTypeId`) REFERENCES `StoreAssetSubTypes` (`StoreAssetSubTypeId`),
    CONSTRAINT `FK_StoreAssets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssets_opt_AssetStatuses_AssetStatusId` FOREIGN KEY (`AssetStatusId`) REFERENCES `opt_AssetStatuses` (`Id`),
    CONSTRAINT `FK_StoreAssets_opt_StoreAssetOwners_StoreAssetOwnerId` FOREIGN KEY (`StoreAssetOwnerId`) REFERENCES `opt_StoreAssetOwners` (`Id`),
    CONSTRAINT `FK_StoreAssets_opt_StoreAssetTypes_StoreAssetTypeId` FOREIGN KEY (`StoreAssetTypeId`) REFERENCES `opt_StoreAssetTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreBankDetails` (
    `StoreBankDetailId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `Bank` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `BranchCode` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `AccountNumber` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `AccountHolder` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `IsPrimary` tinyint(1) NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreBankDetails` PRIMARY KEY (`StoreBankDetailId`),
    CONSTRAINT `FK_StoreBankDetails_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreConceptAttributeValues` (
    `StoreConceptAttributeValueId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StoreConceptAttributeId` int NOT NULL,
    `Value` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StoreConceptAttributeOptionId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreConceptAttributeValues` PRIMARY KEY (`StoreConceptAttributeValueId`),
    CONSTRAINT `FK_StoreConceptAttributeValues_StoreConceptAttributeOptions_Sto~` FOREIGN KEY (`StoreConceptAttributeOptionId`) REFERENCES `StoreConceptAttributeOptions` (`StoreConceptAttributeOptionId`),
    CONSTRAINT `FK_StoreConceptAttributeValues_StoreConceptAttributes_StoreConc~` FOREIGN KEY (`StoreConceptAttributeId`) REFERENCES `StoreConceptAttributes` (`StoreConceptAttributeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreConceptAttributeValues_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreConceptLevels` (
    `StoreConceptLevelId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StoreConceptId` int NOT NULL,
    `Level` int NOT NULL,
    `Files` json NULL,
    `Concepts` json NULL,
    `BlobUrl` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `BlobName` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Target` int NOT NULL,
    `Actual` int NOT NULL,
    `Score` double NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreConceptLevels` PRIMARY KEY (`StoreConceptLevelId`),
    CONSTRAINT `FK_StoreConceptLevels_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreConceptLevels_opt_StoreConcepts_StoreConceptId` FOREIGN KEY (`StoreConceptId`) REFERENCES `opt_StoreConcepts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreCycles` (
    `StoreCycleId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `EngageDepartmentId` int NOT NULL,
    `StoreCycleOperationId` int NOT NULL,
    `FrequencyTypeId` int NOT NULL,
    `Monday` tinyint(1) NOT NULL,
    `Tuesday` tinyint(1) NOT NULL,
    `Wednesday` tinyint(1) NOT NULL,
    `Thursday` tinyint(1) NOT NULL,
    `Friday` tinyint(1) NOT NULL,
    `Saturday` tinyint(1) NOT NULL,
    `Sunday` tinyint(1) NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreCycles` PRIMARY KEY (`StoreCycleId`),
    CONSTRAINT `FK_StoreCycles_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreCycles_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreCycles_opt_FrequencyTypes_FrequencyTypeId` FOREIGN KEY (`FrequencyTypeId`) REFERENCES `opt_FrequencyTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreCycles_opt_StoreCycleOperations_StoreCycleOperationId` FOREIGN KEY (`StoreCycleOperationId`) REFERENCES `opt_StoreCycleOperations` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreFilters` (
    `StoreFilterId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `FileUploadId` int NULL,
    `Filter` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `AS400` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreFilters` PRIMARY KEY (`StoreFilterId`),
    CONSTRAINT `FK_StoreFilters_FileUploads_FileUploadId` FOREIGN KEY (`FileUploadId`) REFERENCES `FileUploads` (`FileUploadId`),
    CONSTRAINT `FK_StoreFilters_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreOwners` (
    `StoreOwnerId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StoreGroupId` int NOT NULL,
    `StoreOwnerTypeId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `IsPrimaryOwner` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreOwners` PRIMARY KEY (`StoreOwnerId`),
    CONSTRAINT `FK_StoreOwners_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreOwners_opt_StoreGroups_StoreGroupId` FOREIGN KEY (`StoreGroupId`) REFERENCES `opt_StoreGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreOwners_opt_StoreOwnerTypes_StoreOwnerTypeId` FOREIGN KEY (`StoreOwnerTypeId`) REFERENCES `opt_StoreOwnerTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StorePOS` (
    `StorePOSId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StorePOSTypeId` int NOT NULL,
    `A0PosterQty` int NOT NULL,
    `A1PosterQty` int NOT NULL,
    `A2PosterQty` int NOT NULL,
    `A3BuntingQty` int NOT NULL,
    `AisleBladesQty` int NOT NULL,
    `HangingMobilesQty` int NOT NULL,
    `ShelfStripsQty` int NOT NULL,
    `ShelfTalkersQty` int NOT NULL,
    `TentCardsQty` int NOT NULL,
    `TableClothsQty` int NOT NULL,
    `WobblersQty` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StorePOS` PRIMARY KEY (`StorePOSId`),
    CONSTRAINT `FK_StorePOS_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StorePOS_opt_StorePOSTypes_StorePOSTypeId` FOREIGN KEY (`StorePOSTypeId`) REFERENCES `opt_StorePOSTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StorePOSFreezerQuestions` (
    `StorePOSFreezerQuestionId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StorePOSTypeId` int NOT NULL,
    `StorePOSFreezerTypeId` int NOT NULL,
    `IsWobblers` tinyint(1) NOT NULL,
    `WobblersComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsFreezerDecals` tinyint(1) NOT NULL,
    `FreezerDecalsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsShelfTalker` tinyint(1) NOT NULL,
    `ShelfTalkerComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StorePOSFreezerQuestions` PRIMARY KEY (`StorePOSFreezerQuestionId`),
    CONSTRAINT `FK_StorePOSFreezerQuestions_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StorePOSFreezerQuestions_opt_StorePOSFreezerTypes_StorePOSFr~` FOREIGN KEY (`StorePOSFreezerTypeId`) REFERENCES `opt_StorePOSFreezerTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_StorePOSFreezerQuestions_opt_StorePOSTypes_StorePOSTypeId` FOREIGN KEY (`StorePOSTypeId`) REFERENCES `opt_StorePOSTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StorePOSQuestions` (
    `StorePOSQuestionId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StorePOSTypeId` int NOT NULL,
    `IsFridgeDecals` tinyint(1) NOT NULL,
    `FridgeDecalsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsFloorDecals` tinyint(1) NOT NULL,
    `FloorDecalsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsFSUDecals` tinyint(1) NOT NULL,
    `FSUDecalsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsFSUDecalsPaid` tinyint(1) NOT NULL,
    `FSUDecalsPaidComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsShelfStrips` tinyint(1) NOT NULL,
    `ShelfStripsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsAisleBlades` tinyint(1) NOT NULL,
    `AisleBladesComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsStandee` tinyint(1) NOT NULL,
    `StandeeComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsEntryBox` tinyint(1) NOT NULL,
    `EntryBoxComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsBaseWrap` tinyint(1) NOT NULL,
    `BaseWrapComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsGondolaHeader` tinyint(1) NOT NULL,
    `GondolaHeaderComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsHangingMobiles` tinyint(1) NOT NULL,
    `HangingMobilesComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsPollUpBanner` tinyint(1) NOT NULL,
    `PollUpBannerComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsParaciteUnits` tinyint(1) NOT NULL,
    `ParaciteUnitsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsSensorSleaves` tinyint(1) NOT NULL,
    `SensorSleavesComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `IsNeckTags` tinyint(1) NOT NULL,
    `NeckTagsComment` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StorePOSQuestions` PRIMARY KEY (`StorePOSQuestionId`),
    CONSTRAINT `FK_StorePOSQuestions_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StorePOSQuestions_opt_StorePOSTypes_StorePOSTypeId` FOREIGN KEY (`StorePOSTypeId`) REFERENCES `opt_StorePOSTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreStoreConceptPerformances` (
    `StoreId` int NOT NULL,
    `StoreConceptId` int NOT NULL,
    `YearMonth` datetime(6) NOT NULL,
    `FormatTarget` int NOT NULL,
    `StoreSkuCount` int NOT NULL,
    `StoreSkuPercentDist` int NOT NULL,
    `KpiTier` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StoreStoreConceptPerformances` PRIMARY KEY (`StoreId`, `StoreConceptId`),
    CONSTRAINT `FK_StoreStoreConceptPerformances_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_StoreStoreConceptPerformances_opt_StoreConcepts_StoreConcept~` FOREIGN KEY (`StoreConceptId`) REFERENCES `opt_StoreConcepts` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreStoreConcepts` (
    `StoreId` int NOT NULL,
    `StoreConceptId` int NOT NULL,
    `StoreStoreConceptId` int NOT NULL,
    `Level` int NOT NULL,
    `ImageUrl` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreStoreConcepts` PRIMARY KEY (`StoreId`, `StoreConceptId`),
    CONSTRAINT `FK_StoreStoreConcepts_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreStoreConcepts_opt_StoreConcepts_StoreConceptId` FOREIGN KEY (`StoreConceptId`) REFERENCES `opt_StoreConcepts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreStoreDepartments` (
    `StoreId` int NOT NULL,
    `StoreDepartmentId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StoreStoreDepartments` PRIMARY KEY (`StoreId`, `StoreDepartmentId`),
    CONSTRAINT `FK_StoreStoreDepartments_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_StoreStoreDepartments_opt_StoreDepartments_StoreDepartmentId` FOREIGN KEY (`StoreDepartmentId`) REFERENCES `opt_StoreDepartments` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreStoreList` (
    `StoreId` int NOT NULL,
    `StoreListId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StoreStoreList` PRIMARY KEY (`StoreId`, `StoreListId`),
    CONSTRAINT `FK_StoreStoreList_StoreList_StoreListId` FOREIGN KEY (`StoreListId`) REFERENCES `StoreList` (`StoreListId`),
    CONSTRAINT `FK_StoreStoreList_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebFiles` (
    `WebFileId` int NOT NULL AUTO_INCREMENT,
    `WebFileCategoryId` int NOT NULL,
    `FileTypeId` int NOT NULL,
    `TargetStrategyId` int NOT NULL,
    `EmployeeId` int NULL,
    `StoreId` int NULL,
    `NPrintingId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `DisplayName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebFiles` PRIMARY KEY (`WebFileId`),
    CONSTRAINT `FK_WebFiles_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_WebFiles_FileTypes_FileTypeId` FOREIGN KEY (`FileTypeId`) REFERENCES `FileTypes` (`FileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFiles_NPrintings_NPrintingId` FOREIGN KEY (`NPrintingId`) REFERENCES `NPrintings` (`NPrintingId`),
    CONSTRAINT `FK_WebFiles_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_WebFiles_TargetStrategies_TargetStrategyId` FOREIGN KEY (`TargetStrategyId`) REFERENCES `TargetStrategies` (`TargetStrategyId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFiles_WebFileCategories_WebFileCategoryId` FOREIGN KEY (`WebFileCategoryId`) REFERENCES `WebFileCategories` (`WebFileCategoryId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubContractorBrands` (
    `SubContractorBrandId` int NOT NULL AUTO_INCREMENT,
    `ParentId` int NULL,
    `SupplierId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SubContractorBrands` PRIMARY KEY (`SubContractorBrandId`),
    CONSTRAINT `FK_SubContractorBrands_Suppliers_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SubContractorBrands_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SubContractorBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SubContractorBrands_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierAllowanceContracts` (
    `SupplierAllowanceContractId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `SupplierSalesLeadId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NCircularReference` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EncoreReference` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Vendor` varchar(100) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Comment` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierAllowanceContracts` PRIMARY KEY (`SupplierAllowanceContractId`),
    CONSTRAINT `FK_SupplierAllowanceContracts_SupplierSalesLeads_SupplierSalesL~` FOREIGN KEY (`SupplierSalesLeadId`) REFERENCES `SupplierSalesLeads` (`SupplierSalesLeadId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierAllowanceContracts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierAllowances` (
    `SupplierAllowanceId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `Vendor` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NCircular` varchar(100) CHARACTER SET utf8mb4 NULL,
    `WarehouseAllowancePercent` float NOT NULL,
    `WarehouseAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RedistributionPercent` float NOT NULL,
    `RedistributionNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SwellPercent` float NOT NULL,
    `SwellNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RebatePercent` float NOT NULL,
    `RebateNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SettlementPercent` float NOT NULL,
    `SettlementNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EncoreHouseAllowancePercent` float NOT NULL,
    `EncoreHouseAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EncoreTradeMarketingPercent` float NOT NULL,
    `EncoreTradeMarketingNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AdvertisingMarketingAllowancePercent` float NOT NULL,
    `AdvertisingMarketingAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CatmanPercent` float NOT NULL,
    `CatmanNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EngagePercent` float NOT NULL,
    `EngageNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Comment` longtext CHARACTER SET utf8mb4 NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `SupplierSalesLeadId` int NOT NULL,
    `GlSubCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `GlMainCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierAllowances` PRIMARY KEY (`SupplierAllowanceId`),
    CONSTRAINT `FK_SupplierAllowances_SupplierSalesLeads_SupplierSalesLeadId` FOREIGN KEY (`SupplierSalesLeadId`) REFERENCES `SupplierSalesLeads` (`SupplierSalesLeadId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierAllowances_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierClaimClassifications` (
    `SupplierId` int NOT NULL,
    `ClaimClassificationId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SupplierClaimClassifications` PRIMARY KEY (`SupplierId`, `ClaimClassificationId`),
    CONSTRAINT `FK_SupplierClaimClassifications_ClaimClassifications_ClaimClass~` FOREIGN KEY (`ClaimClassificationId`) REFERENCES `ClaimClassifications` (`ClaimClassificationId`),
    CONSTRAINT `FK_SupplierClaimClassifications_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContracts` (
    `SupplierContractId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `SupplierContractTypeId` int NOT NULL,
    `SupplierContractGroupId` int NULL,
    `SupplierContractSubGroupId` int NULL,
    `SupplierContactId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `Vendor` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `Reference1` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Reference2` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `IsEngage` tinyint(1) NOT NULL,
    `IsEncore` tinyint(1) NOT NULL,
    `IsEngine` tinyint(1) NOT NULL,
    `IsSpar` tinyint(1) NOT NULL,
    `IsTops` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContracts` PRIMARY KEY (`SupplierContractId`),
    CONSTRAINT `FK_SupplierContracts_EntityContacts_SupplierContactId` FOREIGN KEY (`SupplierContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContracts_SupplierContractGroups_SupplierContractGro~` FOREIGN KEY (`SupplierContractGroupId`) REFERENCES `SupplierContractGroups` (`SupplierContractGroupId`),
    CONSTRAINT `FK_SupplierContracts_SupplierContractSubGroups_SupplierContract~` FOREIGN KEY (`SupplierContractSubGroupId`) REFERENCES `SupplierContractSubGroups` (`SupplierContractSubGroupId`),
    CONSTRAINT `FK_SupplierContracts_SupplierContractTypes_SupplierContractType~` FOREIGN KEY (`SupplierContractTypeId`) REFERENCES `SupplierContractTypes` (`SupplierContractTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContracts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierEngageBrands` (
    `SupplierId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SupplierEngageBrands` PRIMARY KEY (`SupplierId`, `EngageBrandId`),
    CONSTRAINT `FK_SupplierEngageBrands_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SupplierEngageBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierProducts` (
    `SupplierId` int NOT NULL,
    `EngageMasterProductId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SupplierProducts` PRIMARY KEY (`SupplierId`, `EngageMasterProductId`),
    CONSTRAINT `FK_SupplierProducts_EngageMasterProducts_EngageMasterProductId` FOREIGN KEY (`EngageMasterProductId`) REFERENCES `EngageMasterProducts` (`EngageMasterProductId`),
    CONSTRAINT `FK_SupplierProducts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSettings` (
    `SupplierSettingId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `SettingId` int NOT NULL,
    `Value` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSettings` PRIMARY KEY (`SupplierSettingId`),
    CONSTRAINT `FK_SupplierSettings_Settings_SettingId` FOREIGN KEY (`SettingId`) REFERENCES `Settings` (`SettingId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierSettings_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierStores` (
    `SupplierStoreId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `StoreId` int NOT NULL,
    `EngageSubGroupId` int NOT NULL,
    `FrequencyTypeId` int NOT NULL,
    `SupplierRegionId` int NOT NULL,
    `SupplierSubRegionId` int NULL,
    `Frequency` int NOT NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `AccountNumber` varchar(120) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierStores` PRIMARY KEY (`SupplierStoreId`),
    CONSTRAINT `FK_SupplierStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_SupplierStores_SupplierSubRegions_SupplierSubRegionId` FOREIGN KEY (`SupplierSubRegionId`) REFERENCES `SupplierSubRegions` (`SupplierSubRegionId`),
    CONSTRAINT `FK_SupplierStores_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SupplierStores_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierStores_opt_FrequencyTypes_FrequencyTypeId` FOREIGN KEY (`FrequencyTypeId`) REFERENCES `opt_FrequencyTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierStores_opt_SupplierRegions_SupplierRegionId` FOREIGN KEY (`SupplierRegionId`) REFERENCES `opt_SupplierRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSupplierTypes` (
    `SupplierId` int NOT NULL,
    `SupplierTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SupplierSupplierTypes` PRIMARY KEY (`SupplierId`, `SupplierTypeId`),
    CONSTRAINT `FK_SupplierSupplierTypes_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SupplierSupplierTypes_opt_SupplierTypes_SupplierTypeId` FOREIGN KEY (`SupplierTypeId`) REFERENCES `opt_SupplierTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyForms` (
    `SurveyFormId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormTypeId` int NOT NULL,
    `EngageSubgroupId` int NULL,
    `SupplierId` int NULL,
    `EngageBrandId` int NULL,
    `IsStoreRecurring` tinyint(1) NOT NULL,
    `IsEmployeeSurvey` tinyint(1) NOT NULL,
    `IgnoreSubgroup` tinyint(1) NOT NULL,
    `IsTemplate` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `IsRequired` tinyint(1) NOT NULL,
    `IsRecurring` tinyint(1) NOT NULL,
    `IsDisabled` tinyint(1) NOT NULL,
    `Files` json NULL,
    `Rules` json NULL,
    `Links` json NULL,
    CONSTRAINT `PK_SurveyForms` PRIMARY KEY (`SurveyFormId`),
    CONSTRAINT `FK_SurveyForms_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SurveyForms_SurveyFormTypes_SurveyFormTypeId` FOREIGN KEY (`SurveyFormTypeId`) REFERENCES `SurveyFormTypes` (`SurveyFormTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyForms_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`),
    CONSTRAINT `FK_SurveyForms_opt_EngageSubGroups_EngageSubgroupId` FOREIGN KEY (`EngageSubgroupId`) REFERENCES `opt_EngageSubGroups` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Surveys` (
    `SurveyId` int NOT NULL AUTO_INCREMENT,
    `SurveyTypeId` int NOT NULL,
    `EngageSubGroupId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `EngageMasterProductId` int NULL,
    `Title` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `IsRecurring` tinyint(1) NOT NULL,
    `IsEmployeeTargeting` tinyint(1) NOT NULL,
    `IsRequired` tinyint(1) NOT NULL,
    `IsDisabled` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Surveys` PRIMARY KEY (`SurveyId`),
    CONSTRAINT `FK_Surveys_EngageMasterProducts_EngageMasterProductId` FOREIGN KEY (`EngageMasterProductId`) REFERENCES `EngageMasterProducts` (`EngageMasterProductId`),
    CONSTRAINT `FK_Surveys_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Surveys_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Surveys_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Surveys_opt_SurveyTypes_SurveyTypeId` FOREIGN KEY (`SurveyTypeId`) REFERENCES `opt_SurveyTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserOrganizations` (
    `UserOrganizationId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `SupplierId` int NOT NULL,
    `ThemeName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ThemeColor` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserOrganizations` PRIMARY KEY (`UserOrganizationId`),
    CONSTRAINT `FK_UserOrganizations_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `UserId` int NOT NULL AUTO_INCREMENT,
    `FirstName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `FullName` longtext CHARACTER SET utf8mb4 AS (concat(FirstName,' ',LastName)) NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `MobilePhone` varchar(30) CHARACTER SET utf8mb4 NULL,
    `ExternalId` varchar(120) CHARACTER SET utf8mb4 NULL,
    `SupplierId` int NOT NULL,
    `Settings` json NULL,
    `IgnoreOrderProductFilters` tinyint(1) NOT NULL,
    `OrganizationId` int NULL,
    `RoleId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`UserId`),
    CONSTRAINT `FK_Users_Organizations_OrganizationId` FOREIGN KEY (`OrganizationId`) REFERENCES `Organizations` (`OrganizationId`),
    CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`RoleId`),
    CONSTRAINT `FK_Users_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Vendors` (
    `VendorId` int NOT NULL AUTO_INCREMENT,
    `DistributionCenterId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `AccountNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Vendors` PRIMARY KEY (`VendorId`),
    CONSTRAINT `FK_Vendors_DistributionCenters_DistributionCenterId` FOREIGN KEY (`DistributionCenterId`) REFERENCES `DistributionCenters` (`DistributionCenterId`),
    CONSTRAINT `FK_Vendors_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Vouchers` (
    `VoucherId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
    `Total` decimal(65,30) NOT NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `VoucherStatusId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `ClosedDate` datetime(6) NULL,
    `ClosedBy` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_Vouchers` PRIMARY KEY (`VoucherId`),
    CONSTRAINT `FK_Vouchers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Vouchers_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Vouchers_opt_VoucherStatuses_VoucherStatusId` FOREIGN KEY (`VoucherStatusId`) REFERENCES `opt_VoucherStatuses` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetBlobs` (
    `StoreAssetBlobId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetId` int NOT NULL,
    `ImageUrl` longtext CHARACTER SET utf8mb4 NULL,
    `StoreAssetFileTypeId` int NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetBlobs` PRIMARY KEY (`StoreAssetBlobId`),
    CONSTRAINT `FK_StoreAssetBlobs_StoreAssetFileTypes_StoreAssetFileTypeId` FOREIGN KEY (`StoreAssetFileTypeId`) REFERENCES `StoreAssetFileTypes` (`StoreAssetFileTypeId`),
    CONSTRAINT `FK_StoreAssetBlobs_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetFiles` (
    `StoreAssetFileId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetId` int NOT NULL,
    `ImageUrl` longtext CHARACTER SET utf8mb4 NULL,
    `StoreAssetFileTypeId` int NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetFiles` PRIMARY KEY (`StoreAssetFileId`),
    CONSTRAINT `FK_StoreAssetFiles_StoreAssetFileTypes_StoreAssetFileTypeId` FOREIGN KEY (`StoreAssetFileTypeId`) REFERENCES `StoreAssetFileTypes` (`StoreAssetFileTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssetFiles_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreAssetStoreAssetTypeContacts` (
    `StoreAssetStoreAssetTypeContactId` int NOT NULL AUTO_INCREMENT,
    `StoreAssetId` int NOT NULL,
    `StoreAssetTypeContactId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_StoreAssetStoreAssetTypeContacts` PRIMARY KEY (`StoreAssetStoreAssetTypeContactId`),
    CONSTRAINT `FK_StoreAssetStoreAssetTypeContacts_StoreAssetTypeContacts_Stor~` FOREIGN KEY (`StoreAssetTypeContactId`) REFERENCES `StoreAssetTypeContacts` (`StoreAssetTypeContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_StoreAssetStoreAssetTypeContacts_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StoreConceptAttributeStoreAssets` (
    `StoreConceptAttributeId` int NOT NULL,
    `StoreAssetId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_StoreConceptAttributeStoreAssets` PRIMARY KEY (`StoreConceptAttributeId`, `StoreAssetId`),
    CONSTRAINT `FK_StoreConceptAttributeStoreAssets_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`),
    CONSTRAINT `FK_StoreConceptAttributeStoreAssets_StoreConceptAttributes_Stor~` FOREIGN KEY (`StoreConceptAttributeId`) REFERENCES `StoreConceptAttributes` (`StoreConceptAttributeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WebFileTargets` (
    `WebFileTargetId` int NOT NULL AUTO_INCREMENT,
    `WebFileId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EmployeeId` int NULL,
    `EmployeeDivisionId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EngageDepartmentId` int NULL,
    `EngageRegionId` int NULL,
    `StoreId` int NULL,
    `StoreFormatId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_WebFileTargets` PRIMARY KEY (`WebFileTargetId`),
    CONSTRAINT `FK_WebFileTargets_EmployeeDivisions_EmployeeDivisionId` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_WebFiles_WebFileId` FOREIGN KEY (`WebFileId`) REFERENCES `WebFiles` (`WebFileId`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_WebFileTargets_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierAllowanceSubContracts` (
    `SupplierAllowanceSubContractId` int NOT NULL AUTO_INCREMENT,
    `SupplierAllowanceContractId` int NOT NULL,
    `Category` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Vendor` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Comment` longtext CHARACTER SET utf8mb4 NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `GlSubCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `GlMainCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NCircular` varchar(100) CHARACTER SET utf8mb4 NULL,
    `WarehouseAllowancePercent` float NOT NULL,
    `WarehouseAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RedistributionPercent` float NOT NULL,
    `RedistributionNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SwellPercent` float NOT NULL,
    `SwellNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `RebatePercent` float NOT NULL,
    `RebateNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SettlementPercent` float NOT NULL,
    `SettlementNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EncoreHouseAllowancePercent` float NOT NULL,
    `EncoreHouseAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EncoreTradeMarketingPercent` float NOT NULL,
    `EncoreTradeMarketingNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AdvertisingMarketingAllowancePercent` float NOT NULL,
    `AdvertisingMarketingAllowanceNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CatmanPercent` float NOT NULL,
    `CatmanNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EngagePercent` float NOT NULL,
    `EngageNote` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierAllowanceSubContracts` PRIMARY KEY (`SupplierAllowanceSubContractId`),
    CONSTRAINT `FK_SupplierAllowanceSubContracts_SupplierAllowanceContracts_Sup~` FOREIGN KEY (`SupplierAllowanceContractId`) REFERENCES `SupplierAllowanceContracts` (`SupplierAllowanceContractId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierContractDetails` (
    `SupplierContractDetailId` int NOT NULL AUTO_INCREMENT,
    `SupplierContractId` int NOT NULL,
    `SupplierContractDetailTypeId` int NOT NULL,
    `EngageRegionId` int NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Amount` float NOT NULL,
    `RangeStartAmount` float NULL,
    `RangeEndAmount` float NULL,
    `GlCode` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `GlSubCode` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NOT NULL,
    `Reference1` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierContractDetails` PRIMARY KEY (`SupplierContractDetailId`),
    CONSTRAINT `FK_SupplierContractDetails_SupplierContractDetailTypes_Supplier~` FOREIGN KEY (`SupplierContractDetailTypeId`) REFERENCES `SupplierContractDetailTypes` (`SupplierContractDetailTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContractDetails_SupplierContracts_SupplierContractId` FOREIGN KEY (`SupplierContractId`) REFERENCES `SupplierContracts` (`SupplierContractId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContractDetails_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierSubContracts` (
    `SupplierSubContractId` int NOT NULL AUTO_INCREMENT,
    `SupplierContractId` int NOT NULL,
    `SupplierSubContractTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Reference1` varchar(100) CHARACTER SET utf8mb4 NULL,
    `GlMainCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `GlSubCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(220) CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubContracts` PRIMARY KEY (`SupplierSubContractId`),
    CONSTRAINT `FK_SupplierSubContracts_SupplierContracts_SupplierContractId` FOREIGN KEY (`SupplierContractId`) REFERENCES `SupplierContracts` (`SupplierContractId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierSubContracts_SupplierSubContractTypes_SupplierSubCon~` FOREIGN KEY (`SupplierSubContractTypeId`) REFERENCES `SupplierSubContractTypes` (`SupplierSubContractTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormProducts` (
    `SurveyFormProductId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormId` int NOT NULL,
    `EngageMasterProductId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormProducts` PRIMARY KEY (`SurveyFormProductId`),
    CONSTRAINT `FK_SurveyFormProducts_EngageMasterProducts_EngageMasterProductId` FOREIGN KEY (`EngageMasterProductId`) REFERENCES `EngageMasterProducts` (`EngageMasterProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormProducts_SurveyForms_SurveyFormId` FOREIGN KEY (`SurveyFormId`) REFERENCES `SurveyForms` (`SurveyFormId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionGroups` (
    `SurveyFormQuestionGroupId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormId` int NOT NULL,
    `IsVirtualGroup` tinyint(1) NOT NULL,
    `CategoryTargetValue` float NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DisplayOrder` int NULL,
    `IsRequired` tinyint(1) NULL,
    `Rules` json NULL,
    `Files` json NULL,
    `Metadata` json NULL,
    `Links` json NULL,
    CONSTRAINT `PK_SurveyFormQuestionGroups` PRIMARY KEY (`SurveyFormQuestionGroupId`),
    CONSTRAINT `FK_SurveyFormQuestionGroups_SurveyForms_SurveyFormId` FOREIGN KEY (`SurveyFormId`) REFERENCES `SurveyForms` (`SurveyFormId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormTargets` (
    `SurveyFormTargetId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EmployeeId` int NULL,
    `EmployeeDivisionId` int NULL,
    `EmployeeEngageRegionId` int NULL,
    `EmployeeJobTitleId` int NULL,
    `EngageDepartmentId` int NULL,
    `ExcludedEmployeeId` int NULL,
    `ExcludedStoreId` int NULL,
    `StoreId` int NULL,
    `StoreClusterId` int NULL,
    `StoreEngageRegionId` int NULL,
    `StoreFormatId` int NULL,
    `StoreLSMId` int NULL,
    `StoreTypeId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormTargets` PRIMARY KEY (`SurveyFormTargetId`),
    CONSTRAINT `FK_SurveyFormTargets_EmployeeDivisions_EmployeeDivisionId` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_Employees_ExcludedEmployeeId` FOREIGN KEY (`ExcludedEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_Stores_ExcludedStoreId` FOREIGN KEY (`ExcludedStoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_SurveyForms_SurveyFormId` FOREIGN KEY (`SurveyFormId`) REFERENCES `SurveyForms` (`SurveyFormId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_EngageRegions_EmployeeEngageRegionId` FOREIGN KEY (`EmployeeEngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_EngageRegions_StoreEngageRegionId` FOREIGN KEY (`StoreEngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_StoreClusters_StoreClusterId` FOREIGN KEY (`StoreClusterId`) REFERENCES `opt_StoreClusters` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_StoreLSMs_StoreLSMId` FOREIGN KEY (`StoreLSMId`) REFERENCES `opt_StoreLSMs` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormTargets_opt_StoreTypes_StoreTypeId` FOREIGN KEY (`StoreTypeId`) REFERENCES `opt_StoreTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyEmployees` (
    `SurveyId` int NOT NULL,
    `EmployeeId` int NOT NULL,
    `TargetingId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SurveyEmployees` PRIMARY KEY (`SurveyId`, `EmployeeId`),
    CONSTRAINT `FK_SurveyEmployees_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_SurveyEmployees_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`),
    CONSTRAINT `FK_SurveyEmployees_Targetings_TargetingId` FOREIGN KEY (`TargetingId`) REFERENCES `Targetings` (`TargetingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyEngageRegions` (
    `SurveyId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SurveyEngageRegions` PRIMARY KEY (`SurveyId`, `EngageRegionId`),
    CONSTRAINT `FK_SurveyEngageRegions_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`),
    CONSTRAINT `FK_SurveyEngageRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyInstances` (
    `SurveyInstanceId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `SurveyId` int NOT NULL,
    `Note` varchar(5000) CHARACTER SET utf8mb4 NULL,
    `SurveyDate` date NOT NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyInstances` PRIMARY KEY (`SurveyInstanceId`),
    CONSTRAINT `FK_SurveyInstances_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_SurveyInstances_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_SurveyInstances_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyQuestions` (
    `SurveyQuestionId` int NOT NULL AUTO_INCREMENT,
    `SurveyId` int NOT NULL,
    `QuestionTypeId` int NOT NULL,
    `EngageVariantProductId` int NULL,
    `StoreConceptId` int NULL,
    `StoreConceptAttributeId` int NULL,
    `Question` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `DisplayOrder` int NULL,
    `IsRequired` tinyint(1) NOT NULL,
    `IsFalseOptionRequired` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyQuestions` PRIMARY KEY (`SurveyQuestionId`),
    CONSTRAINT `FK_SurveyQuestions_EngageVariantProducts_EngageVariantProductId` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`),
    CONSTRAINT `FK_SurveyQuestions_StoreConceptAttributes_StoreConceptAttribute~` FOREIGN KEY (`StoreConceptAttributeId`) REFERENCES `StoreConceptAttributes` (`StoreConceptAttributeId`),
    CONSTRAINT `FK_SurveyQuestions_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyQuestions_opt_QuestionTypes_QuestionTypeId` FOREIGN KEY (`QuestionTypeId`) REFERENCES `opt_QuestionTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyQuestions_opt_StoreConcepts_StoreConceptId` FOREIGN KEY (`StoreConceptId`) REFERENCES `opt_StoreConcepts` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyStoreFormats` (
    `SurveyId` int NOT NULL,
    `StoreFormatId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SurveyStoreFormats` PRIMARY KEY (`SurveyId`, `StoreFormatId`),
    CONSTRAINT `FK_SurveyStoreFormats_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`),
    CONSTRAINT `FK_SurveyStoreFormats_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyStores` (
    `SurveyId` int NOT NULL,
    `StoreId` int NOT NULL,
    `TargetingId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SurveyStores` PRIMARY KEY (`SurveyId`, `StoreId`),
    CONSTRAINT `FK_SurveyStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_SurveyStores_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`),
    CONSTRAINT `FK_SurveyStores_Targetings_TargetingId` FOREIGN KEY (`TargetingId`) REFERENCES `Targetings` (`TargetingId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyTargets` (
    `SurveyTargetId` int NOT NULL AUTO_INCREMENT,
    `SurveyId` int NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `EmployeeJobTitleId` int NULL,
    `EmployeeId` int NULL,
    `EngageRegionId` int NULL,
    `StoreFormatId` int NULL,
    `StoreId` int NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyTargets` PRIMARY KEY (`SurveyTargetId`),
    CONSTRAINT `FK_SurveyTargets_EmployeeJobTitles_EmployeeJobTitleId` FOREIGN KEY (`EmployeeJobTitleId`) REFERENCES `EmployeeJobTitles` (`EmployeeJobTitleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyTargets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyTargets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyTargets_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyTargets_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyTargets_opt_StoreFormats_StoreFormatId` FOREIGN KEY (`StoreFormatId`) REFERENCES `opt_StoreFormats` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierClaimAccountManagers` (
    `SupplierId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SupplierClaimAccountManagers` PRIMARY KEY (`SupplierId`, `UserId`),
    CONSTRAINT `FK_SupplierClaimAccountManagers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`),
    CONSTRAINT `FK_SupplierClaimAccountManagers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormSubmissions` (
    `SurveyFormSubmissionId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormId` int NOT NULL,
    `StoreId` int NULL,
    `SubmissionUuid` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StartedDate` datetime(6) NOT NULL,
    `IsComplete` tinyint(1) NOT NULL,
    `CompletedDate` datetime(6) NULL,
    `IsAbandoned` tinyint(1) NOT NULL,
    `AbandonedDate` datetime(6) NULL,
    `Note` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `EmployeeId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_SurveyFormSubmissions` PRIMARY KEY (`SurveyFormSubmissionId`),
    CONSTRAINT `FK_SurveyFormSubmissions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_SurveyFormSubmissions_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_SurveyFormSubmissions_SurveyForms_SurveyFormId` FOREIGN KEY (`SurveyFormId`) REFERENCES `SurveyForms` (`SurveyFormId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormSubmissions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserCommunicationTypes` (
    `UserCommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserCommunicationTypes` PRIMARY KEY (`UserCommunicationTypeId`),
    CONSTRAINT `FK_UserCommunicationTypes_CommunicationTypes_CommunicationTypeId` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserCommunicationTypes_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserEngageSubGroups` (
    `UserEngageSubGroupId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `EngageSubGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserEngageSubGroups` PRIMARY KEY (`UserEngageSubGroupId`),
    CONSTRAINT `FK_UserEngageSubGroups_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserEngageSubGroups_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserEntities` (
    `UserEntityId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `Entity` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Deny` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserEntities` PRIMARY KEY (`UserEntityId`),
    CONSTRAINT `FK_UserEntities_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserOrganizationRoles` (
    `UserOrganizationRoleId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `UserOrganizationId` int NOT NULL,
    `UserRoleId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserOrganizationRoles` PRIMARY KEY (`UserOrganizationRoleId`),
    CONSTRAINT `FK_UserOrganizationRoles_UserOrganizations_UserOrganizationId` FOREIGN KEY (`UserOrganizationId`) REFERENCES `UserOrganizations` (`UserOrganizationId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserOrganizationRoles_UserRoles_UserRoleId` FOREIGN KEY (`UserRoleId`) REFERENCES `UserRoles` (`UserRoleId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserOrganizationRoles_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserRegions` (
    `UserRegionId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserRegions` PRIMARY KEY (`UserRegionId`),
    CONSTRAINT `FK_UserRegions_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserStores` (
    `UserStoreId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `StoreId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserStores` PRIMARY KEY (`UserStoreId`),
    CONSTRAINT `FK_UserStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserStores_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserUserGroups` (
    `UserUserGroupId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `UserGroupId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserUserGroups` PRIMARY KEY (`UserUserGroupId`),
    CONSTRAINT `FK_UserUserGroups_UserGroups_UserGroupId` FOREIGN KEY (`UserGroupId`) REFERENCES `UserGroups` (`UserGroupId`),
    CONSTRAINT `FK_UserUserGroups_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `VoucherDetails` (
    `VoucherDetailId` int NOT NULL AUTO_INCREMENT,
    `VoucherId` int NOT NULL,
    `VoucherDetailStatusId` int NOT NULL,
    `EmployeeId` int NULL,
    `StoreId` int NULL,
    `StoreContactId` int NULL,
    `ClaimId` int NULL,
    `VoucherNumber` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `Note` varchar(300) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `AssignedDate` datetime(6) NULL,
    `AssignedBy` longtext CHARACTER SET utf8mb4 NULL,
    `ClosedDate` datetime(6) NULL,
    `ClosedBy` longtext CHARACTER SET utf8mb4 NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_VoucherDetails` PRIMARY KEY (`VoucherDetailId`),
    CONSTRAINT `FK_VoucherDetails_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`),
    CONSTRAINT `FK_VoucherDetails_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`),
    CONSTRAINT `FK_VoucherDetails_EntityContacts_StoreContactId` FOREIGN KEY (`StoreContactId`) REFERENCES `EntityContacts` (`EntityContactId`),
    CONSTRAINT `FK_VoucherDetails_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`),
    CONSTRAINT `FK_VoucherDetails_Vouchers_VoucherId` FOREIGN KEY (`VoucherId`) REFERENCES `Vouchers` (`VoucherId`) ON DELETE CASCADE,
    CONSTRAINT `FK_VoucherDetails_opt_VoucherDetailStatuses_VoucherDetailStatus~` FOREIGN KEY (`VoucherDetailStatusId`) REFERENCES `opt_VoucherDetailStatuses` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SupplierBudgets` (
    `SupplierBudgetId` int NOT NULL AUTO_INCREMENT,
    `SupplierBudgetVersionId` int NOT NULL,
    `SupplierBudgetTypeId` int NOT NULL,
    `SupplierId` int NOT NULL,
    `SupplierContractDetailId` int NULL,
    `EngageRegionId` int NULL,
    `Amount` float NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierBudgets` PRIMARY KEY (`SupplierBudgetId`),
    CONSTRAINT `FK_SupplierBudgets_SupplierBudgetTypes_SupplierBudgetTypeId` FOREIGN KEY (`SupplierBudgetTypeId`) REFERENCES `SupplierBudgetTypes` (`SupplierBudgetTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierBudgets_SupplierBudgetVersions_SupplierBudgetVersion~` FOREIGN KEY (`SupplierBudgetVersionId`) REFERENCES `SupplierBudgetVersions` (`SupplierBudgetVersionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierBudgets_SupplierContractDetails_SupplierContractDeta~` FOREIGN KEY (`SupplierContractDetailId`) REFERENCES `SupplierContractDetails` (`SupplierContractDetailId`),
    CONSTRAINT `FK_SupplierBudgets_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierBudgets_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionGroupProducts` (
    `SurveyFormQuestionGroupProductId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormQuestionGroupId` int NOT NULL,
    `EngageVariantProductId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormQuestionGroupProducts` PRIMARY KEY (`SurveyFormQuestionGroupProductId`),
    CONSTRAINT `FK_SurveyFormQuestionGroupProducts_EngageVariantProducts_Engage~` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormQuestionGroupProducts_SurveyFormQuestionGroups_Sur~` FOREIGN KEY (`SurveyFormQuestionGroupId`) REFERENCES `SurveyFormQuestionGroups` (`SurveyFormQuestionGroupId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestions` (
    `SurveyFormQuestionId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormQuestionGroupId` int NOT NULL,
    `SurveyFormQuestionTypeId` int NOT NULL,
    `IsReasonRequired` tinyint(1) NOT NULL,
    `MinDateTime` datetime(6) NULL,
    `MaxDateTime` datetime(6) NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `QuestionText` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DisplayOrder` int NULL,
    `IsRequired` tinyint(1) NULL,
    `Notes` longtext CHARACTER SET utf8mb4 NULL,
    `Rules` json NULL,
    `Files` json NULL,
    `Metadata` json NULL,
    `Links` json NULL,
    CONSTRAINT `PK_SurveyFormQuestions` PRIMARY KEY (`SurveyFormQuestionId`),
    CONSTRAINT `FK_SurveyFormQuestions_SurveyFormQuestionGroups_SurveyFormQuest~` FOREIGN KEY (`SurveyFormQuestionGroupId`) REFERENCES `SurveyFormQuestionGroups` (`SurveyFormQuestionGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormQuestions_SurveyFormQuestionTypes_SurveyFormQuesti~` FOREIGN KEY (`SurveyFormQuestionTypeId`) REFERENCES `SurveyFormQuestionTypes` (`SurveyFormQuestionTypeId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyAnswers` (
    `SurveyAnswerId` int NOT NULL AUTO_INCREMENT,
    `SurveyInstanceId` int NOT NULL,
    `SurveyQuestionId` int NOT NULL,
    `QuestionFalseReasonId` int NULL,
    `Answer` varchar(5000) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyAnswers` PRIMARY KEY (`SurveyAnswerId`),
    CONSTRAINT `FK_SurveyAnswers_SurveyInstances_SurveyInstanceId` FOREIGN KEY (`SurveyInstanceId`) REFERENCES `SurveyInstances` (`SurveyInstanceId`),
    CONSTRAINT `FK_SurveyAnswers_SurveyQuestions_SurveyQuestionId` FOREIGN KEY (`SurveyQuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyAnswers_opt_QuestionFalseReasons_QuestionFalseReasonId` FOREIGN KEY (`QuestionFalseReasonId`) REFERENCES `opt_QuestionFalseReasons` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyQuestionFalseReasons` (
    `SurveyQuestionId` int NOT NULL,
    `QuestionFalseReasonId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SurveyQuestionFalseReasons` PRIMARY KEY (`SurveyQuestionId`, `QuestionFalseReasonId`),
    CONSTRAINT `FK_SurveyQuestionFalseReasons_SurveyQuestions_SurveyQuestionId` FOREIGN KEY (`SurveyQuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`),
    CONSTRAINT `FK_SurveyQuestionFalseReasons_opt_QuestionFalseReasons_Question~` FOREIGN KEY (`QuestionFalseReasonId`) REFERENCES `opt_QuestionFalseReasons` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyQuestionOptions` (
    `SurveyQuestionOptionId` int NOT NULL AUTO_INCREMENT,
    `SurveyQuestionId` int NOT NULL,
    `Option` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `DisplayOrder` int NOT NULL,
    `CompleteSurvey` tinyint(1) NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyQuestionOptions` PRIMARY KEY (`SurveyQuestionOptionId`),
    CONSTRAINT `FK_SurveyQuestionOptions_SurveyQuestions_SurveyQuestionId` FOREIGN KEY (`SurveyQuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyQuestionRules` (
    `SurveyQuestionRuleId` int NOT NULL AUTO_INCREMENT,
    `QuestionId` int NOT NULL,
    `TargetQuestionId` int NOT NULL,
    `Operation` longtext CHARACTER SET utf8mb4 NULL,
    `RuleIndex` int NOT NULL,
    `RuleText` longtext CHARACTER SET utf8mb4 NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    `ValueType` int NOT NULL,
    `RuleType` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyQuestionRules` PRIMARY KEY (`SurveyQuestionRuleId`),
    CONSTRAINT `FK_SurveyQuestionRules_SurveyQuestions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyQuestionRules_SurveyQuestions_TargetQuestionId` FOREIGN KEY (`TargetQuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserEntityRecords` (
    `UserEntityRecordId` int NOT NULL AUTO_INCREMENT,
    `UserEntityId` int NOT NULL,
    `EntityId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserEntityRecords` PRIMARY KEY (`UserEntityRecordId`),
    CONSTRAINT `FK_UserEntityRecords_UserEntities_UserEntityId` FOREIGN KEY (`UserEntityId`) REFERENCES `UserEntities` (`UserEntityId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormAnswers` (
    `SurveyFormAnswerId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormSubmissionId` int NOT NULL,
    `SurveyFormQuestionId` int NOT NULL,
    `SurveyFormReasonId` int NULL,
    `AnswerUuid` longtext CHARACTER SET utf8mb4 NOT NULL,
    `SurveyFormId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    `AnswerText` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Files` json NULL,
    `Metadata` json NULL,
    `AnswerDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormAnswers` PRIMARY KEY (`SurveyFormAnswerId`),
    CONSTRAINT `FK_SurveyFormAnswers_SurveyFormQuestions_SurveyFormQuestionId` FOREIGN KEY (`SurveyFormQuestionId`) REFERENCES `SurveyFormQuestions` (`SurveyFormQuestionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormAnswers_SurveyFormReasons_SurveyFormReasonId` FOREIGN KEY (`SurveyFormReasonId`) REFERENCES `SurveyFormReasons` (`SurveyFormReasonId`),
    CONSTRAINT `FK_SurveyFormAnswers_SurveyFormSubmissions_SurveyFormSubmission~` FOREIGN KEY (`SurveyFormSubmissionId`) REFERENCES `SurveyFormSubmissions` (`SurveyFormSubmissionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormAnswers_SurveyForms_SurveyFormId` FOREIGN KEY (`SurveyFormId`) REFERENCES `SurveyForms` (`SurveyFormId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionOptions` (
    `SurveyFormQuestionOptionId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormQuestionId` int NOT NULL,
    `SurveyFormOptionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormQuestionOptions` PRIMARY KEY (`SurveyFormQuestionOptionId`),
    CONSTRAINT `FK_SurveyFormQuestionOptions_SurveyFormOptions_SurveyFormOption~` FOREIGN KEY (`SurveyFormOptionId`) REFERENCES `SurveyFormOptions` (`SurveyFormOptionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormQuestionOptions_SurveyFormQuestions_SurveyFormQues~` FOREIGN KEY (`SurveyFormQuestionId`) REFERENCES `SurveyFormQuestions` (`SurveyFormQuestionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionProducts` (
    `SurveyFormQuestionProductId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormQuestionId` int NOT NULL,
    `EngageVariantProductId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormQuestionProducts` PRIMARY KEY (`SurveyFormQuestionProductId`),
    CONSTRAINT `FK_SurveyFormQuestionProducts_EngageVariantProducts_EngageVaria~` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormQuestionProducts_SurveyFormQuestions_SurveyFormQue~` FOREIGN KEY (`SurveyFormQuestionId`) REFERENCES `SurveyFormQuestions` (`SurveyFormQuestionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionReasons` (
    `SurveyFormQuestionReasonId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormQuestionId` int NOT NULL,
    `SurveyFormReasonId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormQuestionReasons` PRIMARY KEY (`SurveyFormQuestionReasonId`),
    CONSTRAINT `FK_SurveyFormQuestionReasons_SurveyFormQuestions_SurveyFormQues~` FOREIGN KEY (`SurveyFormQuestionId`) REFERENCES `SurveyFormQuestions` (`SurveyFormQuestionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormQuestionReasons_SurveyFormReasons_SurveyFormReason~` FOREIGN KEY (`SurveyFormReasonId`) REFERENCES `SurveyFormReasons` (`SurveyFormReasonId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyAnswerPhotos` (
    `SurveyAnswerPhotoId` int NOT NULL AUTO_INCREMENT,
    `SurveyAnswerId` int NOT NULL,
    `FileName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Folder` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyAnswerPhotos` PRIMARY KEY (`SurveyAnswerPhotoId`),
    CONSTRAINT `FK_SurveyAnswerPhotos_SurveyAnswers_SurveyAnswerId` FOREIGN KEY (`SurveyAnswerId`) REFERENCES `SurveyAnswers` (`SurveyAnswerId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyAnswerOptions` (
    `SurveyAnswerOptionId` int NOT NULL AUTO_INCREMENT,
    `SurveyAnswerId` int NOT NULL,
    `SurveyQuestionOptionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyAnswerOptions` PRIMARY KEY (`SurveyAnswerOptionId`),
    CONSTRAINT `FK_SurveyAnswerOptions_SurveyAnswers_SurveyAnswerId` FOREIGN KEY (`SurveyAnswerId`) REFERENCES `SurveyAnswers` (`SurveyAnswerId`),
    CONSTRAINT `FK_SurveyAnswerOptions_SurveyQuestionOptions_SurveyQuestionOpti~` FOREIGN KEY (`SurveyQuestionOptionId`) REFERENCES `SurveyQuestionOptions` (`SurveyQuestionOptionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormAnswerHistories` (
    `SurveyFormAnswerHistoryId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormAnswerId` int NOT NULL,
    `SurveyFormReasonId` int NULL,
    `AnswerText` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormAnswerHistories` PRIMARY KEY (`SurveyFormAnswerHistoryId`),
    CONSTRAINT `FK_SurveyFormAnswerHistories_SurveyFormAnswers_SurveyFormAnswer~` FOREIGN KEY (`SurveyFormAnswerId`) REFERENCES `SurveyFormAnswers` (`SurveyFormAnswerId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormAnswerHistories_SurveyFormReasons_SurveyFormReason~` FOREIGN KEY (`SurveyFormReasonId`) REFERENCES `SurveyFormReasons` (`SurveyFormReasonId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormAnswerOptions` (
    `SurveyFormAnswerOptionId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormAnswerId` int NOT NULL,
    `SurveyFormOptionId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormAnswerOptions` PRIMARY KEY (`SurveyFormAnswerOptionId`),
    CONSTRAINT `FK_SurveyFormAnswerOptions_SurveyFormAnswers_SurveyFormAnswerId` FOREIGN KEY (`SurveyFormAnswerId`) REFERENCES `SurveyFormAnswers` (`SurveyFormAnswerId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormAnswerOptions_SurveyFormOptions_SurveyFormOptionId` FOREIGN KEY (`SurveyFormOptionId`) REFERENCES `SurveyFormOptions` (`SurveyFormOptionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormAnswerOptionHistories` (
    `SurveyFormAnswerOptionHistoryId` int NOT NULL AUTO_INCREMENT,
    `SurveyFormAnswerHistoryId` int NOT NULL,
    `SurveyFormOptionId` int NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyFormAnswerOptionHistories` PRIMARY KEY (`SurveyFormAnswerOptionHistoryId`),
    CONSTRAINT `FK_SurveyFormAnswerOptionHistories_SurveyFormAnswerHistories_Su~` FOREIGN KEY (`SurveyFormAnswerHistoryId`) REFERENCES `SurveyFormAnswerHistories` (`SurveyFormAnswerHistoryId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyFormAnswerOptionHistories_SurveyFormOptions_SurveyForm~` FOREIGN KEY (`SurveyFormOptionId`) REFERENCES `SurveyFormOptions` (`SurveyFormOptionId`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AnalysisPillarSubGroups_AnalysisPillarGroupId` ON `AnalysisPillarSubGroups` (`AnalysisPillarGroupId`);

CREATE INDEX `IX_AuditEntryProperties_AuditEntryID` ON `AuditEntryProperties` (`AuditEntryID`);

CREATE INDEX `IX_BudgetPeriods_BudgetYearId` ON `BudgetPeriods` (`BudgetYearId`);

CREATE INDEX `IX_Budgets_BudgetPeriodId` ON `Budgets` (`BudgetPeriodId`);

CREATE INDEX `IX_Budgets_BudgetTypeId` ON `Budgets` (`BudgetTypeId`);

CREATE UNIQUE INDEX `IX_Budgets_BudgetVersionId_BudgetYearId_BudgetPeriodId_GLAccoun~` ON `Budgets` (`BudgetVersionId`, `BudgetYearId`, `BudgetPeriodId`, `GLAccountId`);

CREATE INDEX `IX_Budgets_BudgetYearId` ON `Budgets` (`BudgetYearId`);

CREATE INDEX `IX_Budgets_GLAccountId` ON `Budgets` (`GLAccountId`);

CREATE INDEX `IX_BudgetYearVersions_BudgetVersionId` ON `BudgetYearVersions` (`BudgetVersionId`);

CREATE INDEX `IX_CategoryFiles_CategoryFileTypeId` ON `CategoryFiles` (`CategoryFileTypeId`);

CREATE INDEX `IX_CategoryFiles_CategoryGroupId` ON `CategoryFiles` (`CategoryGroupId`);

CREATE INDEX `IX_CategoryFiles_CategorySubGroupId` ON `CategoryFiles` (`CategorySubGroupId`);

CREATE INDEX `IX_CategoryFiles_StoreId` ON `CategoryFiles` (`StoreId`);

CREATE INDEX `IX_CategoryFileTargets_CategoryFileId` ON `CategoryFileTargets` (`CategoryFileId`);

CREATE INDEX `IX_CategoryFileTargets_CategoryGroupId` ON `CategoryFileTargets` (`CategoryGroupId`);

CREATE INDEX `IX_CategoryFileTargets_EmployeeId` ON `CategoryFileTargets` (`EmployeeId`);

CREATE INDEX `IX_CategoryFileTargets_EmployeeJobTitleId` ON `CategoryFileTargets` (`EmployeeJobTitleId`);

CREATE INDEX `IX_CategoryFileTargets_EngageRegionId` ON `CategoryFileTargets` (`EngageRegionId`);

CREATE INDEX `IX_CategoryFileTargets_EngageSubGroupId` ON `CategoryFileTargets` (`EngageSubGroupId`);

CREATE INDEX `IX_CategoryFileTargets_StoreFormatId` ON `CategoryFileTargets` (`StoreFormatId`);

CREATE INDEX `IX_CategoryFileTargets_StoreId` ON `CategoryFileTargets` (`StoreId`);

CREATE INDEX `IX_CategoryStoreGroups_CategoryGroupId` ON `CategoryStoreGroups` (`CategoryGroupId`);

CREATE INDEX `IX_CategoryStoreGroups_StoreId` ON `CategoryStoreGroups` (`StoreId`);

CREATE INDEX `IX_CategoryTargetAnswerHistories_CategoryTargetAnswerId` ON `CategoryTargetAnswerHistories` (`CategoryTargetAnswerId`);

CREATE INDEX `IX_CategoryTargetAnswerHistories_CategoryTargetId` ON `CategoryTargetAnswerHistories` (`CategoryTargetId`);

CREATE INDEX `IX_CategoryTargetAnswerHistories_CategoryTargetStoreId` ON `CategoryTargetAnswerHistories` (`CategoryTargetStoreId`);

CREATE INDEX `IX_CategoryTargetAnswerHistories_CategoryTargetTypeId` ON `CategoryTargetAnswerHistories` (`CategoryTargetTypeId`);

CREATE INDEX `IX_CategoryTargetAnswerHistories_EmployeeId` ON `CategoryTargetAnswerHistories` (`EmployeeId`);

CREATE INDEX `IX_CategoryTargetAnswers_CategoryTargetId` ON `CategoryTargetAnswers` (`CategoryTargetId`);

CREATE INDEX `IX_CategoryTargetAnswers_CategoryTargetStoreId` ON `CategoryTargetAnswers` (`CategoryTargetStoreId`);

CREATE INDEX `IX_CategoryTargetAnswers_CategoryTargetTypeId` ON `CategoryTargetAnswers` (`CategoryTargetTypeId`);

CREATE INDEX `IX_CategoryTargetAnswers_EmployeeId` ON `CategoryTargetAnswers` (`EmployeeId`);

CREATE INDEX `IX_CategoryTargets_CategoryTargetTypeId` ON `CategoryTargets` (`CategoryTargetTypeId`);

CREATE INDEX `IX_CategoryTargets_SupplierId` ON `CategoryTargets` (`SupplierId`);

CREATE INDEX `IX_CategoryTargetStores_CategoryTargetId` ON `CategoryTargetStores` (`CategoryTargetId`);

CREATE INDEX `IX_CategoryTargetStores_StoreId` ON `CategoryTargetStores` (`StoreId`);

CREATE INDEX `IX_ClaimBatchDetails_ClaimBatchId` ON `ClaimBatchDetails` (`ClaimBatchId`);

CREATE INDEX `IX_ClaimBatchDetails_ClaimId` ON `ClaimBatchDetails` (`ClaimId`);

CREATE INDEX `IX_ClaimBatches_ClaimClassificationId` ON `ClaimBatches` (`ClaimClassificationId`);

CREATE INDEX `IX_ClaimBatches_ClaimStatusId` ON `ClaimBatches` (`ClaimStatusId`);

CREATE INDEX `IX_ClaimBatches_ClaimSupplierStatusId` ON `ClaimBatches` (`ClaimSupplierStatusId`);

CREATE INDEX `IX_ClaimBatches_EngageRegionId` ON `ClaimBatches` (`EngageRegionId`);

CREATE INDEX `IX_ClaimClassifications_ClaimTypeId` ON `ClaimClassifications` (`ClaimTypeId`);

CREATE INDEX `IX_ClaimClassifications_SupplierId` ON `ClaimClassifications` (`SupplierId`);

CREATE INDEX `IX_ClaimClassificationTypes_ClaimTypeId` ON `ClaimClassificationTypes` (`ClaimTypeId`);

CREATE INDEX `IX_ClaimFloatClaims_ClaimId` ON `ClaimFloatClaims` (`ClaimId`);

CREATE INDEX `IX_ClaimFloats_ClaimTypeId` ON `ClaimFloats` (`ClaimTypeId`);

CREATE INDEX `IX_ClaimFloats_EngageRegionId` ON `ClaimFloats` (`EngageRegionId`);

CREATE INDEX `IX_ClaimFloats_SupplierId` ON `ClaimFloats` (`SupplierId`);

CREATE INDEX `IX_ClaimFloatTopUpHistories_ClaimFloatId` ON `ClaimFloatTopUpHistories` (`ClaimFloatId`);

CREATE INDEX `IX_ClaimHistories_ClaimHistoryHeaderId` ON `ClaimHistories` (`ClaimHistoryHeaderId`);

CREATE INDEX `IX_ClaimHistories_ClaimId` ON `ClaimHistories` (`ClaimId`);

CREATE INDEX `IX_ClaimHistories_ClaimPendingReasonId` ON `ClaimHistories` (`ClaimPendingReasonId`);

CREATE INDEX `IX_ClaimHistories_ClaimRejectedReasonId` ON `ClaimHistories` (`ClaimRejectedReasonId`);

CREATE INDEX `IX_ClaimHistories_ClaimStatusId` ON `ClaimHistories` (`ClaimStatusId`);

CREATE INDEX `IX_ClaimHistories_ClaimSupplierStatusId` ON `ClaimHistories` (`ClaimSupplierStatusId`);

CREATE INDEX `IX_ClaimHistoryHeaders_ClaimClassificationId` ON `ClaimHistoryHeaders` (`ClaimClassificationId`);

CREATE INDEX `IX_ClaimHistoryHeaders_ClaimStatusId` ON `ClaimHistoryHeaders` (`ClaimStatusId`);

CREATE INDEX `IX_ClaimHistoryHeaders_ClaimSupplierStatusId` ON `ClaimHistoryHeaders` (`ClaimSupplierStatusId`);

CREATE INDEX `IX_ClaimHistoryHeaders_EngageRegionId` ON `ClaimHistoryHeaders` (`EngageRegionId`);

CREATE UNIQUE INDEX `IX_ClaimNotificationUsers_ClaimStatusId_UserId_EngageRegionId` ON `ClaimNotificationUsers` (`ClaimStatusId`, `UserId`, `EngageRegionId`);

CREATE INDEX `IX_ClaimNotificationUsers_EngageRegionId` ON `ClaimNotificationUsers` (`EngageRegionId`);

CREATE INDEX `IX_ClaimNotificationUsers_UserId` ON `ClaimNotificationUsers` (`UserId`);

CREATE INDEX `IX_ClaimPeriods_ClaimYearId` ON `ClaimPeriods` (`ClaimYearId`);

CREATE INDEX `IX_Claims_ClaimAccountManagerId` ON `Claims` (`ClaimAccountManagerId`);

CREATE INDEX `IX_Claims_ClaimClassificationId` ON `Claims` (`ClaimClassificationId`);

CREATE INDEX `IX_Claims_ClaimFloatId` ON `Claims` (`ClaimFloatId`);

CREATE INDEX `IX_Claims_ClaimManagerId` ON `Claims` (`ClaimManagerId`);

CREATE INDEX `IX_Claims_ClaimNumber` ON `Claims` (`ClaimNumber`);

CREATE INDEX `IX_Claims_ClaimPeriodId` ON `Claims` (`ClaimPeriodId`);

CREATE INDEX `IX_Claims_ClaimStatusId` ON `Claims` (`ClaimStatusId`);

CREATE INDEX `IX_Claims_ClaimSupplierStatusId` ON `Claims` (`ClaimSupplierStatusId`);

CREATE INDEX `IX_Claims_ClaimTypeId` ON `Claims` (`ClaimTypeId`);

CREATE INDEX `IX_Claims_ClientTypeId` ON `Claims` (`ClientTypeId`);

CREATE INDEX `IX_Claims_DistributionCenterId` ON `Claims` (`DistributionCenterId`);

CREATE INDEX `IX_Claims_EmployeeDivisionId` ON `Claims` (`EmployeeDivisionId`);

CREATE INDEX `IX_Claims_StoreId` ON `Claims` (`StoreId`);

CREATE INDEX `IX_Claims_SupplierId` ON `Claims` (`SupplierId`);

CREATE INDEX `IX_Claims_VatId` ON `Claims` (`VatId`);

CREATE UNIQUE INDEX `IX_ClaimSkus_ClaimId_DCProductId` ON `ClaimSkus` (`ClaimId`, `DCProductId`);

CREATE INDEX `IX_ClaimSkus_ClaimQuantityTypeId` ON `ClaimSkus` (`ClaimQuantityTypeId`);

CREATE INDEX `IX_ClaimSkus_ClaimSkuStatusId` ON `ClaimSkus` (`ClaimSkuStatusId`);

CREATE INDEX `IX_ClaimSkus_ClaimSkuTypeId` ON `ClaimSkus` (`ClaimSkuTypeId`);

CREATE INDEX `IX_ClaimSkus_DCProductId` ON `ClaimSkus` (`DCProductId`);

CREATE UNIQUE INDEX `IX_ClaimStatusUsers_ClaimStatusId_UserId` ON `ClaimStatusUsers` (`ClaimStatusId`, `UserId`);

CREATE INDEX `IX_ClaimStatusUsers_UserId` ON `ClaimStatusUsers` (`UserId`);

CREATE INDEX `IX_ClaimTypeReportTypes_ClaimReportTypeId` ON `ClaimTypeReportTypes` (`ClaimReportTypeId`);

CREATE INDEX `IX_ClaimTypes_SupplierId` ON `ClaimTypes` (`SupplierId`);

CREATE INDEX `IX_ClaimTypes_VatId` ON `ClaimTypes` (`VatId`);

CREATE INDEX `IX_CommunicationHistories_ClaimFloatId` ON `CommunicationHistories` (`ClaimFloatId`);

CREATE INDEX `IX_CommunicationHistories_ClaimId` ON `CommunicationHistories` (`ClaimId`);

CREATE INDEX `IX_CommunicationHistories_CommunicationTemplateId` ON `CommunicationHistories` (`CommunicationTemplateId`);

CREATE INDEX `IX_CommunicationHistories_EmployeeId` ON `CommunicationHistories` (`EmployeeId`);

CREATE INDEX `IX_CommunicationHistories_EmployeeStoreCalendarId` ON `CommunicationHistories` (`EmployeeStoreCalendarId`);

CREATE INDEX `IX_CommunicationHistories_OrderId` ON `CommunicationHistories` (`OrderId`);

CREATE INDEX `IX_CommunicationHistories_ProjectId` ON `CommunicationHistories` (`ProjectId`);

CREATE INDEX `IX_CommunicationHistories_StoreId` ON `CommunicationHistories` (`StoreId`);

CREATE INDEX `IX_CommunicationTemplates_CommunicationTemplateTypeId` ON `CommunicationTemplates` (`CommunicationTemplateTypeId`);

CREATE INDEX `IX_CommunicationTemplates_CommunicationTypeId` ON `CommunicationTemplates` (`CommunicationTypeId`);

CREATE INDEX `IX_ContactEvents_ContactId` ON `ContactEvents` (`ContactId`);

CREATE INDEX `IX_ContactEvents_EventTypeId` ON `ContactEvents` (`EventTypeId`);

CREATE INDEX `IX_ContactEvents_FrequencyId` ON `ContactEvents` (`FrequencyId`);

CREATE INDEX `IX_ContactItems_ContactId` ON `ContactItems` (`ContactId`);

CREATE INDEX `IX_ContactItems_ContactTypeId` ON `ContactItems` (`ContactTypeId`);

CREATE INDEX `IX_Contacts_FullName` ON `Contacts` (`FullName`);

CREATE INDEX `IX_Contacts_PrimaryEmailContactItemId` ON `Contacts` (`PrimaryEmailContactItemId`);

CREATE INDEX `IX_Contacts_PrimaryMobileContactItemId` ON `Contacts` (`PrimaryMobileContactItemId`);

CREATE INDEX `IX_Contacts_StakeholderId` ON `Contacts` (`StakeholderId`);

CREATE INDEX `IX_CostCenterDepartments_CostCenterId` ON `CostCenterDepartments` (`CostCenterId`);

CREATE INDEX `IX_CostCenterDepartments_CostDepartmentId` ON `CostCenterDepartments` (`CostDepartmentId`);

CREATE INDEX `IX_CostCenterEmployees_CostCenterId` ON `CostCenterEmployees` (`CostCenterId`);

CREATE INDEX `IX_CostCenterEmployees_EmployeeId` ON `CostCenterEmployees` (`EmployeeId`);

CREATE INDEX `IX_CostCenters_CostTypeId` ON `CostCenters` (`CostTypeId`);

CREATE INDEX `IX_CostSubDepartments_CostDepartmentId` ON `CostSubDepartments` (`CostDepartmentId`);

CREATE INDEX `IX_CreditorBankAccounts_BankAccountTypeId` ON `CreditorBankAccounts` (`BankAccountTypeId`);

CREATE INDEX `IX_CreditorBankAccounts_BankNameId` ON `CreditorBankAccounts` (`BankNameId`);

CREATE INDEX `IX_CreditorFiles_CreditorFileTypeId` ON `CreditorFiles` (`CreditorFileTypeId`);

CREATE INDEX `IX_CreditorFiles_CreditorId` ON `CreditorFiles` (`CreditorId`);

CREATE INDEX `IX_CreditorNotificationStatusUsers_CreditorStatusId` ON `CreditorNotificationStatusUsers` (`CreditorStatusId`);

CREATE INDEX `IX_CreditorNotificationStatusUsers_EngageRegionId` ON `CreditorNotificationStatusUsers` (`EngageRegionId`);

CREATE INDEX `IX_CreditorNotificationStatusUsers_UserId` ON `CreditorNotificationStatusUsers` (`UserId`);

CREATE INDEX `IX_Creditors_CreditorStatusId` ON `Creditors` (`CreditorStatusId`);

CREATE INDEX `IX_CreditorStatusHistories_CreditorId` ON `CreditorStatusHistories` (`CreditorId`);

CREATE INDEX `IX_CreditorStatusHistories_CreditorStatusId` ON `CreditorStatusHistories` (`CreditorStatusId`);

CREATE INDEX `IX_DCAccounts_DistributionCenterId` ON `DCAccounts` (`DistributionCenterId`);

CREATE INDEX `IX_DCAccounts_StoreId` ON `DCAccounts` (`StoreId`);

CREATE INDEX `IX_DCDepts_DCDepartmentId` ON `DCDepts` (`DCDepartmentId`);

CREATE UNIQUE INDEX `IX_DCProducts_Code` ON `DCProducts` (`Code`);

CREATE INDEX `IX_DCProducts_DistributionCenterId` ON `DCProducts` (`DistributionCenterId`);

CREATE INDEX `IX_DCProducts_EngageVariantProductId` ON `DCProducts` (`EngageVariantProductId`);

CREATE INDEX `IX_DCProducts_ManufacturerId` ON `DCProducts` (`ManufacturerId`);

CREATE INDEX `IX_DCProducts_Name` ON `DCProducts` (`Name`);

CREATE INDEX `IX_DCProducts_ProductActiveStatusId` ON `DCProducts` (`ProductActiveStatusId`);

CREATE INDEX `IX_DCProducts_ProductClassId` ON `DCProducts` (`ProductClassId`);

CREATE INDEX `IX_DCProducts_ProductStatusId` ON `DCProducts` (`ProductStatusId`);

CREATE INDEX `IX_DCProducts_ProductSubWarehouseId` ON `DCProducts` (`ProductSubWarehouseId`);

CREATE INDEX `IX_DCProducts_ProductWarehouseStatusId` ON `DCProducts` (`ProductWarehouseStatusId`);

CREATE INDEX `IX_DCProducts_UnitTypeId` ON `DCProducts` (`UnitTypeId`);

CREATE INDEX `IX_DCProducts_VendorId` ON `DCProducts` (`VendorId`);

CREATE INDEX `IX_DCStockOnHands_DCProductId` ON `DCStockOnHands` (`DCProductId`);

CREATE INDEX `IX_EmailHistories_EmailTemplateId` ON `EmailHistories` (`EmailTemplateId`);

CREATE INDEX `IX_EmailHistoryCCEmails_EmailHistoryId` ON `EmailHistoryCCEmails` (`EmailHistoryId`);

CREATE INDEX `IX_EmailHistoryTemplateVariables_EmailHistoryId` ON `EmailHistoryTemplateVariables` (`EmailHistoryId`);

CREATE INDEX `IX_EmailTemplateHistories_EmailTemplateId` ON `EmailTemplateHistories` (`EmailTemplateId`);

CREATE INDEX `IX_EmailTemplateHistories_UserId` ON `EmailTemplateHistories` (`UserId`);

CREATE INDEX `IX_EmailTemplates_EmailTemplateTypeId` ON `EmailTemplates` (`EmailTemplateTypeId`);

CREATE INDEX `IX_EmailTemplates_EmailTypeId` ON `EmailTemplates` (`EmailTypeId`);

CREATE INDEX `IX_EmailTemplateVariableClaimNumbers_EmailHistoryTemplateVariab~` ON `EmailTemplateVariableClaimNumbers` (`EmailHistoryTemplateVariableId`);

CREATE INDEX `IX_EmployeeAddresses_CountryId` ON `EmployeeAddresses` (`CountryId`);

CREATE INDEX `IX_EmployeeAddresses_EmployeeId` ON `EmployeeAddresses` (`EmployeeId`);

CREATE INDEX `IX_EmployeeAddresses_PostalCountryId` ON `EmployeeAddresses` (`PostalCountryId`);

CREATE INDEX `IX_EmployeeAddresses_PostalProvinceId` ON `EmployeeAddresses` (`PostalProvinceId`);

CREATE INDEX `IX_EmployeeAddresses_ProvinceId` ON `EmployeeAddresses` (`ProvinceId`);

CREATE INDEX `IX_EmployeeAssetHistories_EmployeeAssetId` ON `EmployeeAssetHistories` (`EmployeeAssetId`);

CREATE INDEX `IX_EmployeeAssetHistories_NewEmployeeId` ON `EmployeeAssetHistories` (`NewEmployeeId`);

CREATE INDEX `IX_EmployeeAssetHistories_OldEmployeeId` ON `EmployeeAssetHistories` (`OldEmployeeId`);

CREATE INDEX `IX_EmployeeAssets_AssetStatusId` ON `EmployeeAssets` (`AssetStatusId`);

CREATE INDEX `IX_EmployeeAssets_EmployeeAssetBrandId` ON `EmployeeAssets` (`EmployeeAssetBrandId`);

CREATE INDEX `IX_EmployeeAssets_EmployeeAssetTypeId` ON `EmployeeAssets` (`EmployeeAssetTypeId`);

CREATE INDEX `IX_EmployeeAssets_EmployeeId` ON `EmployeeAssets` (`EmployeeId`);

CREATE INDEX `IX_EmployeeBadges_EmployeeBadgeTypeId` ON `EmployeeBadges` (`EmployeeBadgeTypeId`);

CREATE INDEX `IX_EmployeeBankDetailFiles_EmployeeBankDetailId` ON `EmployeeBankDetailFiles` (`EmployeeBankDetailId`);

CREATE INDEX `IX_EmployeeBankDetailFiles_FileContainerId` ON `EmployeeBankDetailFiles` (`FileContainerId`);

CREATE INDEX `IX_EmployeeBankDetails_BankAccountOwnerId` ON `EmployeeBankDetails` (`BankAccountOwnerId`);

CREATE INDEX `IX_EmployeeBankDetails_BankAccountTypeId` ON `EmployeeBankDetails` (`BankAccountTypeId`);

CREATE INDEX `IX_EmployeeBankDetails_BankNameId` ON `EmployeeBankDetails` (`BankNameId`);

CREATE INDEX `IX_EmployeeBankDetails_BankPaymentMethodId` ON `EmployeeBankDetails` (`BankPaymentMethodId`);

CREATE INDEX `IX_EmployeeBankDetails_EmployeeId` ON `EmployeeBankDetails` (`EmployeeId`);

CREATE INDEX `IX_EmployeeBenefits_BenefitTypeId` ON `EmployeeBenefits` (`BenefitTypeId`);

CREATE INDEX `IX_EmployeeBenefits_EmployeeId` ON `EmployeeBenefits` (`EmployeeId`);

CREATE INDEX `IX_EmployeeCoolerBoxes_EmployeeCoolerBoxConditionId` ON `EmployeeCoolerBoxes` (`EmployeeCoolerBoxConditionId`);

CREATE INDEX `IX_EmployeeCoolerBoxes_EmployeeId` ON `EmployeeCoolerBoxes` (`EmployeeId`);

CREATE INDEX `IX_EmployeeCoolerBoxFiles_EmployeeCoolerBoxId` ON `EmployeeCoolerBoxFiles` (`EmployeeCoolerBoxId`);

CREATE INDEX `IX_EmployeeCoolerBoxFiles_FileContainerId` ON `EmployeeCoolerBoxFiles` (`FileContainerId`);

CREATE INDEX `IX_EmployeeCoolerBoxHistories_EmployeeCoolerBoxId` ON `EmployeeCoolerBoxHistories` (`EmployeeCoolerBoxId`);

CREATE INDEX `IX_EmployeeCoolerBoxHistories_NewEmployeeId` ON `EmployeeCoolerBoxHistories` (`NewEmployeeId`);

CREATE INDEX `IX_EmployeeCoolerBoxHistories_OldEmployeeId` ON `EmployeeCoolerBoxHistories` (`OldEmployeeId`);

CREATE INDEX `IX_EmployeeDeductions_DeductionCycleTypeId` ON `EmployeeDeductions` (`DeductionCycleTypeId`);

CREATE INDEX `IX_EmployeeDeductions_DeductionTypeId` ON `EmployeeDeductions` (`DeductionTypeId`);

CREATE INDEX `IX_EmployeeDeductions_EmployeeId` ON `EmployeeDeductions` (`EmployeeId`);

CREATE INDEX `IX_EmployeeDepartments_EngageDepartmentId` ON `EmployeeDepartments` (`EngageDepartmentId`);

CREATE INDEX `IX_EmployeeDisciplinaryProcedures_EmployeeId` ON `EmployeeDisciplinaryProcedures` (`EmployeeId`);

CREATE INDEX `IX_EmployeeEmployeeBadges_EmployeeBadgeId` ON `EmployeeEmployeeBadges` (`EmployeeBadgeId`);

CREATE INDEX `IX_EmployeeEmployeeDivisions_EmployeeDivisionId` ON `EmployeeEmployeeDivisions` (`EmployeeDivisionId`);

CREATE INDEX `IX_EmployeeEmployeeHealthConditions_EmployeeHealthConditionId` ON `EmployeeEmployeeHealthConditions` (`EmployeeHealthConditionId`);

CREATE INDEX `IX_EmployeeEmployeeJobTitles_EmployeeJobTitleId` ON `EmployeeEmployeeJobTitles` (`EmployeeJobTitleId`);

CREATE INDEX `IX_EmployeeEmployeeKpis_EmployeeKpiId` ON `EmployeeEmployeeKpis` (`EmployeeKpiId`);

CREATE INDEX `IX_EmployeeEmployeeKpis_EmployeeKpiTierId` ON `EmployeeEmployeeKpis` (`EmployeeKpiTierId`);

CREATE INDEX `IX_EmployeeExpenseClaims_EmployeeId` ON `EmployeeExpenseClaims` (`EmployeeId`);

CREATE INDEX `IX_EmployeeExpenseClaims_StatusId` ON `EmployeeExpenseClaims` (`StatusId`);

CREATE INDEX `IX_EmployeeFiles_EmployeeFileTypeId` ON `EmployeeFiles` (`EmployeeFileTypeId`);

CREATE INDEX `IX_EmployeeFiles_EmployeeId` ON `EmployeeFiles` (`EmployeeId`);

CREATE INDEX `IX_EmployeeFuels_EmployeeFuelExpenseTypeId` ON `EmployeeFuels` (`EmployeeFuelExpenseTypeId`);

CREATE INDEX `IX_EmployeeFuels_EmployeeId` ON `EmployeeFuels` (`EmployeeId`);

CREATE INDEX `IX_EmployeeFuels_EmployeePaymentTypeId` ON `EmployeeFuels` (`EmployeePaymentTypeId`);

CREATE INDEX `IX_EmployeeFuels_EmployeeVehicleId` ON `EmployeeFuels` (`EmployeeVehicleId`);

CREATE INDEX `IX_EmployeeJobTitles_ParentId` ON `EmployeeJobTitles` (`ParentId`);

CREATE INDEX `IX_EmployeeJobTitleTimes_EmployeeJobTitleId` ON `EmployeeJobTitleTimes` (`EmployeeJobTitleId`);

CREATE INDEX `IX_EmployeeJobTitleTypes_EmployeeJobTitleId` ON `EmployeeJobTitleTypes` (`EmployeeJobTitleId`);

CREATE UNIQUE INDEX `IX_EmployeeJobTitleUserGroups_EmployeeJobTitleId_UserGroupId` ON `EmployeeJobTitleUserGroups` (`EmployeeJobTitleId`, `UserGroupId`);

CREATE INDEX `IX_EmployeeJobTitleUserGroups_UserGroupId` ON `EmployeeJobTitleUserGroups` (`UserGroupId`);

CREATE INDEX `IX_EmployeeKpis_EmployeeKpiTypeId` ON `EmployeeKpis` (`EmployeeKpiTypeId`);

CREATE INDEX `IX_EmployeeKpiScores_EmployeeId` ON `EmployeeKpiScores` (`EmployeeId`);

CREATE INDEX `IX_EmployeeKpiScores_EmployeeKpiId` ON `EmployeeKpiScores` (`EmployeeKpiId`);

CREATE INDEX `IX_EmployeeKpiScores_EmployeeKpiTierId` ON `EmployeeKpiScores` (`EmployeeKpiTierId`);

CREATE INDEX `IX_EmployeeKpiTiers_EmployeeKpiId` ON `EmployeeKpiTiers` (`EmployeeKpiId`);

CREATE INDEX `IX_EmployeeLeaveEntries_EmployeeId` ON `EmployeeLeaveEntries` (`EmployeeId`);

CREATE INDEX `IX_EmployeeLeaveEntries_LeaveTypeId` ON `EmployeeLeaveEntries` (`LeaveTypeId`);

CREATE INDEX `IX_EmployeeLoans_EmployeeId` ON `EmployeeLoans` (`EmployeeId`);

CREATE INDEX `IX_EmployeeManager_ManagerId` ON `EmployeeManager` (`ManagerId`);

CREATE INDEX `IX_EmployeeNotifications_NotificationId` ON `EmployeeNotifications` (`NotificationId`);

CREATE INDEX `IX_EmployeePayRates_EmployeeId` ON `EmployeePayRates` (`EmployeeId`);

CREATE INDEX `IX_EmployeePayRates_EmployeePayRateFrequencyId` ON `EmployeePayRates` (`EmployeePayRateFrequencyId`);

CREATE INDEX `IX_EmployeePayRates_EmployeePayRatePackageId` ON `EmployeePayRates` (`EmployeePayRatePackageId`);

CREATE INDEX `IX_EmployeePensions_EmployeeId` ON `EmployeePensions` (`EmployeeId`);

CREATE INDEX `IX_EmployeePensions_EmployeePensionCategoryId` ON `EmployeePensions` (`EmployeePensionCategoryId`);

CREATE INDEX `IX_EmployeePensions_EmployeePensionContributionPercentageId` ON `EmployeePensions` (`EmployeePensionContributionPercentageId`);

CREATE INDEX `IX_EmployeePensions_EmployeePensionSchemeId` ON `EmployeePensions` (`EmployeePensionSchemeId`);

CREATE INDEX `IX_EmployeePopiConsents_EmployeeId` ON `EmployeePopiConsents` (`EmployeeId`);

CREATE INDEX `IX_EmployeeQualificationFiles_EmployeeQualificationId` ON `EmployeeQualificationFiles` (`EmployeeQualificationId`);

CREATE INDEX `IX_EmployeeQualificationFiles_FileContainerId` ON `EmployeeQualificationFiles` (`FileContainerId`);

CREATE INDEX `IX_EmployeeQualifications_EducationLevelId` ON `EmployeeQualifications` (`EducationLevelId`);

CREATE INDEX `IX_EmployeeQualifications_EmployeeId` ON `EmployeeQualifications` (`EmployeeId`);

CREATE INDEX `IX_EmployeeQualifications_InstitutionTypeId` ON `EmployeeQualifications` (`InstitutionTypeId`);

CREATE INDEX `IX_EmployeeRecurringTransactions_CreditorBankAccountId` ON `EmployeeRecurringTransactions` (`CreditorBankAccountId`);

CREATE INDEX `IX_EmployeeRecurringTransactions_EmployeeId` ON `EmployeeRecurringTransactions` (`EmployeeId`);

CREATE INDEX `IX_EmployeeRecurringTransactions_EmployeeRecurringTransactionSt~` ON `EmployeeRecurringTransactions` (`EmployeeRecurringTransactionStatusId`);

CREATE INDEX `IX_EmployeeRecurringTransactions_EmployeeTransactionTypeId` ON `EmployeeRecurringTransactions` (`EmployeeTransactionTypeId`);

CREATE INDEX `IX_EmployeeRecurringTransactions_PayrollPeriodId` ON `EmployeeRecurringTransactions` (`PayrollPeriodId`);

CREATE INDEX `IX_EmployeeRegionContacts_EmployeeId` ON `EmployeeRegionContacts` (`EmployeeId`);

CREATE INDEX `IX_EmployeeRegionContacts_EngageRegionId` ON `EmployeeRegionContacts` (`EngageRegionId`);

CREATE INDEX `IX_EmployeeRegions_EngageRegionId` ON `EmployeeRegions` (`EngageRegionId`);

CREATE INDEX `IX_EmployeeReinstatementHistories_EmployeeId` ON `EmployeeReinstatementHistories` (`EmployeeId`);

CREATE INDEX `IX_EmployeeReinstatementHistories_EmployeeReinstatementReasonId` ON `EmployeeReinstatementHistories` (`EmployeeReinstatementReasonId`);

CREATE INDEX `IX_EmployeeReports_ReportId` ON `EmployeeReports` (`ReportId`);

CREATE UNIQUE INDEX `IX_Employees_Code` ON `Employees` (`Code`);

CREATE INDEX `IX_Employees_CostCenterManagerId` ON `Employees` (`CostCenterManagerId`);

CREATE INDEX `IX_Employees_EmployeeCitzenshipId` ON `Employees` (`EmployeeCitzenshipId`);

CREATE INDEX `IX_Employees_EmployeeDefaultPayslipId` ON `Employees` (`EmployeeDefaultPayslipId`);

CREATE INDEX `IX_Employees_EmployeeDisabledTypeId` ON `Employees` (`EmployeeDisabledTypeId`);

CREATE INDEX `IX_Employees_EmployeeGenderId` ON `Employees` (`EmployeeGenderId`);

CREATE INDEX `IX_Employees_EmployeeIdentificationTypeId` ON `Employees` (`EmployeeIdentificationTypeId`);

CREATE INDEX `IX_Employees_EmployeeIncentiveTierId` ON `Employees` (`EmployeeIncentiveTierId`);

CREATE INDEX `IX_Employees_EmployeeJobTitleId` ON `Employees` (`EmployeeJobTitleId`);

CREATE INDEX `IX_Employees_EmployeeJobTitleTimeId` ON `Employees` (`EmployeeJobTitleTimeId`);

CREATE INDEX `IX_Employees_EmployeeJobTitleTypeId` ON `Employees` (`EmployeeJobTitleTypeId`);

CREATE INDEX `IX_Employees_EmployeeLanguageId` ON `Employees` (`EmployeeLanguageId`);

CREATE INDEX `IX_Employees_EmployeeNationalityId` ON `Employees` (`EmployeeNationalityId`);

CREATE INDEX `IX_Employees_EmployeePassportNationalityId` ON `Employees` (`EmployeePassportNationalityId`);

CREATE INDEX `IX_Employees_EmployeePersonTypeId` ON `Employees` (`EmployeePersonTypeId`);

CREATE INDEX `IX_Employees_EmployeeRaceId` ON `Employees` (`EmployeeRaceId`);

CREATE INDEX `IX_Employees_EmployeeReinstatementReasonId` ON `Employees` (`EmployeeReinstatementReasonId`);

CREATE INDEX `IX_Employees_EmployeeSDLExemptionId` ON `Employees` (`EmployeeSDLExemptionId`);

CREATE INDEX `IX_Employees_EmployeeStandardIndustryCodeId` ON `Employees` (`EmployeeStandardIndustryCodeId`);

CREATE INDEX `IX_Employees_EmployeeStandardIndustryGroupCodeId` ON `Employees` (`EmployeeStandardIndustryGroupCodeId`);

CREATE INDEX `IX_Employees_EmployeeStateId` ON `Employees` (`EmployeeStateId`);

CREATE INDEX `IX_Employees_EmployeeTaxStatusId` ON `Employees` (`EmployeeTaxStatusId`);

CREATE INDEX `IX_Employees_EmployeeTerminationReasonId` ON `Employees` (`EmployeeTerminationReasonId`);

CREATE INDEX `IX_Employees_EmployeeTitleId` ON `Employees` (`EmployeeTitleId`);

CREATE INDEX `IX_Employees_EmployeeTypeId` ON `Employees` (`EmployeeTypeId`);

CREATE INDEX `IX_Employees_EmployeeUIFExemptionId` ON `Employees` (`EmployeeUIFExemptionId`);

CREATE INDEX `IX_Employees_EmploymentActionId` ON `Employees` (`EmploymentActionId`);

CREATE INDEX `IX_Employees_EmploymentTypeId` ON `Employees` (`EmploymentTypeId`);

CREATE INDEX `IX_Employees_EngageRegionId` ON `Employees` (`EngageRegionId`);

CREATE INDEX `IX_Employees_EngageSubRegionId` ON `Employees` (`EngageSubRegionId`);

CREATE INDEX `IX_Employees_FirstName` ON `Employees` (`FirstName`);

CREATE INDEX `IX_Employees_LastName` ON `Employees` (`LastName`);

CREATE INDEX `IX_Employees_LeaveManagerId` ON `Employees` (`LeaveManagerId`);

CREATE INDEX `IX_Employees_ManagerId` ON `Employees` (`ManagerId`);

CREATE INDEX `IX_Employees_MaritalStatusId` ON `Employees` (`MaritalStatusId`);

CREATE INDEX `IX_Employees_NextOfKinTypeId` ON `Employees` (`NextOfKinTypeId`);

CREATE INDEX `IX_Employees_PayrollPeriodId` ON `Employees` (`PayrollPeriodId`);

CREATE UNIQUE INDEX `IX_Employees_StakeholderId` ON `Employees` (`StakeholderId`);

CREATE INDEX `IX_Employees_UniformSizeId` ON `Employees` (`UniformSizeId`);

CREATE INDEX `IX_Employees_UserId` ON `Employees` (`UserId`);

CREATE INDEX `IX_EmployeeSkillFiles_EmployeeSkillId` ON `EmployeeSkillFiles` (`EmployeeSkillId`);

CREATE INDEX `IX_EmployeeSkillFiles_FileContainerId` ON `EmployeeSkillFiles` (`FileContainerId`);

CREATE INDEX `IX_EmployeeSkills_EmployeeId` ON `EmployeeSkills` (`EmployeeId`);

CREATE INDEX `IX_EmployeeSkills_ExperienceId` ON `EmployeeSkills` (`ExperienceId`);

CREATE INDEX `IX_EmployeeSkills_ProficiencyId` ON `EmployeeSkills` (`ProficiencyId`);

CREATE INDEX `IX_EmployeeSkills_SkillCategoryId` ON `EmployeeSkills` (`SkillCategoryId`);

CREATE INDEX `IX_EmployeeSkillsDevelopment_EmployeeId` ON `EmployeeSkillsDevelopment` (`EmployeeId`);

CREATE UNIQUE INDEX `IX_EmployeeStoreArchives_EmployeeId_StoreId_EngageSubGroupId` ON `EmployeeStoreArchives` (`EmployeeId`, `StoreId`, `EngageSubGroupId`);

CREATE INDEX `IX_EmployeeStoreArchives_EngageDepartmentCategoryId` ON `EmployeeStoreArchives` (`EngageDepartmentCategoryId`);

CREATE INDEX `IX_EmployeeStoreArchives_EngageSubGroupId` ON `EmployeeStoreArchives` (`EngageSubGroupId`);

CREATE INDEX `IX_EmployeeStoreArchives_FrequencyTypeId` ON `EmployeeStoreArchives` (`FrequencyTypeId`);

CREATE INDEX `IX_EmployeeStoreArchives_StoreId` ON `EmployeeStoreArchives` (`StoreId`);

CREATE INDEX `IX_EmployeeStoreCalendarBlockDays_EmployeeId` ON `EmployeeStoreCalendarBlockDays` (`EmployeeId`);

CREATE INDEX `IX_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarPeriodId` ON `EmployeeStoreCalendarBlockDays` (`EmployeeStoreCalendarPeriodId`);

CREATE INDEX `IX_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarStatusId` ON `EmployeeStoreCalendarBlockDays` (`EmployeeStoreCalendarStatusId`);

CREATE INDEX `IX_EmployeeStoreCalendarBlockDays_EmployeeStoreCalendarTypeId` ON `EmployeeStoreCalendarBlockDays` (`EmployeeStoreCalendarTypeId`);

CREATE UNIQUE INDEX `IX_EmployeeStoreCalendarPeriods_EmployeeStoreCalendarYearId_Num~` ON `EmployeeStoreCalendarPeriods` (`EmployeeStoreCalendarYearId`, `Number`);

CREATE UNIQUE INDEX `IX_EmployeeStoreCalendars_EmployeeId_StoreId_CalendarDate` ON `EmployeeStoreCalendars` (`EmployeeId`, `StoreId`, `CalendarDate`);

CREATE INDEX `IX_EmployeeStoreCalendars_EmployeeStoreCalendarGroupId` ON `EmployeeStoreCalendars` (`EmployeeStoreCalendarGroupId`);

CREATE INDEX `IX_EmployeeStoreCalendars_EmployeeStoreCalendarPeriodId` ON `EmployeeStoreCalendars` (`EmployeeStoreCalendarPeriodId`);

CREATE INDEX `IX_EmployeeStoreCalendars_EmployeeStoreCalendarStatusId` ON `EmployeeStoreCalendars` (`EmployeeStoreCalendarStatusId`);

CREATE INDEX `IX_EmployeeStoreCalendars_EmployeeStoreCalendarTypeId` ON `EmployeeStoreCalendars` (`EmployeeStoreCalendarTypeId`);

CREATE INDEX `IX_EmployeeStoreCalendars_StoreId` ON `EmployeeStoreCalendars` (`StoreId`);

CREATE INDEX `IX_EmployeeStoreCalendars_SurveyInstanceId` ON `EmployeeStoreCalendars` (`SurveyInstanceId`);

CREATE INDEX `IX_EmployeeStoreCalendarSurveyFormSubmissions_EmployeeStoreCale~` ON `EmployeeStoreCalendarSurveyFormSubmissions` (`EmployeeStoreCalendarId`);

CREATE INDEX `IX_EmployeeStoreCalendarSurveyFormSubmissions_SurveyFormSubmiss~` ON `EmployeeStoreCalendarSurveyFormSubmissions` (`SurveyFormSubmissionId`);

CREATE UNIQUE INDEX `IX_EmployeeStoreCheckIns_CheckInUuid` ON `EmployeeStoreCheckIns` (`CheckInUuid`);

CREATE INDEX `IX_EmployeeStoreCheckIns_EmployeeId` ON `EmployeeStoreCheckIns` (`EmployeeId`);

CREATE INDEX `IX_EmployeeStoreCheckIns_StoreId` ON `EmployeeStoreCheckIns` (`StoreId`);

CREATE INDEX `IX_EmployeeStoreKpis_EmployeeKpiId` ON `EmployeeStoreKpis` (`EmployeeKpiId`);

CREATE INDEX `IX_EmployeeStoreKpis_EmployeeKpiTierId` ON `EmployeeStoreKpis` (`EmployeeKpiTierId`);

CREATE INDEX `IX_EmployeeStoreKpis_StoreId` ON `EmployeeStoreKpis` (`StoreId`);

CREATE INDEX `IX_EmployeeStoreKpiScores_EmployeeId` ON `EmployeeStoreKpiScores` (`EmployeeId`);

CREATE INDEX `IX_EmployeeStoreKpiScores_EmployeeKpiId` ON `EmployeeStoreKpiScores` (`EmployeeKpiId`);

CREATE INDEX `IX_EmployeeStoreKpiScores_EmployeeKpiTierId` ON `EmployeeStoreKpiScores` (`EmployeeKpiTierId`);

CREATE INDEX `IX_EmployeeStoreKpiScores_StoreId` ON `EmployeeStoreKpiScores` (`StoreId`);

CREATE UNIQUE INDEX `IX_EmployeeStores_EmployeeId_StoreId_EngageSubGroupId` ON `EmployeeStores` (`EmployeeId`, `StoreId`, `EngageSubGroupId`);

CREATE INDEX `IX_EmployeeStores_EngageDepartmentCategoryId` ON `EmployeeStores` (`EngageDepartmentCategoryId`);

CREATE INDEX `IX_EmployeeStores_EngageSubGroupId` ON `EmployeeStores` (`EngageSubGroupId`);

CREATE INDEX `IX_EmployeeStores_FrequencyTypeId` ON `EmployeeStores` (`FrequencyTypeId`);

CREATE INDEX `IX_EmployeeStores_StoreId` ON `EmployeeStores` (`StoreId`);

CREATE INDEX `IX_EmployeeSuspensions_EmployeeId` ON `EmployeeSuspensions` (`EmployeeId`);

CREATE INDEX `IX_EmployeeSuspensions_EmployeeSuspensionReasonId` ON `EmployeeSuspensions` (`EmployeeSuspensionReasonId`);

CREATE INDEX `IX_EmployeeTerminationHistories_EmployeeId` ON `EmployeeTerminationHistories` (`EmployeeId`);

CREATE INDEX `IX_EmployeeTerminationHistories_EmployeeTerminationReasonId` ON `EmployeeTerminationHistories` (`EmployeeTerminationReasonId`);

CREATE INDEX `IX_EmployeeTrainingRecords_EmployeeId` ON `EmployeeTrainingRecords` (`EmployeeId`);

CREATE INDEX `IX_EmployeeTrainingRecords_EmployeeTrainingStatusId` ON `EmployeeTrainingRecords` (`EmployeeTrainingStatusId`);

CREATE INDEX `IX_EmployeeTrainings_TrainingId` ON `EmployeeTrainings` (`TrainingId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeId` ON `EmployeeTransactions` (`EmployeeId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeRecurringTransactionId` ON `EmployeeTransactions` (`EmployeeRecurringTransactionId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeRecurringTransactionStatusId` ON `EmployeeTransactions` (`EmployeeRecurringTransactionStatusId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeTransactionRemunerationTypeId` ON `EmployeeTransactions` (`EmployeeTransactionRemunerationTypeId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeTransactionStatusId` ON `EmployeeTransactions` (`EmployeeTransactionStatusId`);

CREATE INDEX `IX_EmployeeTransactions_EmployeeTransactionTypeId` ON `EmployeeTransactions` (`EmployeeTransactionTypeId`);

CREATE INDEX `IX_EmployeeTransactions_PayrollPeriodId` ON `EmployeeTransactions` (`PayrollPeriodId`);

CREATE INDEX `IX_EmployeeTransactionTypes_EmployeeTransactionTypeGroupId` ON `EmployeeTransactionTypes` (`EmployeeTransactionTypeGroupId`);

CREATE INDEX `IX_EmployeeVehicleHistories_EmployeeVehicleId` ON `EmployeeVehicleHistories` (`EmployeeVehicleId`);

CREATE INDEX `IX_EmployeeVehicleHistories_NewEmployeeId` ON `EmployeeVehicleHistories` (`NewEmployeeId`);

CREATE INDEX `IX_EmployeeVehicleHistories_OldEmployeeId` ON `EmployeeVehicleHistories` (`OldEmployeeId`);

CREATE INDEX `IX_EmployeeVehicles_AssetOwnerId` ON `EmployeeVehicles` (`AssetOwnerId`);

CREATE INDEX `IX_EmployeeVehicles_AssetStatusId` ON `EmployeeVehicles` (`AssetStatusId`);

CREATE INDEX `IX_EmployeeVehicles_EmployeeId` ON `EmployeeVehicles` (`EmployeeId`);

CREATE INDEX `IX_EmployeeVehicles_VehicleBrandId` ON `EmployeeVehicles` (`VehicleBrandId`);

CREATE INDEX `IX_EmployeeVehicles_VehicleTypeId` ON `EmployeeVehicles` (`VehicleTypeId`);

CREATE INDEX `IX_EmployeeWorkRoleContacts_EmployeeWorkRoleId` ON `EmployeeWorkRoleContacts` (`EmployeeWorkRoleId`);

CREATE INDEX `IX_EmployeeWorkRoles_EmployeeId` ON `EmployeeWorkRoles` (`EmployeeId`);

CREATE INDEX `IX_EmployeeWorkRoles_EmploymentTypeId` ON `EmployeeWorkRoles` (`EmploymentTypeId`);

CREATE INDEX `IX_EmployeeWorkRoles_GradeId` ON `EmployeeWorkRoles` (`GradeId`);

CREATE INDEX `IX_EmployeeWorkRoles_ManagerId` ON `EmployeeWorkRoles` (`ManagerId`);

CREATE INDEX `IX_EmployeeWorkRoles_StatusId` ON `EmployeeWorkRoles` (`StatusId`);

CREATE INDEX `IX_EmployeeWorkRoles_VacancyId` ON `EmployeeWorkRoles` (`VacancyId`);

CREATE UNIQUE INDEX `IX_EngageMasterProducts_Code` ON `EngageMasterProducts` (`Code`);

CREATE INDEX `IX_EngageMasterProducts_EngageBrandId` ON `EngageMasterProducts` (`EngageBrandId`);

CREATE INDEX `IX_EngageMasterProducts_EngageDepartmentId` ON `EngageMasterProducts` (`EngageDepartmentId`);

CREATE INDEX `IX_EngageMasterProducts_EngageSubCategoryId` ON `EngageMasterProducts` (`EngageSubCategoryId`);

CREATE INDEX `IX_EngageMasterProducts_Name` ON `EngageMasterProducts` (`Name`);

CREATE INDEX `IX_EngageMasterProducts_ProductClassificationId` ON `EngageMasterProducts` (`ProductClassificationId`);

CREATE INDEX `IX_EngageMasterProducts_SupplierId` ON `EngageMasterProducts` (`SupplierId`);

CREATE INDEX `IX_EngageMasterProducts_VatId` ON `EngageMasterProducts` (`VatId`);

CREATE INDEX `IX_EngageProductTags_EngageTagId` ON `EngageProductTags` (`EngageTagId`);

CREATE INDEX `IX_EngageRegionClaimManagers_UserId` ON `EngageRegionClaimManagers` (`UserId`);

CREATE INDEX `IX_EngageSubGroupEngageBrands_EngageBrandId` ON `EngageSubGroupEngageBrands` (`EngageBrandId`);

CREATE INDEX `IX_EngageSubGroupSuppliers_SupplierId` ON `EngageSubGroupSuppliers` (`SupplierId`);

CREATE INDEX `IX_EngageSubRegions_EngageRegionId` ON `EngageSubRegions` (`EngageRegionId`);

CREATE UNIQUE INDEX `IX_EngageVariantProducts_Code` ON `EngageVariantProducts` (`Code`);

CREATE INDEX `IX_EngageVariantProducts_EngageMasterProductId` ON `EngageVariantProducts` (`EngageMasterProductId`);

CREATE INDEX `IX_EngageVariantProducts_Name` ON `EngageVariantProducts` (`Name`);

CREATE INDEX `IX_EngageVariantProducts_UnitTypeId` ON `EngageVariantProducts` (`UnitTypeId`);

CREATE INDEX `IX_EntityBlobs_ClaimId` ON `EntityBlobs` (`ClaimId`);

CREATE INDEX `IX_EntityContactCommunicationTypes_CommunicationTypeId` ON `EntityContactCommunicationTypes` (`CommunicationTypeId`);

CREATE INDEX `IX_EntityContactCommunicationTypes_EntityContactId` ON `EntityContactCommunicationTypes` (`EntityContactId`);

CREATE INDEX `IX_EntityContactRegions_EngageRegionId` ON `EntityContactRegions` (`EngageRegionId`);

CREATE INDEX `IX_EntityContactRegions_EntityContactId` ON `EntityContactRegions` (`EntityContactId`);

CREATE INDEX `IX_EntityContacts_EngageRegionId` ON `EntityContacts` (`EngageRegionId`);

CREATE INDEX `IX_EntityContacts_EntityContactTypeId` ON `EntityContacts` (`EntityContactTypeId`);

CREATE INDEX `IX_EntityContacts_StoreId` ON `EntityContacts` (`StoreId`);

CREATE INDEX `IX_EntityContacts_SupplierId` ON `EntityContacts` (`SupplierId`);

CREATE INDEX `IX_EntityContacts_UserId` ON `EntityContacts` (`UserId`);

CREATE INDEX `IX_EvoLedgers_AnalysisPillarSubGroupId` ON `EvoLedgers` (`AnalysisPillarSubGroupId`);

CREATE INDEX `IX_GLAccounts_EngageRegionId` ON `GLAccounts` (`EngageRegionId`);

CREATE INDEX `IX_GLAccounts_GLAccountTypeId` ON `GLAccounts` (`GLAccountTypeId`);

CREATE INDEX `IX_GLAdjustments_BudgetPeriodId` ON `GLAdjustments` (`BudgetPeriodId`);

CREATE INDEX `IX_GLAdjustments_BudgetYearId` ON `GLAdjustments` (`BudgetYearId`);

CREATE INDEX `IX_GLAdjustments_GLAdjustmentTypeId` ON `GLAdjustments` (`GLAdjustmentTypeId`);

CREATE INDEX `IX_GLAdjustments_SupplierId` ON `GLAdjustments` (`SupplierId`);

CREATE UNIQUE INDEX `IX_ImportPromotionStores_ImportFileId_RowNo` ON `ImportPromotionStores` (`ImportFileId`, `RowNo`);

CREATE INDEX `IX_ImportPromotionStores_PromotionId` ON `ImportPromotionStores` (`PromotionId`);

CREATE INDEX `IX_ImportPromotionStores_StoreId` ON `ImportPromotionStores` (`StoreId`);

CREATE UNIQUE INDEX `IX_ImportSurveyStores_ImportFileId_RowNo` ON `ImportSurveyStores` (`ImportFileId`, `RowNo`);

CREATE INDEX `IX_ImportSurveyStores_StoreId` ON `ImportSurveyStores` (`StoreId`);

CREATE INDEX `IX_ImportSurveyStores_SurveyId` ON `ImportSurveyStores` (`SurveyId`);

CREATE INDEX `IX_Incidents_ClientTypeId` ON `Incidents` (`ClientTypeId`);

CREATE INDEX `IX_Incidents_IncidentNumber` ON `Incidents` (`IncidentNumber`);

CREATE INDEX `IX_Incidents_IncidentStatusId` ON `Incidents` (`IncidentStatusId`);

CREATE INDEX `IX_Incidents_IncidentTypeId` ON `Incidents` (`IncidentTypeId`);

CREATE INDEX `IX_Incidents_StoreId` ON `Incidents` (`StoreId`);

CREATE INDEX `IX_Incidents_SupplierId` ON `Incidents` (`SupplierId`);

CREATE INDEX `IX_IncidentSkus_DCProductId` ON `IncidentSkus` (`DCProductId`);

CREATE UNIQUE INDEX `IX_IncidentSkus_IncidentId_DCProductId` ON `IncidentSkus` (`IncidentId`, `DCProductId`);

CREATE INDEX `IX_IncidentSkus_IncidentSkuStatusId` ON `IncidentSkus` (`IncidentSkuStatusId`);

CREATE INDEX `IX_IncidentSkus_IncidentSkuTypeId` ON `IncidentSkus` (`IncidentSkuTypeId`);

CREATE INDEX `IX_Inventories_InventoryGroupId` ON `Inventories` (`InventoryGroupId`);

CREATE INDEX `IX_Inventories_InventoryStatusId` ON `Inventories` (`InventoryStatusId`);

CREATE INDEX `IX_Inventories_InventoryUnitTypeId` ON `Inventories` (`InventoryUnitTypeId`);

CREATE UNIQUE INDEX `IX_InventoryPeriods_InventoryYearId_Number` ON `InventoryPeriods` (`InventoryYearId`, `Number`);

CREATE INDEX `IX_InventoryTransactions_InventoryId` ON `InventoryTransactions` (`InventoryId`);

CREATE INDEX `IX_InventoryTransactions_InventoryTransactionStatusId` ON `InventoryTransactions` (`InventoryTransactionStatusId`);

CREATE INDEX `IX_InventoryTransactions_InventoryTransactionTypeId` ON `InventoryTransactions` (`InventoryTransactionTypeId`);

CREATE INDEX `IX_InventoryTransactions_InventoryWarehouseId` ON `InventoryTransactions` (`InventoryWarehouseId`);

CREATE INDEX `IX_Locations_EngageLocationId` ON `Locations` (`EngageLocationId`);

CREATE INDEX `IX_Locations_EngageRegionId` ON `Locations` (`EngageRegionId`);

CREATE INDEX `IX_Locations_LocationTypeId` ON `Locations` (`LocationTypeId`);

CREATE INDEX `IX_Locations_StakeholderId` ON `Locations` (`StakeholderId`);

CREATE INDEX `IX_Manufacturers_EngageRegionId` ON `Manufacturers` (`EngageRegionId`);

CREATE INDEX `IX_Manufacturers_SupplierId` ON `Manufacturers` (`SupplierId`);

CREATE INDEX `IX_NotificationEmployeeReads_EmployeeId` ON `NotificationEmployeeReads` (`EmployeeId`);

CREATE INDEX `IX_NotificationNotificationChannels_NotificationChannelId` ON `NotificationNotificationChannels` (`NotificationChannelId`);

CREATE INDEX `IX_NotificationRegions_EngageRegionId` ON `NotificationRegions` (`EngageRegionId`);

CREATE INDEX `IX_Notifications_NotificationCategoryId` ON `Notifications` (`NotificationCategoryId`);

CREATE INDEX `IX_Notifications_NotificationTypeId` ON `Notifications` (`NotificationTypeId`);

CREATE INDEX `IX_NotificationTargets_EmployeeId` ON `NotificationTargets` (`EmployeeId`);

CREATE INDEX `IX_NotificationTargets_EmployeeJobTitleId` ON `NotificationTargets` (`EmployeeJobTitleId`);

CREATE INDEX `IX_NotificationTargets_EngageRegionId` ON `NotificationTargets` (`EngageRegionId`);

CREATE UNIQUE INDEX `IX_NotificationTargets_NotificationId_EmployeeId` ON `NotificationTargets` (`NotificationId`, `EmployeeId`);

CREATE UNIQUE INDEX `IX_NotificationTargets_NotificationId_EmployeeJobTitleId` ON `NotificationTargets` (`NotificationId`, `EmployeeJobTitleId`);

CREATE UNIQUE INDEX `IX_NotificationTargets_NotificationId_EngageRegionId` ON `NotificationTargets` (`NotificationId`, `EngageRegionId`);

CREATE UNIQUE INDEX `IX_NotificationTargets_NotificationId_StoreFormatId` ON `NotificationTargets` (`NotificationId`, `StoreFormatId`);

CREATE UNIQUE INDEX `IX_NotificationTargets_NotificationId_StoreId` ON `NotificationTargets` (`NotificationId`, `StoreId`);

CREATE INDEX `IX_NotificationTargets_StoreFormatId` ON `NotificationTargets` (`StoreFormatId`);

CREATE INDEX `IX_NotificationTargets_StoreId` ON `NotificationTargets` (`StoreId`);

CREATE INDEX `IX_NPrintingBatches_FileTypeId` ON `NPrintingBatches` (`FileTypeId`);

CREATE INDEX `IX_NPrintingBatches_WebFileCategoryId` ON `NPrintingBatches` (`WebFileCategoryId`);

CREATE INDEX `IX_NPrintings_NPrintingBatchId` ON `NPrintings` (`NPrintingBatchId`);

CREATE INDEX `IX_opt_DCProductClasses_DCDepartmentId` ON `opt_DCProductClasses` (`DCDepartmentId`);

CREATE INDEX `IX_opt_EngageCategories_EngageSubGroupId` ON `opt_EngageCategories` (`EngageSubGroupId`);

CREATE INDEX `IX_opt_EngageDepartmentCategories_EngageDepartmentId` ON `opt_EngageDepartmentCategories` (`EngageDepartmentId`);

CREATE INDEX `IX_opt_EngageDepartments_EngageDepartmentGroupId` ON `opt_EngageDepartments` (`EngageDepartmentGroupId`);

CREATE INDEX `IX_opt_EngageRegions_StoreSparRegionId` ON `opt_EngageRegions` (`StoreSparRegionId`);

CREATE INDEX `IX_opt_EngageSubCategories_EngageCategoryId` ON `opt_EngageSubCategories` (`EngageCategoryId`);

CREATE INDEX `IX_opt_EngageSubGroups_EngageDepartmentCategoryId` ON `opt_EngageSubGroups` (`EngageDepartmentCategoryId`);

CREATE INDEX `IX_opt_EngageSubGroups_EngageDepartmentId` ON `opt_EngageSubGroups` (`EngageDepartmentId`);

CREATE INDEX `IX_opt_EngageSubGroups_EngageGroupId` ON `opt_EngageSubGroups` (`EngageGroupId`);

CREATE INDEX `IX_opt_EngageSubGroups_StoreDepartmentId` ON `opt_EngageSubGroups` (`StoreDepartmentId`);

CREATE INDEX `IX_opt_StoreConcepts_EngageDepartmentId` ON `opt_StoreConcepts` (`EngageDepartmentId`);

CREATE INDEX `IX_opt_SupplierRegions_SupplierId` ON `opt_SupplierRegions` (`SupplierId`);

CREATE INDEX `IX_OptionTypes_OptionTypeGroupId` ON `OptionTypes` (`OptionTypeGroupId`);

CREATE INDEX `IX_OrderEngageDepartments_EngageDepartmentId` ON `OrderEngageDepartments` (`EngageDepartmentId`);

CREATE INDEX `IX_Orders_DistributionCenterId` ON `Orders` (`DistributionCenterId`);

CREATE INDEX `IX_Orders_OrderStatusId` ON `Orders` (`OrderStatusId`);

CREATE INDEX `IX_Orders_OrderTemplateId` ON `Orders` (`OrderTemplateId`);

CREATE INDEX `IX_Orders_OrderTypeId` ON `Orders` (`OrderTypeId`);

CREATE INDEX `IX_Orders_StoreId` ON `Orders` (`StoreId`);

CREATE INDEX `IX_Orders_SupplierId` ON `Orders` (`SupplierId`);

CREATE INDEX `IX_OrderSkus_DCProductId` ON `OrderSkus` (`DCProductId`);

CREATE INDEX `IX_OrderSkus_OrderId` ON `OrderSkus` (`OrderId`);

CREATE INDEX `IX_OrderSkus_OrderQuantityTypeId` ON `OrderSkus` (`OrderQuantityTypeId`);

CREATE INDEX `IX_OrderSkus_OrderSkuStatusId` ON `OrderSkus` (`OrderSkuStatusId`);

CREATE INDEX `IX_OrderSkus_OrderSkuTypeId` ON `OrderSkus` (`OrderSkuTypeId`);

CREATE INDEX `IX_OrderSkus_OrderTemplateProductId` ON `OrderSkus` (`OrderTemplateProductId`);

CREATE INDEX `IX_OrderStagingSkus_OrderStagingId` ON `OrderStagingSkus` (`OrderStagingId`);

CREATE INDEX `IX_OrderTemplateGroups_OrderTemplateId` ON `OrderTemplateGroups` (`OrderTemplateId`);

CREATE INDEX `IX_OrderTemplateProducts_DCProductId` ON `OrderTemplateProducts` (`DCProductId`);

CREATE INDEX `IX_OrderTemplateProducts_OrderTemplateGroupId` ON `OrderTemplateProducts` (`OrderTemplateGroupId`);

CREATE INDEX `IX_OrderTemplates_DistributionCenterId` ON `OrderTemplates` (`DistributionCenterId`);

CREATE INDEX `IX_OrderTemplates_OrderTemplateStatusId` ON `OrderTemplates` (`OrderTemplateStatusId`);

CREATE INDEX `IX_Organizations_OrganizationSettingId` ON `Organizations` (`OrganizationSettingId`);

CREATE INDEX `IX_PaymentBatchRegions_EngageRegionId` ON `PaymentBatchRegions` (`EngageRegionId`);

CREATE INDEX `IX_PaymentBatchRegions_PaymentBatchId` ON `PaymentBatchRegions` (`PaymentBatchId`);

CREATE INDEX `IX_PaymentLineCostCenters_CostCenterId` ON `PaymentLineCostCenters` (`CostCenterId`);

CREATE INDEX `IX_PaymentLineCostCenters_PaymentLineId` ON `PaymentLineCostCenters` (`PaymentLineId`);

CREATE INDEX `IX_PaymentLineCostSubDepartments_CostSubDepartmentId` ON `PaymentLineCostSubDepartments` (`CostSubDepartmentId`);

CREATE INDEX `IX_PaymentLineCostSubDepartments_PaymentLineId` ON `PaymentLineCostSubDepartments` (`PaymentLineId`);

CREATE INDEX `IX_PaymentLineDivisions_EmployeeDivisionId` ON `PaymentLineDivisions` (`EmployeeDivisionId`);

CREATE INDEX `IX_PaymentLineDivisions_PaymentLineId` ON `PaymentLineDivisions` (`PaymentLineId`);

CREATE INDEX `IX_PaymentLineEmployees_EmployeeId` ON `PaymentLineEmployees` (`EmployeeId`);

CREATE INDEX `IX_PaymentLineEmployees_PaymentLineId` ON `PaymentLineEmployees` (`PaymentLineId`);

CREATE INDEX `IX_PaymentLineFiles_PaymentLineFileTypeId` ON `PaymentLineFiles` (`PaymentLineFileTypeId`);

CREATE INDEX `IX_PaymentLineFiles_PaymentLineId` ON `PaymentLineFiles` (`PaymentLineId`);

CREATE INDEX `IX_PaymentLines_ExpenseTypeId` ON `PaymentLines` (`ExpenseTypeId`);

CREATE INDEX `IX_PaymentLines_PaymentId` ON `PaymentLines` (`PaymentId`);

CREATE INDEX `IX_PaymentLines_VatId` ON `PaymentLines` (`VatId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_EngageRegionId` ON `PaymentNotificationStatusUsers` (`EngageRegionId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_PaymentStatusId` ON `PaymentNotificationStatusUsers` (`PaymentStatusId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_UserId` ON `PaymentNotificationStatusUsers` (`UserId`);

CREATE UNIQUE INDEX `IX_PaymentPeriods_PaymentYearId_Number` ON `PaymentPeriods` (`PaymentYearId`, `Number`);

CREATE INDEX `IX_PaymentProofPayments_PaymentId` ON `PaymentProofPayments` (`PaymentId`);

CREATE INDEX `IX_PaymentProofPayments_PaymentProofId` ON `PaymentProofPayments` (`PaymentProofId`);

CREATE INDEX `IX_Payments_CreditorId` ON `Payments` (`CreditorId`);

CREATE INDEX `IX_Payments_PaymentArchiveId` ON `Payments` (`PaymentArchiveId`);

CREATE INDEX `IX_Payments_PaymentBatchId` ON `Payments` (`PaymentBatchId`);

CREATE INDEX `IX_Payments_PaymentPeriodId` ON `Payments` (`PaymentPeriodId`);

CREATE INDEX `IX_Payments_PaymentStatusId` ON `Payments` (`PaymentStatusId`);

CREATE INDEX `IX_Payments_VatId` ON `Payments` (`VatId`);

CREATE INDEX `IX_PaymentStatusHistories_PaymentId` ON `PaymentStatusHistories` (`PaymentId`);

CREATE INDEX `IX_PaymentStatusHistories_PaymentStatusId` ON `PaymentStatusHistories` (`PaymentStatusId`);

CREATE UNIQUE INDEX `IX_PayrollPeriods_PayrollYearId_Number` ON `PayrollPeriods` (`PayrollYearId`, `Number`);

CREATE INDEX `IX_ProductAnalyses_DistributionCenterId` ON `ProductAnalyses` (`DistributionCenterId`);

CREATE INDEX `IX_ProductAnalyses_EngageCategoryId` ON `ProductAnalyses` (`EngageCategoryId`);

CREATE INDEX `IX_ProductAnalyses_EngageGroupId` ON `ProductAnalyses` (`EngageGroupId`);

CREATE INDEX `IX_ProductAnalyses_EngageSubCategoryId` ON `ProductAnalyses` (`EngageSubCategoryId`);

CREATE INDEX `IX_ProductAnalyses_EngageSubGroupId` ON `ProductAnalyses` (`EngageSubGroupId`);

CREATE INDEX `IX_ProductAnalyses_ProductAnalysisDivisionId` ON `ProductAnalyses` (`ProductAnalysisDivisionId`);

CREATE INDEX `IX_ProductAnalyses_ProductAnalysisGroupId` ON `ProductAnalyses` (`ProductAnalysisGroupId`);

CREATE INDEX `IX_ProductCategories_ProductSubGroupId` ON `ProductCategories` (`ProductSubGroupId`);

CREATE INDEX `IX_ProductFilters_EngageVariantProductId` ON `ProductFilters` (`EngageVariantProductId`);

CREATE INDEX `IX_ProductFilters_FileUploadId` ON `ProductFilters` (`FileUploadId`);

CREATE INDEX `IX_ProductFilterUploads_FileUploadId` ON `ProductFilterUploads` (`FileUploadId`);

CREATE INDEX `IX_ProductManufacturers_ProductSupplierId` ON `ProductManufacturers` (`ProductSupplierId`);

CREATE INDEX `IX_ProductMasterColors_ProductMasterId` ON `ProductMasterColors` (`ProductMasterId`);

CREATE INDEX `IX_ProductMasters_ProductBrandId` ON `ProductMasters` (`ProductBrandId`);

CREATE INDEX `IX_ProductMasters_ProductManufacturerId` ON `ProductMasters` (`ProductManufacturerId`);

CREATE INDEX `IX_ProductMasters_ProductMasterStatusId` ON `ProductMasters` (`ProductMasterStatusId`);

CREATE INDEX `IX_ProductMasters_ProductMasterSystemStatusId` ON `ProductMasters` (`ProductMasterSystemStatusId`);

CREATE INDEX `IX_ProductMasters_ProductReasonId` ON `ProductMasters` (`ProductReasonId`);

CREATE INDEX `IX_ProductMasters_ProductSubCategoryId` ON `ProductMasters` (`ProductSubCategoryId`);

CREATE INDEX `IX_ProductMasters_ProductVendorId` ON `ProductMasters` (`ProductVendorId`);

CREATE INDEX `IX_ProductMasterSizes_ProductMasterId` ON `ProductMasterSizes` (`ProductMasterId`);

CREATE INDEX `IX_ProductOrderHistories_ProductOrderId` ON `ProductOrderHistories` (`ProductOrderId`);

CREATE INDEX `IX_ProductOrderHistories_ProductOrderStatusId` ON `ProductOrderHistories` (`ProductOrderStatusId`);

CREATE INDEX `IX_ProductOrderLines_ProductId` ON `ProductOrderLines` (`ProductId`);

CREATE INDEX `IX_ProductOrderLines_ProductOrderId` ON `ProductOrderLines` (`ProductOrderId`);

CREATE INDEX `IX_ProductOrderLines_ProductOrderLineStatusId` ON `ProductOrderLines` (`ProductOrderLineStatusId`);

CREATE INDEX `IX_ProductOrderLines_ProductOrderLineTypeId` ON `ProductOrderLines` (`ProductOrderLineTypeId`);

CREATE INDEX `IX_ProductOrders_ProductOrderStatusId` ON `ProductOrders` (`ProductOrderStatusId`);

CREATE INDEX `IX_ProductOrders_ProductOrderTypeId` ON `ProductOrders` (`ProductOrderTypeId`);

CREATE INDEX `IX_ProductOrders_ProductPeriodId` ON `ProductOrders` (`ProductPeriodId`);

CREATE INDEX `IX_ProductOrders_ProductSupplierId` ON `ProductOrders` (`ProductSupplierId`);

CREATE INDEX `IX_ProductOrders_ProductWarehouseId` ON `ProductOrders` (`ProductWarehouseId`);

CREATE INDEX `IX_ProductOrders_ProductWarehouseOutId` ON `ProductOrders` (`ProductWarehouseOutId`);

CREATE UNIQUE INDEX `IX_ProductPeriods_ProductYearId_Name` ON `ProductPeriods` (`ProductYearId`, `Name`);

CREATE INDEX `IX_ProductPrices_ProductId` ON `ProductPrices` (`ProductId`);

CREATE INDEX `IX_Products_ProductMasterColorId` ON `Products` (`ProductMasterColorId`);

CREATE INDEX `IX_Products_ProductMasterId` ON `Products` (`ProductMasterId`);

CREATE INDEX `IX_Products_ProductMasterSizeId` ON `Products` (`ProductMasterSizeId`);

CREATE INDEX `IX_Products_ProductModuleStatusId` ON `Products` (`ProductModuleStatusId`);

CREATE INDEX `IX_Products_ProductPackSizeTypeId` ON `Products` (`ProductPackSizeTypeId`);

CREATE INDEX `IX_Products_ProductSizeTypeId` ON `Products` (`ProductSizeTypeId`);

CREATE INDEX `IX_Products_ProductSystemStatusId` ON `Products` (`ProductSystemStatusId`);

CREATE INDEX `IX_Products_ProductWarehouseId` ON `Products` (`ProductWarehouseId`);

CREATE INDEX `IX_Products_RelatedProductId` ON `Products` (`RelatedProductId`);

CREATE INDEX `IX_ProductSubCategories_ProductCategoryId` ON `ProductSubCategories` (`ProductCategoryId`);

CREATE INDEX `IX_ProductSubGroups_ProductGroupId` ON `ProductSubGroups` (`ProductGroupId`);

CREATE INDEX `IX_ProductTransactions_EmployeeId` ON `ProductTransactions` (`EmployeeId`);

CREATE INDEX `IX_ProductTransactions_EngageRegionId` ON `ProductTransactions` (`EngageRegionId`);

CREATE INDEX `IX_ProductTransactions_ProductId` ON `ProductTransactions` (`ProductId`);

CREATE INDEX `IX_ProductTransactions_ProductPeriodId` ON `ProductTransactions` (`ProductPeriodId`);

CREATE INDEX `IX_ProductTransactions_ProductTransactionStatusId` ON `ProductTransactions` (`ProductTransactionStatusId`);

CREATE INDEX `IX_ProductTransactions_ProductTransactionTypeId` ON `ProductTransactions` (`ProductTransactionTypeId`);

CREATE INDEX `IX_ProductTransactions_ProductWarehouseId` ON `ProductTransactions` (`ProductWarehouseId`);

CREATE INDEX `IX_ProductWarehouseRegions_EngageRegionId` ON `ProductWarehouseRegions` (`EngageRegionId`);

CREATE INDEX `IX_ProductWarehouses_EngageRegionId` ON `ProductWarehouses` (`EngageRegionId`);

CREATE INDEX `IX_ProductWarehouses_ParentId` ON `ProductWarehouses` (`ParentId`);

CREATE INDEX `IX_ProductWarehouseSummaries_EngageRegionId` ON `ProductWarehouseSummaries` (`EngageRegionId`);

CREATE UNIQUE INDEX `IX_ProductWarehouseSummaries_ProductId_ProductWarehouseId_Produ~` ON `ProductWarehouseSummaries` (`ProductId`, `ProductWarehouseId`, `ProductPeriodId`, `EngageRegionId`);

CREATE INDEX `IX_ProductWarehouseSummaries_ProductPeriodId` ON `ProductWarehouseSummaries` (`ProductPeriodId`);

CREATE INDEX `IX_ProductWarehouseSummaries_ProductWarehouseId` ON `ProductWarehouseSummaries` (`ProductWarehouseId`);

CREATE INDEX `IX_ProjectAssignees_ProjectId` ON `ProjectAssignees` (`ProjectId`);

CREATE INDEX `IX_ProjectAssignees_ProjectStakeholderId` ON `ProjectAssignees` (`ProjectStakeholderId`);

CREATE INDEX `IX_ProjectAssignees_ProjectStatusId` ON `ProjectAssignees` (`ProjectStatusId`);

CREATE INDEX `IX_ProjectCampaigns_EngageRegionId` ON `ProjectCampaigns` (`EngageRegionId`);

CREATE INDEX `IX_ProjectCategorySuppliers_ProjectCategoryId` ON `ProjectCategorySuppliers` (`ProjectCategoryId`);

CREATE INDEX `IX_ProjectCategorySuppliers_SupplierId` ON `ProjectCategorySuppliers` (`SupplierId`);

CREATE INDEX `IX_ProjectComments_ProjectId` ON `ProjectComments` (`ProjectId`);

CREATE INDEX `IX_ProjectComments_ProjectStatusId` ON `ProjectComments` (`ProjectStatusId`);

CREATE INDEX `IX_ProjectDcProducts_DcProductId` ON `ProjectDcProducts` (`DcProductId`);

CREATE INDEX `IX_ProjectDcProducts_ProjectId` ON `ProjectDcProducts` (`ProjectId`);

CREATE INDEX `IX_ProjectEngageBrands_EngageBrandId` ON `ProjectEngageBrands` (`EngageBrandId`);

CREATE INDEX `IX_ProjectEngageBrands_ProjectId` ON `ProjectEngageBrands` (`ProjectId`);

CREATE INDEX `IX_ProjectExternalUserCommunicationTypes_CommunicationTypeId` ON `ProjectExternalUserCommunicationTypes` (`CommunicationTypeId`);

CREATE INDEX `IX_ProjectExternalUserCommunicationTypes_ProjectExternalUserId` ON `ProjectExternalUserCommunicationTypes` (`ProjectExternalUserId`);

CREATE INDEX `IX_ProjectExternalUsers_ExternalUserTypeId` ON `ProjectExternalUsers` (`ExternalUserTypeId`);

CREATE INDEX `IX_ProjectFiles_ProjectFileTypeId` ON `ProjectFiles` (`ProjectFileTypeId`);

CREATE INDEX `IX_ProjectFiles_ProjectId` ON `ProjectFiles` (`ProjectId`);

CREATE INDEX `IX_ProjectNotes_ProjectId` ON `ProjectNotes` (`ProjectId`);

CREATE INDEX `IX_ProjectProjectTags_ClaimId` ON `ProjectProjectTags` (`ClaimId`);

CREATE INDEX `IX_ProjectProjectTags_DCProductId` ON `ProjectProjectTags` (`DCProductId`);

CREATE INDEX `IX_ProjectProjectTags_EmployeeJobTitleId` ON `ProjectProjectTags` (`EmployeeJobTitleId`);

CREATE INDEX `IX_ProjectProjectTags_EngageRegionId` ON `ProjectProjectTags` (`EngageRegionId`);

CREATE INDEX `IX_ProjectProjectTags_OrderId` ON `ProjectProjectTags` (`OrderId`);

CREATE INDEX `IX_ProjectProjectTags_ProjectId` ON `ProjectProjectTags` (`ProjectId`);

CREATE INDEX `IX_ProjectProjectTags_StoreAssetId` ON `ProjectProjectTags` (`StoreAssetId`);

CREATE INDEX `IX_ProjectProjectTags_StoreId` ON `ProjectProjectTags` (`StoreId`);

CREATE INDEX `IX_ProjectProjectTags_SupplierId` ON `ProjectProjectTags` (`SupplierId`);

CREATE INDEX `IX_ProjectProjectTags_UserId` ON `ProjectProjectTags` (`UserId`);

CREATE INDEX `IX_Projects_EngageRegionId` ON `Projects` (`EngageRegionId`);

CREATE INDEX `IX_Projects_OwnerId` ON `Projects` (`OwnerId`);

CREATE INDEX `IX_Projects_ProjectCampaignId` ON `Projects` (`ProjectCampaignId`);

CREATE INDEX `IX_Projects_ProjectCategoryId` ON `Projects` (`ProjectCategoryId`);

CREATE INDEX `IX_Projects_ProjectPriorityId` ON `Projects` (`ProjectPriorityId`);

CREATE INDEX `IX_Projects_ProjectStatusId` ON `Projects` (`ProjectStatusId`);

CREATE INDEX `IX_Projects_ProjectSubCategoryId` ON `Projects` (`ProjectSubCategoryId`);

CREATE INDEX `IX_Projects_ProjectSubTypeId` ON `Projects` (`ProjectSubTypeId`);

CREATE INDEX `IX_Projects_ProjectTypeId` ON `Projects` (`ProjectTypeId`);

CREATE INDEX `IX_Projects_StoreId` ON `Projects` (`StoreId`);

CREATE INDEX `IX_ProjectStakeholders_EmployeeRegionContactId` ON `ProjectStakeholders` (`EmployeeRegionContactId`);

CREATE INDEX `IX_ProjectStakeholders_ProjectExternalUserId` ON `ProjectStakeholders` (`ProjectExternalUserId`);

CREATE INDEX `IX_ProjectStakeholders_ProjectId` ON `ProjectStakeholders` (`ProjectId`);

CREATE INDEX `IX_ProjectStakeholders_StoreContactId` ON `ProjectStakeholders` (`StoreContactId`);

CREATE INDEX `IX_ProjectStakeholders_SupplierContactId` ON `ProjectStakeholders` (`SupplierContactId`);

CREATE INDEX `IX_ProjectStakeholders_UserId` ON `ProjectStakeholders` (`UserId`);

CREATE INDEX `IX_projectStatusHistories_ProjectId` ON `projectStatusHistories` (`ProjectId`);

CREATE INDEX `IX_projectStatusHistories_ProjectStatusId` ON `projectStatusHistories` (`ProjectStatusId`);

CREATE INDEX `IX_ProjectStoreAssets_ProjectId` ON `ProjectStoreAssets` (`ProjectId`);

CREATE INDEX `IX_ProjectStoreAssets_StoreAssetId` ON `ProjectStoreAssets` (`StoreAssetId`);

CREATE INDEX `IX_ProjectSubCategories_EngageSubGroupId` ON `ProjectSubCategories` (`EngageSubGroupId`);

CREATE INDEX `IX_ProjectSubCategories_ProjectCategoryId` ON `ProjectSubCategories` (`ProjectCategoryId`);

CREATE INDEX `IX_ProjectSubTypes_ProjectTypeId` ON `ProjectSubTypes` (`ProjectTypeId`);

CREATE INDEX `IX_ProjectSuppliers_ProjectId` ON `ProjectSuppliers` (`ProjectId`);

CREATE INDEX `IX_ProjectSuppliers_SupplierId` ON `ProjectSuppliers` (`SupplierId`);

CREATE INDEX `IX_ProjectTacOpRegions_EngageRegionId` ON `ProjectTacOpRegions` (`EngageRegionId`);

CREATE INDEX `IX_ProjectTacOps_UserId` ON `ProjectTacOps` (`UserId`);

CREATE INDEX `IX_ProjectTaskAssignees_ProjectId` ON `ProjectTaskAssignees` (`ProjectId`);

CREATE INDEX `IX_ProjectTaskAssignees_ProjectTaskId` ON `ProjectTaskAssignees` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTaskAssignees_ProjectTaskStatusId` ON `ProjectTaskAssignees` (`ProjectTaskStatusId`);

CREATE INDEX `IX_ProjectTaskComments_ProjectStatusId` ON `ProjectTaskComments` (`ProjectStatusId`);

CREATE INDEX `IX_ProjectTaskComments_ProjectTaskId` ON `ProjectTaskComments` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTaskNotes_ProjectTaskId` ON `ProjectTaskNotes` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTaskProjectStakeholderUsers_ProjectStakeholderId` ON `ProjectTaskProjectStakeholderUsers` (`ProjectStakeholderId`);

CREATE INDEX `IX_ProjectTaskProjectStakeholderUsers_ProjectTaskId` ON `ProjectTaskProjectStakeholderUsers` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTasks_ProjectId` ON `ProjectTasks` (`ProjectId`);

CREATE INDEX `IX_ProjectTasks_ProjectStakeholderId` ON `ProjectTasks` (`ProjectStakeholderId`);

CREATE INDEX `IX_ProjectTasks_ProjectTaskPriorityId` ON `ProjectTasks` (`ProjectTaskPriorityId`);

CREATE INDEX `IX_ProjectTasks_ProjectTaskSeverityId` ON `ProjectTasks` (`ProjectTaskSeverityId`);

CREATE INDEX `IX_ProjectTasks_ProjectTaskStatusId` ON `ProjectTasks` (`ProjectTaskStatusId`);

CREATE INDEX `IX_ProjectTasks_ProjectTaskTypeId` ON `ProjectTasks` (`ProjectTaskTypeId`);

CREATE INDEX `IX_ProjectTasks_UserId` ON `ProjectTasks` (`UserId`);

CREATE INDEX `IX_ProjectTaskStakeholders_ProjectStakeholderId` ON `ProjectTaskStakeholders` (`ProjectStakeholderId`);

CREATE INDEX `IX_ProjectTaskStakeholders_ProjectTaskId` ON `ProjectTaskStakeholders` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTaskStakeholders_ProjectTaskStatusId` ON `ProjectTaskStakeholders` (`ProjectTaskStatusId`);

CREATE INDEX `IX_ProjectTaskStatusHistories_ProjectTaskId` ON `ProjectTaskStatusHistories` (`ProjectTaskId`);

CREATE INDEX `IX_ProjectTaskStatusHistories_ProjectTaskStatusId` ON `ProjectTaskStatusHistories` (`ProjectTaskStatusId`);

CREATE INDEX `IX_ProjectUsers_UserId` ON `ProjectUsers` (`UserId`);

CREATE INDEX `IX_PromotionProducts_EngageVariantProductId` ON `PromotionProducts` (`EngageVariantProductId`);

CREATE INDEX `IX_PromotionProducts_PromotionId` ON `PromotionProducts` (`PromotionId`);

CREATE INDEX `IX_PromotionProducts_PromotionProductTypeId` ON `PromotionProducts` (`PromotionProductTypeId`);

CREATE INDEX `IX_Promotions_PromotionTypeId` ON `Promotions` (`PromotionTypeId`);

CREATE INDEX `IX_PromotionStores_StoreId` ON `PromotionStores` (`StoreId`);

CREATE INDEX `IX_PromotionStores_TargetingId` ON `PromotionStores` (`TargetingId`);

CREATE UNIQUE INDEX `IX_RoleUserGroups_RoleId_UserGroupId` ON `RoleUserGroups` (`RoleId`, `UserGroupId`);

CREATE INDEX `IX_RoleUserGroups_UserGroupId` ON `RoleUserGroups` (`UserGroupId`);

CREATE INDEX `IX_SecurityPermissionRoles_SecurityPermissionId` ON `SecurityPermissionRoles` (`SecurityPermissionId`);

CREATE INDEX `IX_SecurityPermissionRoles_SecurityRoleId` ON `SecurityPermissionRoles` (`SecurityRoleId`);

CREATE INDEX `IX_SecurityRoleUsers_SecurityRoleId` ON `SecurityRoleUsers` (`SecurityRoleId`);

CREATE INDEX `IX_SecurityRoleUsers_UserId` ON `SecurityRoleUsers` (`UserId`);

CREATE UNIQUE INDEX `IX_Settings_Name` ON `Settings` (`Name`);

CREATE INDEX `IX_SparProducts_EngageBrandId` ON `SparProducts` (`EngageBrandId`);

CREATE INDEX `IX_SparProducts_EngageSubCategoryId` ON `SparProducts` (`EngageSubCategoryId`);

CREATE INDEX `IX_SparProducts_EvoLedgerId` ON `SparProducts` (`EvoLedgerId`);

CREATE INDEX `IX_SparProducts_SparAnalysisGroupId` ON `SparProducts` (`SparAnalysisGroupId`);

CREATE INDEX `IX_SparProducts_SparProductStatusId` ON `SparProducts` (`SparProductStatusId`);

CREATE INDEX `IX_SparProducts_SparSystemStatusId` ON `SparProducts` (`SparSystemStatusId`);

CREATE INDEX `IX_SparProducts_SparUnitTypeId` ON `SparProducts` (`SparUnitTypeId`);

CREATE INDEX `IX_SparProducts_SupplierId` ON `SparProducts` (`SupplierId`);

CREATE INDEX `IX_SparSubProducts_DistributionCenterId` ON `SparSubProducts` (`DistributionCenterId`);

CREATE INDEX `IX_SparSubProducts_SparProductId` ON `SparSubProducts` (`SparProductId`);

CREATE INDEX `IX_SparSubProducts_SparSourceId` ON `SparSubProducts` (`SparSourceId`);

CREATE INDEX `IX_SparSubProducts_SparSubProductStatusId` ON `SparSubProducts` (`SparSubProductStatusId`);

CREATE INDEX `IX_Stakeholders_VendorId` ON `Stakeholders` (`VendorId`);

CREATE INDEX `IX_StatsOrdersByRegions_EngageRegionId` ON `StatsOrdersByRegions` (`EngageRegionId`);

CREATE INDEX `IX_StatsStoresByRegions_EngageRegionId` ON `StatsStoresByRegions` (`EngageRegionId`);

CREATE INDEX `IX_StoreAssetBlobs_StoreAssetFileTypeId` ON `StoreAssetBlobs` (`StoreAssetFileTypeId`);

CREATE INDEX `IX_StoreAssetBlobs_StoreAssetId` ON `StoreAssetBlobs` (`StoreAssetId`);

CREATE INDEX `IX_StoreAssetFiles_StoreAssetFileTypeId` ON `StoreAssetFiles` (`StoreAssetFileTypeId`);

CREATE INDEX `IX_StoreAssetFiles_StoreAssetId` ON `StoreAssetFiles` (`StoreAssetId`);

CREATE INDEX `IX_StoreAssetOwnerStoreAssetTypes_StoreAssetOwnerId` ON `StoreAssetOwnerStoreAssetTypes` (`StoreAssetOwnerId`);

CREATE INDEX `IX_StoreAssetOwnerStoreAssetTypes_StoreAssetTypeId` ON `StoreAssetOwnerStoreAssetTypes` (`StoreAssetTypeId`);

CREATE INDEX `IX_StoreAssets_AssetStatusId` ON `StoreAssets` (`AssetStatusId`);

CREATE INDEX `IX_StoreAssets_StoreAssetConditionId` ON `StoreAssets` (`StoreAssetConditionId`);

CREATE INDEX `IX_StoreAssets_StoreAssetOwnerId` ON `StoreAssets` (`StoreAssetOwnerId`);

CREATE INDEX `IX_StoreAssets_StoreAssetStatusId` ON `StoreAssets` (`StoreAssetStatusId`);

CREATE INDEX `IX_StoreAssets_StoreAssetSubTypeId` ON `StoreAssets` (`StoreAssetSubTypeId`);

CREATE INDEX `IX_StoreAssets_StoreAssetTypeId` ON `StoreAssets` (`StoreAssetTypeId`);

CREATE INDEX `IX_StoreAssets_StoreId` ON `StoreAssets` (`StoreId`);

CREATE INDEX `IX_StoreAssetStoreAssetTypeContacts_StoreAssetId` ON `StoreAssetStoreAssetTypeContacts` (`StoreAssetId`);

CREATE INDEX `IX_StoreAssetStoreAssetTypeContacts_StoreAssetTypeContactId` ON `StoreAssetStoreAssetTypeContacts` (`StoreAssetTypeContactId`);

CREATE INDEX `IX_StoreAssetSubTypes_StoreAssetTypeId` ON `StoreAssetSubTypes` (`StoreAssetTypeId`);

CREATE INDEX `IX_StoreAssetTypeStoreAssetSubTypes_StoreAssetSubTypeId` ON `StoreAssetTypeStoreAssetSubTypes` (`StoreAssetSubTypeId`);

CREATE INDEX `IX_StoreAssetTypeStoreAssetSubTypes_StoreAssetTypeId` ON `StoreAssetTypeStoreAssetSubTypes` (`StoreAssetTypeId`);

CREATE INDEX `IX_StoreAssetTypeStoreAssetTypeContacts_StoreAssetTypeContactId` ON `StoreAssetTypeStoreAssetTypeContacts` (`StoreAssetTypeContactId`);

CREATE INDEX `IX_StoreAssetTypeStoreAssetTypeContacts_StoreAssetTypeId` ON `StoreAssetTypeStoreAssetTypeContacts` (`StoreAssetTypeId`);

CREATE INDEX `IX_StoreBankDetails_StoreId` ON `StoreBankDetails` (`StoreId`);

CREATE INDEX `IX_StoreConceptAttributeOptions_StoreConceptAttributeId` ON `StoreConceptAttributeOptions` (`StoreConceptAttributeId`);

CREATE INDEX `IX_StoreConceptAttributes_StoreConceptAttributeTypeId` ON `StoreConceptAttributes` (`StoreConceptAttributeTypeId`);

CREATE INDEX `IX_StoreConceptAttributes_StoreConceptId` ON `StoreConceptAttributes` (`StoreConceptId`);

CREATE INDEX `IX_StoreConceptAttributeStoreAssets_StoreAssetId` ON `StoreConceptAttributeStoreAssets` (`StoreAssetId`);

CREATE INDEX `IX_StoreConceptAttributeValues_StoreConceptAttributeId` ON `StoreConceptAttributeValues` (`StoreConceptAttributeId`);

CREATE INDEX `IX_StoreConceptAttributeValues_StoreConceptAttributeOptionId` ON `StoreConceptAttributeValues` (`StoreConceptAttributeOptionId`);

CREATE UNIQUE INDEX `IX_StoreConceptAttributeValues_StoreId_StoreConceptAttributeId` ON `StoreConceptAttributeValues` (`StoreId`, `StoreConceptAttributeId`);

CREATE INDEX `IX_StoreConceptLevels_StoreConceptId` ON `StoreConceptLevels` (`StoreConceptId`);

CREATE UNIQUE INDEX `IX_StoreConceptLevels_StoreId_StoreConceptId` ON `StoreConceptLevels` (`StoreId`, `StoreConceptId`);

CREATE INDEX `IX_StoreCycles_EngageDepartmentId` ON `StoreCycles` (`EngageDepartmentId`);

CREATE INDEX `IX_StoreCycles_FrequencyTypeId` ON `StoreCycles` (`FrequencyTypeId`);

CREATE INDEX `IX_StoreCycles_StoreCycleOperationId` ON `StoreCycles` (`StoreCycleOperationId`);

CREATE INDEX `IX_StoreCycles_StoreId` ON `StoreCycles` (`StoreId`);

CREATE INDEX `IX_StoreFilters_FileUploadId` ON `StoreFilters` (`FileUploadId`);

CREATE INDEX `IX_StoreFilters_StoreId` ON `StoreFilters` (`StoreId`);

CREATE INDEX `IX_StoreFilterUploads_FileUploadId` ON `StoreFilterUploads` (`FileUploadId`);

CREATE INDEX `IX_StoreOwners_StoreGroupId` ON `StoreOwners` (`StoreGroupId`);

CREATE INDEX `IX_StoreOwners_StoreId` ON `StoreOwners` (`StoreId`);

CREATE INDEX `IX_StoreOwners_StoreOwnerTypeId` ON `StoreOwners` (`StoreOwnerTypeId`);

CREATE UNIQUE INDEX `IX_StorePOS_StoreId_StorePOSTypeId` ON `StorePOS` (`StoreId`, `StorePOSTypeId`);

CREATE INDEX `IX_StorePOS_StorePOSTypeId` ON `StorePOS` (`StorePOSTypeId`);

CREATE UNIQUE INDEX `IX_StorePOSFreezerQuestions_StoreId_StorePOSTypeId` ON `StorePOSFreezerQuestions` (`StoreId`, `StorePOSTypeId`);

CREATE INDEX `IX_StorePOSFreezerQuestions_StorePOSFreezerTypeId` ON `StorePOSFreezerQuestions` (`StorePOSFreezerTypeId`);

CREATE INDEX `IX_StorePOSFreezerQuestions_StorePOSTypeId` ON `StorePOSFreezerQuestions` (`StorePOSTypeId`);

CREATE UNIQUE INDEX `IX_StorePOSQuestions_StoreId_StorePOSTypeId` ON `StorePOSQuestions` (`StoreId`, `StorePOSTypeId`);

CREATE INDEX `IX_StorePOSQuestions_StorePOSTypeId` ON `StorePOSQuestions` (`StorePOSTypeId`);

CREATE UNIQUE INDEX `IX_Stores_Code` ON `Stores` (`Code`);

CREATE INDEX `IX_Stores_EngageLocationId` ON `Stores` (`EngageLocationId`);

CREATE INDEX `IX_Stores_EngageRegionId` ON `Stores` (`EngageRegionId`);

CREATE INDEX `IX_Stores_EngageSubRegionId` ON `Stores` (`EngageSubRegionId`);

CREATE INDEX `IX_Stores_Name` ON `Stores` (`Name`);

CREATE INDEX `IX_Stores_ParentStoreId` ON `Stores` (`ParentStoreId`);

CREATE INDEX `IX_Stores_PrimaryContactId` ON `Stores` (`PrimaryContactId`);

CREATE INDEX `IX_Stores_PrimaryLocationId` ON `Stores` (`PrimaryLocationId`);

CREATE UNIQUE INDEX `IX_Stores_StakeholderId` ON `Stores` (`StakeholderId`);

CREATE INDEX `IX_Stores_StoreClusterId` ON `Stores` (`StoreClusterId`);

CREATE INDEX `IX_Stores_StoreFormatId` ON `Stores` (`StoreFormatId`);

CREATE INDEX `IX_Stores_StoreGroupId` ON `Stores` (`StoreGroupId`);

CREATE UNIQUE INDEX `IX_Stores_StoreId_ParentStoreId` ON `Stores` (`StoreId`, `ParentStoreId`);

CREATE INDEX `IX_Stores_StoreLocationTypeId` ON `Stores` (`StoreLocationTypeId`);

CREATE INDEX `IX_Stores_StoreLSMId` ON `Stores` (`StoreLSMId`);

CREATE INDEX `IX_Stores_StoreMediaGroupId` ON `Stores` (`StoreMediaGroupId`);

CREATE INDEX `IX_Stores_StoreSparRegionId` ON `Stores` (`StoreSparRegionId`);

CREATE INDEX `IX_Stores_StoreTypeId` ON `Stores` (`StoreTypeId`);

CREATE INDEX `IX_StoreStoreConceptPerformances_StoreConceptId` ON `StoreStoreConceptPerformances` (`StoreConceptId`);

CREATE INDEX `IX_StoreStoreConcepts_StoreConceptId` ON `StoreStoreConcepts` (`StoreConceptId`);

CREATE INDEX `IX_StoreStoreDepartments_StoreDepartmentId` ON `StoreStoreDepartments` (`StoreDepartmentId`);

CREATE INDEX `IX_StoreStoreList_StoreListId` ON `StoreStoreList` (`StoreListId`);

CREATE INDEX `IX_SubContractorBrands_EngageBrandId` ON `SubContractorBrands` (`EngageBrandId`);

CREATE INDEX `IX_SubContractorBrands_EngageRegionId` ON `SubContractorBrands` (`EngageRegionId`);

CREATE INDEX `IX_SubContractorBrands_ParentId` ON `SubContractorBrands` (`ParentId`);

CREATE INDEX `IX_SubContractorBrands_SupplierId` ON `SubContractorBrands` (`SupplierId`);

CREATE INDEX `IX_SupplierAllowanceContracts_SupplierId` ON `SupplierAllowanceContracts` (`SupplierId`);

CREATE INDEX `IX_SupplierAllowanceContracts_SupplierSalesLeadId` ON `SupplierAllowanceContracts` (`SupplierSalesLeadId`);

CREATE INDEX `IX_SupplierAllowances_SupplierId` ON `SupplierAllowances` (`SupplierId`);

CREATE INDEX `IX_SupplierAllowances_SupplierSalesLeadId` ON `SupplierAllowances` (`SupplierSalesLeadId`);

CREATE INDEX `IX_SupplierAllowanceSubContracts_SupplierAllowanceContractId` ON `SupplierAllowanceSubContracts` (`SupplierAllowanceContractId`);

CREATE INDEX `IX_SupplierBudgets_EngageRegionId` ON `SupplierBudgets` (`EngageRegionId`);

CREATE INDEX `IX_SupplierBudgets_SupplierBudgetTypeId` ON `SupplierBudgets` (`SupplierBudgetTypeId`);

CREATE INDEX `IX_SupplierBudgets_SupplierBudgetVersionId` ON `SupplierBudgets` (`SupplierBudgetVersionId`);

CREATE INDEX `IX_SupplierBudgets_SupplierContractDetailId` ON `SupplierBudgets` (`SupplierContractDetailId`);

CREATE INDEX `IX_SupplierBudgets_SupplierId` ON `SupplierBudgets` (`SupplierId`);

CREATE INDEX `IX_SupplierBudgetVersions_SupplierBudgetVersionTypeId` ON `SupplierBudgetVersions` (`SupplierBudgetVersionTypeId`);

CREATE INDEX `IX_SupplierBudgetVersions_SupplierPeriodId` ON `SupplierBudgetVersions` (`SupplierPeriodId`);

CREATE INDEX `IX_SupplierClaimAccountManagers_UserId` ON `SupplierClaimAccountManagers` (`UserId`);

CREATE INDEX `IX_SupplierClaimClassifications_ClaimClassificationId` ON `SupplierClaimClassifications` (`ClaimClassificationId`);

CREATE INDEX `IX_SupplierContractAmounts_SupplierContractAmountTypeId` ON `SupplierContractAmounts` (`SupplierContractAmountTypeId`);

CREATE INDEX `IX_SupplierContractAmounts_SupplierContractSplitId` ON `SupplierContractAmounts` (`SupplierContractSplitId`);

CREATE INDEX `IX_SupplierContractAmounts_SupplierSubContractDetailId` ON `SupplierContractAmounts` (`SupplierSubContractDetailId`);

CREATE INDEX `IX_SupplierContractDetails_EngageRegionId` ON `SupplierContractDetails` (`EngageRegionId`);

CREATE INDEX `IX_SupplierContractDetails_SupplierContractDetailTypeId` ON `SupplierContractDetails` (`SupplierContractDetailTypeId`);

CREATE INDEX `IX_SupplierContractDetails_SupplierContractId` ON `SupplierContractDetails` (`SupplierContractId`);

CREATE INDEX `IX_SupplierContracts_SupplierContactId` ON `SupplierContracts` (`SupplierContactId`);

CREATE INDEX `IX_SupplierContracts_SupplierContractGroupId` ON `SupplierContracts` (`SupplierContractGroupId`);

CREATE INDEX `IX_SupplierContracts_SupplierContractSubGroupId` ON `SupplierContracts` (`SupplierContractSubGroupId`);

CREATE INDEX `IX_SupplierContracts_SupplierContractTypeId` ON `SupplierContracts` (`SupplierContractTypeId`);

CREATE INDEX `IX_SupplierContracts_SupplierId` ON `SupplierContracts` (`SupplierId`);

CREATE INDEX `IX_SupplierContractSubGroups_SupplierContractGroupId` ON `SupplierContractSubGroups` (`SupplierContractGroupId`);

CREATE INDEX `IX_SupplierEngageBrands_EngageBrandId` ON `SupplierEngageBrands` (`EngageBrandId`);

CREATE INDEX `IX_SupplierPeriods_SupplierYearId` ON `SupplierPeriods` (`SupplierYearId`);

CREATE INDEX `IX_SupplierProducts_EngageMasterProductId` ON `SupplierProducts` (`EngageMasterProductId`);

CREATE UNIQUE INDEX `IX_Suppliers_Code` ON `Suppliers` (`Code`);

CREATE INDEX `IX_Suppliers_Name` ON `Suppliers` (`Name`);

CREATE INDEX `IX_Suppliers_PrimaryContactId` ON `Suppliers` (`PrimaryContactId`);

CREATE INDEX `IX_Suppliers_PrimaryLocationId` ON `Suppliers` (`PrimaryLocationId`);

CREATE UNIQUE INDEX `IX_Suppliers_StakeholderId` ON `Suppliers` (`StakeholderId`);

CREATE INDEX `IX_Suppliers_SupplierGroupId` ON `Suppliers` (`SupplierGroupId`);

CREATE UNIQUE INDEX `IX_SupplierSettings_SettingId_SupplierId` ON `SupplierSettings` (`SettingId`, `SupplierId`);

CREATE INDEX `IX_SupplierSettings_SupplierId` ON `SupplierSettings` (`SupplierId`);

CREATE INDEX `IX_SupplierStores_EngageSubGroupId` ON `SupplierStores` (`EngageSubGroupId`);

CREATE INDEX `IX_SupplierStores_FrequencyTypeId` ON `SupplierStores` (`FrequencyTypeId`);

CREATE INDEX `IX_SupplierStores_StoreId` ON `SupplierStores` (`StoreId`);

CREATE UNIQUE INDEX `IX_SupplierStores_SupplierId_StoreId_EngageSubGroupId` ON `SupplierStores` (`SupplierId`, `StoreId`, `EngageSubGroupId`);

CREATE INDEX `IX_SupplierStores_SupplierRegionId` ON `SupplierStores` (`SupplierRegionId`);

CREATE INDEX `IX_SupplierStores_SupplierSubRegionId` ON `SupplierStores` (`SupplierSubRegionId`);

CREATE INDEX `IX_SupplierSubContractDetails_SupplierSubContractDetailTypeId` ON `SupplierSubContractDetails` (`SupplierSubContractDetailTypeId`);

CREATE INDEX `IX_SupplierSubContractDetails_SupplierSubContractTypeId` ON `SupplierSubContractDetails` (`SupplierSubContractTypeId`);

CREATE INDEX `IX_SupplierSubContracts_SupplierContractId` ON `SupplierSubContracts` (`SupplierContractId`);

CREATE INDEX `IX_SupplierSubContracts_SupplierSubContractTypeId` ON `SupplierSubContracts` (`SupplierSubContractTypeId`);

CREATE INDEX `IX_SupplierSubRegions_SupplierRegionId` ON `SupplierSubRegions` (`SupplierRegionId`);

CREATE INDEX `IX_SupplierSupplierTypes_SupplierTypeId` ON `SupplierSupplierTypes` (`SupplierTypeId`);

CREATE UNIQUE INDEX `IX_SurveyAnswerOptions_SurveyAnswerId_SurveyAnswerOptionId` ON `SurveyAnswerOptions` (`SurveyAnswerId`, `SurveyAnswerOptionId`);

CREATE INDEX `IX_SurveyAnswerOptions_SurveyQuestionOptionId` ON `SurveyAnswerOptions` (`SurveyQuestionOptionId`);

CREATE INDEX `IX_SurveyAnswerPhotos_SurveyAnswerId` ON `SurveyAnswerPhotos` (`SurveyAnswerId`);

CREATE INDEX `IX_SurveyAnswers_QuestionFalseReasonId` ON `SurveyAnswers` (`QuestionFalseReasonId`);

CREATE UNIQUE INDEX `IX_SurveyAnswers_SurveyInstanceId_SurveyQuestionId` ON `SurveyAnswers` (`SurveyInstanceId`, `SurveyQuestionId`);

CREATE INDEX `IX_SurveyAnswers_SurveyQuestionId` ON `SurveyAnswers` (`SurveyQuestionId`);

CREATE INDEX `IX_SurveyEmployees_EmployeeId` ON `SurveyEmployees` (`EmployeeId`);

CREATE INDEX `IX_SurveyEmployees_TargetingId` ON `SurveyEmployees` (`TargetingId`);

CREATE INDEX `IX_SurveyEngageRegions_EngageRegionId` ON `SurveyEngageRegions` (`EngageRegionId`);

CREATE INDEX `IX_SurveyFormAnswerHistories_SurveyFormAnswerId` ON `SurveyFormAnswerHistories` (`SurveyFormAnswerId`);

CREATE INDEX `IX_SurveyFormAnswerHistories_SurveyFormReasonId` ON `SurveyFormAnswerHistories` (`SurveyFormReasonId`);

CREATE INDEX `IX_SurveyFormAnswerOptionHistories_SurveyFormAnswerHistoryId` ON `SurveyFormAnswerOptionHistories` (`SurveyFormAnswerHistoryId`);

CREATE INDEX `IX_SurveyFormAnswerOptionHistories_SurveyFormOptionId` ON `SurveyFormAnswerOptionHistories` (`SurveyFormOptionId`);

CREATE INDEX `IX_SurveyFormAnswerOptions_SurveyFormAnswerId` ON `SurveyFormAnswerOptions` (`SurveyFormAnswerId`);

CREATE INDEX `IX_SurveyFormAnswerOptions_SurveyFormOptionId` ON `SurveyFormAnswerOptions` (`SurveyFormOptionId`);

CREATE INDEX `IX_SurveyFormAnswers_SurveyFormId` ON `SurveyFormAnswers` (`SurveyFormId`);

CREATE INDEX `IX_SurveyFormAnswers_SurveyFormQuestionId` ON `SurveyFormAnswers` (`SurveyFormQuestionId`);

CREATE INDEX `IX_SurveyFormAnswers_SurveyFormReasonId` ON `SurveyFormAnswers` (`SurveyFormReasonId`);

CREATE INDEX `IX_SurveyFormAnswers_SurveyFormSubmissionId` ON `SurveyFormAnswers` (`SurveyFormSubmissionId`);

CREATE INDEX `IX_SurveyFormProducts_EngageMasterProductId` ON `SurveyFormProducts` (`EngageMasterProductId`);

CREATE INDEX `IX_SurveyFormProducts_SurveyFormId` ON `SurveyFormProducts` (`SurveyFormId`);

CREATE INDEX `IX_SurveyFormQuestionGroupProducts_EngageVariantProductId` ON `SurveyFormQuestionGroupProducts` (`EngageVariantProductId`);

CREATE INDEX `IX_SurveyFormQuestionGroupProducts_SurveyFormQuestionGroupId` ON `SurveyFormQuestionGroupProducts` (`SurveyFormQuestionGroupId`);

CREATE INDEX `IX_SurveyFormQuestionGroups_SurveyFormId` ON `SurveyFormQuestionGroups` (`SurveyFormId`);

CREATE INDEX `IX_SurveyFormQuestionOptions_SurveyFormOptionId` ON `SurveyFormQuestionOptions` (`SurveyFormOptionId`);

CREATE INDEX `IX_SurveyFormQuestionOptions_SurveyFormQuestionId` ON `SurveyFormQuestionOptions` (`SurveyFormQuestionId`);

CREATE INDEX `IX_SurveyFormQuestionProducts_EngageVariantProductId` ON `SurveyFormQuestionProducts` (`EngageVariantProductId`);

CREATE INDEX `IX_SurveyFormQuestionProducts_SurveyFormQuestionId` ON `SurveyFormQuestionProducts` (`SurveyFormQuestionId`);

CREATE INDEX `IX_SurveyFormQuestionReasons_SurveyFormQuestionId` ON `SurveyFormQuestionReasons` (`SurveyFormQuestionId`);

CREATE INDEX `IX_SurveyFormQuestionReasons_SurveyFormReasonId` ON `SurveyFormQuestionReasons` (`SurveyFormReasonId`);

CREATE INDEX `IX_SurveyFormQuestions_SurveyFormQuestionGroupId` ON `SurveyFormQuestions` (`SurveyFormQuestionGroupId`);

CREATE INDEX `IX_SurveyFormQuestions_SurveyFormQuestionTypeId` ON `SurveyFormQuestions` (`SurveyFormQuestionTypeId`);

CREATE INDEX `IX_SurveyForms_EngageBrandId` ON `SurveyForms` (`EngageBrandId`);

CREATE INDEX `IX_SurveyForms_EngageSubgroupId` ON `SurveyForms` (`EngageSubgroupId`);

CREATE INDEX `IX_SurveyForms_SupplierId` ON `SurveyForms` (`SupplierId`);

CREATE INDEX `IX_SurveyForms_SurveyFormTypeId` ON `SurveyForms` (`SurveyFormTypeId`);

CREATE INDEX `IX_SurveyFormSubmissions_EmployeeId` ON `SurveyFormSubmissions` (`EmployeeId`);

CREATE INDEX `IX_SurveyFormSubmissions_StoreId` ON `SurveyFormSubmissions` (`StoreId`);

CREATE INDEX `IX_SurveyFormSubmissions_SurveyFormId` ON `SurveyFormSubmissions` (`SurveyFormId`);

CREATE INDEX `IX_SurveyFormSubmissions_UserId` ON `SurveyFormSubmissions` (`UserId`);

CREATE INDEX `IX_SurveyFormTargets_EmployeeDivisionId` ON `SurveyFormTargets` (`EmployeeDivisionId`);

CREATE INDEX `IX_SurveyFormTargets_EmployeeEngageRegionId` ON `SurveyFormTargets` (`EmployeeEngageRegionId`);

CREATE INDEX `IX_SurveyFormTargets_EmployeeId` ON `SurveyFormTargets` (`EmployeeId`);

CREATE INDEX `IX_SurveyFormTargets_EmployeeJobTitleId` ON `SurveyFormTargets` (`EmployeeJobTitleId`);

CREATE INDEX `IX_SurveyFormTargets_EngageDepartmentId` ON `SurveyFormTargets` (`EngageDepartmentId`);

CREATE INDEX `IX_SurveyFormTargets_ExcludedEmployeeId` ON `SurveyFormTargets` (`ExcludedEmployeeId`);

CREATE INDEX `IX_SurveyFormTargets_ExcludedStoreId` ON `SurveyFormTargets` (`ExcludedStoreId`);

CREATE INDEX `IX_SurveyFormTargets_StoreClusterId` ON `SurveyFormTargets` (`StoreClusterId`);

CREATE INDEX `IX_SurveyFormTargets_StoreEngageRegionId` ON `SurveyFormTargets` (`StoreEngageRegionId`);

CREATE INDEX `IX_SurveyFormTargets_StoreFormatId` ON `SurveyFormTargets` (`StoreFormatId`);

CREATE INDEX `IX_SurveyFormTargets_StoreId` ON `SurveyFormTargets` (`StoreId`);

CREATE INDEX `IX_SurveyFormTargets_StoreLSMId` ON `SurveyFormTargets` (`StoreLSMId`);

CREATE INDEX `IX_SurveyFormTargets_StoreTypeId` ON `SurveyFormTargets` (`StoreTypeId`);

CREATE INDEX `IX_SurveyFormTargets_SurveyFormId` ON `SurveyFormTargets` (`SurveyFormId`);

CREATE UNIQUE INDEX `IX_SurveyInstances_EmployeeId_StoreId_SurveyId_SurveyDate` ON `SurveyInstances` (`EmployeeId`, `StoreId`, `SurveyId`, `SurveyDate`);

CREATE INDEX `IX_SurveyInstances_StoreId` ON `SurveyInstances` (`StoreId`);

CREATE INDEX `IX_SurveyInstances_SurveyId` ON `SurveyInstances` (`SurveyId`);

CREATE INDEX `IX_SurveyQuestionFalseReasons_QuestionFalseReasonId` ON `SurveyQuestionFalseReasons` (`QuestionFalseReasonId`);

CREATE UNIQUE INDEX `IX_SurveyQuestionOptions_SurveyQuestionId_DisplayOrder` ON `SurveyQuestionOptions` (`SurveyQuestionId`, `DisplayOrder`);

CREATE INDEX `IX_SurveyQuestionRules_QuestionId` ON `SurveyQuestionRules` (`QuestionId`);

CREATE INDEX `IX_SurveyQuestionRules_TargetQuestionId` ON `SurveyQuestionRules` (`TargetQuestionId`);

CREATE INDEX `IX_SurveyQuestions_EngageVariantProductId` ON `SurveyQuestions` (`EngageVariantProductId`);

CREATE INDEX `IX_SurveyQuestions_QuestionTypeId` ON `SurveyQuestions` (`QuestionTypeId`);

CREATE INDEX `IX_SurveyQuestions_StoreConceptAttributeId` ON `SurveyQuestions` (`StoreConceptAttributeId`);

CREATE INDEX `IX_SurveyQuestions_StoreConceptId` ON `SurveyQuestions` (`StoreConceptId`);

CREATE INDEX `IX_SurveyQuestions_SurveyId` ON `SurveyQuestions` (`SurveyId`);

CREATE INDEX `IX_Surveys_EngageBrandId` ON `Surveys` (`EngageBrandId`);

CREATE INDEX `IX_Surveys_EngageMasterProductId` ON `Surveys` (`EngageMasterProductId`);

CREATE INDEX `IX_Surveys_EngageSubGroupId` ON `Surveys` (`EngageSubGroupId`);

CREATE INDEX `IX_Surveys_SupplierId` ON `Surveys` (`SupplierId`);

CREATE INDEX `IX_Surveys_SurveyTypeId` ON `Surveys` (`SurveyTypeId`);

CREATE INDEX `IX_SurveyStoreFormats_StoreFormatId` ON `SurveyStoreFormats` (`StoreFormatId`);

CREATE INDEX `IX_SurveyStores_StoreId` ON `SurveyStores` (`StoreId`);

CREATE INDEX `IX_SurveyStores_TargetingId` ON `SurveyStores` (`TargetingId`);

CREATE INDEX `IX_SurveyTargets_EmployeeId` ON `SurveyTargets` (`EmployeeId`);

CREATE INDEX `IX_SurveyTargets_EmployeeJobTitleId` ON `SurveyTargets` (`EmployeeJobTitleId`);

CREATE INDEX `IX_SurveyTargets_EngageRegionId` ON `SurveyTargets` (`EngageRegionId`);

CREATE INDEX `IX_SurveyTargets_StoreFormatId` ON `SurveyTargets` (`StoreFormatId`);

CREATE INDEX `IX_SurveyTargets_StoreId` ON `SurveyTargets` (`StoreId`);

CREATE INDEX `IX_SurveyTargets_SurveyId` ON `SurveyTargets` (`SurveyId`);

CREATE UNIQUE INDEX `IX_SurveyTargets_SurveyId_EmployeeId` ON `SurveyTargets` (`SurveyId`, `EmployeeId`);

CREATE UNIQUE INDEX `IX_SurveyTargets_SurveyId_EmployeeJobTitleId` ON `SurveyTargets` (`SurveyId`, `EmployeeJobTitleId`);

CREATE UNIQUE INDEX `IX_SurveyTargets_SurveyId_EngageRegionId` ON `SurveyTargets` (`SurveyId`, `EngageRegionId`);

CREATE UNIQUE INDEX `IX_SurveyTargets_SurveyId_StoreFormatId` ON `SurveyTargets` (`SurveyId`, `StoreFormatId`);

CREATE UNIQUE INDEX `IX_SurveyTargets_SurveyId_StoreId` ON `SurveyTargets` (`SurveyId`, `StoreId`);

CREATE UNIQUE INDEX `IX_TenantSettings_SettingId` ON `TenantSettings` (`SettingId`);

CREATE INDEX `IX_TrainingFacilitators_TrainingId` ON `TrainingFacilitators` (`TrainingId`);

CREATE INDEX `IX_TrainingFiles_TrainingFileTypeId` ON `TrainingFiles` (`TrainingFileTypeId`);

CREATE INDEX `IX_TrainingFiles_TrainingId` ON `TrainingFiles` (`TrainingId`);

CREATE INDEX `IX_TrainingPeriods_TrainingYearId` ON `TrainingPeriods` (`TrainingYearId`);

CREATE INDEX `IX_Trainings_EngageRegionId` ON `Trainings` (`EngageRegionId`);

CREATE INDEX `IX_Trainings_TrainingCategoryId` ON `Trainings` (`TrainingCategoryId`);

CREATE INDEX `IX_Trainings_TrainingDurationId` ON `Trainings` (`TrainingDurationId`);

CREATE INDEX `IX_Trainings_TrainingPeriodId` ON `Trainings` (`TrainingPeriodId`);

CREATE INDEX `IX_Trainings_TrainingProviderId` ON `Trainings` (`TrainingProviderId`);

CREATE INDEX `IX_Trainings_TrainingTypeId` ON `Trainings` (`TrainingTypeId`);

CREATE INDEX `IX_UserCommunicationTypes_CommunicationTypeId` ON `UserCommunicationTypes` (`CommunicationTypeId`);

CREATE INDEX `IX_UserCommunicationTypes_UserId` ON `UserCommunicationTypes` (`UserId`);

CREATE INDEX `IX_UserEngageSubGroups_EngageSubGroupId` ON `UserEngageSubGroups` (`EngageSubGroupId`);

CREATE INDEX `IX_UserEngageSubGroups_UserId` ON `UserEngageSubGroups` (`UserId`);

CREATE UNIQUE INDEX `IX_UserEntities_Entity` ON `UserEntities` (`Entity`);

CREATE INDEX `IX_UserEntities_UserId` ON `UserEntities` (`UserId`);

CREATE INDEX `IX_UserEntityRecords_UserEntityId` ON `UserEntityRecords` (`UserEntityId`);

CREATE INDEX `IX_UserGroups_EngageRegionId` ON `UserGroups` (`EngageRegionId`);

CREATE INDEX `IX_UserOrganizationRoles_UserId` ON `UserOrganizationRoles` (`UserId`);

CREATE INDEX `IX_UserOrganizationRoles_UserOrganizationId` ON `UserOrganizationRoles` (`UserOrganizationId`);

CREATE INDEX `IX_UserOrganizationRoles_UserRoleId` ON `UserOrganizationRoles` (`UserRoleId`);

CREATE INDEX `IX_UserOrganizations_SupplierId` ON `UserOrganizations` (`SupplierId`);

CREATE INDEX `IX_UserRegions_EngageRegionId` ON `UserRegions` (`EngageRegionId`);

CREATE INDEX `IX_UserRegions_UserId` ON `UserRegions` (`UserId`);

CREATE INDEX `IX_UserRolePermissions_UserPermissionId` ON `UserRolePermissions` (`UserPermissionId`);

CREATE INDEX `IX_UserRolePermissions_UserRoleId` ON `UserRolePermissions` (`UserRoleId`);

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

CREATE INDEX `IX_Users_OrganizationId` ON `Users` (`OrganizationId`);

CREATE INDEX `IX_Users_RoleId` ON `Users` (`RoleId`);

CREATE INDEX `IX_Users_SupplierId` ON `Users` (`SupplierId`);

CREATE INDEX `IX_UserStores_StoreId` ON `UserStores` (`StoreId`);

CREATE INDEX `IX_UserStores_UserId` ON `UserStores` (`UserId`);

CREATE INDEX `IX_UserUserGroups_UserGroupId` ON `UserUserGroups` (`UserGroupId`);

CREATE UNIQUE INDEX `IX_UserUserGroups_UserId_UserGroupId` ON `UserUserGroups` (`UserId`, `UserGroupId`);

CREATE UNIQUE INDEX `IX_Vat_Code` ON `Vat` (`Code`);

CREATE INDEX `IX_VatPeriods_VatId` ON `VatPeriods` (`VatId`);

CREATE INDEX `IX_Vendors_DistributionCenterId` ON `Vendors` (`DistributionCenterId`);

CREATE INDEX `IX_Vendors_Name` ON `Vendors` (`Name`);

CREATE INDEX `IX_Vendors_SupplierId` ON `Vendors` (`SupplierId`);

CREATE INDEX `IX_VoucherDetails_ClaimId` ON `VoucherDetails` (`ClaimId`);

CREATE INDEX `IX_VoucherDetails_EmployeeId` ON `VoucherDetails` (`EmployeeId`);

CREATE INDEX `IX_VoucherDetails_StoreContactId` ON `VoucherDetails` (`StoreContactId`);

CREATE INDEX `IX_VoucherDetails_StoreId` ON `VoucherDetails` (`StoreId`);

CREATE INDEX `IX_VoucherDetails_VoucherDetailStatusId` ON `VoucherDetails` (`VoucherDetailStatusId`);

CREATE UNIQUE INDEX `IX_VoucherDetails_VoucherId_VoucherNumber` ON `VoucherDetails` (`VoucherId`, `VoucherNumber`);

CREATE INDEX `IX_Vouchers_EngageRegionId` ON `Vouchers` (`EngageRegionId`);

CREATE INDEX `IX_Vouchers_SupplierId` ON `Vouchers` (`SupplierId`);

CREATE INDEX `IX_Vouchers_VoucherStatusId` ON `Vouchers` (`VoucherStatusId`);

CREATE INDEX `IX_Warehouses_DCId` ON `Warehouses` (`DCId`);

CREATE INDEX `IX_WebEvents_WebEventTypeId` ON `WebEvents` (`WebEventTypeId`);

CREATE INDEX `IX_WebFileCategories_WebFileGroupId` ON `WebFileCategories` (`WebFileGroupId`);

CREATE INDEX `IX_WebFiles_EmployeeId` ON `WebFiles` (`EmployeeId`);

CREATE INDEX `IX_WebFiles_FileTypeId` ON `WebFiles` (`FileTypeId`);

CREATE INDEX `IX_WebFiles_NPrintingId` ON `WebFiles` (`NPrintingId`);

CREATE INDEX `IX_WebFiles_StoreId` ON `WebFiles` (`StoreId`);

CREATE INDEX `IX_WebFiles_TargetStrategyId` ON `WebFiles` (`TargetStrategyId`);

CREATE INDEX `IX_WebFiles_WebFileCategoryId` ON `WebFiles` (`WebFileCategoryId`);

CREATE INDEX `IX_WebFileTargets_EmployeeDivisionId` ON `WebFileTargets` (`EmployeeDivisionId`);

CREATE INDEX `IX_WebFileTargets_EmployeeId` ON `WebFileTargets` (`EmployeeId`);

CREATE INDEX `IX_WebFileTargets_EmployeeJobTitleId` ON `WebFileTargets` (`EmployeeJobTitleId`);

CREATE INDEX `IX_WebFileTargets_EngageDepartmentId` ON `WebFileTargets` (`EngageDepartmentId`);

CREATE INDEX `IX_WebFileTargets_EngageRegionId` ON `WebFileTargets` (`EngageRegionId`);

CREATE INDEX `IX_WebFileTargets_StoreFormatId` ON `WebFileTargets` (`StoreFormatId`);

CREATE INDEX `IX_WebFileTargets_StoreId` ON `WebFileTargets` (`StoreId`);

CREATE INDEX `IX_WebFileTargets_WebFileId` ON `WebFileTargets` (`WebFileId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EmployeeDivisionId` ON `WebFileTargets` (`WebFileId`, `EmployeeDivisionId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EmployeeId` ON `WebFileTargets` (`WebFileId`, `EmployeeId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EmployeeJobTitleId` ON `WebFileTargets` (`WebFileId`, `EmployeeJobTitleId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EngageDepartmentId` ON `WebFileTargets` (`WebFileId`, `EngageDepartmentId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EngageRegionId` ON `WebFileTargets` (`WebFileId`, `EngageRegionId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_StoreFormatId` ON `WebFileTargets` (`WebFileId`, `StoreFormatId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_StoreId` ON `WebFileTargets` (`WebFileId`, `StoreId`);

CREATE INDEX `IX_WebPageEmployees_EmployeeId` ON `WebPageEmployees` (`EmployeeId`);

CREATE INDEX `IX_WebPageEmployees_WebPageId` ON `WebPageEmployees` (`WebPageId`);

ALTER TABLE `CategoryFiles` ADD CONSTRAINT `FK_CategoryFiles_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `CategoryFileTargets` ADD CONSTRAINT `FK_CategoryFileTargets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `CategoryFileTargets` ADD CONSTRAINT `FK_CategoryFileTargets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `CategoryStoreGroups` ADD CONSTRAINT `FK_CategoryStoreGroups_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswerHistories` ADD CONSTRAINT `FK_CategoryTargetAnswerHistories_CategoryTargetAnswers_Category~` FOREIGN KEY (`CategoryTargetAnswerId`) REFERENCES `CategoryTargetAnswers` (`CategoryTargetAnswerId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswerHistories` ADD CONSTRAINT `FK_CategoryTargetAnswerHistories_CategoryTargetStores_CategoryT~` FOREIGN KEY (`CategoryTargetStoreId`) REFERENCES `CategoryTargetStores` (`CategoryTargetStoreId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswerHistories` ADD CONSTRAINT `FK_CategoryTargetAnswerHistories_CategoryTargets_CategoryTarget~` FOREIGN KEY (`CategoryTargetId`) REFERENCES `CategoryTargets` (`CategoryTargetId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswerHistories` ADD CONSTRAINT `FK_CategoryTargetAnswerHistories_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `CategoryTargetAnswers` ADD CONSTRAINT `FK_CategoryTargetAnswers_CategoryTargetStores_CategoryTargetSto~` FOREIGN KEY (`CategoryTargetStoreId`) REFERENCES `CategoryTargetStores` (`CategoryTargetStoreId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswers` ADD CONSTRAINT `FK_CategoryTargetAnswers_CategoryTargets_CategoryTargetId` FOREIGN KEY (`CategoryTargetId`) REFERENCES `CategoryTargets` (`CategoryTargetId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetAnswers` ADD CONSTRAINT `FK_CategoryTargetAnswers_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `CategoryTargets` ADD CONSTRAINT `FK_CategoryTargets_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `CategoryTargetStores` ADD CONSTRAINT `FK_CategoryTargetStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `ClaimBatchDetails` ADD CONSTRAINT `FK_ClaimBatchDetails_ClaimBatches_ClaimBatchId` FOREIGN KEY (`ClaimBatchId`) REFERENCES `ClaimBatches` (`ClaimBatchId`) ON DELETE CASCADE;

ALTER TABLE `ClaimBatchDetails` ADD CONSTRAINT `FK_ClaimBatchDetails_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE;

ALTER TABLE `ClaimBatches` ADD CONSTRAINT `FK_ClaimBatches_ClaimClassifications_ClaimClassificationId` FOREIGN KEY (`ClaimClassificationId`) REFERENCES `ClaimClassifications` (`ClaimClassificationId`) ON DELETE CASCADE;

ALTER TABLE `ClaimClassifications` ADD CONSTRAINT `FK_ClaimClassifications_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`);

ALTER TABLE `ClaimClassifications` ADD CONSTRAINT `FK_ClaimClassifications_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `ClaimClassificationTypes` ADD CONSTRAINT `FK_ClaimClassificationTypes_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`);

ALTER TABLE `ClaimFloatClaims` ADD CONSTRAINT `FK_ClaimFloatClaims_ClaimFloats_ClaimFloatId` FOREIGN KEY (`ClaimFloatId`) REFERENCES `ClaimFloats` (`ClaimFloatId`);

ALTER TABLE `ClaimFloatClaims` ADD CONSTRAINT `FK_ClaimFloatClaims_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`);

ALTER TABLE `ClaimFloats` ADD CONSTRAINT `FK_ClaimFloats_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`);

ALTER TABLE `ClaimFloats` ADD CONSTRAINT `FK_ClaimFloats_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `ClaimHistories` ADD CONSTRAINT `FK_ClaimHistories_Claims_ClaimId` FOREIGN KEY (`ClaimId`) REFERENCES `Claims` (`ClaimId`) ON DELETE CASCADE;

ALTER TABLE `ClaimNotificationUsers` ADD CONSTRAINT `FK_ClaimNotificationUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`) ON DELETE CASCADE;

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_Users_ClaimAccountManagerId` FOREIGN KEY (`ClaimAccountManagerId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_Users_ClaimManagerId` FOREIGN KEY (`ClaimManagerId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `ClaimSkus` ADD CONSTRAINT `FK_ClaimSkus_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE;

ALTER TABLE `ClaimStatusUsers` ADD CONSTRAINT `FK_ClaimStatusUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `ClaimTypeReportTypes` ADD CONSTRAINT `FK_ClaimTypeReportTypes_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`);

ALTER TABLE `ClaimTypes` ADD CONSTRAINT `FK_ClaimTypes_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `CommunicationHistories` ADD CONSTRAINT `FK_CommunicationHistories_EmployeeStoreCalendars_EmployeeStoreC~` FOREIGN KEY (`EmployeeStoreCalendarId`) REFERENCES `EmployeeStoreCalendars` (`EmployeeStoreCalendarId`) ON DELETE CASCADE;

ALTER TABLE `CommunicationHistories` ADD CONSTRAINT `FK_CommunicationHistories_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `CommunicationHistories` ADD CONSTRAINT `FK_CommunicationHistories_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`OrderId`) ON DELETE CASCADE;

ALTER TABLE `CommunicationHistories` ADD CONSTRAINT `FK_CommunicationHistories_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `CommunicationHistories` ADD CONSTRAINT `FK_CommunicationHistories_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `ContactEvents` ADD CONSTRAINT `FK_ContactEvents_Contacts_ContactId` FOREIGN KEY (`ContactId`) REFERENCES `Contacts` (`ContactId`);

ALTER TABLE `ContactItems` ADD CONSTRAINT `FK_ContactItems_Contacts_ContactId` FOREIGN KEY (`ContactId`) REFERENCES `Contacts` (`ContactId`);

ALTER TABLE `Contacts` ADD CONSTRAINT `FK_Contacts_Stakeholders_StakeholderId` FOREIGN KEY (`StakeholderId`) REFERENCES `Stakeholders` (`StakeholderId`);

ALTER TABLE `CostCenterEmployees` ADD CONSTRAINT `FK_CostCenterEmployees_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `CreditorNotificationStatusUsers` ADD CONSTRAINT `FK_CreditorNotificationStatusUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `DCAccounts` ADD CONSTRAINT `FK_DCAccounts_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `DCProducts` ADD CONSTRAINT `FK_DCProducts_EngageVariantProducts_EngageVariantProductId` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`);

ALTER TABLE `DCProducts` ADD CONSTRAINT `FK_DCProducts_Manufacturers_ManufacturerId` FOREIGN KEY (`ManufacturerId`) REFERENCES `Manufacturers` (`ManufacturerId`);

ALTER TABLE `DCProducts` ADD CONSTRAINT `FK_DCProducts_Vendors_VendorId` FOREIGN KEY (`VendorId`) REFERENCES `Vendors` (`VendorId`) ON DELETE CASCADE;

ALTER TABLE `EmailTemplateHistories` ADD CONSTRAINT `FK_EmailTemplateHistories_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeAddresses` ADD CONSTRAINT `FK_EmployeeAddresses_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeAssetHistories` ADD CONSTRAINT `FK_EmployeeAssetHistories_EmployeeAssets_EmployeeAssetId` FOREIGN KEY (`EmployeeAssetId`) REFERENCES `EmployeeAssets` (`EmployeeAssetId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeAssetHistories` ADD CONSTRAINT `FK_EmployeeAssetHistories_Employees_NewEmployeeId` FOREIGN KEY (`NewEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeAssetHistories` ADD CONSTRAINT `FK_EmployeeAssetHistories_Employees_OldEmployeeId` FOREIGN KEY (`OldEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeAssets` ADD CONSTRAINT `FK_EmployeeAssets_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeBankDetailFiles` ADD CONSTRAINT `FK_EmployeeBankDetailFiles_EmployeeBankDetails_EmployeeBankDeta~` FOREIGN KEY (`EmployeeBankDetailId`) REFERENCES `EmployeeBankDetails` (`EmployeeBankDetailId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeBankDetails` ADD CONSTRAINT `FK_EmployeeBankDetails_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeBenefits` ADD CONSTRAINT `FK_EmployeeBenefits_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeCoolerBoxes` ADD CONSTRAINT `FK_EmployeeCoolerBoxes_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeCoolerBoxHistories` ADD CONSTRAINT `FK_EmployeeCoolerBoxHistories_Employees_NewEmployeeId` FOREIGN KEY (`NewEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeCoolerBoxHistories` ADD CONSTRAINT `FK_EmployeeCoolerBoxHistories_Employees_OldEmployeeId` FOREIGN KEY (`OldEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeDeductions` ADD CONSTRAINT `FK_EmployeeDeductions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeDepartments` ADD CONSTRAINT `FK_EmployeeDepartments_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeDisciplinaryProcedures` ADD CONSTRAINT `FK_EmployeeDisciplinaryProcedures_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeEmployeeBadges` ADD CONSTRAINT `FK_EmployeeEmployeeBadges_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeEmployeeDivisions` ADD CONSTRAINT `FK_EmployeeEmployeeDivisions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeEmployeeHealthConditions` ADD CONSTRAINT `FK_EmployeeEmployeeHealthConditions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeEmployeeJobTitles` ADD CONSTRAINT `FK_EmployeeEmployeeJobTitles_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeEmployeeKpis` ADD CONSTRAINT `FK_EmployeeEmployeeKpis_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeExpenseClaims` ADD CONSTRAINT `FK_EmployeeExpenseClaims_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeFiles` ADD CONSTRAINT `FK_EmployeeFiles_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeFuels` ADD CONSTRAINT `FK_EmployeeFuels_EmployeeVehicles_EmployeeVehicleId` FOREIGN KEY (`EmployeeVehicleId`) REFERENCES `EmployeeVehicles` (`EmployeeVehicleId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeFuels` ADD CONSTRAINT `FK_EmployeeFuels_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeKpiScores` ADD CONSTRAINT `FK_EmployeeKpiScores_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeLeaveEntries` ADD CONSTRAINT `FK_EmployeeLeaveEntries_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeLoans` ADD CONSTRAINT `FK_EmployeeLoans_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeManager` ADD CONSTRAINT `FK_EmployeeManager_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeManager` ADD CONSTRAINT `FK_EmployeeManager_Employees_ManagerId` FOREIGN KEY (`ManagerId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeNotifications` ADD CONSTRAINT `FK_EmployeeNotifications_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeePayRates` ADD CONSTRAINT `FK_EmployeePayRates_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeePensions` ADD CONSTRAINT `FK_EmployeePensions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeePopiConsents` ADD CONSTRAINT `FK_EmployeePopiConsents_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeQualificationFiles` ADD CONSTRAINT `FK_EmployeeQualificationFiles_EmployeeQualifications_EmployeeQu~` FOREIGN KEY (`EmployeeQualificationId`) REFERENCES `EmployeeQualifications` (`EmployeeQualificationId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeQualifications` ADD CONSTRAINT `FK_EmployeeQualifications_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeRecurringTransactions` ADD CONSTRAINT `FK_EmployeeRecurringTransactions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeRegionContacts` ADD CONSTRAINT `FK_EmployeeRegionContacts_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeRegions` ADD CONSTRAINT `FK_EmployeeRegions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `EmployeeReinstatementHistories` ADD CONSTRAINT `FK_EmployeeReinstatementHistories_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeReports` ADD CONSTRAINT `FK_EmployeeReports_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_Stakeholders_StakeholderId` FOREIGN KEY (`StakeholderId`) REFERENCES `Stakeholders` (`StakeholderId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `EmployeeStoreArchives` ADD CONSTRAINT `FK_EmployeeStoreArchives_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `EmployeeStoreCalendars` ADD CONSTRAINT `FK_EmployeeStoreCalendars_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeStoreCalendars` ADD CONSTRAINT `FK_EmployeeStoreCalendars_SurveyInstances_SurveyInstanceId` FOREIGN KEY (`SurveyInstanceId`) REFERENCES `SurveyInstances` (`SurveyInstanceId`);

ALTER TABLE `EmployeeStoreCalendarSurveyFormSubmissions` ADD CONSTRAINT `FK_EmployeeStoreCalendarSurveyFormSubmissions_SurveyFormSubmiss~` FOREIGN KEY (`SurveyFormSubmissionId`) REFERENCES `SurveyFormSubmissions` (`SurveyFormSubmissionId`) ON DELETE CASCADE;

ALTER TABLE `EmployeeStoreCheckIns` ADD CONSTRAINT `FK_EmployeeStoreCheckIns_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `EmployeeStoreKpis` ADD CONSTRAINT `FK_EmployeeStoreKpis_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `EmployeeStoreKpiScores` ADD CONSTRAINT `FK_EmployeeStoreKpiScores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `EmployeeStores` ADD CONSTRAINT `FK_EmployeeStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `EngageMasterProducts` ADD CONSTRAINT `FK_EngageMasterProducts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `EngageRegionClaimManagers` ADD CONSTRAINT `FK_EngageRegionClaimManagers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `EngageSubGroupSuppliers` ADD CONSTRAINT `FK_EngageSubGroupSuppliers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `EntityContactCommunicationTypes` ADD CONSTRAINT `FK_EntityContactCommunicationTypes_EntityContacts_EntityContact~` FOREIGN KEY (`EntityContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE;

ALTER TABLE `EntityContactRegions` ADD CONSTRAINT `FK_EntityContactRegions_EntityContacts_EntityContactId` FOREIGN KEY (`EntityContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE;

ALTER TABLE `EntityContacts` ADD CONSTRAINT `FK_EntityContacts_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `EntityContacts` ADD CONSTRAINT `FK_EntityContacts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `EntityContacts` ADD CONSTRAINT `FK_EntityContacts_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `GLAdjustments` ADD CONSTRAINT `FK_GLAdjustments_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `ImportPromotionStores` ADD CONSTRAINT `FK_ImportPromotionStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `ImportSurveyStores` ADD CONSTRAINT `FK_ImportSurveyStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `ImportSurveyStores` ADD CONSTRAINT `FK_ImportSurveyStores_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`) ON DELETE CASCADE;

ALTER TABLE `Incidents` ADD CONSTRAINT `FK_Incidents_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `Incidents` ADD CONSTRAINT `FK_Incidents_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `Locations` ADD CONSTRAINT `FK_Locations_Stakeholders_StakeholderId` FOREIGN KEY (`StakeholderId`) REFERENCES `Stakeholders` (`StakeholderId`);

ALTER TABLE `Manufacturers` ADD CONSTRAINT `FK_Manufacturers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `NotificationTargets` ADD CONSTRAINT `FK_NotificationTargets_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `opt_SupplierRegions` ADD CONSTRAINT `FK_opt_SupplierRegions_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `OrderEngageDepartments` ADD CONSTRAINT `FK_OrderEngageDepartments_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`OrderId`);

ALTER TABLE `Orders` ADD CONSTRAINT `FK_Orders_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `Orders` ADD CONSTRAINT `FK_Orders_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `PaymentNotificationStatusUsers` ADD CONSTRAINT `FK_PaymentNotificationStatusUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `ProjectAssignees` ADD CONSTRAINT `FK_ProjectAssignees_ProjectStakeholders_ProjectStakeholderId` FOREIGN KEY (`ProjectStakeholderId`) REFERENCES `ProjectStakeholders` (`ProjectStakeholderId`) ON DELETE CASCADE;

ALTER TABLE `ProjectAssignees` ADD CONSTRAINT `FK_ProjectAssignees_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectCategorySuppliers` ADD CONSTRAINT `FK_ProjectCategorySuppliers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `ProjectComments` ADD CONSTRAINT `FK_ProjectComments_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectDcProducts` ADD CONSTRAINT `FK_ProjectDcProducts_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectEngageBrands` ADD CONSTRAINT `FK_ProjectEngageBrands_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectFiles` ADD CONSTRAINT `FK_ProjectFiles_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectNotes` ADD CONSTRAINT `FK_ProjectNotes_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectProjectTags` ADD CONSTRAINT `FK_ProjectProjectTags_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE;

ALTER TABLE `ProjectProjectTags` ADD CONSTRAINT `FK_ProjectProjectTags_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`) ON DELETE CASCADE;

ALTER TABLE `ProjectProjectTags` ADD CONSTRAINT `FK_ProjectProjectTags_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `ProjectProjectTags` ADD CONSTRAINT `FK_ProjectProjectTags_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `ProjectProjectTags` ADD CONSTRAINT `FK_ProjectProjectTags_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `Projects` ADD CONSTRAINT `FK_Projects_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

ALTER TABLE `Projects` ADD CONSTRAINT `FK_Projects_Users_OwnerId` FOREIGN KEY (`OwnerId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `ProjectStakeholders` ADD CONSTRAINT `FK_ProjectStakeholders_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `ProjectStoreAssets` ADD CONSTRAINT `FK_ProjectStoreAssets_StoreAssets_StoreAssetId` FOREIGN KEY (`StoreAssetId`) REFERENCES `StoreAssets` (`StoreAssetId`) ON DELETE CASCADE;

ALTER TABLE `ProjectSuppliers` ADD CONSTRAINT `FK_ProjectSuppliers_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTacOpRegions` ADD CONSTRAINT `FK_ProjectTacOpRegions_ProjectTacOps_ProjectTacOpId` FOREIGN KEY (`ProjectTacOpId`) REFERENCES `ProjectTacOps` (`ProjectTacOpId`);

ALTER TABLE `ProjectTacOps` ADD CONSTRAINT `FK_ProjectTacOps_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTaskAssignees` ADD CONSTRAINT `FK_ProjectTaskAssignees_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTaskComments` ADD CONSTRAINT `FK_ProjectTaskComments_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTaskNotes` ADD CONSTRAINT `FK_ProjectTaskNotes_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTaskProjectStakeholderUsers` ADD CONSTRAINT `FK_ProjectTaskProjectStakeholderUsers_ProjectTasks_ProjectTaskId` FOREIGN KEY (`ProjectTaskId`) REFERENCES `ProjectTasks` (`ProjectTaskId`) ON DELETE CASCADE;

ALTER TABLE `ProjectTasks` ADD CONSTRAINT `FK_ProjectTasks_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `ProjectUsers` ADD CONSTRAINT `FK_ProjectUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

ALTER TABLE `PromotionStores` ADD CONSTRAINT `FK_PromotionStores_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`);

ALTER TABLE `SecurityRoleUsers` ADD CONSTRAINT `FK_SecurityRoleUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE;

ALTER TABLE `SparProducts` ADD CONSTRAINT `FK_SparProducts_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE;

ALTER TABLE `Stakeholders` ADD CONSTRAINT `FK_Stakeholders_Vendors_VendorId` FOREIGN KEY (`VendorId`) REFERENCES `Vendors` (`VendorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241021062400_Initial', '8.0.2');

COMMIT;

