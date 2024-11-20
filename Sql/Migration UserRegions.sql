START TRANSACTION;

ALTER TABLE `Suppliers` ADD `IsSubContractor` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `ProjectSubCategories` ADD `EngageSubGroupId` int NULL;

ALTER TABLE `opt_EngageBrands` ADD `IsSparBrand` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `Employees` ADD `CostCenterManagerId` int NULL;

ALTER TABLE `Employees` ADD `EmployeeJobTitleTimeId` int NULL;

ALTER TABLE `Employees` ADD `EmployeeJobTitleTypeId` int NULL;

ALTER TABLE `CommunicationHistories` MODIFY COLUMN `Body` varchar(10000) CHARACTER SET utf8mb4 NOT NULL;

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
    CONSTRAINT `FK_EntityContactRegions_EntityContacts_EntityContactId` FOREIGN KEY (`EntityContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EntityContactRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubContractorBrands` (
    `SubContractorBrandId` int NOT NULL AUTO_INCREMENT,
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
    CONSTRAINT `FK_SubContractorBrands_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SubContractorBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SubContractorBrands_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
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

CREATE INDEX `IX_ProjectSubCategories_EngageSubGroupId` ON `ProjectSubCategories` (`EngageSubGroupId`);

CREATE INDEX `IX_Employees_CostCenterManagerId` ON `Employees` (`CostCenterManagerId`);

CREATE INDEX `IX_Employees_EmployeeJobTitleTimeId` ON `Employees` (`EmployeeJobTitleTimeId`);

CREATE INDEX `IX_Employees_EmployeeJobTitleTypeId` ON `Employees` (`EmployeeJobTitleTypeId`);

CREATE INDEX `IX_EmployeeJobTitleTimes_EmployeeJobTitleId` ON `EmployeeJobTitleTimes` (`EmployeeJobTitleId`);

CREATE INDEX `IX_EmployeeJobTitleTypes_EmployeeJobTitleId` ON `EmployeeJobTitleTypes` (`EmployeeJobTitleId`);

CREATE INDEX `IX_EntityContactRegions_EngageRegionId` ON `EntityContactRegions` (`EngageRegionId`);

CREATE INDEX `IX_EntityContactRegions_EntityContactId` ON `EntityContactRegions` (`EntityContactId`);

CREATE INDEX `IX_SubContractorBrands_EngageBrandId` ON `SubContractorBrands` (`EngageBrandId`);

CREATE INDEX `IX_SubContractorBrands_EngageRegionId` ON `SubContractorBrands` (`EngageRegionId`);

CREATE INDEX `IX_SubContractorBrands_SupplierId` ON `SubContractorBrands` (`SupplierId`);

CREATE INDEX `IX_UserRegions_EngageRegionId` ON `UserRegions` (`EngageRegionId`);

CREATE INDEX `IX_UserRegions_UserId` ON `UserRegions` (`UserId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_EmployeeJobTitleTimes_EmployeeJobTitleTimeId` FOREIGN KEY (`EmployeeJobTitleTimeId`) REFERENCES `EmployeeJobTitleTimes` (`EmployeeJobTitleTimeId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_EmployeeJobTitleTypes_EmployeeJobTitleTypeId` FOREIGN KEY (`EmployeeJobTitleTypeId`) REFERENCES `EmployeeJobTitleTypes` (`EmployeeJobTitleTypeId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_Employees_CostCenterManagerId` FOREIGN KEY (`CostCenterManagerId`) REFERENCES `Employees` (`EmployeeId`);

ALTER TABLE `ProjectSubCategories` ADD CONSTRAINT `FK_ProjectSubCategories_opt_EngageSubGroups_EngageSubGroupId` FOREIGN KEY (`EngageSubGroupId`) REFERENCES `opt_EngageSubGroups` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240911142006_UserRegions', '8.0.2');

COMMIT;

