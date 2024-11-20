START TRANSACTION;

ALTER TABLE `SurveyQuestions` DROP FOREIGN KEY `FK_SurveyQuestions_EngageMasterProducts_EngageMasterProductId`;

ALTER TABLE `SurveyQuestions` RENAME COLUMN `EngageMasterProductId` TO `EngageVariantProductId`;

ALTER TABLE `SurveyQuestions` RENAME INDEX `IX_SurveyQuestions_EngageMasterProductId` TO `IX_SurveyQuestions_EngageVariantProductId`;

ALTER TABLE `SurveyQuestions` ADD CONSTRAINT `FK_SurveyQuestions_EngageVariantProducts_EngageVariantProductId` FOREIGN KEY (`EngageVariantProductId`) REFERENCES `EngageVariantProducts` (`EngageVariantProductId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221219215456_Change_Master_to_Varient_on_SurveyQuestion', '7.0.0');

COMMIT;

