START TRANSACTION;

ALTER TABLE `Surveys` ADD `IsDisabled` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221213042725_Survey_Add_IsDisabled', '6.0.2');

COMMIT;

