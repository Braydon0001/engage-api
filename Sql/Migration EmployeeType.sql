START TRANSACTION;

ALTER TABLE `Employees` ADD `EmployeeTypeId` int NULL;

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

CREATE INDEX `IX_Employees_EmployeeTypeId` ON `Employees` (`EmployeeTypeId`);

ALTER TABLE `Employees` ADD CONSTRAINT `FK_Employees_EmployeeTypes_EmployeeTypeId` FOREIGN KEY (`EmployeeTypeId`) REFERENCES `EmployeeTypes` (`EmployeeTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240814093510_EmployeeType', '8.0.2');

COMMIT;

