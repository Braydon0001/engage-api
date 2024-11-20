use `sparzw`;

START TRANSACTION;

ALTER TABLE `SurveyFormAnswers` ADD `AnswerDate` datetime(6) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240214071254_AddAnswerDate', '7.0.5');

COMMIT;