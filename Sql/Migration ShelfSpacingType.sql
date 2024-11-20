USE `engage-dev`;

START TRANSACTION;

ALTER TABLE `SurveyFormTypes` ADD `HideGroupRules` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `SurveyFormTypes` ADD `HideQuestionRules` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241015063322_ShelfSpacingType', '8.0.2');

COMMIT;

