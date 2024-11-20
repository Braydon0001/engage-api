START TRANSACTION;

ALTER TABLE `Suppliers` ADD `IsEmployeeClaim` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `Claims` ADD `EmployeeDivisionId` int NULL;

CREATE INDEX `IX_Claims_EmployeeDivisionId` ON `Claims` (`EmployeeDivisionId`);

ALTER TABLE `Claims` ADD CONSTRAINT `FK_Claims_EmployeeDivisions_EmployeeDivisionId` FOREIGN KEY (`EmployeeDivisionId`) REFERENCES `EmployeeDivisions` (`EmployeeDivisionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240917060828_EmployeeDivisionClaim', '8.0.2');

COMMIT;