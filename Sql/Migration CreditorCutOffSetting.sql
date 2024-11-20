START TRANSACTION;

ALTER TABLE `CreditorCutOffSettings` DROP FOREIGN KEY `FK_CreditorCutOffSettings_Settings_SettingId`;

ALTER TABLE `CreditorCutOffSettings` DROP INDEX `IX_CreditorCutOffSettings_SettingId`;

ALTER TABLE `CreditorCutOffSettings` DROP COLUMN `SettingId`;

ALTER TABLE `CreditorCutOffSettings` DROP COLUMN `Value`;

ALTER TABLE `CreditorCutOffSettings` ADD `CreditorCutOff` varchar(30) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CreditorCutOffSettings` ADD `EndDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `CreditorCutOffSettings` ADD `PaymentCutOff` varchar(30) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CreditorCutOffSettings` ADD `StartDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241014091416_CreditorCutOffSetting', '8.0.2');

COMMIT;