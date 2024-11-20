USE `engage-dev`;

START TRANSACTION;

ALTER TABLE `SurveyForms` ADD `Links` json NULL;

ALTER TABLE `SurveyFormQuestions` ADD `Links` json NULL;

ALTER TABLE `SurveyFormQuestionGroups` ADD `Links` json NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241009084043_SurveyLinks', '8.0.2');

COMMIT;

