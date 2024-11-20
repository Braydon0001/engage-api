use `sparzw`;

START TRANSACTION;

ALTER TABLE `SurveyFormTypes` ADD `HideDeleteQuestion` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SurveyFormTypes` ADD `HideDisableGroup` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SurveyFormTypes` ADD `HideDisableQuestion` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240425150402_AddHideQuestionDeleteAndDisableFlags', '8.0.2');

COMMIT;