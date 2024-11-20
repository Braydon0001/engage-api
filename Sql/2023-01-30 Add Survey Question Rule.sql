
START TRANSACTION;

CREATE TABLE `SurveyQuestionRules` (
    `SurveyQuestionRuleId` int NOT NULL AUTO_INCREMENT,
    `QuestionId` int NOT NULL,
    `TargetQuestionId` int NOT NULL,
    `Operation` longtext CHARACTER SET utf8mb4 NULL,
    `Path` longtext CHARACTER SET utf8mb4 NULL,
    `RuleIndex` int NOT NULL,
    `RuleText` longtext CHARACTER SET utf8mb4 NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    `ValueType` int NOT NULL,
    `RuleType` int NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_SurveyQuestionRules` PRIMARY KEY (`SurveyQuestionRuleId`),
    CONSTRAINT `FK_SurveyQuestionRules_SurveyQuestions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE,
    CONSTRAINT `FK_SurveyQuestionRules_SurveyQuestions_TargetQuestionId` FOREIGN KEY (`TargetQuestionId`) REFERENCES `SurveyQuestions` (`SurveyQuestionId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_SurveyQuestionRules_QuestionId` ON `SurveyQuestionRules` (`QuestionId`);

CREATE INDEX `IX_SurveyQuestionRules_TargetQuestionId` ON `SurveyQuestionRules` (`TargetQuestionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130122350_Add_SurveyQuestionRule', '7.0.0');

COMMIT;

