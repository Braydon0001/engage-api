START TRANSACTION;

ALTER TABLE `SurveyQuestions` ADD `IsFalseOptionRequired` tinyint(1) NOT NULL DEFAULT TRUE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221214134033_SurveyQuestion_Add_Fields', '6.0.2');

COMMIT;

