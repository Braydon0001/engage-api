START TRANSACTION;

ALTER TABLE `SurveyQuestionOptions` ADD `CompleteSurvey` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221214144001_SurveyQuestionOption_Add_CompleteSurvey', '6.0.2');

COMMIT;

