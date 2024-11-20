USE `engage-dev`;

START TRANSACTION;

CREATE TABLE `SurveyFormQuestionValueComparisonOperations` (
    `SurveyFormQuestionValueComparisonOperationId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SurveyFormQuestionValueComparisonOperations` PRIMARY KEY (`SurveyFormQuestionValueComparisonOperationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SurveyFormQuestionValueComparisonTargetTypes` (
    `SurveyFormQuestionValueComparisonTargetTypeId` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SurveyFormQuestionValueComparisonTargetTypes` PRIMARY KEY (`SurveyFormQuestionValueComparisonTargetTypeId`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241014065709_ValueComparisonOptions', '8.0.2');

COMMIT;

