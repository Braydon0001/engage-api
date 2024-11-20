use `engage-prod`;

START TRANSACTION;

ALTER TABLE `SurveyFormTargets` ADD `ExcludedEmployeeId` int NULL;

ALTER TABLE `SurveyFormTargets` ADD `ExcludedStoreId` int NULL;

CREATE INDEX `IX_SurveyFormTargets_ExcludedEmployeeId` ON `SurveyFormTargets` (`ExcludedEmployeeId`);

CREATE INDEX `IX_SurveyFormTargets_ExcludedStoreId` ON `SurveyFormTargets` (`ExcludedStoreId`);

ALTER TABLE `SurveyFormTargets` ADD CONSTRAINT `FK_SurveyFormTargets_Employees_ExcludedEmployeeId` FOREIGN KEY (`ExcludedEmployeeId`) REFERENCES `Employees` (`EmployeeId`) ON DELETE CASCADE;

ALTER TABLE `SurveyFormTargets` ADD CONSTRAINT `FK_SurveyFormTargets_Stores_ExcludedStoreId` FOREIGN KEY (`ExcludedStoreId`) REFERENCES `Stores` (`StoreId`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240930094250_SurveyTargetingExclusions', '8.0.2');

COMMIT;