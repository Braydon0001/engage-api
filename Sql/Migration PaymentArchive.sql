START TRANSACTION;

ALTER TABLE `Payments` ADD `PaymentArchiveId` int NULL;

CREATE TABLE `PaymentArchives` (
    `PaymentArchiveId` int NOT NULL AUTO_INCREMENT,
    `ArchiveDate` datetime(6) NOT NULL,
    `Files` json NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_PaymentArchives` PRIMARY KEY (`PaymentArchiveId`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Payments_PaymentArchiveId` ON `Payments` (`PaymentArchiveId`);

ALTER TABLE `Payments` ADD CONSTRAINT `FK_Payments_PaymentArchives_PaymentArchiveId` FOREIGN KEY (`PaymentArchiveId`) REFERENCES `PaymentArchives` (`PaymentArchiveId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241015063410_PaymentArchive', '8.0.2');

COMMIT;