USE `engage-dev`;

START TRANSACTION;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_CostCenters_CostCenterId`;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_CostDepartments_CostDepartmentId`;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_CostSubDepartments_CostSubDepartmentId`;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_Employees_EmployeeId`;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_PaymentLineCostTypes_PaymentLineCostTypeId`;

ALTER TABLE `PaymentLines` DROP FOREIGN KEY `FK_PaymentLines_Vat_VatId`;

ALTER TABLE `Payments` DROP FOREIGN KEY `FK_Payments_PaymentCostTypes_PaymentCostTypeId`;

ALTER TABLE `Payments` DROP FOREIGN KEY `FK_Payments_Vat_VatId`;

ALTER TABLE `Payments` DROP FOREIGN KEY `FK_Payments_opt_EngageRegions_EngageRegionId`;

DROP TABLE `PaymentCostTypes`;

DROP TABLE `PaymentLineCostTypes`;

ALTER TABLE `Payments` DROP INDEX `IX_Payments_EngageRegionId`;

ALTER TABLE `PaymentLines` DROP INDEX `IX_PaymentLines_CostCenterId`;

ALTER TABLE `PaymentLines` DROP INDEX `IX_PaymentLines_CostDepartmentId`;

ALTER TABLE `PaymentLines` DROP INDEX `IX_PaymentLines_CostSubDepartmentId`;

ALTER TABLE `PaymentLines` DROP INDEX `IX_PaymentLines_EmployeeId`;

ALTER TABLE `PaymentLines` DROP INDEX `IX_PaymentLines_PaymentLineCostTypeId`;

ALTER TABLE `Payments` DROP COLUMN `EngageRegionId`;

ALTER TABLE `PaymentLines` DROP COLUMN `CostCenterId`;

ALTER TABLE `PaymentLines` DROP COLUMN `CostDepartmentId`;

ALTER TABLE `PaymentLines` DROP COLUMN `CostSubDepartmentId`;

ALTER TABLE `PaymentLines` DROP COLUMN `EmployeeId`;

ALTER TABLE `PaymentLines` DROP COLUMN `PaymentLineCostTypeId`;

ALTER TABLE `Payments` RENAME COLUMN `PaymentCostTypeId` TO `PaymentBatchId`;

ALTER TABLE `Payments` RENAME INDEX `IX_Payments_PaymentCostTypeId` TO `IX_Payments_PaymentBatchId`;

ALTER TABLE `Payments` MODIFY COLUMN `VatId` int NULL;

ALTER TABLE `Payments` MODIFY COLUMN `InvoiceNumber` varchar(200) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Payments` ADD `IsClaimFromSupplier` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PaymentLines` MODIFY COLUMN `VatId` int NULL;

ALTER TABLE `PaymentLines` ADD `Files` json NULL;

ALTER TABLE `PaymentLines` ADD `HasInvoice` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PaymentLines` ADD `HasQuote` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PaymentLines` ADD `IsSplitAmount` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PaymentLines` ADD `IsVat` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PaymentLines` ADD `Note` varchar(1000) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Creditors` MODIFY COLUMN `VatNumber` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Creditors` MODIFY COLUMN `Name` varchar(300) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Creditors` ADD `BankConfirmationDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Creditors` ADD `Files` json NULL;

ALTER TABLE `Creditors` ADD `TradingName` varchar(300) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

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

ALTER TABLE `PaymentLines` ADD CONSTRAINT `FK_PaymentLines_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`);

ALTER TABLE `Payments` ADD CONSTRAINT `FK_Payments_PaymentBatches_PaymentBatchId` FOREIGN KEY (`PaymentBatchId`) REFERENCES `PaymentBatches` (`PaymentBatchId`) ON DELETE CASCADE;

ALTER TABLE `Payments` ADD CONSTRAINT `FK_Payments_Vat_VatId` FOREIGN KEY (`VatId`) REFERENCES `Vat` (`VatId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240909084342_CreditorPaymentEntities', '8.0.2');

COMMIT;

