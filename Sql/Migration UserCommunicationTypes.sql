START TRANSACTION;

CREATE TABLE `EntityContactCommunicationTypes` (
    `EntityContactCommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `EntityContactId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_EntityContactCommunicationTypes` PRIMARY KEY (`EntityContactCommunicationTypeId`),
    CONSTRAINT `FK_EntityContactCommunicationTypes_CommunicationTypes_Communica~` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_EntityContactCommunicationTypes_EntityContacts_EntityContact~` FOREIGN KEY (`EntityContactId`) REFERENCES `EntityContacts` (`EntityContactId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserCommunicationTypes` (
    `UserCommunicationTypeId` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `CommunicationTypeId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_UserCommunicationTypes` PRIMARY KEY (`UserCommunicationTypeId`),
    CONSTRAINT `FK_UserCommunicationTypes_CommunicationTypes_CommunicationTypeId` FOREIGN KEY (`CommunicationTypeId`) REFERENCES `CommunicationTypes` (`CommunicationTypeId`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserCommunicationTypes_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_EntityContactCommunicationTypes_CommunicationTypeId` ON `EntityContactCommunicationTypes` (`CommunicationTypeId`);

CREATE INDEX `IX_EntityContactCommunicationTypes_EntityContactId` ON `EntityContactCommunicationTypes` (`EntityContactId`);

CREATE INDEX `IX_UserCommunicationTypes_CommunicationTypeId` ON `UserCommunicationTypes` (`CommunicationTypeId`);

CREATE INDEX `IX_UserCommunicationTypes_UserId` ON `UserCommunicationTypes` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240902125357_UserCommunicationTypes', '8.0.2');

COMMIT;

