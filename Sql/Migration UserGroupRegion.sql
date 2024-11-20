START TRANSACTION;

ALTER TABLE `UserGroups` ADD `EngageRegionId` int NULL;

CREATE INDEX `IX_UserGroups_EngageRegionId` ON `UserGroups` (`EngageRegionId`);

ALTER TABLE `UserGroups` ADD CONSTRAINT `FK_UserGroups_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240930133926_UserGroupRegion', '8.0.2');

COMMIT;

