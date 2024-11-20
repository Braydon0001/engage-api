START TRANSACTION;

ALTER TABLE `ProjectExternalUsers` ADD `ExternalUserTypeId` int NULL;

ALTER TABLE `ClaimTypes` ADD `IsEmployeeClaim` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `ClaimTypes` ADD `SupplierId` int NULL;

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

CREATE INDEX `IX_ProjectExternalUsers_ExternalUserTypeId` ON `ProjectExternalUsers` (`ExternalUserTypeId`);

CREATE INDEX `IX_ClaimTypes_SupplierId` ON `ClaimTypes` (`SupplierId`);

ALTER TABLE `ClaimTypes` ADD CONSTRAINT `FK_ClaimTypes_Suppliers_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`SupplierId`);

ALTER TABLE `ProjectExternalUsers` ADD CONSTRAINT `FK_ProjectExternalUsers_ExternalUserTypes_ExternalUserTypeId` FOREIGN KEY (`ExternalUserTypeId`) REFERENCES `ExternalUserTypes` (`ExternalUserTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240917042532_EmployeeClaims', '8.0.2');

COMMIT;

