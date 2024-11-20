-- Employees

ALTER TABLE `Employees` ADD `EmploymentActionId` int NULL;
ALTER TABLE `Employees` ADD `IsEncashLeave` tinyint(1) NOT NULL DEFAULT FALSE;
ALTER TABLE `Employees` ADD `PayrollPeriodId` int NULL;

CREATE TABLE `opt_EmploymentActions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `Order` int NULL,
    CONSTRAINT `PK_opt_EmploymentActions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EmployeeRecurringTransactionStatuses` (
    `EmployeeRecurringTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `EmployeeTransactionStatuses` (
    `EmployeeTransactionStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `PayrollYears` (
    `PayrollYearId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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
    CONSTRAINT `PK_PayrollYears` PRIMARY KEY (`PayrollYearId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PayrollPeriods` (
    `PayrollPeriodId` int NOT NULL AUTO_INCREMENT,
    `PayrollYearId` int NOT NULL,
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
    CONSTRAINT `PK_PayrollPeriods` PRIMARY KEY (`PayrollPeriodId`),
    CONSTRAINT `FK_PayrollPeriods_PayrollYears_PayrollYearId` FOREIGN KEY (`PayrollYearId`) REFERENCES `PayrollYears` (`PayrollYearId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

-- General 
ALTER TABLE `opt_StoreConcepts` ADD `Files` json NULL;
ALTER TABLE `Notifications` ADD `Files` json NULL;

-- Inventory

CREATE TABLE `InventoryGroups` (
    `InventoryGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `Inventories` (
    `InventoryId` int NOT NULL AUTO_INCREMENT,
    `InventoryGroupId` int NOT NULL,
    `InventoryStatusId` int NOT NULL,
    `InventoryUnitTypeId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `BarCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Files` json NULL,
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

-- Order Templates

CREATE TABLE `OrderTemplateLineStatuses` (
    `OrderTemplateLineStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateLineStatuses` PRIMARY KEY (`OrderTemplateLineStatusId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplateProductGroups` (
    `OrderTemplateProductGroupId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateProductGroups` PRIMARY KEY (`OrderTemplateProductGroupId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `OrderTemplateStatuses` (
    `OrderTemplateStatusId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `OrderTemplates` (
    `OrderTemplateId` int NOT NULL AUTO_INCREMENT,
    `OrderTemplateStatusId` int NOT NULL,
    `DistributionCenterId` int NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
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

CREATE TABLE `OrderTemplateSkus` (
    `OrderTemplateSkuId` int NOT NULL AUTO_INCREMENT,
    `OrderTemplateId` int NOT NULL,
    `OrderTemplateLineStatusId` int NOT NULL,
    `OrderTemplateProductGroupId` int NOT NULL,
    `DCProductId` int NOT NULL,
    `Order` int NOT NULL,
    `Quantity` int NOT NULL,
    `Price` decimal(65,30) NOT NULL,
    `PromotionPrice` decimal(65,30) NOT NULL,
    `RecommendedPrice` decimal(65,30) NOT NULL,
    `GrossProfitPercent` decimal(65,30) NOT NULL,
    `Suffix` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_OrderTemplateSkus` PRIMARY KEY (`OrderTemplateSkuId`),
    CONSTRAINT `FK_OrderTemplateSkus_DCProducts_DCProductId` FOREIGN KEY (`DCProductId`) REFERENCES `DCProducts` (`DCProductId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderTemplateSkus_OrderTemplateLineStatuses_OrderTemplateLin~` FOREIGN KEY (`OrderTemplateLineStatusId`) REFERENCES `OrderTemplateLineStatuses` (`OrderTemplateLineStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderTemplateSkus_OrderTemplateProductGroups_OrderTemplatePr~` FOREIGN KEY (`OrderTemplateProductGroupId`) REFERENCES `OrderTemplateProductGroups` (`OrderTemplateProductGroupId`) ON DELETE CASCADE,
    CONSTRAINT `FK_OrderTemplateSkus_OrderTemplates_OrderTemplateId` FOREIGN KEY (`OrderTemplateId`) REFERENCES `OrderTemplates` (`OrderTemplateId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

-- Targeting

ALTER TABLE `WebFileTargets` ADD `EngageRegionId` int NULL;
ALTER TABLE `WebFileTargets` ADD `StoreFormatId` int NULL;
ALTER TABLE `NotificationTargets` ADD `EngageRegionId` int NULL;
ALTER TABLE `NotificationTargets` ADD `StoreFormatId` int NULL;
ALTER TABLE `NotificationTargets` ADD `StoreId` int NULL;

CREATE TABLE `SurveyTargets` (
    `SurveyTargetId` int NOT NULL AUTO_INCREMENT,
    `SurveyId` int NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyTargets` PRIMARY KEY (`SurveyTargetId`),
    CONSTRAINT `FK_SurveyTargets_Surveys_SurveyId` FOREIGN KEY (`SurveyId`) REFERENCES `Surveys` (`SurveyId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

ALTER TABLE `SurveyTargets` ADD `Discriminator` longtext CHARACTER SET utf8mb4 NOT NULL;
ALTER TABLE `SurveyTargets` ADD `EmployeeId` int NULL;
ALTER TABLE `SurveyTargets` ADD `EmployeeJobTitleId` int NULL;
ALTER TABLE `SurveyTargets` ADD `EngageRegionId` int NULL;
ALTER TABLE `SurveyTargets` ADD `StoreFormatId` int NULL;
ALTER TABLE `SurveyTargets` ADD `StoreId` int NULL;
