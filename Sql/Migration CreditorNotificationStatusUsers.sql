START TRANSACTION;

CREATE TABLE `CreditorNotificationStatusUsers` (
    `CreditorNotificationStatusUserId` int NOT NULL AUTO_INCREMENT,
    `CreditorStatusId` int NOT NULL,
    `EngageRegionId` int NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_CreditorNotificationStatusUsers` PRIMARY KEY (`CreditorNotificationStatusUserId`),
    CONSTRAINT `FK_CreditorNotificationStatusUsers_CreditorStatuses_CreditorSta~` FOREIGN KEY (`CreditorStatusId`) REFERENCES `CreditorStatuses` (`CreditorStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorNotificationStatusUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CreditorNotificationStatusUsers_opt_EngageRegions_EngageRegi~` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentNotificationStatusUsers` (
    `PaymentNotificationStatusUserId` int NOT NULL AUTO_INCREMENT,
    `PaymentStatusId` int NOT NULL,
    `EngageRegionId` int NOT NULL,
    `UserId` int NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_PaymentNotificationStatusUsers` PRIMARY KEY (`PaymentNotificationStatusUserId`),
    CONSTRAINT `FK_PaymentNotificationStatusUsers_PaymentStatuses_PaymentStatus~` FOREIGN KEY (`PaymentStatusId`) REFERENCES `PaymentStatuses` (`PaymentStatusId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentNotificationStatusUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE,
    CONSTRAINT `FK_PaymentNotificationStatusUsers_opt_EngageRegions_EngageRegio~` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_CreditorNotificationStatusUsers_CreditorStatusId` ON `CreditorNotificationStatusUsers` (`CreditorStatusId`);

CREATE INDEX `IX_CreditorNotificationStatusUsers_EngageRegionId` ON `CreditorNotificationStatusUsers` (`EngageRegionId`);

CREATE INDEX `IX_CreditorNotificationStatusUsers_UserId` ON `CreditorNotificationStatusUsers` (`UserId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_EngageRegionId` ON `PaymentNotificationStatusUsers` (`EngageRegionId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_PaymentStatusId` ON `PaymentNotificationStatusUsers` (`PaymentStatusId`);

CREATE INDEX `IX_PaymentNotificationStatusUsers_UserId` ON `PaymentNotificationStatusUsers` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240927135347_CreditorNotificationStatusUsers', '8.0.2');

COMMIT;

