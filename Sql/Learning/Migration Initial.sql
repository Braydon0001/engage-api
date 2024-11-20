CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Departments` (
    `DepartmentId` int NOT NULL AUTO_INCREMENT,
    `ApiDepartmentId` int NULL,
    `AccountId` int NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(1024) CHARACTER SET utf8mb4 NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Departments` PRIMARY KEY (`DepartmentId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Designations` (
    `DesignationId` int NOT NULL AUTO_INCREMENT,
    `ApiDesignationId` int NULL,
    `AccountId` int NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(1024) CHARACTER SET utf8mb4 NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Designations` PRIMARY KEY (`DesignationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Regions` (
    `RegionId` int NOT NULL AUTO_INCREMENT,
    `ApiRegionId` int NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(1024) CHARACTER SET utf8mb4 NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Regions` PRIMARY KEY (`RegionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Topics` (
    `TopicId` int NOT NULL AUTO_INCREMENT,
    `ApiTopicId` int NULL,
    `AccountId` int NULL,
    `ModuleName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ModulePassmark` decimal(65,30) NOT NULL,
    `ModuleTags` longtext CHARACTER SET utf8mb4 NULL,
    `ModuleTrainerDrivenOffsite` tinyint(1) NOT NULL,
    `ModuleLearnerDriven` tinyint(1) NOT NULL,
    `ModuleTrainerDrivenOnsite` tinyint(1) NOT NULL,
    `ModuleIsActive` tinyint(1) NOT NULL,
    `ModuleIsCoreModule` tinyint(1) NOT NULL,
    `ModuleIsCriticalModule` tinyint(1) NOT NULL,
    `ModuleIsDevelopmentModule` tinyint(1) NOT NULL,
    `TopicName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    `ExternalModuleCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Topics` PRIMARY KEY (`TopicId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stores` (
    `StoreId` int NOT NULL AUTO_INCREMENT,
    `ApiStoreId` int NULL,
    `AccountId` int NULL,
    `RegionId` int NULL,
    `DivisionId` int NULL,
    `RegionName` longtext CHARACTER SET utf8mb4 NULL,
    `DivisionName` longtext CHARACTER SET utf8mb4 NULL,
    `StoreName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Country` longtext CHARACTER SET utf8mb4 NULL,
    `IsElearningStore` tinyint(1) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    `Pin` longtext CHARACTER SET utf8mb4 NULL,
    `Code` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Stores` PRIMARY KEY (`StoreId`),
    CONSTRAINT `FK_Stores_Regions_RegionId` FOREIGN KEY (`RegionId`) REFERENCES `Regions` (`RegionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Staffs` (
    `StaffId` int NOT NULL AUTO_INCREMENT,
    `ApiStaffId` int NULL,
    `RegionId` int NULL,
    `StoreId` int NULL,
    `DepartmentId` int NULL,
    `DesignationId` int NULL,
    `DivisionId` int NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Surname` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Race` longtext CHARACTER SET utf8mb4 NULL,
    `Disability` longtext CHARACTER SET utf8mb4 NULL,
    `Gender` longtext CHARACTER SET utf8mb4 NULL,
    `IdentityNumber` varchar(15) CHARACTER SET utf8mb4 NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `RegionName` longtext CHARACTER SET utf8mb4 NULL,
    `StoreName` longtext CHARACTER SET utf8mb4 NULL,
    `DepartmentName` longtext CHARACTER SET utf8mb4 NULL,
    `DivisionName` longtext CHARACTER SET utf8mb4 NULL,
    `DesignationName` longtext CHARACTER SET utf8mb4 NULL,
    `Role` longtext CHARACTER SET utf8mb4 NULL,
    `StaffNumber` longtext CHARACTER SET utf8mb4 NULL,
    `Country_Code` longtext CHARACTER SET utf8mb4 NULL,
    `Cellphone` varchar(15) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `IsSetaLearner` tinyint(1) NOT NULL,
    `ExternalCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Staffs` PRIMARY KEY (`StaffId`),
    CONSTRAINT `FK_Staffs_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Departments` (`DepartmentId`),
    CONSTRAINT `FK_Staffs_Designations_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designations` (`DesignationId`),
    CONSTRAINT `FK_Staffs_Regions_RegionId` FOREIGN KEY (`RegionId`) REFERENCES `Regions` (`RegionId`),
    CONSTRAINT `FK_Staffs_Stores_StoreId` FOREIGN KEY (`StoreId`) REFERENCES `Stores` (`StoreId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `StaffLearningPaths` (
    `StaffLearningPathId` int NOT NULL AUTO_INCREMENT,
    `AccountId` int NULL,
    `StaffId` int NULL,
    `TopicId` int NULL,
    `AssessmentId` int NULL,
    `StaffName` longtext CHARACTER SET utf8mb4 NULL,
    `ModuleName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TopicName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ControlSheetName` longtext CHARACTER SET utf8mb4 NULL,
    `ModuleStarted` datetime(6) NULL,
    `TopicStarted` datetime(6) NULL,
    `ControlSheetStarted` datetime(6) NULL,
    `AssessmentTrainerFullName` longtext CHARACTER SET utf8mb4 NULL,
    `AssessmentScore` decimal(65,30) NOT NULL,
    `IsModuleCompleted` tinyint(1) NOT NULL,
    `IsTopicCompleted` tinyint(1) NOT NULL,
    `IsControlSheetCompleted` tinyint(1) NOT NULL,
    `NoOfTopicsCompleted` int NULL,
    `DateOfTopicCompletion` datetime(6) NULL,
    `ExternalStaffCode` longtext CHARACTER SET utf8mb4 NULL,
    `ExternalTopicCode` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_StaffLearningPaths` PRIMARY KEY (`StaffLearningPathId`),
    CONSTRAINT `FK_StaffLearningPaths_Staffs_StaffId` FOREIGN KEY (`StaffId`) REFERENCES `Staffs` (`StaffId`),
    CONSTRAINT `FK_StaffLearningPaths_Topics_TopicId` FOREIGN KEY (`TopicId`) REFERENCES `Topics` (`TopicId`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_StaffLearningPaths_StaffId` ON `StaffLearningPaths` (`StaffId`);

CREATE INDEX `IX_StaffLearningPaths_TopicId` ON `StaffLearningPaths` (`TopicId`);

CREATE INDEX `IX_Staffs_DepartmentId` ON `Staffs` (`DepartmentId`);

CREATE INDEX `IX_Staffs_DesignationId` ON `Staffs` (`DesignationId`);

CREATE INDEX `IX_Staffs_Name` ON `Staffs` (`Name`);

CREATE INDEX `IX_Staffs_RegionId` ON `Staffs` (`RegionId`);

CREATE INDEX `IX_Staffs_StoreId` ON `Staffs` (`StoreId`);

CREATE INDEX `IX_Staffs_Surname` ON `Staffs` (`Surname`);

CREATE UNIQUE INDEX `IX_Stores_Code` ON `Stores` (`Code`);

CREATE INDEX `IX_Stores_RegionId` ON `Stores` (`RegionId`);

CREATE INDEX `IX_Stores_StoreName` ON `Stores` (`StoreName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221025060345_Initial', '6.0.2');

COMMIT;

