use `engage-dev`;
START TRANSACTION;

ALTER TABLE `WebFileTargets` ADD `EngageDepartmentId` int NULL;

CREATE TABLE `ProjectEngageBrands` (
    `ProjectEngageBrandId` int NOT NULL AUTO_INCREMENT,
    `ProjectId` int NOT NULL,
    `EngageBrandId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ProjectEngageBrands` PRIMARY KEY (`ProjectEngageBrandId`),
    CONSTRAINT `FK_ProjectEngageBrands_Projects_ProjectId` FOREIGN KEY (`ProjectId`) REFERENCES `Projects` (`ProjectId`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProjectEngageBrands_opt_EngageBrands_EngageBrandId` FOREIGN KEY (`EngageBrandId`) REFERENCES `opt_EngageBrands` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_WebFileTargets_EngageDepartmentId` ON `WebFileTargets` (`EngageDepartmentId`);

CREATE UNIQUE INDEX `IX_WebFileTargets_WebFileId_EngageDepartmentId` ON `WebFileTargets` (`WebFileId`, `EngageDepartmentId`);

CREATE INDEX `IX_ProjectEngageBrands_EngageBrandId` ON `ProjectEngageBrands` (`EngageBrandId`);

CREATE INDEX `IX_ProjectEngageBrands_ProjectId` ON `ProjectEngageBrands` (`ProjectId`);

ALTER TABLE `WebFileTargets` ADD CONSTRAINT `FK_WebFileTargets_opt_EngageDepartments_EngageDepartmentId` FOREIGN KEY (`EngageDepartmentId`) REFERENCES `opt_EngageDepartments` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240812070048_projectBrandAndWebFileDepartment', '8.0.2');

COMMIT;

