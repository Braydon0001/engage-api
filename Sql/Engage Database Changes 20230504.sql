-- Employees
CREATE TABLE `EmployeeHealthConditions` (
    `EmployeeHealthConditionId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(120) CHARACTER SET utf8mb4 NULL,
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

CREATE TABLE `EmployeeEmployeeHealthConditions` (
    `EmployeeId` int NOT NULL,
    `EmployeeHealthConditionId` int NOT NULL,
    CONSTRAINT `PK_EmployeeEmployeeHealthConditions` PRIMARY KEY (`EmployeeId`, `EmployeeHealthConditionId`),
    CONSTRAINT `FK_EmployeeEmployeeHealthConditions_EmployeeHealthConditions_Em~` FOREIGN KEY (`EmployeeHealthConditionId`) REFERENCES `EmployeeHealthConditions` (`EmployeeHealthConditionId`),
    CONSTRAINT `FK_EmployeeEmployeeHealthConditions_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeStoreCalendarGroups` (
    `EmployeeStoreCalendarGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
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

CREATE TABLE `EmployeeStoreCalendarYears` (
    `EmployeeStoreCalendarYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NULL,
    `EndDate` datetime(6) NULL,
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

CREATE TABLE `EmployeeStoreCalendarPeriods` (
    `EmployeeStoreCalendarPeriodId` int NOT NULL AUTO_INCREMENT,
    `EmployeeStoreCalendarYearId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Number` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
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

CREATE TABLE `EmployeeStoreCalendars` (
    `EmployeeStoreCalendarId` int NOT NULL AUTO_INCREMENT,
    `EmployeeId` int NOT NULL,
    `StoreId` int NOT NULL,
    `CalendarDate` datetime(6) NOT NULL,
    `Order` int NULL,
    `EmployeeStoreCalendarPeriodId` int NOT NULL,
    `EmployeeStoreCalendarGroupId` int NOT NULL,
    `SurveyInstanceId` int NULL,
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
    CONSTRAINT `FK_EmployeeStoreCalendars_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendars_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeeStoreCalendars_SurveyInstances_SurveyInstanceId` FOREIGN KEY (`SurveyInstanceId`) REFERENCES `SurveyInstances` (`SurveyInstanceId`)
) CHARACTER SET=utf8mb4;

-- Emails
ALTER TABLE `EmailHistoryTemplateVariables` ADD `EmployeeName` longtext CHARACTER SET utf8mb4 NULL;
ALTER TABLE `EmailHistoryTemplateVariables` ADD `TerminationDate` longtext CHARACTER SET utf8mb4 NULL;
ALTER TABLE `EmailHistoryTemplateVariables` ADD `TerminationReason` longtext CHARACTER SET utf8mb4 NULL;
ALTER TABLE `EmailHistoryTemplateVariables` ADD `TerminatorName` longtext CHARACTER SET utf8mb4 NULL;

-- Stores
CREATE TABLE `StoreOwners` (
    `StoreOwnerId` int NOT NULL AUTO_INCREMENT,
    `StoreId` int NOT NULL,
    `StoreGroupId` int NOT NULL,
    `StoreOwnerTypeId` int NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(1000) CHARACTER SET utf8mb4 NULL,
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

CREATE TABLE `opt_StoreOwnerTypes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreOwnerTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

ALTER TABLE `StoreConceptLevels` ADD `Actual` int NOT NULL DEFAULT 0;
ALTER TABLE `StoreConceptLevels` ADD `Score` double NOT NULL DEFAULT 0.0;
ALTER TABLE `StoreConceptLevels` ADD `Target` int NOT NULL DEFAULT 0;

CREATE TABLE `opt_StoreAssetOwners` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_StoreAssetOwners` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

ALTER TABLE `StoreAssets` ADD `StoreAssetOwnerId` int NOT NULL DEFAULT 0;

-- Claims
ALTER TABLE `ClaimFloats` MODIFY COLUMN `StartDate` datetime(6) NULL;
ALTER TABLE `ClaimFloats` MODIFY COLUMN `ClaimTypeId` int NULL;

CREATE INDEX `IX_ClaimFloats_ClaimTypeId` ON `ClaimFloats` (`ClaimTypeId`);
CREATE UNIQUE INDEX `IX_ClaimFloats_SupplierId_EngageRegionId` ON `ClaimFloats` (`SupplierId`, `EngageRegionId`);

ALTER TABLE `ClaimFloats` ADD CONSTRAINT `FK_ClaimFloats_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);
ALTER TABLE `ClaimFloats` ADD CONSTRAINT `FK_ClaimFloats_ClaimTypes_ClaimTypeId` FOREIGN KEY (`ClaimTypeId`) REFERENCES `ClaimTypes` (`ClaimTypeId`);
ALTER TABLE `ClaimFloats` ADD CONSTRAINT `FK_ClaimFloats_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);
ALTER TABLE `ClaimFloats` ADD `LastToppedUp` datetime(6) NULL;
ALTER TABLE `ClaimFloats` ADD `LastToppedUpBy` longtext CHARACTER SET utf8mb4 NULL;
ALTER TABLE `ClaimFloats` ADD `TopUpAmount` decimal(65,30) NULL;

CREATE TABLE `ClaimFloatTopUpHistories` (
    `ClaimFloatTopUpHistoryId` int NOT NULL AUTO_INCREMENT,
    `ClaimFloatId` int NOT NULL,
    `TopUpAmount` decimal(65,30) NOT NULL,
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

-- Vouchers
ALTER TABLE `Vouchers` MODIFY COLUMN `StartDate` datetime(6) NULL;
ALTER TABLE `Vouchers` MODIFY COLUMN `EndDate` datetime(6) NULL;

-- Suppliers
CREATE TABLE `SupplierSalesLeads` (
    `SupplierSalesLeadId` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `KnownAs` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EmailAddress` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ContactNumber` varchar(100) CHARACTER SET utf8mb4 NULL,
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

CREATE TABLE `SupplierAllowances` (
    `SupplierAllowanceId` int NOT NULL AUTO_INCREMENT,
    `SupplierId` int NOT NULL,
    `Vendor` longtext CHARACTER SET utf8mb4 NOT NULL,
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
    `SalesLeadId` int NOT NULL,
    `GlSubCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `GlMainCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierAllowances` PRIMARY KEY (`SupplierAllowanceId`),
    CONSTRAINT `FK_SupplierAllowances_SalesLeads_SalesLeadId` FOREIGN KEY (`SalesLeadId`) REFERENCES `SalesLeads` (`SalesLeadId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierAllowances_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

ALTER TABLE `SupplierAllowances` RENAME COLUMN `SalesLeadId` TO `SupplierSalesLeadId`;
ALTER TABLE `SupplierAllowances` RENAME INDEX `IX_SupplierAllowances_SalesLeadId` TO `IX_SupplierAllowances_SupplierSalesLeadId`;
ALTER TABLE `SupplierAllowances` ADD CONSTRAINT `FK_SupplierAllowances_SupplierSalesLeads_SupplierSalesLeadId` FOREIGN KEY (`SupplierSalesLeadId`) REFERENCES `SupplierSalesLeads` (`SupplierSalesLeadId`) ON DELETE CASCADE;
ALTER TABLE `SupplierAllowances` MODIFY COLUMN `Vendor` varchar(100) CHARACTER SET utf8mb4 NULL;