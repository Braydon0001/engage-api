START TRANSACTION;

ALTER TABLE `Suppliers` ADD `BooleanSettings` json NULL;

ALTER TABLE `Suppliers` ADD `NumberSettings` json NULL;

ALTER TABLE `Suppliers` ADD `ShortName` varchar(30) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Suppliers` ADD `StringSettings` json NULL;

ALTER TABLE `SupplierContracts` ADD `IsEncore` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SupplierContracts` ADD `IsEngage` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SupplierContracts` ADD `IsEngine` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SupplierContracts` ADD `IsSpar` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SupplierContracts` ADD `IsTops` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `EmployeeStoreCalendars` ADD `Note` varchar(200) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `SupplierContractAmountTypes` (
    `SupplierContractAmountTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `SupplierContractSplits` (
    `SupplierContractSplitId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `SupplierSubContractTypes` (
    `SupplierSubContractTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `SupplierSubContractDetails` (
    `SupplierSubContractDetailId` int NOT NULL AUTO_INCREMENT,
    `SupplierSubContractTypeId` int NOT NULL,
    `Detail` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Note` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SupplierSubContractDetails` PRIMARY KEY (`SupplierSubContractDetailId`),
    CONSTRAINT `FK_SupplierSubContractDetails_SupplierSubContractTypes_Supplier~` FOREIGN KEY (`SupplierSubContractTypeId`) REFERENCES `SupplierSubContractTypes` (`SupplierSubContractTypeId`) ON DELETE CASCADE
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

CREATE TABLE `SupplierContractAmounts` (
    `SupplierContractAmountId` int NOT NULL AUTO_INCREMENT,
    `SupplierSubContractDetailId` int NOT NULL,
    `SupplierContractAmountTypeId` int NOT NULL,
    `SupplierContractSplitId` int NOT NULL,
    `Amount` float NOT NULL,
    `StartRangeAmount` float NULL,
    `EndRangeAmount` float NULL,
    `IsAmountPercent` tinyint(1) NOT NULL,
    `IsRangeAmountPercent` tinyint(1) NOT NULL,
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
    CONSTRAINT `FK_SupplierContractAmounts_SupplierContractSplits_SupplierContr~` FOREIGN KEY (`SupplierContractSplitId`) REFERENCES `SupplierContractSplits` (`SupplierContractSplitId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SupplierContractAmounts_SupplierSubContractDetails_SupplierS~` FOREIGN KEY (`SupplierSubContractDetailId`) REFERENCES `SupplierSubContractDetails` (`SupplierSubContractDetailId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_SupplierContractAmounts_SupplierContractAmountTypeId` ON `SupplierContractAmounts` (`SupplierContractAmountTypeId`);

CREATE INDEX `IX_SupplierContractAmounts_SupplierContractSplitId` ON `SupplierContractAmounts` (`SupplierContractSplitId`);

CREATE INDEX `IX_SupplierContractAmounts_SupplierSubContractDetailId` ON `SupplierContractAmounts` (`SupplierSubContractDetailId`);

CREATE INDEX `IX_SupplierSubContractDetails_SupplierSubContractTypeId` ON `SupplierSubContractDetails` (`SupplierSubContractTypeId`);

CREATE INDEX `IX_SupplierSubContracts_SupplierContractId` ON `SupplierSubContracts` (`SupplierContractId`);

CREATE INDEX `IX_SupplierSubContracts_SupplierSubContractTypeId` ON `SupplierSubContracts` (`SupplierSubContractTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230920092135_SupplierContractSupplierCalendarNote', '7.0.5');

COMMIT;

