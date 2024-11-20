use `sparzw`;

START TRANSACTION;

ALTER TABLE `SurveyFormTypes` ADD `HideStoreRecurring` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240424091549_addHideStoreRecurringFlag', '8.0.2');

COMMIT;