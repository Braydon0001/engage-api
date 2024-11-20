use `engage-dev`;

START TRANSACTION;

ALTER TABLE `SubContractorBrands` ADD `ParentId` int NULL;

ALTER TABLE `EmployeeDivisions` ADD `IsRihCallCycles` tinyint(1) NOT NULL DEFAULT FALSE;

CREATE INDEX `IX_SubContractorBrands_ParentId` ON `SubContractorBrands` (`ParentId`);

ALTER TABLE `SubContractorBrands` ADD CONSTRAINT `FK_SubContractorBrands_Suppliers_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `Suppliers` (`SupplierId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240925082354_SubContractor', '8.0.2');

COMMIT;

